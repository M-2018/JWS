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

  private ciclosUrl = 'https://localhost:7246/api/Ciclos';
  private materiasApiUrl = 'https://localhost:7246/api/Materia';
  private apiUrl = 'https://localhost:7246/api/Estudiante';

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

  onMateriaChange(): void {
    console.log('Materia seleccionada: ', this.selectedMateriaId);
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
    if (selectedId) {
      this.estudiantesFiltrados = this.estudiantes.filter(
        (estudiante) => estudiante.cicloId === selectedId
      );
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

  estudiante.notas.notaTaller = this.validarNota(estudiante.notas.notaTaller, 'Taller');
    estudiante.notas.notaTrabajo = this.validarNota(estudiante.notas.notaTrabajo, 'Trabajo');
    estudiante.notas.notaExposicion = this.validarNota(estudiante.notas.notaExposicion, 'Exposición');
    estudiante.notas.notaTarea = this.validarNota(estudiante.notas.notaTarea, 'Tarea');
    estudiante.notas.notaQuiz1 = this.validarNota(estudiante.notas.notaQuiz1, 'Quiz 1');
    estudiante.notas.notaQuiz2 = this.validarNota(estudiante.notas.notaQuiz2, 'Quiz 2');
    estudiante.notas.notaActitudinal = this.validarNota(estudiante.notas.notaActitudinal, 'Actitudinal');
    estudiante.notas.notaExamFinal = this.validarNota(estudiante.notas.notaExamFinal, 'Examen Final');

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
      alert(`Se ingresó un valor inválido para ${categoria}. La nota será establecida en 0.`);
      return 0;
    }
    return nota;
  }
}
