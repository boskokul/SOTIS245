import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  selectedFile: File | null = null;
  fileUrls: SafeResourceUrl[] = [];
  tree: any = null;

  constructor(private http: HttpClient, private sanitizer: DomSanitizer) {}

  ngOnInit(): void {
    this.fetchHierarchy('Networks');
    this.loadExistingPDFs();
  }

  private loadExistingPDFs(): void {
    this.http
      .get<string[]>('http://localhost:5265/api/python/getAllPDFs')
      .subscribe(
        (response) => {
          this.fileUrls = response.map((fileName) =>
            this.sanitizer.bypassSecurityTrustResourceUrl(
              `http://localhost:5265/api/python/getPDF/${fileName}`
            )
          );
        },
        (error) => {
          console.error('Error fetching existing PDFs', error);
        }
      );
  }

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
            const safeUrl = this.sanitizer.bypassSecurityTrustResourceUrl(url);

            this.fileUrls.unshift(safeUrl);

            this.selectedFile = null;
          },
          (error) => {
            console.error('Error uploading file', error);
          }
        );
    } else {
      alert('Please select a file first!');
    }
  }

  fetchHierarchy(term: string): void {
    this.http.get<any>(`http://localhost:5265/api/python/acmSubtree`).subscribe(
      (response) => {
        // console.log('Fetched hierarchy:', response);
        if (response) {
          this.tree = response;
        } else {
          console.error('Invalid or empty hierarchy response:', response);
        }
      },
      (error) => {
        console.error('Error fetching hierarchy:', error);
      }
    );
  }
}
