import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Router, RouterModule } from '@angular/router';
import { StorageService } from '../../services/storage/storage.service';
import { AuthService } from '../../services/login/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports:[
    CommonModule,
    RouterModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  userName: string = "";
  private loginService = inject(AuthService);

  @Output() logoutEvent = new EventEmitter<{userName: string, logged: boolean }>();

  constructor(private router: Router) { }

  ngOnInit() {
    this.userName = this.loginService.getUserName();
  }

  logout() {
    // Lógica para fazer logout
    console.log('Usuário deslogado!');
    this.loginService.logout();
    this.logoutEvent.emit({ userName: "", logged: false });
    this.router.navigate(['/login']); // Redireciona para a tela de login
  }

}
