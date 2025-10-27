import { Component, ChangeDetectionStrategy, input, output, viewChild, ElementRef, effect } from '@angular/core';
import { Photo } from '../../models/album.model';

@Component({
  selector: 'app-photo-viewer',
  templateUrl: './photo-viewer.component.html',
  styleUrl: './photo-viewer.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PhotoViewerComponent {
  readonly photo = input.required<Photo>();
  readonly hasNext = input<boolean>(false);
  readonly hasPrevious = input<boolean>(false);
  
  readonly close = output<void>();
  readonly next = output<void>();
  readonly previous = output<void>();

  private viewerOverlay = viewChild<ElementRef<HTMLDivElement>>('viewerOverlay');

  constructor() {
    // Auto-focus the overlay when component is created for keyboard navigation
    effect(() => {
      const overlay = this.viewerOverlay()?.nativeElement;
      if (overlay) {
        setTimeout(() => overlay.focus(), 0);
      }
    });
  }

  protected onClose(): void {
    this.close.emit();
  }

  protected onNext(): void {
    if (this.hasNext()) {
      this.next.emit();
    }
  }

  protected onPrevious(): void {
    if (this.hasPrevious()) {
      this.previous.emit();
    }
  }

  protected onKeyDown(event: KeyboardEvent): void {
    if (event.key === 'Escape') {
      this.onClose();
    } else if (event.key === 'ArrowRight') {
      this.onNext();
    } else if (event.key === 'ArrowLeft') {
      this.onPrevious();
    }
  }
}
