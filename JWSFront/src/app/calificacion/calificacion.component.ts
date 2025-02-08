import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DescriptionService } from '../services/description.service';

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
  estudiantes: any[] = [];
  estudiantesFiltrados: any[] = [];

  notaTaller: number | null = null;
  notaTrabajo: number | null = null;
  notaExposicion: number | null = null;
  notaTarea: number | null = null;
  notaQuiz1: number | null = null;
  notaQuiz2: number | null = null;
  notaActitudinal: number | null = null;
  notaExamFinal: number | null = null;
  promedioTrabajos: number = 0;
  promedioQuizes: number = 0;
  notaRecuperacion: number | null = null;
  notaHabilitacion: number | null = null;

  private ciclosUrl = 'https://localhost:7246/api/Ciclos';
  private materiasApiUrl = 'https://localhost:7246/api/Materia';
  private apiUrl = 'https://localhost:7246/api/Estudiante';
  private calificacionUrl = 'https://localhost:7246/api/Calificacion';

  constructor(
    private http: HttpClient,
    private descriptionService: DescriptionService
  ) {}

  ngOnInit(): void {
    this.getMaterias();
    this.getCiclos();
    this.getEstudiantes();
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

  // Filtrar materias por ciclo seleccionado
  filtrarMateriasPorCiclo(): void {
    const selectedId = Number(this.selectedCicloId);
    console.log('selectedCicloId: ' + this.selectedCicloId);

    // if (typeof selectedId === typeof this.ciclos[0].id) {
    //   console.log("ciclos coinciden en tipo");
    // } else {
    //   console.error("ciclos no coinciden en tipo");

    // }

    if (selectedId) {
      // Encuentra el ciclo seleccionado
      const ciclo = this.ciclos.find((c) => c.id === selectedId);

      if (ciclo && ciclo.materiasIds) {
        // Filtra las materias basándote en los IDs de materias del ciclo
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
    this.filtrarEstudiantesPorCiclo();
  }

 // Agregar este método en CalificacionComponent
cargarCalificacionesExistentes(): void {
  if (!this.selectedCicloId || !this.selectedMateriaId) {
    return;
  }

  this.http.get<any[]>(this.calificacionUrl).subscribe({
    next: (calificaciones) => {
      // Filtrar calificaciones por ciclo y materia
      const calificacionesFiltradas = calificaciones.filter(c => 
        c.cicloId === this.selectedCicloId && 
        c.materiaId === this.selectedMateriaId
      );

      // Actualizar las notas de los estudiantes con las calificaciones existentes
      this.estudiantesFiltrados.forEach(estudiante => {
        const calificacionExistente = calificacionesFiltradas.find(c => 
          c.estudianteId === estudiante.id
        );

        if (calificacionExistente) {
          estudiante.notas = {
            notaTaller: calificacionExistente.notaTrabajo1,
            notaTrabajo: calificacionExistente.notaTrabajo2,
            notaQuiz1: calificacionExistente.notaEvaluacion1,
            notaQuiz2: calificacionExistente.notaEvaluacion2,
            notaActitudinal: calificacionExistente.notaActitudinal,
            notaExamFinal: calificacionExistente.notaExamenFinal,
            definitiva: calificacionExistente.notaDefinitiva,
            // ... actualizar el resto de las propiedades según sea necesario
          };
          this.calcularPromedio(estudiante);
        }
      });
    },
    error: (error) => {
      console.error('Error al cargar calificaciones:', error);
    }
  });
}

// Modificar onMateriaChange para cargar las calificaciones
onMateriaChange(): void {
  console.log('Materia seleccionada: ', this.selectedMateriaId);
  this.cargarCalificacionesExistentes();
}

  getEstudiantes(): void {
    this.http.get<any[]>(this.apiUrl).subscribe({
      next: (data) => {
        this.estudiantes = data.map((estudiante) => ({
          ...estudiante,
          notas: {
            notaTaller: 0,
            notaTrabajo: 0,
            notaExposicion: 0,
            notaTarea: 0,
            promedioTrabajos: 0,
            desTrabajos: '',
            notaQuiz1: 0,
            notaQuiz2: 0,
            promedioQuizes: 0,
            desQuizes: '', // Nueva propiedad
            notaActitudinal: 0,
            desActitudinal: '', // Nueva propiedad
            notaExamFinal: 0,
            desExamenFinal: '', // Nueva propiedad
            definitiva: 0,
            desDefinitiva: '', // Nueva propiedad
          },
        }));
      },
      error: (error) => {
        console.error('Error al obtener estudiantes:', error);
      },
    });
  }

  // Método para formatear la fecha al mostrar
  private formatDate(dateString: string): string {
    return dateString.split('T')[0]; // Devuelve solo la parte de la fecha
  }

  // Filtrar estudiantes por ciclo seleccionado
  filtrarEstudiantesPorCiclo(): void {
    const selectedId = Number(this.selectedCicloId);
    console.log('selectedId:', selectedId);
    if (selectedId) {
      this.estudiantesFiltrados = this.estudiantes.filter((estudiante) => {
        console.log('Estudiante:', estudiante);
        return estudiante.cicloId === selectedId;
      });
    } else {
      this.estudiantesFiltrados = [];
    }
  }

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

    estudiante.notas.notaTaller = this.validarNota(
      estudiante.notas.notaTaller,
      'Taller'
    );
    estudiante.notas.notaTrabajo = this.validarNota(
      estudiante.notas.notaTrabajo,
      'Trabajo'
    );
    estudiante.notas.notaExposicion = this.validarNota(
      estudiante.notas.notaExposicion,
      'Exposición'
    );
    estudiante.notas.notaTarea = this.validarNota(
      estudiante.notas.notaTarea,
      'Tarea'
    );
    estudiante.notas.notaQuiz1 = this.validarNota(
      estudiante.notas.notaQuiz1,
      'Quiz 1'
    );
    estudiante.notas.notaQuiz2 = this.validarNota(
      estudiante.notas.notaQuiz2,
      'Quiz 2'
    );
    estudiante.notas.notaActitudinal = this.validarNota(
      estudiante.notas.notaActitudinal,
      'Actitudinal'
    );
    estudiante.notas.notaExamFinal = this.validarNota(
      estudiante.notas.notaExamFinal,
      'Examen Final'
    );

    // Cálculo de promedios
    estudiante.notas.promedioTrabajos =
      ((notaTaller || 0) +
        (notaTrabajo || 0) +
        (notaExposicion || 0) +
        (notaTarea || 0)) /
      4;

    estudiante.notas.promedioQuizes = ((notaQuiz1 || 0) + (notaQuiz2 || 0)) / 2;

    estudiante.notas.definitiva =
      estudiante.notas.promedioTrabajos * 0.4 +
      estudiante.notas.promedioQuizes * 0.2 +
      (notaActitudinal || 0) * 0.1 +
      (notaExamFinal || 0) * 0.3;

    // Actualizar todas las descripciones
    estudiante.notas.desTrabajos = this.descriptionService.getDescription(
      estudiante.notas.promedioTrabajos
    );
    estudiante.notas.desQuizes = this.descriptionService.getDescription(
      estudiante.notas.promedioQuizes
    );
    estudiante.notas.desActitudinal = this.descriptionService.getDescription(
      estudiante.notas.notaActitudinal || 0
    );
    estudiante.notas.desExamenFinal = this.descriptionService.getDescription(
      estudiante.notas.notaExamFinal || 0
    );
    estudiante.notas.desDefinitiva = this.descriptionService.getDescription(
      estudiante.notas.definitiva
    );
  }

  // Validar que la nota esté en el rango correcto
  private validarNota(nota: number | null, categoria: string): number {
    if (nota === null || nota < 0 || nota > 10) {
      alert(
        `Se ingresó un valor inválido para ${categoria}. La nota será establecida en 0.`
      );
      return 0;
    }
    return nota;
  }

  guardarSeleccion(): void {
    if (!this.selectedCicloId || !this.selectedMateriaId) {
      alert('Por favor, seleccione un ciclo y una materia antes de guardar.');
      return;
    }
  
    // Crear array de CalificacionDTO
    const calificaciones = this.estudiantesFiltrados.map(estudiante => {
      const notaRecuperacion = estudiante.notas.notaRecuperacion || 0;
      const notaHabilitacion = estudiante.notas.notaHabilitacion || 0;
  
      // Determinar valores de recuperación y habilitación
      const recuperacion = notaRecuperacion > 0;
      const habilitacion = notaHabilitacion > 0;
  
      return {
        id: 0,
        Taller: estudiante.notas.notaTaller || 1,
        Trabajo: estudiante.notas.notaTrabajo || 1,
        Exposicion: estudiante.notas.notaExposicion || 1,
        Tarea: estudiante.notas.notaTarea || 1,
        Quiz1: estudiante.notas.notaQuiz1 || 1,
        Quiz2: estudiante.notas.notaQuiz2 || 1,
        Actitudinal: estudiante.notas.notaActitudinal || 1,
        ExamenFinal: estudiante.notas.notaExamFinal || 1,
        Definitiva: estudiante.notas.definitiva || 1,
        recuperacion,
        notaRecuperacion: estudiante.notas.notaRecuperacion || 1,
        habilitacion,
        notaHabilitacion: estudiante.notas.notaHabilitacion || 1,
        estudianteId: estudiante.id,
        cicloId: this.selectedCicloId,
        materiaId: this.selectedMateriaId,
      };
    });
  
    // Enviar el array de calificaciones directamente
    this.http.post(this.calificacionUrl, calificaciones).subscribe({
      next: (response) => {
        alert('Calificaciones guardadas exitosamente.');
        this.limpiarCampos();
      },
      error: (error) => {
        console.error('Error al guardar los datos:', error);
        alert('Ocurrió un error al guardar las calificaciones. Por favor, verifique los datos e intente nuevamente.');
      }
    });
  }
  
  

  limpiarCampos(): void {
    this.estudiantesFiltrados.forEach((estudiante) => {
      estudiante.notas = {
        notaTaller: 0,
        notaTrabajo: 0,
        notaExposicion: 0,
        notaTarea: 0,
        promedioTrabajos: 0,
        desTrabajos: '',
        notaQuiz1: 0,
        notaQuiz2: 0,
        promedioQuizes: 0,
        desQuizes: '',
        notaActitudinal: 0,
        desActitudinal: '',
        notaExamFinal: 0,
        desExamenFinal: '',
        definitiva: 0,
        desDefinitiva: '',
        notaRecuperacion: 0,
        notaHabilitacion: 0,
      };
    });
  }
  
  

  // Método para validar si la selección es válida (opcional)
  isSelectionValid(): boolean {
    return (
      this.selectedCicloId !== null &&
      this.selectedMateriaId !== null &&
      this.estudiantesFiltrados.length > 0
    );
  }

}
