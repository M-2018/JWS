import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DescriptionService } from '../services/description.service';
import { Observable } from 'rxjs';

interface Calificacion {
  id: number;
  taller: number;
  trabajo: number;
  exposicion: number;
  tarea: number;
  quiz1: number;
  quiz2: number;
  actitudinal: number;
  examenFinal: number;
  definitiva: number;
  recuperacion: number | null;
  notaRecuperacion: number;
  habilitacion: number | null;
  notaHabilitacion: number;
  estudianteId: number;
  cicloId: number;
  materiaId: number;
  nombreCompleto: string;
}

interface EstudianteDTO {
  id: number;
  nombres: string;
  apellidos: string;
  nroDocumento: string;
  tipoDocumento: string;
  fechaNacimiento: string;
  direccion: string;
  telefono: string;
  email: string;
  semestrePagado: boolean;
  cicloId: number;
}

@Component({
  selector: 'app-calificacion',
  imports: [FormsModule, CommonModule],
  standalone: true,
  templateUrl: './calificacion.component.html',
  styleUrls: ['./calificacion.component.css'],
})
export class CalificacionComponent implements OnInit {
  materias: any[] = [];
  materiasFiltradas: any[] = [];
  ciclos: any[] = [];
  selectedCicloId: number | null = null;
  selectedMateriaId: number | null = null;
  estudiantesFiltrados: any[] = [];

  // URLs de la API
  private ciclosUrl = 'https://localhost:7246/api/Ciclos';
  private materiasApiUrl = 'https://localhost:7246/api/Materia';
  private calificacionUrl = 'https://localhost:7246/api/Calificacion/Calificaciones';
  private estudiantesPorCicloUrl = 'https://localhost:7246/api/Estudiante/Ciclo'; // Nueva URL

  constructor(
    private http: HttpClient,
    private descriptionService: DescriptionService
  ) {}

  ngOnInit(): void {
    this.getMaterias();
    this.getCiclos();
  }

  // Obtener materias desde la API
  getMaterias(): void {
    this.http.get<any[]>(this.materiasApiUrl).subscribe({
      next: (data) => {
        this.materias = data;
      },
      error: (error) => {
        console.error('Error al obtener materias:', error);
      },
    });
  }

  // Obtener ciclos desde la API
  getCiclos(): void {
    this.http.get<any[]>(this.ciclosUrl).subscribe({
      next: (data) => {
        this.ciclos = data;
      },
      error: (error) => {
        console.error('Error al obtener ciclos:', error);
      },
    });
  }

  // Obtener calificaciones desde la API
  getCalificaciones(cicloId: number, materiaId: number): Observable<Calificacion[]> {
    const url = `${this.calificacionUrl}?cicloId=${cicloId}&materiaId=${materiaId}`;
    return this.http.get<Calificacion[]>(url);
  }

  // Obtener estudiantes por ciclo desde la API
  getEstudiantesPorCiclo(cicloId: number): Observable<EstudianteDTO[]> {
    const url = `${this.estudiantesPorCicloUrl}/${cicloId}`;
    return this.http.get<EstudianteDTO[]>(url);
  }

  // Filtrar materias por ciclo seleccionado
  filtrarMateriasPorCiclo(): void {
    const selectedId = Number(this.selectedCicloId);
    if (selectedId) {
      const ciclo = this.ciclos.find((c) => c.id === selectedId);
      if (ciclo && ciclo.materiasIds) {
        this.materiasFiltradas = this.materias.filter((materia) =>
          ciclo.materiasIds.includes(materia.id)
        );
      } else {
        this.materiasFiltradas = [];
      }
    }
  }

  // Al cambiar el ciclo seleccionado
  onCicloChange(): void {
    this.selectedMateriaId = null;
    this.filtrarMateriasPorCiclo();
    this.estudiantesFiltrados = []; // Limpiar la lista de estudiantes
  }

