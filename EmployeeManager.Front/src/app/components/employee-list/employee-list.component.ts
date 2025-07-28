import { Component, inject, OnInit, signal } from '@angular/core';
import { EmployeeService } from '../../services/employee/employee.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Employee } from '../../interfaces/employee';
import { EmployeeTableComponent } from '../employee-table/employee-table.component';
import { Router, RouterLink } from '@angular/router';
import { EmployeeFilter } from '../../interfaces/employee-filter';
import { JobLevelEnum } from '../../enums/job-level-enum';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports:[
    CommonModule,
    RouterLink,
    FormsModule,
    EmployeeTableComponent
  ],
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  inputSearch: string = 'ricardogiani@gmail.com';  

  employeeId = signal('1');

  employees: Employee[] = [];

  private employeeService = inject(EmployeeService);

  constructor(private router: Router) { }

  ngOnInit() {

  }

  employeesFind(typeFilter: string)
  {
    //alert(typeFilter);    
    console.log(`search employees type: ${typeFilter}, value: ${this.inputSearch}`);

    let filter = { } as EmployeeFilter ;
    if (typeFilter == 'email')
      filter.email = this.inputSearch;

    if (typeFilter == 'jobLevel')
      filter.jobLevel = (JobLevelEnum as any)[this.inputSearch];

    this.employeeService.getByFilter().subscribe({
      // 1. Bloco `next`: Executado quando a requisição é bem-sucedida e os dados chegam.
      next: (data: Employee[]) => {
        //this.foundEmployees = data; // Atribui os dados recebidos à sua propriedade
        //this.loading = false;
        this.employees = data;
        console.log('Dados recebidos com sucesso:', this.employees);
      },
      // 2. Bloco `error`: Executado se a requisição falhar (erro HTTP, erro de rede, etc.).
      error: (err: any) => { // 'err' é o erro propagado do seu service (HttpErrorResponse ou Error)
        //this.error = `Falha na busca: ${err.message || 'Erro desconhecido'}`;
        //this.loading = false;
        console.error('Erro ao buscar funcionários:', err);
      },
      // 3. Bloco `complete`: Executado quando o Observable é concluído (seja com sucesso ou erro).
      // Geralmente usado para finalizar estados de carregamento ou recursos.
      complete: () => {
        console.log(`Busca por ${typeFilter} concluída.`);
        // Opcional: pode definir this.loading = false aqui se não for definido em next/error
      }
    });


  }

  newEmployee(){
    this.router.navigate(['/employee']);
  }

}
