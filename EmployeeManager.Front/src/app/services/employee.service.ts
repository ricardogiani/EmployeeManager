import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  // URL base da sua API. Substitua pelo endereço real do seu backend.
  private apiUrl = 'https://sua-api.com/api/employees';
  
  constructor(private http: HttpClient) { }

}
