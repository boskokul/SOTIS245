import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder } from '@angular/forms';
import { ApiService } from '../services/api.service';
import {
  ConnectAnswerDTO,
  DefinitionAnswerDTO,
  Test,
  TestSampleDTO,
} from '../model/test';
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-test-execution',
  templateUrl: './test-execution.component.html',
  styleUrls: ['./test-execution.component.css'],
})
export class TestExecutionComponent {
  test: Test = {
    id: 0,
    name: '',
    field: {
      id: 0,
      rootTerm: '',
      name: '',
    },
    connectQuestions: [],
    definitionQuestions: [],
  };

  testSample: TestSampleDTO = {
    id: 0,
    takenOn: new Date(),
    studentId: 0,
    testId: 0,
    definitionAnswers: [],
    connectAnswers: [],
  };

  connections: {
    term: string;
    belongingTerm: string;
    color: string;
    questionId: number;
  }[] = [];

  constructor(
    private dialogRef: MatDialogRef<TestExecutionComponent>,
    private fb: FormBuilder,
    private service: ApiService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: { passedTest: Test }
  ) {
    this.test = data.passedTest;
    this.testSample.testId = this.test.id;
    this.testSample.studentId = this.service.getUser().id;
    this.testSample.definitionAnswers = this.test.definitionQuestions.map(
      (question) => {
        const defAnswer: DefinitionAnswerDTO = {
          id: 0,
          term: question.term,
          defQuestionId: question.id,
          answeredDefinition: '',
          isCorrect: false,
          testSampleId: this.testSample.id,
        };
        return defAnswer;
      }
    );
    console.log(this.test);
  }

  drop(event: CdkDragDrop<string[]>, question: any) {
    if (event.previousContainer !== event.container) {
      const draggedTerm = event.previousContainer.data[event.previousIndex];
      const droppedOnTerm = event.container.data[event.currentIndex];

      if (draggedTerm && droppedOnTerm) {
        // Generate a random color
        const color = this.generateRandomColor();

        // Check if a connection already exists for the dragged term in the same question
        const existingConnectionIndex = this.connections.findIndex(
          (connection) =>
            (connection.term === draggedTerm ||
              connection.belongingTerm === draggedTerm) &&
            connection.questionId === question.id
        );

        if (existingConnectionIndex !== -1) {
          // Update the existing connection
          this.connections[existingConnectionIndex] = {
            term: draggedTerm,
            belongingTerm: droppedOnTerm,
            color,
            questionId: question.id,
          };
        } else {
          // Store the new connection
          this.connections.push({
            term: draggedTerm,
            belongingTerm: droppedOnTerm,
            color,
            questionId: question.id,
          });
        }

        console.log(
          `Connected ${draggedTerm} with ${droppedOnTerm} with color ${color}`
        );

        // Detect changes to ensure the view is updated
        this.cdr.detectChanges();
      }
    }
  }

  generateRandomColor(): string {
    const letters = '0123456789ABCDEF';
    let color = '#';
    for (let i = 0; i < 6; i++) {
      color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
  }

  getConnectionColor(term: string, questionId: number): string {
    const connection = this.connections.find(
      (connection) =>
        (connection.term === term || connection.belongingTerm === term) &&
        connection.questionId === questionId
    );
    return connection ? connection.color : '';
  }

  // Method to post the TestSampleDTO
  submitTestSample(): void {
    // Collect Connect Answers
    const connectAnswersMap: { [key: number]: ConnectAnswerDTO } = {};
    this.connections.forEach((connection) => {
      if (!connectAnswersMap[connection.questionId]) {
        connectAnswersMap[connection.questionId] = {
          id: 0, // or assign a unique identifier if available
          connectQuestionId: connection.questionId,
          connectedPairs: [],
          testSampleId: this.testSample.id,
        };
      }
      connectAnswersMap[connection.questionId].connectedPairs.push({
        belongTerm: connection.term,
        belongingTerm: connection.belongingTerm,
        isCorrect: false, // or implement logic to determine correctness
      });
    });
    this.testSample.connectAnswers = Object.values(connectAnswersMap);
    this.service.postTestSample(this.testSample).subscribe(
      (response) => {
        console.log('TestSampleDTO posted successfully', response);
        this.dialogRef.close();
      },
      (error) => {
        console.error('Error posting TestSampleDTO', error);
      }
    );
  }
}
