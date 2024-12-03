import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css'],
})
export class AboutComponent {
  constructor(private router: Router) {}

  navigateToNetworksPage() {
    this.router.navigate(['/home', 'Networks']).then(() => {
      console.log('Navigated to Second Page');
    });
  }

  navigateToHardwarePage() {
    this.router.navigate(['/home', 'Hardware']).then(() => {
      console.log('Navigated to Second Page');
    });
  }
}
