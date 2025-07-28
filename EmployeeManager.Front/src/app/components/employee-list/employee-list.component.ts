import { Component, inject, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/employee.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Employee } from '../../interfaces/employee';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports:[
    CommonModule,
    FormsModule // <--- Add FormsModule to your imports array!
  ],
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  inputSearch: string = '';  

  private employeeService = inject(EmployeeService);

  constructor() { }

  ngOnInit() {

  }

  employeesFind(typeFilter: string)
  {
    //alert(typeFilter);    
    console.log(`search employees ${typeFilter}`);

    if (typeFilter == 'email')
      this.employeeService.get({ email: this.inputSearch }).subscribe({
      // 1. Bloco `next`: Executado quando a requisição é bem-sucedida e os dados chegam.
      next: (data: Employee[]) => {
        //this.foundEmployees = data; // Atribui os dados recebidos à sua propriedade
        //this.loading = false;
        console.log('Dados recebidos com sucesso:', data);
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
        console.log('Busca por email concluída.');
        // Opcional: pode definir this.loading = false aqui se não for definido em next/error
      }
    });


  }

}
