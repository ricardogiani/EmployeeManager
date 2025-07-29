import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EmployeeDetailComponent } from './components/employee-detail/employee-detail.component';
import { AuthGuard } from './auth-guard';
import { HomeComponent } from './components/home/home.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent, title: 'Login Usuário'},
    { path: 'change-password', component: ChangePasswordComponent, title: 'Alterar senha', canActivate: [AuthGuard]},
    { path: 'home', component: HomeComponent, title: 'Gerenciador de Funcionários', canActivate: [AuthGuard]},
    { path: 'employee-list', component: EmployeeListComponent, title: 'Lista de Funcionários', canActivate: [AuthGuard]},
    { path: 'employee', component: EmployeeDetailComponent, title: 'Cadastro de Funcionário', canActivate: [AuthGuard]},
    { path: 'employee/:id', component: EmployeeDetailComponent, title: 'Editar Funcionário', canActivate: [AuthGuard] },
    { path: '**', redirectTo: 'login', pathMatch: 'full' }
];
