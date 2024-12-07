import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestCreationComponent } from './test-creation.component';

describe('TestCreationComponent', () => {
  let component: TestCreationComponent;
  let fixture: ComponentFixture<TestCreationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TestCreationComponent]
    });
    fixture = TestBed.createComponent(TestCreationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
