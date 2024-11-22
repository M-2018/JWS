import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-matricula',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './matricula.component.html',
  styleUrls: ['./matricula.component.css']
})
export class MatriculaComponent implements OnInit {
  matriculaForm: FormGroup;
  estudiantes: any[] = [];
  ciclos: any[] = [];
  matriculas: any[] = [];
  editMode = false;
  selectedMatriculaId: number | null = null;

  private apiUrl = 'https://localhost:7246/api/Matricula';
  private estudiantesUrl = 'https://localhost:7246/api/Estudiante';
  private ciclosUrl = 'https://localhost:7246/api/Ciclos';

  constructor(private fb: FormBuilder, private http: HttpClient, private cd: ChangeDetectorRef) {
    this.matriculaForm = this.fb.group({
      estudianteId: [null, Validators.required],
      cicloId: [null, Validators.required],
      anioLectivo: [null, [Validators.required, Validators.min(1900), Validators.max(new Date().getFullYear())]],
      // anioLectivo: [null, [Validators.required, Validators.min(1900)]]

    });
  }

  ngOnInit(): void {
    this.getEstudiantes();
    this.getCiclos();
    this.getMatriculas();
  }

  // Obtener estudiantes de la API
  getEstudiantes(): void {
    this.http.get<any[]>(this.estudiantesUrl).subscribe({
      next: (data) => {
        this.estudiantes = data;
      },
      error: (err) => {
        console.error('Error al obtener estudiantes:', err);
      }
    });
  }

  // Obtener ciclos de la API
  getCiclos(): void {
    this.http.get<any[]>(this.ciclosUrl).subscribe((data) => {
      console.log("Lista de ciclos: " + JSON.stringify(data));
      this.ciclos = data;
    });    
  }

  onCicloChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    const selectedCicloId = selectElement.value;
  
    // Buscar el ciclo seleccionado por su ID
    const selectedCiclo = this.ciclos.find(ciclo => ciclo.id.toString() === selectedCicloId);
  
    // Mostrar ID y nombre en la consola
    if (selectedCiclo) {
      console.log('ID del ciclo seleccionado:', selectedCiclo.id);
      console.log('Nombre del ciclo seleccionado:', selectedCiclo.nombre);
    } else {
      console.log('Ciclo no encontrado');
    }
  }

  // Obtener matrículas de la API
  getMatriculas(): void {
    this.http.get<any[]>(this.apiUrl).subscribe({
      next: (data) => {
        this.matriculas = data;
      },
      error: (err) => {
        console.error('Error al obtener matrículas:', err);
      }
    });
  }

  // Enviar formulario para crear o actualizar matrícula
  // onSubmit(): void {
  //   if (this.matriculaForm.invalid) {
  //     this.matriculaForm.markAllAsTouched();
  //     return;
  //   }

  //   const matriculaData = {
  //     ...this.matriculaForm.value
  //   };

  //   if (this.editMode && this.selectedMatriculaId !== null) {
  //     // Actualizar matrícula
  //     this.http.put(`${this.apiUrl}/${this.selectedMatriculaId}`, matriculaData).subscribe({
  //       next: () => {
  //         this.getMatriculas();
  //         this.resetForm();
  //       },
  //       error: (err) => {
  //         console.error('Error al actualizar matrícula:', err);
  //       }
  //     });
  //   } else {
  //     // Crear nueva matrícula
  //     this.http.post(this.apiUrl, matriculaData).subscribe({
  //       next: () => {
  //         this.getMatriculas();
  //         this.resetForm();
  //       },
  //       error: (err) => {
  //         console.error('Error al crear matrícula:', err);
  //       }
  //     });
  //   }
  // }

  onSubmit(): void {
    if (this.matriculaForm.invalid) {
      this.matriculaForm.markAllAsTouched();
      return;
    }
  
    let matriculaData = {
      ...this.matriculaForm.value
    };
  
    if (this.editMode && this.selectedMatriculaId !== null) {
      // Agregar el ID al objeto de datos para la actualización
      console.log("On submit Edit");
      matriculaData = {
        id: this.selectedMatriculaId,
        ...matriculaData
      };
      
      // Actualizar matrícula
      this.http.put(`${this.apiUrl}/${this.selectedMatriculaId}`, matriculaData)
        .subscribe({
          next: () => {
            this.getMatriculas();
            this.resetForm();
            alert('Matrícula actualizada exitosamente.');
          },
          error: (err) => {
            console.error('Error al actualizar matrícula:', err);
            alert('Hubo un problema al actualizar la matrícula. Verifica los datos e inténtalo nuevamente.');
          }
        });
    } else {
      // Crear nueva matrícula
      this.http.post(this.apiUrl, matriculaData).subscribe({
        next: () => {
          this.getMatriculas();
          this.resetForm();
          alert('Matrícula creada exitosamente.');
        },
        error: (err) => {
          console.error('Error al crear matrícula:', err);
          alert('Hubo un problema al crear la matrícula. Verifica los datos e inténtalo nuevamente.');
        }
      });
    }
  }

  // Editar matrícula
  onEdit(matricula: any): void {
    console.log("On Edit");
    this.editMode = true;
    this.selectedMatriculaId = matricula.id;
    this.matriculaForm.patchValue({
      estudianteId: matricula.estudianteId,
      cicloId: matricula.cicloId,
      anioLectivo: matricula.anioLectivo
    });
    // Forzar la detección de cambios
  this.cd.detectChanges();
  }

  // Eliminar matrícula
  onDelete(id: number): void {
    if (confirm('¿Estás seguro de que deseas eliminar esta matrícula?')) {
      this.http.delete(`${this.apiUrl}/${id}`).subscribe({
        next: () => {
          this.getMatriculas();
        },
        error: (err) => {
          console.error('Error al eliminar matrícula:', err);
        }
      });
    }
  }

  // Reiniciar formulario
  resetForm(): void {
    this.editMode = false;
    this.selectedMatriculaId = null;
    this.matriculaForm.reset({
      estudianteId: null,
      cicloId: null,
      anioLectivo: null
    });  }

  // Obtener nombre del estudiante por ID
  getEstudianteNombre(id: number): string {
    const estudiante = this.estudiantes.find(e => e.id === id);
    return estudiante ? `${estudiante.nombres} ${estudiante.apellidos}` : 'N/A';
  }

  // Obtener nombre del ciclo por ID
  getCicloNombre(id: number): string {
    const ciclo = this.ciclos.find(c => c.id === id);
    return ciclo ? ciclo.nombre : 'N/A';
  }
}
