import { Component, inject, OnInit, signal } from '@angular/core';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  userName = signal('');
  password = signal('');

  private loginService = inject(LoginService);
  
  constructor() { }

  ngOnInit() {
  }

  isValidEmail() : boolean
  {
    return this.userName().includes('@') && this.userName().includes('.');;
  }

  login()
  {


  }

}
