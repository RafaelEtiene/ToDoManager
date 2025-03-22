import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TaskService } from 'src/app/services/task/task.service';
import { InsertTaskViewModel } from 'src/app/viewModel/insertTaskViewModel';
import { TaskViewModel } from 'src/app/viewModel/taskViewModel';
import * as bootstrap from 'bootstrap';
import { UpdateTaskViewModel } from 'src/app/viewModel/updateTaskViewModel';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent implements OnInit {
  tasks: TaskViewModel[] = [];
  newTask: InsertTaskViewModel = {
    title: "",
    description: ""
  };
  selectedTask: TaskViewModel = {
    title: "",
    description: "",
    id: "",
    createdAt: new Date(),
    isCompleted: false
  };

  constructor(private taskService: TaskService, private toastr: ToastrService){}
  ngOnInit(): void {
    this.getTasks();
  }

  getTasks(){
    this.taskService.getTasks().subscribe({
      next: (response) => {
        console.log(response);
        this.tasks = response;
      },
      error: (error) => {
        this.showError('Falha na busca das tarefas.');
        console.error('Erro na busca de tarefas', error);
      }
    });
  }

    createNewTask(){
      this.taskService.insertTask(this.newTask).subscribe({
        next: () => {
          this.showInfo('Tarefa criada com sucesso!');
          this.hideModal('createTaskModal');
          this.getTasks();
        },
        error: (error) => {
          this.showError('Falha ao tentar criar tarefa.');
          console.error('Erro na criação da tarefa', error);
        }
      });
    }

    updateStateTask(id: string, isCompleted: boolean){
      this.taskService.updateStateTask(id, isCompleted).subscribe(e => this.getTasks());
    }

    editTask(task: TaskViewModel) {
      this.selectedTask = { ...task };
      let modalEdit = document.getElementById('editTaskModal');
      if(modalEdit !== null){
        const modal = new bootstrap.Modal(modalEdit);
        modal.show();
      }
    }
  
    updateTask() {
      let updatedTask: UpdateTaskViewModel = {
        id: this.selectedTask.id,
        title: this.selectedTask.title,
        description: this.selectedTask.description
      };

      this.taskService.updateTask(updatedTask).subscribe({
        next: () => {
          this.showInfo('Tarefa atualizada com sucesso!');
          this.hideModal('editTaskModal');
          this.getTasks();
        },
        error: (error) => {
          this.showError('Falha ao atualizar tarefa.');
          console.error('Erro na atualização da tarefa', error);
        }
      });
    }

    deleteTask(id: string){
      this.taskService.deleteTask(id).subscribe({
        next: () => {
          this.showInfo('Tarefa deletada com sucesso');
          this.getTasks();
        },
        error: (error) => {
          this.showError('Falha ao tentar deletar tarefa.');
          console.error('Erro na remoção da tarefa', error);
        }
      });
    }

    showError(message: string) {
      this.toastr.error(message);
    }
  
    showInfo(message: string) {
      this.toastr.info(message);
    }

    hideModal(template: string){
        let modal = document.getElementById(template);
            if (modal) {
              let bootstrapModal = bootstrap.Modal.getInstance(modal);
              if (bootstrapModal) {
                bootstrapModal.hide();
              }
            }
      
            // Remove manualmente o backdrop do Bootstrap (corrige o problema do cursor)
            setTimeout(() => {
              let backdrop = document.querySelector('.modal-backdrop');
              if (backdrop) {
                backdrop.remove();
              }
            }, 300);
      }
}
