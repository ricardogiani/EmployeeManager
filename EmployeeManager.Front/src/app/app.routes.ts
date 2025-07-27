import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent},
    { path: 'employee-list', component: EmployeeListComponent},
];
