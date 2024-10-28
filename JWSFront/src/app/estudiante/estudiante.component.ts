import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-estudiante',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './estudiante.component.html',
  styleUrls: ['./estudiante.component.css']
})
export class EstudianteComponent implements OnInit {
  estudianteForm: FormGroup;
  estudiantes: any[] = [];
  estudiantesFiltrados: any[] = [];
  ciclos: any[] = [];
  editMode = false;
  selectedEstudianteId: number | null = null;

  private apiUrl = 'https://localhost:7246/api/Estudiante';
  private ciclosUrl = 'https://localhost:7246/api/Ciclos';

  filtroSemestrePagado: string = 'todos';
  filtroCiclo: number | null = null;

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.estudianteForm = this.fb.group({
      nombres: ['', Validators.required],
      apellidos: ['', Validators.required],
      nroDocumento: ['', Validators.required],
      tipoDocumento: ['', Validators.required],
      fechaNacimiento: ['', Validators.required],
      direccion: ['', Validators.required],
      telefono: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      semestrePagado: [false],
      cicloId: [null, Validators.required],
    });
  }

  ngOnInit(): void {
    this.getEstudiantes();
    this.getCiclos();
  }

  // Obtener estudiantes de la API
  getEstudiantes(): void {
    this.http.get<any[]>(this.apiUrl).subscribe({
      next: (data) => {
        this.estudiantes = data.map(est => ({
          ...est,
          fechaNacimiento: this.formatDate(est.fechaNacimiento)
        }));
        this.aplicarFiltros(); // Aplicar filtros cada vez que se obtienen los estudiantes
      },
      error: (err) => {
        console.error("Error al obtener estudiantes:", err);
      }
    });
  }

  aplicarFiltros(): void {
    this.estudiantesFiltrados = this.estudiantes.filter(estudiante => {
      const cumpleFiltroSemestre = 
        this.filtroSemestrePagado === 'todos' ||
        (this.filtroSemestrePagado === 'pagado' && estudiante.semestrePagado) ||
        (this.filtroSemestrePagado === 'noPagado' && !estudiante.semestrePagado);

      const cumpleFiltroCiclo =
        this.filtroCiclo === null || 
        estudiante.cicloId === this.filtroCiclo;

      return cumpleFiltroSemestre && cumpleFiltroCiclo;
    });
  }

  getEstudiantesFiltrados(): any[] {
    return this.estudiantes.filter(estudiante => {
      const cumpleFiltroSemestre = 
        this.filtroSemestrePagado === 'todos' ||
        (this.filtroSemestrePagado === 'pagado' && estudiante.semestrePagado) ||
        (this.filtroSemestrePagado === 'noPagado' && !estudiante.semestrePagado);
  
      const cumpleFiltroCiclo =
        this.filtroCiclo === null || estudiante.cicloId === this.filtroCiclo;
  
      return cumpleFiltroSemestre && cumpleFiltroCiclo;
    });
  }

  // Obtener ciclos de la API
  getCiclos(): void {
    this.http.get<any[]>(this.ciclosUrl).subscribe((data) => {
      console.log("Lista de ciclos: " + JSON.stringify(data));
      this.ciclos = data;
    });    
  }

  getNombreCiclo(cicloId: number): string {
    const ciclo = this.ciclos.find(c => c.id === cicloId);
    return ciclo ? ciclo.nombre : 'Ciclo no encontrado';
  }
  

  // Enviar formulario para crear o actualizar estudiante
  onSubmit(): void {
    console.log("Entre a onsubmit");
    if (this.estudianteForm.invalid) {
      this.estudianteForm.markAllAsTouched();
      console.log("Estudiante invalido");
      return;
    }

    const estudianteData = {
      ...this.estudianteForm.value,
      fechaNacimiento: this.formatDateToApi(this.estudianteForm.value.fechaNacimiento) // Formatear para enviar
    };

    if (this.editMode && this.selectedEstudianteId !== null) {
      const estudianteDataToUpdate = {
        ...estudianteData,
        id: this.selectedEstudianteId,
      };
      this.http.put(`${this.apiUrl}/${this.selectedEstudianteId}`, estudianteDataToUpdate)
        .subscribe(() => {
          this.getEstudiantes();
          this.resetForm();
          console.log("Hago el put");
        });
    } else {
      this.http.post(this.apiUrl, estudianteData).subscribe(() => {
        this.getEstudiantes();
        this.resetForm();
        console.log("Hago el post");
      });
    }
  }

  // Editar estudiante
  onEdit(estudiante: any): void {
    this.editMode = true;
    this.selectedEstudianteId = estudiante.id;
    this.estudianteForm.patchValue({
      nombres: estudiante.nombres, // Cambiado a "nombres"
      apellidos: estudiante.apellidos, // Cambiado a "apellidos"
      nroDocumento: estudiante.nroDocumento, // Cambiado a "nroDocumento"
      tipoDocumento: estudiante.tipoDocumento, // Cambiado a "tipoDocumento"
      fechaNacimiento: estudiante.fechaNacimiento, // Cambiado a "fechaNacimiento"
      direccion: estudiante.direccion, // Cambiado a "direccion"
      telefono: estudiante.telefono, // Cambiado a "telefono"
      email: estudiante.email, // Cambiado a "email"
      semestrePagado: estudiante.semestrePagado, // Cambiado a "semestrePagado"
      cicloId: estudiante.cicloId, // Cambiado a "cicloId"
    });
  }

  // Eliminar estudiante
  onDelete(id: number): void {
    if (confirm('¿Estás seguro de que deseas eliminar este estudiante?')) {
      this.http.delete(`${this.apiUrl}/${id}`).subscribe(() => {
        this.getEstudiantes();
      });
    }
  }

  // Reiniciar formulario
  resetForm(): void {
    this.editMode = false;
    this.selectedEstudianteId = null;
    this.estudianteForm.reset();
  }

  // Método para formatear la fecha al mostrar
  private formatDate(dateString: string): string {
    return dateString.split('T')[0]; // Devuelve solo la parte de la fecha
  }

  // Método para formatear la fecha antes de enviar
  private formatDateToApi(dateString: string): string {
    return dateString + 'T00:00:00.000'; // Formato requerido para la API
  }

  onCicloChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    const selectedCicloId = selectElement.value;
  
    // Buscar el ciclo seleccionado por su ID
    const selectedCiclo = this.ciclos.find(ciclo => ciclo.id.toString() === selectedCicloId);
  
    // Mostrar ID y nombre en la consola
    if (selectedCiclo) {
      console.log('ID del ciclo seleccionado:', selectedCiclo.id);
      console.log('Nombre del ciclo seleccionado:', selectedCiclo.nombre);
    } else {
      console.log('Ciclo no encontrado');
    }
  }
  
}
