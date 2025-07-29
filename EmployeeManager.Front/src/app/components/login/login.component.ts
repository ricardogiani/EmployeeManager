import { Component, EventEmitter, inject, OnInit, Output, signal } from '@angular/core';
import { AuthService } from '../../services/login/auth.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  imports: [
    CommonModule,
    ReactiveFormsModule,    
    MatFormFieldModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule
  ],
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  loginError: string = '';

  @Output() loginEvent = new EventEmitter<{userName: string, logged: boolean }>();
  
/*  userName = signal('');
  password = signal('');*/

  private loginService = inject(AuthService);

  loading: boolean = false;
  
  constructor(private router: Router, private fb: FormBuilder) { 
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(16)]]
    });
  }

  ngOnInit() {
  }

  /*isValidEmail() : boolean
  {
    return this.userName().includes('@') && this.userName().includes('.');;
  }*/

 onSubmit(): void {
    if (this.loginForm.valid) {
        const { username, password } = this.loginForm.value;


        this.loginService.login(username, password).subscribe({
          next: (data) => {
            this.loading = false;
            this.loginEvent.emit({ userName: username, logged: true});
            this.router.navigate(['/employee-list']);
          },
          error: (err) => {
            console.error('Falha ao efetuar o Login:', err);
            this.loginError = 'Falha ao efetuar Login. Tente novamente.';
            this.loading = false;
          }
        });
       
    } else {
      alert('Por favor, preencha os campos corretamente.');
    }
  }

  clearError() {}

}
