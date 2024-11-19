import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule
} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

interface Materia {
  id: number;
  nombre: string;
  profesorId: number;
  cicloId: number;
}

@Component({
  selector: 'app-ciclo',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './ciclo.component.html',
  styleUrl: './ciclo.component.css'
})
export class CicloComponent implements OnInit {
  cicloForm: FormGroup;
  ciclos: any[] = [];
  materias: Materia[] = [];
  selectedMaterias: number[] = [];
  editMode = false;
  selectedCicloId: number | null = null;

  private apiUrl = 'https://localhost:7246/api/Ciclos';
  private materiasApiUrl = 'https://localhost:7246/api/Materia'; 

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.cicloForm = this.fb.group({
      nombre: ['', Validators.required],
      anio: ['', [Validators.required, Validators.min(2000), Validators.max(2100)]],
      semestre: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.getCiclos();
    this.getMaterias(); 
  }

  // Obtener ciclos de la API
  getCiclos(): void {
    this.http.get<any[]>(this.apiUrl).subscribe((data) => {
      this.ciclos = data;
    });
  }

  // Obtener materias de la API y mostrarlas en la consola
  getMaterias(): void {
    this.http.get<Materia[]>(this.materiasApiUrl).subscribe({
      next: (data) => {
        this.materias = data;
        console.log('Materias:', this.materias);
      },
      error: (error) => {
        console.error('Error al obtener materias:', error);
      }
    });
  }

  // Toggle materia selection
  toggleMateriaSelection(materiaId: number): void {
    const index = this.selectedMaterias.indexOf(materiaId);
    if (index > -1) {
      // If already selected, remove it
      this.selectedMaterias.splice(index, 1);
    } else {
      // If not selected, add it
      this.selectedMaterias.push(materiaId);
    }
  }

  // Enviar formulario para crear o actualizar ciclo
  // onSubmit(): void {
  //   if (this.cicloForm.invalid) {
  //     this.cicloForm.markAllAsTouched();
  //     return;
  //   }

  //   const cicloData = this.cicloForm.value;
  //   if (this.editMode && this.selectedCicloId !== null) {
  //     const cicloDataToUpdate = {
  //       ...cicloData,
  //       id: this.selectedCicloId
  //     };
  //     this.http.put(`${this.apiUrl}/${this.selectedCicloId}`, cicloDataToUpdate).subscribe(() => {
  //       this.getCiclos();
  //       this.resetForm();
  //     });
  //   } else {
  //     this.http.post(this.apiUrl, cicloData).subscribe(() => {
  //       this.getCiclos();
  //       this.resetForm();
  //     });
  //   }
  // }

  onSubmit(): void {
    if (this.cicloForm.invalid) {
      this.cicloForm.markAllAsTouched();
      return;
    }

    const cicloData = {
      ...this.cicloForm.value,
      materiasIds: this.selectedMaterias // Include selected materias
    };

    if (this.editMode && this.selectedCicloId !== null) {
      const cicloDataToUpdate = {
        ...cicloData,
        id: this.selectedCicloId
      };
      this.http.put(`${this.apiUrl}/${this.selectedCicloId}`, cicloDataToUpdate).subscribe(() => {
        this.getCiclos();
        this.resetForm();
      });
    } else {
      this.http.post(this.apiUrl, cicloData).subscribe(() => {
        this.getCiclos();
        this.resetForm();
      });
    }
  }

  // Editar ciclo
  // onEdit(ciclo: any): void {
  //   this.editMode = true;
  //   this.selectedCicloId = ciclo.id;
  //   this.cicloForm.patchValue({
  //     nombre: ciclo.nombre,
  //     anio: ciclo.anio,
  //     semestre: ciclo.semestre
  //   });
  // }

  onEdit(ciclo: any): void {
    this.editMode = true;
    this.selectedCicloId = ciclo.id;
    this.selectedMaterias = ciclo.materiasIds || []; // Populate selected materias
    this.cicloForm.patchValue({
      nombre: ciclo.nombre,
      anio: ciclo.anio,
      semestre: ciclo.semestre
    });
  }

  // Eliminar ciclo
  onDelete(id: number): void {
    if (confirm('¿Estás seguro de que deseas eliminar este ciclo?')) {
      this.http.delete(`${this.apiUrl}/${id}`).subscribe(() => {
        this.getCiclos();
      });
    }
  }

  // Reiniciar formulario
  resetForm(): void {
    this.editMode = false;
    this.selectedCicloId = null;
    this.cicloForm.reset();
  }
  // Check if a materia is selected
  isMateriaSelected(materiaId: number): boolean {
    console.log("Materias seleccionadas: " + this.selectedMaterias);
    return this.selectedMaterias.includes(materiaId);
  }
}
