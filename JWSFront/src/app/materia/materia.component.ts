import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule
} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-materia',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './materia.component.html',
  styleUrls: ['./materia.component.css'],
})
export class MateriaComponent implements OnInit {
  materiaForm: FormGroup;
  materias: any[] = [];
  profesores: any[] = [];
  editMode = false;
  selectedMateriaId: number | null = null;

  private apiUrl = 'https://localhost:7246/api/Materia';
  private profesoresApiUrl = 'https://localhost:7246/api/Profesor';

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.materiaForm = this.fb.group({
      nombre: ['', Validators.required],
      profesorId: ['', Validators.required] // Campo para el profesor seleccionado
    });
  }

  ngOnInit(): void {
    this.getMaterias();
    this.getProfesores(); // Obtener todos los profesores al inicializar
  }

  // Obtener todos los profesores para el selector desplegable
  getProfesores() {
    this.http.get<any[]>(this.profesoresApiUrl).subscribe({
      next: (data) => {
        this.profesores = data;
      },
      error: (error) => {
        console.error('Error al obtener profesores:', error);
      }
    });
  }

  // Obtener materias de la API
  getMaterias(): void {
    this.http.get<any[]>(this.apiUrl).subscribe((data) => {
      this.materias = data;
    });
  }

  onSubmit(): void {
    if (this.materiaForm.invalid) {
      this.materiaForm.markAllAsTouched();
      return;
    }

    const materiaData = {
      ...this.materiaForm.value,
      cicloId: 0 // Añadir CicloId con valor 0
    };

    //const materiaData = this.materiaForm.value;
    if (this.editMode && this.selectedMateriaId !== null) {

      materiaData.id = this.selectedMateriaId;
      
      console.log('Datos enviados:', materiaData); 

      // Actualizar materia existente
      this.http
        .put(`${this.apiUrl}/${this.selectedMateriaId}`, materiaData)
        .subscribe({
          next: () => {
            this.getMaterias();
            this.resetForm();
          },
          error: (err) => {
            console.error('Error al actualizar materia:', err);
          }
        });
    } else {
      // Crear una nueva materia
      this.http.post(this.apiUrl, materiaData).subscribe({
        next: () => {
          this.getMaterias();
          this.resetForm();
        },
        error: (err) => {
          console.error('Error al crear materia:', err);
        }
      });
    }
  }

  // Editar materia
  onEdit(materia: any): void {
    this.editMode = true;
    this.selectedMateriaId = materia.id;
    this.materiaForm.patchValue({
      nombre: materia.nombre,
      profesorId: materia.profesorId
    });
  }

  // Eliminar materia
  onDelete(id: number): void {
    if (confirm('¿Estás seguro de que deseas eliminar esta materia?')) {
      this.http.delete(`${this.apiUrl}/${id}`).subscribe(() => {
        this.getMaterias();
      });
    }
  }

  // Obtener el nombre del profesor a partir de su ID
  getProfesorNombreById(id: number): string {
    const profesor = this.profesores.find((p) => p.id === id);
    return profesor ? `${profesor.nombres} ${profesor.apellidos}` : '';
  }

  // Reiniciar formulario
  resetForm(): void {
    this.editMode = false;
    this.selectedMateriaId = null;
    this.materiaForm.reset();
  }
}
