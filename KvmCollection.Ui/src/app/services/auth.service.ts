import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly ADMIN_PASSWORD = 'admin123'; // In production, this should be handled server-side
  private isAuthenticatedSignal = signal<boolean>(false);

  readonly isAuthenticated = this.isAuthenticatedSignal.asReadonly();

  login(password: string): boolean {
    const success = password === this.ADMIN_PASSWORD;
    this.isAuthenticatedSignal.set(success);
    if (success) {
      sessionStorage.setItem('admin_auth', 'true');
    }
    return success;
  }

  logout(): void {
    this.isAuthenticatedSignal.set(false);
    sessionStorage.removeItem('admin_auth');
  }

  checkAuth(): void {
    const auth = sessionStorage.getItem('admin_auth');
    this.isAuthenticatedSignal.set(auth === 'true');
  }
}
