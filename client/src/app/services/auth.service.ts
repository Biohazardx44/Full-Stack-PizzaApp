import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ILogin, IRegister } from '../types/interfaces/auth.interface';
import { Observable, catchError, of, tap } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private http: HttpClient,
    private snackBar: MatSnackBar,
    private router: Router
  ) { }

  register(registerCredentials: IRegister): Observable<any> {
    console.log(registerCredentials)

    return this.http
      .post(`${environment.apiUrl}/User/register`, registerCredentials)
      .pipe(
        tap(() => {
          this.snackBar.open(
            'You have successfully registered!',
            'x',
            environment.snackBarConfig
          )
          this.router.navigate(['/login'])
        }),
        catchError((error) => {
          if (error) {
            console.log(error)
            this.snackBar.open(
              error?.error?.errors?.[0] ||
              'Error while registering!',
              'x',
              environment.snackBarConfig
            )
          }

          return of(null)
        })
      )
  }

  login(loginCredentials: ILogin): Observable<any> {
    console.log(loginCredentials)

    return this.http
      .post(`${environment.apiUrl}/User/login`, loginCredentials)
      .pipe(
        tap(() => {
          this.snackBar.open(
            'You have successfully logged in!',
            'x',
            environment.snackBarConfig
          )
          this.router.navigate(['/'])
        }),
        catchError((error) => {
          if (error) {
            this.snackBar.open(
              error?.error?.errors?.[0] ||
              'Error while logging in!',
              'x',
              environment.snackBarConfig
            )
          }

          return of(null)
        })
      )
  }
}