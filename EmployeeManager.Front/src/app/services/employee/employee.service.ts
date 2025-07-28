import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Employee } from '../../interfaces/employee';
import { map, Observable, throwError } from 'rxjs';
import { EmployeeFilter } from '../../interfaces/employee-filter';
import { StorageService } from '../storage/storage.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  // URL base da sua API. Substitua pelo endereço real do seu backend.
  private apiUrl = 'http://localhost:5148/api/employee';

  private storageService = inject(StorageService);

  constructor(private http: HttpClient) { }


  private getAuthorizeHeader() : any
  {
    const token = this. storageService.getData("token");
    return {
      Authorization: `bearer  ${token}`
    }
  }

  /**
   * Create new employee
   * @param newEmployee 
   * @returns 
   */
  create(newEmployee: Employee): Observable<Employee> {
    return this.http.post<Employee>(this.apiUrl, newEmployee, { headers: this.getAuthorizeHeader() });
  }

  /**
   * Atualiza um funcionário existente.
   *
   * @param id O ID do funcionário a ser atualizado.
   * @param employeeEntity O objeto EmployeeEntity com os dados atualizados.
   * @returns Um Observable do EmployeeEntity atualizado.
   */
  update(id: number, employeeEntity: Employee): Observable<Employee> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.put<Employee>(url, employeeEntity, { headers: this.getAuthorizeHeader() });
  }

  /**
   * Obtém uma coleção de funcionários com base em critérios de filtro.
   * 
   * @param filter (Opcional) O objeto de requisição de filtro.
   * @returns Um Observable de um array de EmployeeEntity.
   */
  getByFilter(filter?: EmployeeFilter): Observable<Employee[]> {
    let params = new HttpParams();
    if (filter) {
      // Converte o objeto de filtro em parâmetros de URL
      Object.keys(filter).forEach(key => {
        const value = (filter as any)[key];
        if (value !== undefined && value !== null) {
          if (value instanceof Date) {
            params = params.append(key, value.toISOString());
          } else if (typeof value === 'boolean') {
            params = params.append(key, value.toString());
          } else {
            params = params.append(key, value);
          }
        }
      });
    }

    return this.http.get<Employee[]>(this.apiUrl, { params, headers: this.getAuthorizeHeader() });
      
  }

  /**
   * Obtém um funcionário pelo ID.
   *
   * @param id O ID do funcionário.
   * @returns Um Observable de EmployeeEntity.
   */
  getById(id: number): Observable<Employee> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Employee>(url, { headers: this.getAuthorizeHeader() });
  }

  /**
   * Exclui um funcionário pelo ID.
   *
   * @param id O ID do funcionário a ser excluído.
   * @returns Um Observable de boolean (assumindo que sua API retorna true/false ou um status 204 No Content).
   */
  delete(id: number): Observable<boolean> {
    const url = `${this.apiUrl}/${id}`;
    // Se a API retornar 204 No Content, o RxJS retornará um Observable<void>.
    // Se a API retornar um boolean no corpo, use <boolean>.
    // Neste exemplo, esperamos um boolean, mas pode ser necessário um map() se for void e você quiser um boolean.
    return this.http.delete<boolean>(url, { headers: this.getAuthorizeHeader() }).pipe(
      map(() => true),
      //catchError(this.handleError)
    );
  }

    /**
   * Método privado para tratamento centralizado de erros HTTP.
   * Ele captura o erro, formata uma mensagem e o propaga.
   */
  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'Um erro desconhecido ocorreu na API!';
    if (error.error instanceof ErrorEvent) {
      // Erro do lado do cliente ou de rede
      errorMessage = `Erro de rede ou cliente: ${error.error.message}`;
    } else {
      // Erro retornado pelo backend
      if (error.status) {
        errorMessage = `Erro do servidor (${error.status}): ${error.statusText || 'Unknown'}`;
        // Se a API retornar uma mensagem de erro no corpo, tente acessá-la
        if (error.error && typeof error.error === 'object' && (error.error as any).message) {
          errorMessage += `\nDetalhes: ${(error.error as any).message}`;
        } else if (typeof error.error === 'string') {
          errorMessage += `\nDetalhes: ${error.error}`;
        }
      } else {
        errorMessage = 'O servidor não retornou um status HTTP (possível problema de CORS ou rede).';
      }
    }
    console.error('Erro na chamada da API:', errorMessage, error);
    return throwError(() => new Error(errorMessage)); // Propaga o erro para o componente que chamou
  }

}
