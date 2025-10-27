import { Component, ChangeDetectionStrategy, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-admin-login',
  imports: [FormsModule],
  templateUrl: './admin-login.component.html',
  styleUrl: './admin-login.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdminLoginComponent {
  private authService = inject(AuthService);
  private router = inject(Router);

  protected password = signal('');
  protected errorMessage = signal('');

  protected onSubmit(): void {
    const success = this.authService.login(this.password());
    if (success) {
      this.router.navigate(['/admin']);
    } else {
      this.errorMessage.set('Invalid password');
      this.password.set('');
    }
  }
}