  // Al cambiar la materia seleccionada
  onMateriaChange(): void {
    if (this.selectedCicloId && this.selectedMateriaId) {
      this.getCalificaciones(this.selectedCicloId, this.selectedMateriaId).subscribe({
        next: (calificaciones) => {
          if (calificaciones.length === 0) {
            // Si no hay calificaciones, obtener estudiantes del ciclo y inicializar notas en 0
            this.getEstudiantesPorCiclo(this.selectedCicloId!).subscribe({
              next: (estudiantes) => {
                this.estudiantesFiltrados = estudiantes.map((estudiante) => ({
                  id: estudiante.id,
                  nombres: estudiante.nombres,
                  apellidos: estudiante.apellidos,
                  notas: {
                    notaTaller: 0,
                    notaTrabajo: 0,
                    notaExposicion: 0,
                    notaTarea: 0,
                    notaQuiz1: 0,
                    notaQuiz2: 0,
                    notaActitudinal: 0,
                    notaExamFinal: 0,
                    definitiva: 0,
                    notaRecuperacion: 0,
                    notaHabilitacion: 0,
                    promedioTrabajos: 0,
                    promedioQuizes: 0,
                    desTrabajos: '',
                    desQuizes: '',
                    desActitudinal: '',
                    desExamenFinal: '',
                    desDefinitiva: '',
                  },
                }));
              },
              error: (error) => {
                console.error('Error al obtener estudiantes:', error);
              },
            });
          } else {
            // Si hay calificaciones, mapear los datos como antes
            this.actualizarCalificacionesEnInterfaz(calificaciones);
          }
        },
        error: (error) => {
          console.error('Error al obtener calificaciones:', error);
        },
      });
    } else {
      console.warn('No se ha seleccionado un ciclo o una materia.');
    }
  }

  // Actualizar las calificaciones en la interfaz
  actualizarCalificacionesEnInterfaz(calificaciones: Calificacion[]): void {
    this.estudiantesFiltrados = calificaciones.map((calificacion) => ({
      id: calificacion.estudianteId,
      nombres: calificacion.nombreCompleto.split(' ')[0],
      apellidos: calificacion.nombreCompleto.split(' ').slice(1).join(' '),
      notas: {
        notaTaller: calificacion.taller,
        notaTrabajo: calificacion.trabajo,
        notaExposicion: calificacion.exposicion,
        notaTarea: calificacion.tarea,
        notaQuiz1: calificacion.quiz1,
        notaQuiz2: calificacion.quiz2,
        notaActitudinal: calificacion.actitudinal,
        notaExamFinal: calificacion.examenFinal,
        definitiva: calificacion.definitiva,
        notaRecuperacion: calificacion.notaRecuperacion,
        notaHabilitacion: calificacion.notaHabilitacion,
        promedioTrabajos: (calificacion.taller + calificacion.trabajo + calificacion.exposicion + calificacion.tarea) / 4,
        promedioQuizes: (calificacion.quiz1 + calificacion.quiz2) / 2,
        desTrabajos: '',
        desQuizes: '',
        desActitudinal: '',
        desExamenFinal: '',
        desDefinitiva: '',
      },
    }));

    // Recalcular promedios para todos los estudiantes
    this.estudiantesFiltrados.forEach((estudiante) => this.calcularPromedio(estudiante));
  }

  // Calcular el promedio de las notas
  calcularPromedio(estudiante: any): void {
    const {
      notaTaller,
      notaTrabajo,
      notaExposicion,
      notaTarea,
      notaQuiz1,
      notaQuiz2,
      notaActitudinal,
      notaExamFinal,
    } = estudiante.notas;

    estudiante.notas.promedioTrabajos =
      ((notaTaller || 0) +
        (notaTrabajo || 0) +
        (notaExposicion || 0) +
        (notaTarea || 0)) /
      4;
    estudiante.notas.promedioQuizes =
      ((notaQuiz1 || 0) + (notaQuiz2 || 0)) / 2;
    estudiante.notas.definitiva =
      estudiante.notas.promedioTrabajos * 0.4 +
      estudiante.notas.promedioQuizes * 0.4 +
      (notaActitudinal || 0) * 0.1 +
      (notaExamFinal || 0) * 0.1;
  }

  // Guardar las calificaciones modificadas
  // guardarSeleccion(): void {
  //   const calificacionesParaGuardar = this.estudiantesFiltrados.map((estudiante) => ({
  //     id: 0, // en minúscula
  //     taller: estudiante.notas.notaTaller || 0,
  //     trabajo: estudiante.notas.notaTrabajo || 0,
  //     exposicion: estudiante.notas.notaExposicion || 0,
  //     tarea: estudiante.notas.notaTarea || 0,
  //     quiz1: estudiante.notas.notaQuiz1 || 0,
  //     quiz2: estudiante.notas.notaQuiz2 || 0,
  //     actitudinal: estudiante.notas.notaActitudinal || 0,
  //     examenFinal: estudiante.notas.notaExamFinal || 0,
  //     definitiva: estudiante.notas.definitiva || 0,
  //     recuperacion: false, // Agregar valor booleano
  //     notaRecuperacion: estudiante.notas.notaRecuperacion || 0,
  //     habilitacion: false, // Agregar valor booleano
  //     notaHabilitacion: estudiante.notas.notaHabilitacion || 0,
  //     estudianteId: estudiante.id,
  //     cicloId: Number(this.selectedCicloId),
  //     materiaId: Number(this.selectedMateriaId)
  //   }));
  
