import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export class LoginResponse {
}


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  // URL base da sua API. Substitua pelo endereço real do seu backend.
  private apiUrl = 'https://sua-api.com/api/employees';
  
  constructor(private http: HttpClient) { }

   /**
   * Cria um novo funcionário.
   * @param employee O objeto EmployeeDto a ser criado.
   * @returns Um Observable do EmployeeDto criado.
   */
  Login(username: string, password: string ): Observable<LoginResponse> {

    return this.http.post<LoginResponse>(this.apiUrl, { username, password });
  }

}
