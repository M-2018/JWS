import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject, of } from 'rxjs';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { tap, map } from 'rxjs/operators';
import { LoginResponseDTO } from '../dto/LoginResponseDTO';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7246/api/Usuario';
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  public isLoggedIn$ = this.isLoggedInSubject.asObservable();
  
  constructor(private http: HttpClient, private router: Router) {
    const savedAuth = localStorage.getItem('authState');
    this.isLoggedInSubject.next(savedAuth ? JSON.parse(savedAuth).isLoggedIn : false);
  }
   

  login(email: string, password: string): Observable<LoginResponseDTO> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const body = { email, password }; 
    return this.http.post<LoginResponseDTO>(`${this.apiUrl}/validate-credentials`, body, { headers }).pipe(
      tap((response: LoginResponseDTO) => {
        if (response.isValid) {
          // Guardar estado en localStorage
          localStorage.setItem('authState', JSON.stringify({
            isLoggedIn: true,
            username: response.username,
            isAdmin: response.isAdmin
          }));
        }
      })
    );
  }

  logout(): void {
    localStorage.removeItem('authState');
    this.isLoggedInSubject.next(false);
    this.router.navigate(['/login']);
  }

  updateLoginStatus(isLoggedIn: boolean): void {
    // Actualiza el subject
    this.isLoggedInSubject.next(isLoggedIn);
    
    // Actualiza localStorage si es necesario
    const savedAuth = localStorage.getItem('authState');
    if (savedAuth) {
      const currentState = JSON.parse(savedAuth);
      localStorage.setItem('authState', JSON.stringify({
        ...currentState,
        isLoggedIn: isLoggedIn
      }));
    } else if (isLoggedIn) {
      // Si no hay estado guardado pero el usuario está logeado, crea uno básico
      localStorage.setItem('authState', JSON.stringify({
        isLoggedIn: true,
        username: '',
        isAdmin: false
      }));
    }
  }

  checkAuthStatus(): Observable<boolean> {
    return this.http.get<{isValid: boolean}>(`${this.apiUrl}/check-auth`).pipe(
      map(response => response.isValid),
      catchError(() => of(false))
    );
  }
}
