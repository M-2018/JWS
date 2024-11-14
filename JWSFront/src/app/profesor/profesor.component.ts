import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

interface Profesor {
  id: number;
  nombres: string;
  apellidos: string;
  nroDocumento: string;
  tipoDocumento: string;
  fechaNacimiento: string;
  direccion: string;
  telefono: string;
  email: string;
  especialidad: string;
  activo: boolean;
}

@Component({
  selector: 'app-profesor',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './profesor.component.html',
  styleUrls: ['./profesor.component.css']
})
export class ProfesorComponent implements OnInit {

  tiposDocumento: string[] = [
    'Registro civil',
    'Tarjeta de identidad',
    'Cedula de ciudadania',
    'Cedula de extranjeria',
    'Pasaporte',
    'Otro'
  ];
  profesores: Profesor[] = [];
  profesorForm: FormGroup;
  isUpdating: boolean = false;
  private apiUrl = 'https://localhost:7246/api/Profesor';

  constructor(private http: HttpClient, private fb: FormBuilder) {
    this.profesorForm = this.fb.group({
      Id: [null],
      Nombres: ['', [Validators.required, Validators.minLength(2)]],
      Apellidos: ['', [Validators.required, Validators.minLength(2)]],
      NroDocumento: ['', [Validators.required, Validators.minLength(5)]],
      TipoDocumento: ['', Validators.required],
      FechaNacimiento: ['', Validators.required],
      Direccion: ['', Validators.required],
      Telefono: ['', [Validators.required, Validators.pattern(/^\+?[\d\-()]*$/)]],
      Email: ['', [Validators.required, Validators.email]],
      Especialidad: ['', Validators.required],
      Activo: [false]
    });
  }

  ngOnInit(): void {
    this.getProfesores();
  }

  getProfesores() {
    console.log('Obteniendo profesores...');
    this.http.get<Profesor[]>(this.apiUrl).subscribe({
      next: (data) => {
        console.log('Profesores obtenidos:', data);
        this.profesores = data;
        if (data.length === 0) {
          console.log('No se encontraron profesores');
        } else {
          console.log(`Se encontraron ${data.length} profesores`);
        }
      },
      error: (error) => {
        console.error('Error al obtener profesores:', error);
      }
    });
  }

  submitForm() {
    console.log('Submit Form llamado');
    console.log('Estado del formulario:', this.profesorForm.valid);
    console.log('Valores del formulario:', this.profesorForm.value);
    
    if (this.profesorForm.valid) {
      console.log('Formulario válido, procediendo a crear/actualizar');
      if (this.isUpdating) {
        this.updateProfesor();
      } else {
        this.createProfesor();
      }
    } else {
      console.log('Formulario inválido. Errores:', this.getFormValidationErrors());
      Object.keys(this.profesorForm.controls).forEach(control => {
        const controlObj = this.profesorForm.get(control);
        if (controlObj?.invalid) {
          controlObj.markAsTouched();
          console.log(`Control ${control} inválido:`, controlObj.errors);
        }
      });
    }
  }

  private getFormValidationErrors() {
    const errors: any = {};
    Object.keys(this.profesorForm.controls).forEach(key => {
      const control = this.profesorForm.get(key);
      if (control?.errors) {
        errors[key] = control.errors;
      }
    });
    return errors;
  }

  createProfesor() {
    console.log('Iniciando creación de profesor');
    const formData = { ...this.profesorForm.value };
    
    // Asegurarse de que la fecha esté en el formato correcto
    if (formData.FechaNacimiento) {
      formData.FechaNacimiento = new Date(formData.FechaNacimiento).toISOString();
    }

    // Eliminar el campo Id si está presente y es null
    if (formData.Id === null) {
      delete formData.Id;
    }

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    console.log('Datos a enviar:', formData);

    this.http.post<Profesor>(this.apiUrl, formData, { headers }).subscribe({
      next: (response) => {
        console.log('Profesor creado exitosamente:', response);
        this.getProfesores();
        this.resetForm();
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error al crear profesor:', error);
        if (error.error instanceof ErrorEvent) {
          console.error('Error del cliente:', error.error.message);
        } else {
          console.error(`Backend retornó código ${error.status}, cuerpo:`, error.error);
        }
      },
      complete: () => {
        console.log('Operación de creación completada');
      }
    });
  }

  updateProfesor() {
    const id = this.profesorForm.get('Id')?.value;
    const formData = { ...this.profesorForm.value };
    
    // Asegurarse de que la fecha esté en el formato correcto
    if (formData.FechaNacimiento) {
      formData.FechaNacimiento = new Date(formData.FechaNacimiento).toISOString();
    }

    console.log('Actualizando profesor:', formData);

    this.http.put<Profesor>(`${this.apiUrl}/${id}`, formData).subscribe({
      next: () => {
        console.log('Profesor actualizado exitosamente');
        this.getProfesores();
        this.resetForm();
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error al actualizar profesor:', error);
        if (error.error instanceof ErrorEvent) {
          console.error('Error del cliente:', error.error.message);
        } else {
          console.error(`Backend retornó código ${error.status}, cuerpo:`, error.error);
        }
      }
    });
  }

  deleteProfesor(id: number | null | undefined) {
    if (id === null || id === undefined) {
      console.error('ID de profesor no válido');
      return;
    }

    if (confirm('¿Estás seguro de que deseas eliminar este profesor?')) {
      this.http.delete(`${this.apiUrl}/${id}`).subscribe({
        next: () => {
          console.log('Profesor eliminado con éxito');
          this.getProfesores();
          this.resetForm();
        },
        error: (error: HttpErrorResponse) => {
          console.error('Error al eliminar profesor:', error);
        }
      });
    }
  }

  editProfesor(profesor: Profesor) {
    console.log('Editando profesor:', profesor);
    this.isUpdating = true;
    
    // Convertir la fecha al formato local para el input type="date"
    const fechaNacimiento = profesor.fechaNacimiento ? 
      new Date(profesor.fechaNacimiento).toISOString().split('T')[0] : '';

    this.profesorForm.patchValue({
      Id: profesor.id,
      Nombres: profesor.nombres,
      Apellidos: profesor.apellidos,
      NroDocumento: profesor.nroDocumento,
      TipoDocumento: profesor.tipoDocumento,
      FechaNacimiento: fechaNacimiento,
      Direccion: profesor.direccion,
      Telefono: profesor.telefono,
      Email: profesor.email,
      Especialidad: profesor.especialidad,
      Activo: profesor.activo
    });
  }

  resetForm() {
    console.log('Reseteando formulario');
    this.isUpdating = false;
    this.profesorForm.reset({
      Id: null,
      Nombres: '',
      Apellidos: '',
      NroDocumento: '',
      TipoDocumento: '',
      FechaNacimiento: '',
      Direccion: '',
      Telefono: '',
      Email: '',
      Especialidad: '',
      Activo: false
    });
  }
}