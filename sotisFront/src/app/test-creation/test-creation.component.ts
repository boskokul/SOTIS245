import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { min } from 'rxjs';
import { MatDialogRef } from '@angular/material/dialog';
import { TestForm } from '../model/testForm';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-test-creation',
  templateUrl: './test-creation.component.html',
  styleUrls: ['./test-creation.component.css'],
})
export class TestCreationComponent {
  testCreationForm: FormGroup;

  testForm: TestForm = {
    fieldName: '',
    name: '',
    defQuestionsNum: 5,
    conQuestionsNum: [],
  };
  constructor(
    private dialogRef: MatDialogRef<TestCreationComponent>,
    private fb: FormBuilder,
    private service: ApiService
  ) {
    this.testCreationForm = this.fb.group({
      name: ['', [Validators.required]],
      field: ['', [Validators.required]],
      defQuestion: [null, [Validators.required, Validators.min(1)]],
      fields: this.fb.array([]),
    });
  }

  get fields(): FormArray {
    return this.testCreationForm.get('fields') as FormArray;
  }
  addFieldPair() {
    const fieldGroup = this.fb.group({
      termNum: [null, [Validators.required, Validators.min(1)]], // First field
      parentNum: [null, [Validators.required, Validators.min(1)]], // Second field
    });
    this.fields.push(fieldGroup);
  }

  // Method to remove a field group by index
  removeField(index: number) {
    this.fields.removeAt(index);
  }

  onSubmit() {
    if (this.testCreationForm.valid) {
      let hasErrors = false;
      this.fields.controls.forEach((control, index) => {
        if (control.get('termNum')?.value > control.get('parentNum')?.value) {
          alert('Parent number must be grater or equal to Term number.');
          hasErrors = true;
        }
      });
      if (this.fields.controls.length == 0) {
        hasErrors = true;
        alert('Test must contain at least one connection question');
      }
      if (!hasErrors) {
        this.testForm.name = this.testCreationForm.get('name')?.value || '';
        this.testForm.fieldName =
          this.testCreationForm.get('field')?.value || '';
        this.testForm.defQuestionsNum =
          this.testCreationForm.get('defQuestion')?.value || 2;

        this.fields.controls.forEach((control, index) => {
          this.testForm.conQuestionsNum.push(control.value);
        });
        this.service.createTest(this.testForm).subscribe((any) => {
          this.dialogRef.close();
        });
      }
    }
  }
}
