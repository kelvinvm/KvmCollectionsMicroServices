import { Component, OnInit, signal, inject, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AdminApiService } from '../../services/admin-api.service';
import { ToastService } from '../../services/toast.service';
import { Album } from '../../models/album.model';
import { CreateAlbumRequest } from '../../models/admin.model';

@Component({
  selector: 'app-admin-edit-album',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './admin-edit-album.component.html',
  styleUrl: './admin-edit-album.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdminEditAlbumComponent implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private adminApiService = inject(AdminApiService);
  private toastService = inject(ToastService);
  private readonly API_BASE_URL = 'https://localhost:7043';

  albumId = signal<number>(0);
  albumForm!: FormGroup;
  selectedFile = signal<File | null>(null);
  previewUrl = signal<string | null>(null);
  originalThumbnailUrl = signal<string | null>(null);
  isLoading = signal(true);
  isSubmitting = signal(false);
  errorMessage = signal('');
  successMessage = signal('');

  ngOnInit() {
    this.albumForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      subtitle: ['', Validators.required]
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.albumId.set(parseInt(id, 10));
      this.loadAlbum();
    }
  }

  loadAlbum() {
    this.isLoading.set(true);
    this.adminApiService.getAlbum(this.albumId()).subscribe({
      next: (album: Album) => {
        this.albumForm.patchValue({
          name: album.name,
          subtitle: album.subtitle
        });
        // Store original thumbnail and create full URL for preview
        this.originalThumbnailUrl.set(album.thumbnailUrl);
        this.previewUrl.set(this.getThumbnailUrl(album.thumbnailUrl));
        this.isLoading.set(false);
      },
      error: (error: any) => {
        console.error('Error loading album:', error);
        this.errorMessage.set('Failed to load album. Please try again.');
        this.isLoading.set(false);
      }
    });
  }

  getThumbnailUrl(thumbnailPath: string): string {
    // If the path already includes the protocol, return as-is
    if (thumbnailPath.startsWith('http://') || thumbnailPath.startsWith('https://')) {
      return thumbnailPath;
    }
    // Otherwise, prepend the API base URL
    return `${this.API_BASE_URL}${thumbnailPath}`;
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];
      
      // Validate file type
      if (!file.type.startsWith('image/')) {
        this.errorMessage.set('Please select an image file');
        return;
      }

      // Validate file size (5MB max)
      if (file.size > 5 * 1024 * 1024) {
        this.errorMessage.set('Image size must be less than 5MB');
        return;
      }

      this.selectedFile.set(file);
      this.errorMessage.set('');

      // Create preview
      const reader = new FileReader();
      reader.onload = () => {
        this.previewUrl.set(reader.result as string);
      };
      reader.readAsDataURL(file);
    }
  }

  removeFile() {
    this.selectedFile.set(null);
    // Reset to original thumbnail if available
    if (this.originalThumbnailUrl()) {
      this.previewUrl.set(this.getThumbnailUrl(this.originalThumbnailUrl()!));
    }
  }

  async onSubmit() {
    if (this.albumForm.invalid) {
      Object.keys(this.albumForm.controls).forEach(key => {
        this.albumForm.get(key)?.markAsTouched();
      });
      return;
    }

    this.isSubmitting.set(true);
    this.errorMessage.set('');
    this.successMessage.set('');

    try {
      let thumbnailUrl: string | undefined;

      // Upload new thumbnail if one was selected
      if (this.selectedFile()) {
        thumbnailUrl = await this.adminApiService.uploadThumbnail(this.selectedFile()!);
      } else {
        // Keep the original thumbnail URL if no new file was selected
        thumbnailUrl = this.originalThumbnailUrl() || undefined;
      }

      // Get form values
      const formValues = this.albumForm.value;
      console.log('Form values:', formValues);

      // Prepare album data
      const albumData: CreateAlbumRequest = {
        name: formValues.name || '',
        subtitle: formValues.subtitle || '',
        thumbnailUrl: thumbnailUrl
      };

      console.log('Updating album with data:', albumData);

      // Send to API
      this.adminApiService.updateAlbum(this.albumId(), albumData).subscribe({
        next: (response: Album) => {
          this.isSubmitting.set(false);
          this.toastService.success('Album updated successfully!');
          // Navigate back to albums list
          this.router.navigate(['/admin/albums']);
        },
        error: (error: any) => {
          console.error('Error updating album:', error);
          let errorMsg = 'Failed to update album. ';
          
          if (error.status === 0) {
            errorMsg += 'Cannot connect to server. Please check if the API is running.';
          } else if (error.status === 404) {
            errorMsg += 'Album not found.';
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
      console.error('Error in form submission:', error);
      this.errorMessage.set('An unexpected error occurred');
      this.isSubmitting.set(false);
    }
  }

  cancel() {
    this.router.navigate(['/admin/albums']);
  }
}
