import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EmployeeDetailComponent } from './components/employee-detail/employee-detail.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent, title: 'Login Usuário'},
    { path: 'employee-list', component: EmployeeListComponent, title: 'Funcionários'},
    { path: 'employee', component: EmployeeDetailComponent, title: 'Cadastro Funcionário'},
    { path: 'employee/:id', component: EmployeeDetailComponent, title: 'Editar Funcionário' },
    { path: '**', redirectTo: 'login', pathMatch: 'full' }
];
