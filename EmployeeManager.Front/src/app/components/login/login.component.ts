import { Component, inject, OnInit, signal } from '@angular/core';
import { LoginService } from '../../services/login.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

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
  
  userName = signal('');
  password = signal('');

  private loginService = inject(LoginService);

  loading = false;
  
  constructor(private fb: FormBuilder) { 
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(16)]]
    });
  }

  ngOnInit() {
  }

  isValidEmail() : boolean
  {
    return this.userName().includes('@') && this.userName().includes('.');;
  }

 onSubmit(): void {
    if (this.loginForm.valid) {
        const { username, password } = this.loginForm.value;


        /*this.authenticationService.login(username, password).subscribe(
          (response: any) => { 
            console.log("login sucesso");
            this.app.onLoginSucess(username, response.token);
            this.router.navigate(['/demo']);
            
          },
          error => { 
            //this.loginError = "falha ao realizar login";
            this.localDataService.clearData();
            alert(error?.error);
            console.log(error);
          },
          () => console.log('encerrou carga do arquivo'),
        );*/
    } else {
      alert('Por favor, preencha os campos corretamente.');
    }
  }

  clearError() {}

}
