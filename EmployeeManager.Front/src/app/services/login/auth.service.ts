import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { empty, Observable, tap } from 'rxjs';
import { StorageService } from '../storage/storage.service';

export class LoginResponse {
  message?: string;
  token?: string;
  sucess?: boolean;
}


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  // URL base da sua API. Substitua pelo endereço real do seu backend.
  private apiUrl = 'http://localhost:5148/api/Login';


  private storageService = inject(StorageService);
  
  constructor(private http: HttpClient) { }

  isAuthenticated(): boolean
  {
    const authenticated = this.storageService.getData("authenticated");

    if (authenticated)
      return  Boolean(authenticated);

    return false;
  }

  getUserName() : string
  {
    const userName = this.storageService.getData("username");
    if (userName)
      return userName as string;

    return "";
  }

  logout() : void
  {
    this.storageService.clearData();
  }
  
   /**
   * Cria um novo funcionário.
   * @param employee O objeto EmployeeDto a ser criado.
   * @returns Um Observable do EmployeeDto criado.
   */
  login(username: string, password: string ): Observable<LoginResponse> {

    return this.http.post<LoginResponse>(this.apiUrl, { username, password })
      .pipe(
        // Opcional: usar 'tap' para fazer algo com a resposta antes de passá-la adiante
        tap(response => {

            this.storageService.saveData("username", username);
            this.storageService.saveData("authenticated", "true");
            this.storageService.saveData("token", response.token as string);   

            response.token = "";
            response.sucess = true;

           console.log('Login bem-sucedido, resposta da API:', username);          
        }),
        // Usa catchError para interceptar e tratar erros da requisição
        //catchError(this.handleError)
      );
  }

}
