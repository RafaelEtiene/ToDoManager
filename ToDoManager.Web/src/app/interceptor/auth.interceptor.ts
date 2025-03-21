// src/app/interceptors/auth.interceptor.ts
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem('token');  // Pega o token do localStorage

    if (token) {
      const clonedRequest = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`  // Adiciona o token no cabeçalho da requisição
        }
      });
      return next.handle(clonedRequest);  // Passa a requisição com o cabeçalho atualizado
    }

    return next.handle(req);  // Caso não haja token, apenas passa a requisição original
  }
}
