import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TestCreationComponent } from '../test-creation/test-creation.component';
import { FormBuilder } from '@angular/forms';
import { ApiService } from '../services/api.service';
import { Test } from '../model/test';

@Component({
  selector: 'app-test-detail',
  templateUrl: './test-detail.component.html',
  styleUrls: ['./test-detail.component.css']
})
export class TestDetailComponent {
  test: Test = {
    id: 0,
    name: "",
    field: {
      id: 0,
      rootTerm: "",
      name: ""
    },
    connectQuestions: [],
    definitionQuestions: []
  }
  constructor(private dialogRef: MatDialogRef<TestDetailComponent>,private fb: FormBuilder,
      private service: ApiService, @Inject(MAT_DIALOG_DATA) public data: {passedTest: Test}
    ){
      this.test = data.passedTest
      console.log(this.test)
    }
}
