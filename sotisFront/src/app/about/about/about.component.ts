import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { LoginComponent } from 'src/app/auth/login/login.component';
import { User } from 'src/app/model/user';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css'],
})
export class AboutComponent implements OnInit {
  user: User = { id: 0, role: 'nothing', username: '' };
  constructor(
    private router: Router,
    private dialog: MatDialog,
    private service: ApiService
  ) {}

  ngOnInit(): void {
    this.user = this.service.getUser();
    console.log('AAAAAAAA' + this.service.getUser().role);
  }

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

  navigateToTestsCreation() {
    this.router.navigate(['/tests-creation']).then(() => {
      console.log('Navigated to Tests');
    });
  }

  navigateToTestsTaking() {
    this.router.navigate(['/tests-taking']).then(() => {
      console.log('Navigated to Tests');
    });
  }
  login() {
    const dialogRef = this.dialog.open(LoginComponent, {
      width: '700px',
      maxHeight: '80vh',
      data: { passedTest: 'data' }, // Pass data if needed
    });

    //TODO Fetch all tests!!
    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
      // this.getTests(0)
      console.log('AAAAAAAA' + this.service.getUser().role);
      this.user = this.service.getUser();
    });
  }
}
