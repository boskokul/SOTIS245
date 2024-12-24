import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { LoginComponent } from 'src/app/auth/login/login.component';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css'],
})
export class AboutComponent {
  constructor(private router: Router,private dialog: MatDialog, private service: ApiService) {}

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

  navigateToTests() {
    this.router.navigate(['/tests']).then(() => {
      console.log('Navigated to Tests');
    });
    
    
  }
  login(){
    const dialogRef = this.dialog.open(LoginComponent, {
        width: '700px', 
        maxHeight: '80vh',
        data: { passedTest: "data" } // Pass data if needed
      });
    
      //TODO Fetch all tests!!
      dialogRef.afterClosed().subscribe(result => {
        console.log('The dialog was closed');
        // this.getTests(0)
      });
  }
}
