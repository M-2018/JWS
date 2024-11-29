import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-asistencia',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './asistencia.component.html',
  styleUrl: './asistencia.component.css',
})
export class AsistenciaComponent implements OnInit {
  private EstudianteUrl = 'https://localhost:7246/api/Estudiante';
  private ciclosUrl = 'https://localhost:7246/api/Ciclos';
  private MateriasUrl = 'https://localhost:7246/api/Materia';
  private AsistenciasUrl = 'https://localhost:7246/api/Asistencias';

  estudiantes: any[] = [];
  ciclos: any[] = [];
  materias: any[] = [];
  estudiantesFiltrados: any[] = [];

  fechaSeleccionada: string = '';
  cicloSeleccionado: number | null = null; 
  materiaSeleccionada: number | null = null; 

  editFechaSeleccionada: string = '';
editMateriaSeleccionada: number | null = null;
editCicloSeleccionado: number | null = null;
editEstudiantes: any[] = [];
mensajeError: string = '';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getCiclos();
    this.getMaterias();
  }

  // Obtener lista de ciclos
  getCiclos(): void {
    this.http.get<any[]>(this.ciclosUrl).subscribe((data) => {
      console.log('Lista de ciclos:', data);
      this.ciclos = data;
    });
  }

  // Obtener lista de materias
  getMaterias(): void {
    this.http.get<any[]>(this.MateriasUrl).subscribe((data) => {
      console.log('Lista de materias:', data);
      this.materias = data;
    });
  }

  // Obtener estudiantes por ciclo
  getEstudiantesXCiclo(event: Event): void {
    const target = event.target as HTMLSelectElement;
  
    if (target) {
      const cicloId = parseInt(target.value, 10);
      this.cicloSeleccionado = cicloId;
  
      // Ahora obtener los estudiantes filtrados
      if (this.cicloSeleccionado) {
        const url = `${this.EstudianteUrl}/Ciclo/${this.cicloSeleccionado}`;
        this.http.get<any[]>(url).subscribe({
          next: (data) => {
            console.log('Estudiantes filtrados por ciclo:', data);
            this.estudiantesFiltrados = data.map((est) => ({
              ...est,
              fechaNacimiento: this.formatDate(est.fechaNacimiento),
            }));
          },
          error: (err) => {
            console.error('Error al obtener estudiantes por ciclo:', err);
          },
        });
      }
    }
  }
  

  // Guardar selección de ciclo
  seleccionarCiclo(event: Event): void {
    // Convertir el target a un elemento de tipo HTMLSelectElement
    const target = event.target as HTMLSelectElement;
    
    // Verificar que target no sea nulo
    if (target) {
      // Convertir el valor a un número entero
      const cicloId = parseInt(target.value, 10);
      
      // Asignar el ciclo seleccionado
      this.cicloSeleccionado = cicloId;
      
      // Log para depuración
      console.log(`Ciclo seleccionado: ${cicloId}`);
    }
  }

  // Guardar selección de materia
  seleccionarMateria(event: Event): void {
    const target = event.target as HTMLSelectElement;
    
    if (target) {
      const materiaId = parseInt(target.value, 10);
      
      this.materiaSeleccionada = materiaId;
      
      console.log(`Materia seleccionada: ${materiaId}`);
    }
  }

  // Formatear fecha
  private formatDate(dateString: string): string {
    return dateString.split('T')[0]; // Devuelve solo la parte de la fecha
  }

   // Método para guardar asistencias
   guardarAsistencias(): void {
    // Asegurarnos de que todos los estudiantes tengan un valor de asistencia
    const asistencias = this.estudiantesFiltrados.map((estudiante) => ({
      fecha: this.fechaSeleccionada,
      presente: estudiante.asistencia !== undefined ? estudiante.asistencia : false,  // Asigna 'false' si no está marcado
      estudianteId: estudiante.id,
      materiaId: this.materiaSeleccionada,
      cicloId: this.cicloSeleccionado,
    }));
  
    if (asistencias.length > 0) {
      // Enviar las asistencias al backend
      this.http.post(this.AsistenciasUrl, asistencias).subscribe({
        next: () => {
          console.log('Asistencias guardadas con éxito');
          alert('Asistencias guardadas con éxito');
        },
        error: (err) => {
          console.error('Error al guardar asistencias:', err);
          alert('Ocurrió un error al guardar las asistencias');
        },
      });
    } else {
      alert('Debe seleccionar al menos una asistencia');
    }
  }

  fetchAssistanceData(): void {
    if (!this.editFechaSeleccionada || !this.editMateriaSeleccionada || !this.editCicloSeleccionado) {
      return; // No realiza ninguna acción si falta algún dato
    }
  
    const url = `${this.AsistenciasUrl}/filtrar?fecha=${this.editFechaSeleccionada}&cicloId=${this.editCicloSeleccionado}&materiaId=${this.editMateriaSeleccionada}`;
  
    this.http.get<any[]>(url).subscribe({
      next: (data) => {
        console.log('Datos de asistencia:', data);
        this.editEstudiantes = data;
        this.mensajeError = '';
      },
      error: () => {
        this.editEstudiantes = [];
        this.mensajeError = 'No se encontraron asistencias para los filtros seleccionados.';
      },
    });
  }
  
  guardarCambiosAsistencia(): void {
    const url = this.AsistenciasUrl;
  
    this.http.put(url, this.editEstudiantes).subscribe({
      next: () => {
        alert('Asistencias actualizadas correctamente');
      },
      error: () => {
        alert('Error al actualizar asistencias');
      },
    });
  }
  

}
