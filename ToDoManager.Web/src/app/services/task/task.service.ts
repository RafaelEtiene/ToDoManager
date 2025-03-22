import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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

  /**
   * Atualiza o estado de uma tarefa (completa ou não)
   * @param idTask ID da Tarefa
   * @param isCompleted Status da tarefa (true = completa, false = pendente)
   * @returns Observable<void>
   */
  updateStateTask(id: string, isCompleted: boolean): Observable<void> {
    console.log(id + " " + isCompleted)
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<void>(`${this.apiUrl}/UpdateStateTask`, { id, isCompleted }, { headers });
  }

  updateTask(viewModel: UpdateTaskViewModel): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/UpdateTask`, {
      id: viewModel.id,
      title: viewModel.title,
      description: viewModel.description
    });
  }
  deleteTask(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeleteTask?id=${id}`);
  }
}
