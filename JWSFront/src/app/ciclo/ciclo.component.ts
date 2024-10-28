import { Component, OnInit  } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

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
  editMode = false;
  selectedCicloId: number | null = null;

  private apiUrl = 'https://localhost:7246/api/Ciclos';

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.cicloForm = this.fb.group({
      nombre: ['', Validators.required],
      anio: ['', [Validators.required, Validators.min(2000), Validators.max(2100)]],
      semestre: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.getCiclos();
  }

  // Obtener ciclos de la API
  getCiclos(): void {
    this.http.get<any[]>(this.apiUrl).subscribe((data) => {
      this.ciclos = data;
    });
  }

  // Enviar formulario para crear o actualizar ciclo
  onSubmit(): void {
    if (this.cicloForm.invalid) {
      this.cicloForm.markAllAsTouched(); // Marca todos los campos como tocados para mostrar errores
      return;
    }

    const cicloData = this.cicloForm.value;
    if (this.editMode && this.selectedCicloId !== null) {
      // Asegúrate de incluir el id en los datos enviados
      const cicloDataToUpdate = {
        ...cicloData,
        id: this.selectedCicloId
      };
      this.http.put(`${this.apiUrl}/${this.selectedCicloId}`, cicloDataToUpdate).subscribe(() => {
        this.getCiclos();
        this.resetForm();
      });
    } else {
      // Crear ciclo
      this.http.post(this.apiUrl, cicloData).subscribe(() => {
        this.getCiclos();
        this.resetForm();
      });
    }
  }

  // Editar ciclo
  onEdit(ciclo: any): void {
    this.editMode = true;
    this.selectedCicloId = ciclo.id;
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
}
