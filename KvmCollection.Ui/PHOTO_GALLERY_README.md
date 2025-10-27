# Photo Gallery Application

An elegant Angular application for displaying photos organized in albums with a modern dark theme.

## Features

- **Dark Grey Theme**: Professional dark grey background (#1a1a1a) for reduced eye strain
- **Left Sidebar Navigation**: Easy album switching with active state indicators
- **Photo Grid Layout**: Responsive grid that adapts to screen size
- **Default Album**: Automatically displays the "Default Album" on load
- **Fixed Logo**: Bottom-right positioned logo that remains on screen
- **Hover Effects**: Smooth transitions and interactive photo cards
- **Signal-based State Management**: Modern Angular signals for reactive state

## Project Structure

```
src/app/
├── components/
│   ├── sidebar/
│   │   ├── sidebar.component.ts
│   │   ├── sidebar.component.html
│   │   └── sidebar.component.scss
│   ├── gallery/
│   │   ├── gallery.component.ts
│   │   ├── gallery.component.html
│   │   └── gallery.component.scss
│   └── logo/
│       ├── logo.component.ts
│       ├── logo.component.html
│       └── logo.component.scss
├── models/
│   └── album.model.ts
├── services/
│   └── album.service.ts
├── app.ts
├── app.html
└── app.scss
```

## Components

### SidebarComponent
- Displays list of available albums
- Shows photo count for each album
- Highlights currently selected album
- Provides album switching functionality

### GalleryComponent
- Displays photos in a responsive grid
- Shows album name and photo count
- Handles empty album states
- Photo hover effects with title overlay

### LogoComponent
- Fixed position in bottom-right corner
- Simple SVG camera icon design
- Remains visible during scroll

## Services

### AlbumService
- Manages album and photo data
- Uses Angular signals for reactive state
- Provides methods to:
  - Get all albums
  - Get photos by album ID
  - Set current album
  - Get current album photos

## Styling

- **Background**: Dark grey (#1a1a1a)
- **Sidebar**: Darker grey (#2a2a2a) with 250px width
- **Active Album**: Blue accent (#4a9eff)
- **Photo Cards**: Hover elevation effect
- **Typography**: Modern, clean font stack

## Sample Data

The application comes with sample albums and photos:
- Default Album (6 photos)
- Vacation 2024 (8 photos)
- Family Photos (12 photos)
- Nature Collection (10 photos)
- Urban Landscapes (7 photos)

Sample images are loaded from picsum.photos for demonstration.

## Running the Application

```bash
npm start
```

The application will start on `http://localhost:4200`

## Customization

### Adding Real Photos
Update the `AlbumService` in `src/app/services/album.service.ts` to use your actual photo URLs.

### Changing Theme Colors
Edit the SCSS files in each component to customize colors:
- Sidebar: `src/app/components/sidebar/sidebar.component.scss`
- Gallery: `src/app/components/gallery/gallery.component.scss`
- Global: `src/styles.scss`

### Modifying the Logo
Edit the SVG in `src/app/components/logo/logo.component.html` to use your own logo design.

## Next Steps

Consider adding:
- Photo lightbox/modal for full-size viewing
- Photo upload functionality
- Album creation/editing
- Search and filter capabilities
- Photo metadata (EXIF data)
- Sharing functionality
- Backend integration for persistent storage
