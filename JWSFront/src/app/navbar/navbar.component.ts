import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  isLoggedIn: boolean = false;
  isCollapsed: boolean = false;
  private subscription: Subscription | null = null;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    // Primero suscríbete al observable
    this.subscription = this.authService.isLoggedIn$.subscribe(
      status => {
        this.isLoggedIn = status;
        if (!this.isLoggedIn) {
          this.router.navigate(['/login']);
        }
      }
    );
    
    // Luego verifica el estado desde localStorage
    // (Esto es redundante si AuthService ya lo hace en su constructor,
    // pero asegura que tu componente tenga el estado actualizado)
    const savedAuth = localStorage.getItem('authState');
    const initialState = savedAuth ? JSON.parse(savedAuth).isLoggedIn : false;
    
    // Si hay diferencia entre el estado del servicio y localStorage,
    // actualiza el servicio (por si acaso)
    // if (initialState !== this.isLoggedIn) {
    //   this.authService.updateLoginStatus(initialState);
    // }
  }

  toggleMenu() {
    this.isCollapsed = !this.isCollapsed;
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  signOut() {
    this.authService.logout();
    this.isCollapsed = true; 
    this.router.navigate(['/login']);
  }
}
