import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7246/api/Usuario';
  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<boolean> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const body = { email, password }; 
    return this.http.post<boolean>(`${this.apiUrl}/validate-credentials`, body, { headers });
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