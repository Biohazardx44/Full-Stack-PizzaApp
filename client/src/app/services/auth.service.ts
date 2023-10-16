import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ILogin, ILoginResponse, IRegister } from '../types/interfaces/auth.interface';
import { BehaviorSubject, Observable, catchError, of, tap } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false)

  isLoggedIn$: Observable<boolean> = this.isLoggedIn.asObservable()

  updateIsLoggedIn(isLoggedIn: boolean) {
    this.isLoggedIn.next(isLoggedIn)
  }

  constructor(
    private http: HttpClient, // HttpClient is a service that allows us to make HTTP requests
    private snackBar: MatSnackBar,
    private router: Router
  ) {
    this.updateIsLoggedIn(this.isTokenValid()) // check if token is valid on app initialization
  }

  // #setToken - real private
  private setToken(token: string, tokenExpirationDate: string) {
    localStorage.setItem('token', token)
    localStorage.setItem('tokenExpirationDate', tokenExpirationDate)
  }

  private isTokenValid(): boolean {
    const tokenExpirationDate: string | null = localStorage.getItem('tokenExpirationDate');

    if (!tokenExpirationDate) {
      return false;
    }

    return new Date(tokenExpirationDate) > new Date()
  }

  register(registerCredentials: IRegister): Observable<any> {
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
    return this.http
      .post<ILoginResponse>(`${environment.apiUrl}/User/login`, loginCredentials)
      .pipe(
        tap((response: ILoginResponse) => {
          if (response?.token) {
            this.setToken(response.token, response.validTo)

            if (!this.isTokenValid()) {
              throw new Error('Error while logging in!')
            }

            this.updateIsLoggedIn(true);

            this.snackBar.open(
              'You have successfully logged in!',
              'x',
              environment.snackBarConfig
            )
            this.router.navigate(['/'])
          }
        }),
        catchError((error) => {
          this.updateIsLoggedIn(false)

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

  logout() {
    this.updateIsLoggedIn(false);
    localStorage.removeItem('token')
    localStorage.removeItem('tokenExpirationDate')
    this.router.navigate(['/login'])
  }
}