import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Login } from 'src/app/model/login';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  isPasswordVisible: boolean;

    constructor(
        private authService: ApiService,
        public dialog: MatDialogRef<LoginComponent>,
        public dialogRef: MatDialog,
        private router: Router,
        
    ) {
        this.isPasswordVisible = false;
    }

    loginForm = new FormGroup({
        username: new FormControl("", [Validators.required]),
        password: new FormControl("", [Validators.required]),
    });

    login(): void {
        const login: Login = {
            username: this.loginForm.value.username || "",
            password: this.loginForm.value.password || "",
        };

        if (this.loginForm.valid) {
            this.authService.login(login).subscribe({
                next: () => {
                    this.onClose();
                },
                error: err => {
                    alert("Login failed!")
                },
            });
        }
    }

    

    onClose(): void {
        this.dialog.close();
    }

    togglePasswordVisibility() {
        this.isPasswordVisible = !this.isPasswordVisible;
    }

    

    
}
