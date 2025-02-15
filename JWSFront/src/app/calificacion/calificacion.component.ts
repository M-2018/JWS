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

  private ciclosUrl = 'https://localhost:7246/api/Ciclos';
  private materiasApiUrl = 'https://localhost:7246/api/Materia';
  private calificacionUrl = 'https://localhost:7246/api/Calificacion/Calificaciones';

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
          this.actualizarCalificacionesEnInterfaz(calificaciones);
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
        desTrabajos: '', // Puedes ajustar esto según tu lógica
        desQuizes: '', // Puedes ajustar esto según tu lógica
        desActitudinal: '', // Puedes ajustar esto según tu lógica
        desExamenFinal: '', // Puedes ajustar esto según tu lógica
        desDefinitiva: '', // Puedes ajustar esto según tu lógica
      },
    }));

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
  guardarSeleccion(): void {
    const calificacionesParaGuardar = this.estudiantesFiltrados.map((estudiante) => ({
      cicloId: this.selectedCicloId,
      materiaId: this.selectedMateriaId,
      estudianteId: estudiante.id,
      notaTrabajo1: estudiante.notas.notaTaller,
      notaTrabajo2: estudiante.notas.notaTrabajo,
      notaEvaluacion1: estudiante.notas.notaQuiz1,
      notaEvaluacion2: estudiante.notas.notaQuiz2,
      notaActitudinal: estudiante.notas.notaActitudinal,
      notaExamenFinal: estudiante.notas.notaExamFinal,
      notaDefinitiva: estudiante.notas.definitiva,
    }));

    this.http.post(this.calificacionUrl, calificacionesParaGuardar).subscribe({
      next: () => {
        console.log('Calificaciones guardadas exitosamente');
      },
      error: (error) => {
        console.error('Error al guardar calificaciones:', error);
      },
    });
  }
}