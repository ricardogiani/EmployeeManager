import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { Employee } from '../../interfaces/employee';
import { JobLevelEnum } from '../../enums/job-level-enum';

@Component({
  selector: 'app-employee-table',
  standalone: true,
  imports: [CommonModule], 
  templateUrl: './employee-table.component.html',
  styleUrls: ['./employee-table.component.css'] 
})
export class EmployeeTableComponent implements OnInit {

  @Input() employees: Employee[] = [];

  JobLevelEnum = JobLevelEnum;

  constructor() { }

  ngOnInit(): void {
    
  }
  
  formatDate(date: Date | string | undefined): string {
    if (!date) return 'N/A';
    // Se for uma string (vindo da API), pode precisar converter para Date primeiro
    const d = typeof date === 'string' ? new Date(date) : date;
    // Formato básico DD/MM/AAAA
    return d.toLocaleDateString('pt-BR');
  }
}