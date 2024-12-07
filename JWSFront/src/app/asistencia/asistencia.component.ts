import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { catchError, of, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';


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
  estudiantesEditar: any[] = [];
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
    this.getEstudiantesEditar();
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
      presente:
        estudiante.asistencia !== undefined ? estudiante.asistencia : false, // Asigna 'false' si no está marcado
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
          this.fechaSeleccionada = '';
          this.materiaSeleccionada = null;
          this.cicloSeleccionado = null;
          this.estudiantesFiltrados = [];
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

  // fetchAssistanceData(): void {
  //   if (!this.editFechaSeleccionada || !this.editMateriaSeleccionada || !this.editCicloSeleccionado) {
  //     return; // No realiza ninguna acción si falta algún dato
  //   }

  //   const url = `${this.AsistenciasUrl}/filtrar?fecha=${this.editFechaSeleccionada}&cicloId=${this.editCicloSeleccionado}&materiaId=${this.editMateriaSeleccionada}`;

  //   this.http.get<any[]>(url).subscribe({
  //     next: (data) => {
  //       console.log('Datos de asistencia:', data);
  //       this.editEstudiantes = data;
  //       this.mensajeError = '';
  //     },
  //     error: () => {
  //       this.editEstudiantes = [];
  //       this.mensajeError = 'No se encontraron asistencias para los filtros seleccionados.';
  //     },
  //   });
  // }

  // guardarCambiosAsistencia(): void {
  //   // Preparar los datos para enviar
  //   const asistenciasActualizar = this.editEstudiantes.map((estudiante) => ({
  //     Id: estudiante.id, // Asegúrate de enviar el ID de la asistencia
  //     Fecha: this.editFechaSeleccionada,
  //     Presente: estudiante.presente,
  //     EstudianteId: estudiante.estudianteId, // Asegúrate de enviar el ID del estudiante
  //     MateriaId: this.editMateriaSeleccionada,
  //     CicloId: this.editCicloSeleccionado,
  //   }));

  //   const url = `${this.AsistenciasUrl}/ActualizarAsistencias`;

  //   console.log(
  //     'asistenciasActualizar: ' + JSON.stringify(asistenciasActualizar)
  //   );

  //   this.http.put(url, asistenciasActualizar).subscribe({
  //     next: (response) => {
  //       console.log('Asistencias actualizadas:', response);
  //       alert('Asistencias actualizadas correctamente');
  //     },
  //     error: (err) => {
  //       console.error('Error al actualizar asistencias:', err);
  //     },
  //   });
  // }

  guardarCambiosAsistencia(): void {
    const asistenciasActualizar = this.editEstudiantes.map((estudiante) => ({
      Id: estudiante.id,
      Fecha: this.editFechaSeleccionada,
      Presente: estudiante.presente,
      EstudianteId: estudiante.estudianteId,
      MateriaId: this.editMateriaSeleccionada,
      CicloId: this.editCicloSeleccionado,
    }));
  
    const url = `${this.AsistenciasUrl}/ActualizarAsistencias`;
  
    this.http.put(url, asistenciasActualizar).pipe(
      // Manejo de errores más robusto
      catchError((error: HttpErrorResponse) => {
        // Si es un error HTTP 200 (OK), tratarlo como éxito
        if (error.status === 200) {
          return of(error.error.text); // Devolver el texto de la respuesta
        }
        // Para otros errores, relanzar la excepción
        return throwError(() => error);
      })
    ).subscribe({
      next: (response: any) => {
        console.log('Asistencias actualizadas:', response);
        alert('Asistencias actualizadas correctamente');
      },
      error: (err) => {
        console.error('Error al actualizar asistencias:', err);
        alert('Error al actualizar las asistencias');
      }
    });
  }





  // Modificar fetchAssistanceData para asegurar que se recuperen todos los datos necesarios
  // fetchAssistanceData(): void {
  //   if (
  //     !this.editFechaSeleccionada ||
  //     !this.editMateriaSeleccionada ||
  //     !this.editCicloSeleccionado
  //   ) {
  //     return; // No realiza ninguna acción si falta algún dato
  //   }

  //   const url = `${this.AsistenciasUrl}/filtrar?fecha=${this.editFechaSeleccionada}&cicloId=${this.editCicloSeleccionado}&materiaId=${this.editMateriaSeleccionada}`;

  //   this.http.get<any[]>(url).subscribe({
  //     next: (data) => {
  //       console.log('Datos de asistencia:', data);
  //       // Mapear los datos para asegurar que tengamos todas las propiedades necesarias
  //       this.editEstudiantes = data.map((asistencia) => ({
  //         ...asistencia,
  //         presente: asistencia.presente ?? false,
  //         estudianteId: asistencia.estudianteId,
  //         nombres: asistencia.nombres,
  //         apellidos: asistencia.apellidos,
  //       }));
  //       this.mensajeError = '';
  //     },
  //     error: () => {
  //       this.editEstudiantes = [];
  //       this.mensajeError =
  //         'No se encontraron asistencias para los filtros seleccionados.';
  //     },

  //   });
  // }

  fetchAssistanceData(): void {
    if (
      !this.editFechaSeleccionada ||
      !this.editMateriaSeleccionada ||
      !this.editCicloSeleccionado
    ) {
      return; // No realiza ninguna acción si falta algún dato
    }

    this.getEstudiantesEditar(); // Trae los estudiantes con sus datos

    const url = `${this.AsistenciasUrl}/filtrar?fecha=${this.editFechaSeleccionada}&cicloId=${this.editCicloSeleccionado}&materiaId=${this.editMateriaSeleccionada}`;

    this.http.get<any[]>(url).subscribe({
      next: (data) => {
        console.log('Datos de asistencia:', data);
        // Mapear los datos y combinar con los nombres y apellidos de estudiantesEditar
        this.editEstudiantes = data.map((asistencia) => {
          const estudiante = this.estudiantesEditar.find(
            (e) => e.id === asistencia.estudianteId
          );
          return {
            ...asistencia,
            presente: asistencia.presente ?? false,
            nombres: estudiante?.nombres || 'No encontrado',
            apellidos: estudiante?.apellidos || 'No encontrado',
          };
        });
        this.mensajeError = '';
        console.log('this.editEstudiantes: ', this.editEstudiantes);
      },
      error: () => {
        this.editEstudiantes = [];
        this.mensajeError =
          'No se encontraron asistencias para los filtros seleccionados.';
      },
    });
  }

  getEstudiantesEditar(): void {
    this.http.get<any[]>(this.EstudianteUrl).subscribe({
      next: (data) => {
        this.estudiantesEditar = data.map((est) => ({
          ...est,
          fechaNacimiento: this.formatDate(est.fechaNacimiento),
        }));
        console.log(
          'this.estudiantesEditar : ' + JSON.stringify(this.estudiantesEditar)
        );
      },
      error: (err) => {
        console.error('Error al obtener estudiantes:', err);
      },
    });
  }
}
