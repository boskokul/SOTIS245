import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TestCreationComponent } from '../test-creation/test-creation.component';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent {
heading = "Test management"
constructor(private dialog: MatDialog) {}

showModalDialog(){
  const dialogRef = this.dialog.open(TestCreationComponent, {
    width: '700px', 
    maxHeight: '80vh',
    data: { message: 'Hello from the parent component!' } // Pass data if needed
  });

  //TODO Fetch all tests!!
  dialogRef.afterClosed().subscribe(result => {
    console.log('The dialog was closed');
    //console.log('Dialog result:', result); // Result returned from the modal
  });
}

}

