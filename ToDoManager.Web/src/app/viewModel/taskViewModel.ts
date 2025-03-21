import { Title } from "@angular/platform-browser";

export interface TaskViewModel {
  id: string;
  title: string;
  description: string;
  isCompleted: boolean;
  createdAt: Date;
}