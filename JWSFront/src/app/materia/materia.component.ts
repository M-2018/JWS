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
  styleUrl: './materia.component.css',
})
export class MateriaComponent implements OnInit {
  materiaForm: FormGroup;
  materias: any[] = [];
  ciclos: any[] = [];
  todasMaterias: any[] = []; // Lista para almacenar todas las materias disponibles
  editMode = false;
  selectedCicloId: number | null = null;

  private apiUrl = 'https://localhost:7246/api/Materia';
  private cicloApiUrl = 'https://localhost:7246/api/Ciclos';
  private materiasApiUrl = 'https://localhost:7246/api/Materias';

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.materiaForm = this.fb.group({
      nombre: ['', Validators.required],
      anio: ['', Validators.required],
      semestre: ['', Validators.required],
      materiasIds: [[], Validators.required] // Campo para múltiples materias seleccionadas
    });
  }

  ngOnInit(): void {
    this.getCiclos();
    this.getTodasMaterias(); // Obtener todas las materias al inicializar
  }

  // Obtener todas las materias para el selector múltiple
  getTodasMaterias() {
    this.http.get<any[]>(this.materiasApiUrl).subscribe({
      next: (data) => {
        this.todasMaterias = data;
      },
      error: (error) => {
        console.error('Error al obtener todas las materias:', error);
      }
    });
  }

  // Obtener ciclos de la API
  getCiclos(): void {
    this.http.get<any[]>(this.cicloApiUrl).subscribe((data) => {
      this.ciclos = data;
    });
  }

  onSubmit(): void {
    if (this.materiaForm.invalid) {
      this.materiaForm.markAllAsTouched();
      return;
    }

    const cicloData = this.materiaForm.value;
    if (this.editMode && this.selectedCicloId !== null) {
      // Actualizar ciclo existente
      this.http
        .put(`${this.cicloApiUrl}/${this.selectedCicloId}`, cicloData)
        .subscribe({
          next: () => {
            this.getCiclos();
            this.resetForm();
          },
          error: (err) => {
            console.error('Error al actualizar ciclo:', err);
          }
        });
    } else {
      // Crear un nuevo ciclo
      this.http.post(this.cicloApiUrl, cicloData).subscribe({
        next: () => {
          this.getCiclos();
          this.resetForm();
        },
        error: (err) => {
          console.error('Error al crear ciclo:', err);
        }
      });
    }
  }

  // Editar ciclo
  onEdit(ciclo: any): void {
    this.editMode = true;
    this.selectedCicloId = ciclo.id;
    this.materiaForm.patchValue({
      nombre: ciclo.nombre,
      anio: ciclo.anio,
      semestre: ciclo.semestre,
      materiasIds: ciclo.materiasIds
    });
  }

  // Eliminar ciclo
  onDelete(id: number): void {
    if (confirm('¿Estás seguro de que deseas eliminar este ciclo?')) {
      this.http.delete(`${this.cicloApiUrl}/${id}`).subscribe(() => {
        this.getCiclos();
      });
    }
  }

  // Obtener los nombres de las materias a partir de una lista de IDs
  getMateriasNombresByIds(ids: number[]): string {
    return this.todasMaterias
      .filter((materia) => ids.includes(materia.id))
      .map((materia) => materia.nombre)
      .join(', ');
  }

  // Reiniciar formulario
  resetForm(): void {
    this.editMode = false;
    this.selectedCicloId = null;
    this.materiaForm.reset();
  }
}
