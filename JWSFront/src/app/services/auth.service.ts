import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginResponseDTO } from '../dto/LoginResponseDTO';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7246/api/Usuario';
  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<LoginResponseDTO> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const body = { email, password }; 
    return this.http.post<LoginResponseDTO>(`${this.apiUrl}/validate-credentials`, body, { headers });
  }
}

/**
 * [
  {
    "id": 1,
    "username": "Administrador",
    "email": "admin@mail.com",
    "nombres": "Luiz",
    "apellidos": "Diaz",
    "isAdmin": true
  },
  {
    "id": 2,
    "username": "Usuario",
    "email": "usuario@mail.com",
    "nombres": "Users",
    "apellidos": "Perez",
    "isAdmin": false
  }
]

Admin123@
Usuario123@
 */