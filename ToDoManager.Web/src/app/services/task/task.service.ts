import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InsertTaskViewModel } from 'src/app/viewModel/insertTaskViewModel';
import { TaskViewModel } from 'src/app/viewModel/taskViewModel';
import { UpdateTaskViewModel } from 'src/app/viewModel/updateTaskViewModel';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private apiUrl = 'https://localhost:44319/Task';

  constructor(private http: HttpClient) {}

  // Método para obter todas as tarefas
  getTasks(): Observable<TaskViewModel[]> {
    return this.http.get<TaskViewModel[]>(`${this.apiUrl}/GetTasks`);
  }

  // Método para criar uma nova tarefa
  insertTask(task: InsertTaskViewModel): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/InsertTask`, task);
  }

  // Método para atualizar o estado de uma tarefa (completa ou não)
  updateStateTask(taskId: string, isCompleted: boolean): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/UpdateStateTask`, { idTask: taskId, isCompleted });
  }

  updateTask(viewModel: UpdateTaskViewModel): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/UpdateStateTask`, { viewModel });
  }
}
