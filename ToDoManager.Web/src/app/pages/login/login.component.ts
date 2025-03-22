import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth/auth.service';
import { LoginViewModel } from 'src/app/viewModel/loginViewModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) {}
  user: LoginViewModel = {username: '', password: ''};
  newUser: LoginViewModel = {username: '', password: ''};

  newPasswordConfirm: string = '';

  login() {
    this.authService.Login(this.user).subscribe({
      next: (token: string) => {
        console.log('Token recebido:', token);
        localStorage.setItem('token', token);
        this.router.navigate(['/task']);
      },
      error: (error) => {
        console.error('Erro ao fazer login:', error);
      }
    });
  }

  register() {
    if(this.newUser.password !== this.newPasswordConfirm){
      this.showError("A senha deve ser igual.");
    }

    this.authService.Login(this.newUser).subscribe({
      next: (response) => {
        this.showInfo('Usuário registrado com sucesso!');
      },
      error: (error) => {
        console.error('Erro durante cadastro do usuário', error);
        this.showError('Erro durante cadastro do usuário');
      }
    });
  }

  showError(message: string) {
    this.toastr.error(message, 'Erro', {
      timeOut: 3000,
      positionClass: 'toast-top-right',
      closeButton: true
    });
  }

  showInfo(message: string) {
    this.toastr.info(message, 'Informação', {
      timeOut: 3000,
      positionClass: 'toast-top-right',
      closeButton: true
    });
  }
}
