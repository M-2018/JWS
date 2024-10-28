import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-usuario',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule], // Asegúrate de incluir ReactiveFormsModule aquí
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css']
})
export class UsuarioComponent implements OnInit {
  usuarioForm: FormGroup;
  usuarios: any[] = [];
  editMode = false;
  selectedUsuarioId: number | null = null;

  private apiUrl = 'https://localhost:7246/api/Usuario';

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.usuarioForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      nombres: ['', Validators.required],
      apellidos: ['', Validators.required],
      isAdmin: [false]
    });
  }

  ngOnInit(): void {
    this.getUsuarios();
  }

  getUsuarios(): void {
    this.http.get<any[]>(this.apiUrl).subscribe((data) => {
      this.usuarios = data;
    });
  }

  onSubmit(): void {
    if (this.usuarioForm.invalid) {
      this.usuarioForm.markAllAsTouched();
      return;
    }

    //const usuarioData = this.usuarioForm.value;
    const usuarioData = {
      ...this.usuarioForm.value,
      isAdmin: this.usuarioForm.value.isAdmin === 'true' 
    };

    console.log('Datos del usuario que se envían:', usuarioData);
    if (this.editMode && this.selectedUsuarioId !== null) {
      const usuarioToUpdate = { ...usuarioData, id: this.selectedUsuarioId };
      this.http.put(`${this.apiUrl}/${this.selectedUsuarioId}`, usuarioToUpdate).subscribe(() => {
        this.getUsuarios();
        this.resetForm();
      });
    } else {this.http.post(this.apiUrl, usuarioData).pipe(
      catchError((error) => {
        console.error('Error al crear el usuario:', error);
        if (error.status === 400 && error.error.errors) {
          console.log('Detalles del error:', error.error.errors);
        }
        return throwError(error);
      })).subscribe(() => {
        this.getUsuarios();
        this.resetForm();
      });
    }
  }

  onEdit(usuario: any): void {
    this.editMode = true;
    this.selectedUsuarioId = usuario.id;
    this.usuarioForm.patchValue(usuario);
  }

  onDelete(id: number): void {
    if (confirm('¿Estás seguro de que deseas eliminar este usuario?')) {
      this.http.delete(`${this.apiUrl}/${id}`).subscribe(() => {
        this.getUsuarios();
      });
    }
  }

  resetForm(): void {
    this.editMode = false;
    this.selectedUsuarioId = null;
    this.usuarioForm.reset({
      username: '',
      email: '',
      password: '',
      nombres: '',
      apellidos: '',
      isAdmin: false
    });
  }
}
