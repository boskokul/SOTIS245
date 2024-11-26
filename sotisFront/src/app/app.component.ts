import { Component, OnInit } from '@angular/core';
import { ApiService } from './services/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  data: any;
  title: string;

  constructor(private apiService: ApiService) {
    this.title = 'SOTIS';
  }

  // ngOnInit(): void {
  //   this.apiService.getData().subscribe((response) => {
  //     this.data = response;
  //   });
  // }
}
