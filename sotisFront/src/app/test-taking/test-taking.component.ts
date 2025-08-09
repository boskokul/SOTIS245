import { Component } from '@angular/core';
import { Test, TestSample } from '../model/test';
import { MatDialog } from '@angular/material/dialog';
import { ApiService } from '../services/api.service';
import { TestDetailComponent } from '../test-detail/test-detail.component';
import { TestExecutionComponent } from '../test-execution/test-execution.component';
import { User } from '../model/user';

@Component({
  selector: 'app-test-taking',
  templateUrl: './test-taking.component.html',
  styleUrls: ['./test-taking.component.css'],
})
export class TestTakingComponent {
  user: User = { id: 0, role: 'nothing', username: '' };
  constructor(private dialog: MatDialog, private service: ApiService) {}
  tests: Test[] = [];
  testSamples: TestSample[] = [];

  tabs: string[] = [
    'Networks',
    'Hardware',
    'Human-centered Computing',
    'Information Systems',
    'Software Engineering',
    'Mathematics in Computing',
    'Security and Privacy',
  ];

  ngOnInit(): void {
    this.user = this.service.getUser();
    console.log('AAAAAAAA' + this.service.getUser().role);
  }

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
        this.testSamples.forEach((t) => {
          const correctDef = t.definitionAnswers.filter(
            (dAnswer) => dAnswer.isCorrect
          ).length;

          const totalDef = t.definitionAnswers.length;

          let correctConn = 0;
          t.connectAnswers.forEach((answer) => {
            correctConn += answer.connectedPairs.filter(
              (pair) => pair.isCorrect
            ).length;
          });

          let totalConn = 0;
          t.connectAnswers.forEach((answer) => {
            totalConn += answer.connectedPairs.length;
          });

          console.log(correctDef, correctConn, totalDef, totalConn);
          t.correctness = (correctDef + correctConn) / (totalConn + totalDef);
        });
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

  logout() {
    this.service.logout();
    this.user = this.service.getUser();
  }
}
