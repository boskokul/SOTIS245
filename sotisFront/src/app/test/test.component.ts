import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TestCreationComponent } from '../test-creation/test-creation.component';
import { ApiService } from '../services/api.service';
import { Test } from '../model/test';
import { TestDetailComponent } from '../test-detail/test-detail.component';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css'],
})
export class TestComponent {
  heading = 'Test management';
  constructor(private dialog: MatDialog, private service: ApiService) {}
  tests: Test[] = [];
  showModalDialog() {
    const dialogRef = this.dialog.open(TestCreationComponent, {
      width: '700px',
      maxHeight: '80vh',
      data: { message: 'Hello from the parent component!' },
    });

    //TODO Fetch all tests!!
    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
      this.getTests(0);
    });
  }

  tabs: string[] = ['Networks', "Hardware", "Human-centered Computing", "Information Systems", "Software Engineering","Mathematics in Computing","Security and Privacy"];

  selectTab(index: number): void {
    this.getTests(index);
  }

  getTests(index: number): void {
    this.service.getTestsByField(this.tabs[index]).subscribe((result) => {
      this.tests = result;
    });
  }

  showTestDetails(test: Test) {
    const dialogRef = this.dialog.open(TestDetailComponent, {
      width: '700px',
      maxHeight: '80vh',
      data: { passedTest: test }, // Pass data if needed
    });

    //TODO Fetch all tests!!
    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
      // this.getTests(0)
    });
  }
}
