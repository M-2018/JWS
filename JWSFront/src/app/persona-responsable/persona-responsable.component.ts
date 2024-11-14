import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-persona-responsable',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './persona-responsable.component.html',
  styleUrl: './persona-responsable.component.css',
})
export class PersonaResponsableComponent implements OnInit {
  responsableForm: FormGroup;
  estudiantes: any[] = [];
  responsables: any[] = [];
  editMode = false;
  selectedResponsableId: number | null = null;

  private apiUrl = 'https://localhost:7246/api/PersonaResponsable';
  private estudiantesUrl = 'https://localhost:7246/api/Estudiante';

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.responsableForm = this.fb.group({
      id: [null], // Agregar campo id al formulario
      nombres: ['', Validators.required],
      apellidos: ['', Validators.required],
      relacion: ['', Validators.required],
      telefono: ['', Validators.required],
      correoElectronico: ['', [Validators.required, Validators.email]],
      estudianteId: [null, Validators.required],
    });
  }

  ngOnInit(): void {
    this.getResponsables();
    this.getEstudiantes();
  }

  getResponsables(): void {
    this.http.get<any[]>(this.apiUrl).subscribe({
      next: (data) => {
        this.responsables = data;
      },
      error: (err) => {
        console.error('Error al obtener responsables:', err);
      },
    });
  }

  getEstudiantes(): void {
    this.http.get<any[]>(this.estudiantesUrl).subscribe(
      (data) => {
        this.estudiantes = data;
      },
      (error) => {
        console.error('Error fetching estudiantes:', error);
      }
    );
  }

  getEstudianteNombre(id: number): string {
  const estudiante = this.estudiantes.find(e => e.id === id);
  return estudiante ? `${estudiante.nombres} ${estudiante.apellidos}` : 'N/A';
}


  onSubmit(): void {
    if (this.responsableForm.invalid) {
      this.responsableForm.markAllAsTouched();
      return;
    }
  
    const responsableData = this.responsableForm.value;
    console.log("responsableData: " + JSON.stringify(responsableData));

  
    if (this.editMode && this.selectedResponsableId !== null) {
      // Asegurarse de que el ID está incluido en los datos
      responsableData.id = this.selectedResponsableId;
      
      // Actualizar responsable
      this.http
        .put(`${this.apiUrl}/${this.selectedResponsableId}`, responsableData)
        .subscribe({
          next: () => {
            this.getResponsables();
            this.resetForm();
          },
          error: (error) => {
            console.error('Error al actualizar:', error);
          }
        });
    } else {
      // Crear responsable
      responsableData.id = 0;
      this.http.post(this.apiUrl, responsableData).subscribe({
        next: () => {
          this.getResponsables();
          this.resetForm();
        },
        error: (error) => {
          console.error('Error al crear:', error);
        }
      });
    }
  }

  onEdit(responsable: any): void {
    this.editMode = true;
    this.selectedResponsableId = responsable.id;
    this.responsableForm.patchValue({
      id: responsable.id, // Incluir el ID en el formulario
      nombres: responsable.nombres,
      apellidos: responsable.apellidos,
      relacion: responsable.relacion,
      telefono: responsable.telefono,
      correoElectronico: responsable.correoElectronico,
      estudianteId: responsable.estudianteId,
    });
  }

  onDelete(id: number): void {
    if (confirm('¿Estás seguro de que deseas eliminar este responsable?')) {
      this.http.delete(`${this.apiUrl}/${id}`).subscribe(() => {
        this.getResponsables();
      });
    }
  }

  resetForm(): void {
    this.editMode = false;
    this.selectedResponsableId = null;
    this.responsableForm.reset();
  }
}