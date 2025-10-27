import { Component, ChangeDetectionStrategy, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminApiService } from '../../services/admin-api.service';
import { ToastService } from '../../services/toast.service';
import { CreateAlbumRequest } from '../../models/admin.model';

@Component({
  selector: 'app-create-album',
  imports: [ReactiveFormsModule],
  templateUrl: './create-album.component.html',
  styleUrl: './create-album.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CreateAlbumComponent {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private adminApiService = inject(AdminApiService);
  private toastService = inject(ToastService);

  protected readonly albumForm: FormGroup;
  protected readonly selectedFile = signal<File | null>(null);
  protected readonly previewUrl = signal<string | null>(null);
  protected readonly isSubmitting = signal(false);
  protected readonly successMessage = signal('');
  protected readonly errorMessage = signal('');

  constructor() {
    this.albumForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      subtitle: ['', Validators.required]
    });
  }

  protected onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];
      
      // Validate file type
      if (!file.type.startsWith('image/')) {
        this.errorMessage.set('Please select an image file');
        return;
      }

      // Validate file size (max 5MB)
      if (file.size > 5 * 1024 * 1024) {
        this.errorMessage.set('File size must be less than 5MB');
        return;
      }

      this.selectedFile.set(file);
      
      // Create preview
      const reader = new FileReader();
      reader.onload = () => {
        this.previewUrl.set(reader.result as string);
      };
      reader.readAsDataURL(file);
      
      this.errorMessage.set('');
    }
  }

  protected removeFile(): void {
    this.selectedFile.set(null);
    this.previewUrl.set(null);
  }

  protected async onSubmit(): Promise<void> {
    if (this.albumForm.invalid) {
      Object.keys(this.albumForm.controls).forEach(key => {
        this.albumForm.get(key)?.markAsTouched();
      });
      return;
    }

    if (!this.selectedFile()) {
      this.errorMessage.set('Please select a thumbnail image');
      return;
    }

    this.isSubmitting.set(true);
    this.errorMessage.set('');
    this.successMessage.set('');

    try {
      // Upload thumbnail to local images directory
      const thumbnailUrl = await this.adminApiService.uploadThumbnail(this.selectedFile()!);

      // Get form values
      const formValues = this.albumForm.value;
      console.log('Form values:', formValues);

      // Prepare album data
      const albumData: CreateAlbumRequest = {
        name: formValues.name || '',
        subtitle: formValues.subtitle || '',
        thumbnailUrl: thumbnailUrl
      };

      console.log('Submitting album data:', albumData);

      // Send to API (without thumbnail file, just the URL)
      this.adminApiService.createAlbum(albumData).subscribe({
        next: (response) => {
          this.isSubmitting.set(false);
          this.toastService.success('Album created successfully!');
          // Navigate back to albums list
          this.router.navigate(['/admin/albums']);
        },
        error: (error) => {
          console.error('Error creating album:', error);
          let errorMsg = 'Failed to create album. ';
          
          if (error.status === 0) {
            errorMsg += 'Cannot connect to server. Please check if the API is running.';
          } else if (error.status === 404) {
            errorMsg += 'API endpoint not found.';
          } else if (error.status === 401 || error.status === 403) {
            errorMsg += 'Authentication required.';
          } else if (error.error?.message) {
            errorMsg += error.error.message;
          } else if (error.message) {
            errorMsg += error.message;
          } else {
            errorMsg += `Server error (${error.status})`;
          }
          
          this.toastService.error(errorMsg, 5000);
          this.isSubmitting.set(false);
        }
      });
    } catch (error) {
      this.errorMessage.set('Failed to upload thumbnail. Please try again.');
      console.error('Error uploading thumbnail:', error);
      this.isSubmitting.set(false);
    }
  }

  protected getFieldError(fieldName: string): string {
    const control = this.albumForm.get(fieldName);
    if (control?.touched && control?.errors) {
      if (control.errors['required']) {
        return `${fieldName.charAt(0).toUpperCase() + fieldName.slice(1)} is required`;
      }
      if (control.errors['minlength']) {
        return `${fieldName.charAt(0).toUpperCase() + fieldName.slice(1)} must be at least ${control.errors['minlength'].requiredLength} characters`;
      }
    }
    return '';
  }
}
