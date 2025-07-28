import { Component, inject, OnInit, signal } from '@angular/core';
import { LoginService } from '../../services/login/login.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  loginError: string = '';
  
/*  userName = signal('');
  password = signal('');*/

  private loginService = inject(LoginService);

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
