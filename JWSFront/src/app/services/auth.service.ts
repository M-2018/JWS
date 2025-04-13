import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { LoginResponseDTO } from '../dto/LoginResponseDTO';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7246/api/Usuario';
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  public isLoggedIn$ = this.isLoggedInSubject.asObservable();


  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<LoginResponseDTO> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const body = { email, password }; 
    return this.http.post<LoginResponseDTO>(`${this.apiUrl}/validate-credentials`, body, { headers });
  }

  updateLoginStatus(isLoggedIn: boolean): void {
    this.isLoggedInSubject.next(isLoggedIn);
  }
}
