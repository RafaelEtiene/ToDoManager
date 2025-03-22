import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { LoginViewModel } from 'src/app/viewModel/loginViewModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:44319/Auth';

  constructor(private http: HttpClient) { }

  Login(user: LoginViewModel): Observable<string> {
    return this.http.post(`${this.apiUrl}/Login`, { username: user.username, // A propriedade username do LoginViewModel
      password: user.password 
      }, { responseType: 'text' });
  }

  Register(newUser: LoginViewModel): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Register`, { newUser });
  }

  Logout() {
    localStorage.removeItem('token');
  }

  IsAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }

  GetToken(): string | null {
    return localStorage.getItem('token');
  }
}
