import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { TaskComponent } from './pages/task/task.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'task', component: TaskComponent },
  { path: '**', redirectTo: 'login' } // Redireciona para login por padrão
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
