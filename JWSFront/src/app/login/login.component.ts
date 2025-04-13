import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service'; // Importa tu servicio
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginResponseDTO } from '../dto/LoginResponseDTO';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';
  username: string | null = '';
  isAdmin: boolean = false;
  isLoggedIn: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const email = this.loginForm.value.email.toLowerCase();
      const password = this.loginForm.value.password;
      this.authService.login(email, password).subscribe(
        (response: LoginResponseDTO) => {
          if (response.isValid) {
            this.username = response.username;
            this.isAdmin = response.isAdmin;
            this.isLoggedIn = true;
            this.authService.updateLoginStatus(true);
          } else {
            this.isLoggedIn = false;
            this.authService.updateLoginStatus(false);
            this.errorMessage = 'Credenciales inválidas';
          }
        },
        (error) => {
          this.errorMessage = 'Error en la conexión con el servidor';
        }
      );
    }
  }

  navigateTo(route: string) {
    this.router.navigate([route]);
  }

}
