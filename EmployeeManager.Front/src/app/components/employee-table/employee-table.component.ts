import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { Employee } from '../../interfaces/employee';
import { JobLevelEnum } from '../../enums/job-level-enum';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-employee-table',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule], 
  templateUrl: './employee-table.component.html',
  styleUrls: ['./employee-table.component.css'] 
})
export class EmployeeTableComponent implements OnInit {

  @Input() employees: Employee[] = [];
  @Output() editEmployeeEvent = new EventEmitter<Employee>();

  JobLevelEnum = JobLevelEnum;

  // DataSource para a mat-table
  dataSource = new MatTableDataSource<Employee>();

  displayedColumns: string[] = [
    'id',
    'firstName',
    'lastName',
    'email',
    'documentNumber',
    'birthDate',
    'jobLevel',
    'phoneNumber',
    'active',
    'managerId',
    'createdAt',
    'updatedAt',
    'actions'
  ];

  constructor() { }

  ngOnInit(): void {
     if (this.employees) {
      this.dataSource.data = this.employees;
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['employees'] && this.employees) {
      this.dataSource.data = this.employees;
    }
  }
  
  editEmployee(employee: Employee) : void
  {
    console.log(`editEmployee: ${employee}`);
    this.editEmployeeEvent.emit(employee);
  }
}