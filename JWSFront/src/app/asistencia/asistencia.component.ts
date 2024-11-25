import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-asistencia',
  standalone: true,
  imports: [],
  templateUrl: './asistencia.component.html',
  styleUrl: './asistencia.component.css',
})
export class AsistenciaComponent implements OnInit {
  private apiUrl = 'https://localhost:7246/api/Estudiante';
  private ciclosUrl = 'https://localhost:7246/api/Ciclos';
  private MateriasUrl = 'https://localhost:7246/api/Materia';

  estudiantes: any[] = [];
  ciclos: any[] = [];
  materias: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getEstudiantes();
    this.getCiclos();
    this.getMaterias();
  }

  getEstudiantes(): void {
    this.http.get<any[]>(this.apiUrl).subscribe({
      next: (data) => {
        console.log('Estudiantes obtenidos:', data);
        this.estudiantes = data.map((est) => ({
          ...est,
          fechaNacimiento: this.formatDate(est.fechaNacimiento),
        }));
      },
      error: (err) => {
        console.error('Error al obtener estudiantes:', err);
      },
    });
  }
  

  getCiclos(): void {
    this.http.get<any[]>(this.ciclosUrl).subscribe((data) => {
      console.log('Lista de ciclos: ' + JSON.stringify(data));
      this.ciclos = data;
    });
  }

  getNombreCiclo(cicloId: number): string {
    const ciclo = this.ciclos.find((c) => c.id === cicloId);
    return ciclo ? ciclo.nombre : 'Ciclo no encontrado';
  }

  getMaterias(): void {
    this.http.get<any[]>(this.MateriasUrl).subscribe((data) => {
        console.log('Lista de materias: ' + JSON.stringify(data));
      this.materias = data;
    });
  }

  private formatDate(dateString: string): string {
    return dateString.split('T')[0]; // Devuelve solo la parte de la fecha
  }
}
