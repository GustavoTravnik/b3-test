import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable()
export class RouterInterceptor implements HttpInterceptor {
  
  constructor(private readonly snackBar: MatSnackBar) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      if (!req.url.startsWith('http')) {
        const updatedRequest = req.clone({
          url: `${environment.serverBaseUrl}${req.url}`,
          withCredentials: false
        });
        return this.handlerWithErrorMessage(updatedRequest, next);
      }      
      return this.handlerWithErrorMessage(req, next);  
  }

  handlerWithErrorMessage (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
        catchError((error: HttpErrorResponse) => {
          let errorMessage = error.error?.detail ?? "Erro fatal";
          
          if (error.status === 504) {
            errorMessage = 'O servidor está offline, tente novamente mais tarde';
          }
  
          this.snackBar.open(errorMessage, 'Close', {
            duration: 3000,
            horizontalPosition: 'center',
            verticalPosition: 'top'
          });
  
          return throwError(() => new Error(errorMessage))
        })
      );
}
}
