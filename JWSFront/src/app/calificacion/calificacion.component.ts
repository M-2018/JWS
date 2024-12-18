import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

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

  private ciclosUrl = 'https://localhost:7246/api/Ciclos';
  private materiasApiUrl = 'https://localhost:7246/api/Materia';
  private apiUrl = 'https://localhost:7246/api/Estudiante';


  constructor(private http: HttpClient) {}

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
    console.log("selectedCicloId: " + this.selectedCicloId);
  
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
        this.materiasFiltradas = this.materias.filter(materia => 
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
    console.log("Materia seleccionada: ", this.selectedMateriaId);
  }

  // Obtener estudiantes de la API
  getEstudiantes(): void {
    this.http.get<any[]>(this.apiUrl).subscribe({
      next: (data) => {
        this.estudiantes = data.map(est => ({
          ...est,
          fechaNacimiento: this.formatDate(est.fechaNacimiento)
        }));
        console.log("Estudiantes: ", this.estudiantes);
      },
      error: (err) => {
        console.error("Error al obtener estudiantes:", err);
      }
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
    this.estudiantesFiltrados = this.estudiantes.filter(estudiante => estudiante.cicloId === selectedId);
  } else {
    this.estudiantesFiltrados = [];
  }
}

}