  //   console.log('Calificaciones a enviar:', calificacionesParaGuardar);
  
  //   this.http.post<any>('https://localhost:7246/api/Calificacion', calificacionesParaGuardar)
  //     .subscribe({
  //       next: (response) => {
  //         console.log('Calificaciones guardadas exitosamente', response);
  //         // Aquí podrías mostrar un mensaje de éxito
  //       },
  //       error: (error) => {
  //         console.error('Error al guardar calificaciones:', error);
  //         if (error.error?.errors) {
  //           console.error('Errores de validación:', error.error.errors);
  //         }
  //         // Aquí podrías mostrar un mensaje de error
  //       }
  //     });
  // }

  // Guardar las calificaciones modificadas
guardarSeleccion(): void {
  if (!this.selectedCicloId || !this.selectedMateriaId) {
    alert('Por favor seleccione un ciclo y una materia antes de guardar');
    return;
  }

  if (this.estudiantesFiltrados.length === 0) {
    alert('No hay estudiantes para guardar');
    return;
  }

  const confirmacion = confirm('¿Está seguro que desea guardar estas calificaciones?');
  if (!confirmacion) {
    return;
  }

  const calificacionesParaGuardar = this.estudiantesFiltrados.map((estudiante) => ({
    id: 0,
    taller: estudiante.notas.notaTaller || 0,
    trabajo: estudiante.notas.notaTrabajo || 0,
    exposicion: estudiante.notas.notaExposicion || 0,
    tarea: estudiante.notas.notaTarea || 0,
    quiz1: estudiante.notas.notaQuiz1 || 0,
    quiz2: estudiante.notas.notaQuiz2 || 0,
    actitudinal: estudiante.notas.notaActitudinal || 0,
    examenFinal: estudiante.notas.notaExamFinal || 0,
    definitiva: estudiante.notas.definitiva || 0,
    recuperacion: false,
    notaRecuperacion: estudiante.notas.notaRecuperacion || 0,
    habilitacion: false,
    notaHabilitacion: estudiante.notas.notaHabilitacion || 0,
    estudianteId: estudiante.id,
    cicloId: Number(this.selectedCicloId),
    materiaId: Number(this.selectedMateriaId)
  }));

  console.log('Calificaciones a enviar:', calificacionesParaGuardar);

  this.http.post<any>('https://localhost:7246/api/Calificacion', calificacionesParaGuardar)
    .subscribe({
      next: (response) => {
        console.log('Calificaciones guardadas exitosamente', response);
        alert('¡Calificaciones guardadas correctamente!');
        
        // Limpiar todo después de guardar exitosamente
        this.selectedCicloId = null;
        this.selectedMateriaId = null;
        this.materiasFiltradas = [];
        this.estudiantesFiltrados = [];
      },
      error: (error) => {
        console.error('Error al guardar calificaciones:', error);
        let errorMessage = 'Ocurrió un error al guardar las calificaciones';
        
        if (error.error?.errors) {
          console.error('Errores de validación:', error.error.errors);
          errorMessage += '\nErrores de validación:\n';
          for (const key in error.error.errors) {
            errorMessage += `- ${error.error.errors[key].join(', ')}\n`;
          }
        } else if (error.error?.title) {
          errorMessage += `\n${error.error.title}`;
        }
        
        alert(errorMessage);
      }
    });
}

// Método para validar la nota al perder el foco
validarNota(event: FocusEvent, campo: string, estudiante: any): void {
  const input = event.target as HTMLInputElement;
  const valor = parseFloat(input.value);
  
  if (isNaN(valor)) {
    this.mostrarErrorYEnfocar(input, 'Debe ingresar un valor numérico');
    return;
  }

  if (valor < 3 || valor > 10) {
    this.mostrarErrorYEnfocar(input, 'La nota debe estar entre 3 y 10');
    return;
  }

  // Si pasa la validación, actualizar el modelo
  estudiante.notas[campo] = valor;
  this.calcularPromedio(estudiante);
}

// Método para mostrar error y regresar el foco
mostrarErrorYEnfocar(input: HTMLInputElement, mensaje: string): void {
  alert(mensaje);
  
  // Usamos setTimeout para asegurar que el alert no bloquee el focus
  setTimeout(() => {
    input.focus();
    input.select();
  }, 0);
}

}