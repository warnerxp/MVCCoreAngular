import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(
    req: import('@angular/common/http').HttpRequest<any>,
    next: import('@angular/common/http').HttpHandler
  ): import('rxjs').Observable<import('@angular/common/http').HttpEvent<any>> {
    return next.handle(req).pipe(
        catchError (error => {
            if (error.status === 401) {
                return throwError(error.statusText);
            }
            if (error instanceof HttpErrorResponse) {
            console.log('Extraer valor Header Application-Error');
            const applicationError = error.headers.get('Application-Error');
            console.log('applicationError ' + applicationError );
            if (applicationError) {
                return throwError(applicationError);
            }
            const serverError = error.error;
            let modalStateErrors  = '';

            if (serverError.error && typeof  serverError.error === 'object') {
                for (const key in serverError.error) {
                    if (serverError.error[key]) {
                        modalStateErrors += serverError.error[key] + '' + '\n';
                    }
                }
            }
            return throwError(modalStateErrors || serverError || 'Server  Error');
            }
        })
    );
  }
}
export const ErrorInterceptorProvider = {
provide: HTTP_INTERCEPTORS,
useClass : ErrorInterceptor,
multi : true
};
