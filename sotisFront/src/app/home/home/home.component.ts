import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  selectedFile: File | null = null;
  fileUrl: SafeResourceUrl | null = null;

  constructor(private http: HttpClient, private sanitizer: DomSanitizer) {}

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }

  onButtonClick(): void {
    if (this.selectedFile) {
      const formData = new FormData();
      formData.append('file', this.selectedFile, this.selectedFile.name);

      this.http
        .post('http://localhost:5265/api/python/uploadPDF', formData)
        .subscribe(
          (response) => {
            console.log('File uploaded successfully', response);

            const url = `http://localhost:5265/api/python/getPDF/${this.selectedFile?.name}`;
            this.fileUrl = this.sanitizer.bypassSecurityTrustResourceUrl(url);
            console.log(this.fileUrl);
          },
          (error) => {
            console.error('Error uploading file', error);
          }
        );
    }
  }
}
