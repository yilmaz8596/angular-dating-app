import { HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { inject } from '@angular/core';
import { ToastService } from '../services/toast-service';
import { NavigationExtras, Router } from '@angular/router';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toast = inject(ToastService);
  const router = inject(Router);
  return next(req).pipe(
    catchError((error) => {
      switch (error.status) {
        case 400:
          if (error.error.erorrs) {
            const modelStateErrors = [];
            for (const key in error.error.errors) {
              if (error.error.errors[key]) {
                modelStateErrors.push(error.error.errors[key]);
              }
              throw modelStateErrors.flat();
            }
          } else {
            toast.error(error.error?.message, error.status);
          }
          break;
        case 401:
          toast.error(error.error?.message || 'Unauthorized');
          break;
        case 404:
          router.navigateByUrl('/not-found');
          break;
        case 500:
          const navigationExtras: NavigationExtras = {
            state: { error: error.error },
          };
          router.navigateByUrl('/server-error', navigationExtras);
          break;
        default:
          toast.error('Unknown Error');
      }
      return throwError(() => error);
    })
  );
};
