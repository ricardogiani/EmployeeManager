// src/app/home/home.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // Necessário para diretivas básicas como ngIf, ngFor

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule], // Importe CommonModule para usar diretivas como *ngIf ou *ngFor se precisar
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  
  imageUrl: string = '../../../assets/images/employee-image.jpg'; 
  imageAlt: string = 'Bem-vindo ao Employee Manager';
}