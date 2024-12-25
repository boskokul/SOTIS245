import { Component } from '@angular/core';
import { Test, TestSample } from '../model/test';
import { MatDialog } from '@angular/material/dialog';
import { ApiService } from '../services/api.service';
import { TestDetailComponent } from '../test-detail/test-detail.component';
import { TestExecutionComponent } from '../test-execution/test-execution.component';

@Component({
  selector: 'app-test-taking',
  templateUrl: './test-taking.component.html',
  styleUrls: ['./test-taking.component.css'],
})
export class TestTakingComponent {
  constructor(private dialog: MatDialog, private service: ApiService) {}
  tests: Test[] = [];
  testSamples: TestSample[] = [];

  tabs: string[] = ['Networks', 'General', 'Reference', 'Hardware'];

  selectTab(index: number): void {
    this.getTests(index);
    this.getTestsSamples(index);
  }

  getTests(index: number): void {
    this.service.getTestsByField(this.tabs[index]).subscribe((result) => {
      this.tests = result;
    });
  }

  getTestsSamples(index: number): void {
    this.service
      .getTestsByFieldTestSamples(this.tabs[index])
      .subscribe((data: TestSample[]) => {
        this.testSamples = data;
        console.log('Test samples fetched successfully', data);
      });
  }

  showTestDetails(test: Test) {
    const dialogRef = this.dialog.open(TestExecutionComponent, {
      width: '700px',
      maxHeight: '80vh',
      data: { passedTest: test }, // Pass data if needed
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
      // this.getTests(0)
    });
  }
}
