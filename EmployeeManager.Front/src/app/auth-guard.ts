import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './services/login/auth.service';
import { inject } from '@angular/core';


export const AuthGuard: CanActivateFn = (route, state) => {
  
  
  const loginService = inject(AuthService);
  const router = inject(Router);

  if (loginService.isAuthenticated()) {
    console.log('[AuthGuard] true')
    return true; 
  } else {
    console.log('[AuthGuard] false')
    return router.navigate(['/login']);
  }

};
