import { isPlatformBrowser } from '@angular/common';
import {  Inject, Injectable, PLATFORM_ID } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class StorageService {

  private isBrowser: boolean = false;

  constructor(@Inject(PLATFORM_ID) private platformId: Object) {    
    this.isBrowser = isPlatformBrowser(this.platformId);
    console.log('StorageService: Construtor chamado. isBrowser:', this.isBrowser);
  }


  public saveData(key: string, value: string) {
    localStorage.setItem(key, value);
  }

  public getData(key: string) {

    if (this.isBrowser) { 
      return localStorage.getItem(key);
    }
    console.warn('Tentativa de acessar localStorage fora do ambiente do navegador.');
    return null;
  }

  public removeData(key: string) {
    localStorage.removeItem(key);
  }

  public clearData() {
    localStorage.clear();
  }

}