import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { LoginComponent } from './components/login/login.component';
import { CommonModule } from '@angular/common';
import { StorageService } from './services/storage/storage.service';
import { AuthService } from './services/login/auth.service';


@Component({
  selector: 'app-root',
  imports: [CommonModule, RouterOutlet, HeaderComponent, LoginComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {

  public userName: string = "";
  public logged: boolean = false;

  protected readonly title = signal('EmployeeManager.Front');

  loginEvent(event: { userName: string, logged: boolean})
  {
    this.setUserLogged(event.userName, event.logged);
  }

  logoutEvent(event: { userName: string, logged: boolean})
  {
    console.log("LogoutEvent");
    this.userName = "";
    this.logged = false;
  }

  setUserLogged(userName: string, logged: boolean) : void
  {
    try {
      
      this.logged = logged;      
      this.userName = userName;

      console.log(`setUserLogged ${userName} - ${logged}`);
      //
    } catch (error) {
      
    }
    
  }
}
