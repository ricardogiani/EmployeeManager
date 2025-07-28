import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { Employee } from '../../interfaces/employee';
import { JobLevelEnum } from '../../enums/job-level-enum';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-employee-table',
  standalone: true,
  imports: [CommonModule, MatTableModule], 
  templateUrl: './employee-table.component.html',
  styleUrls: ['./employee-table.component.css'] 
})
export class EmployeeTableComponent implements OnInit {

  @Input() employees: Employee[] = [];
  @Output() editEmployeeEvent = new EventEmitter<Employee>();

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

  editEmployee(employee: Employee) : void
  {
    console.log(`editEmployee: ${employee}`);
    this.editEmployeeEvent.emit(employee);
  }
}