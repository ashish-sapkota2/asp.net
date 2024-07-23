import { Component, EventEmitter, Output } from '@angular/core';
import { Todo } from '../../Todos';
import { FormsModule,NgForm } from '@angular/forms';
import { formatCurrency } from '@angular/common';
import { from } from 'rxjs';


@Component({
  selector: 'app-add-todo',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-todo.component.html',
  styleUrls: ['./add-todo.component.css',
  '../../../../node_modules/bootstrap/dist/css/bootstrap.min.css'
]
})
export class AddTodoComponent {
title: string="";
desc: string ="";
@Output() todoAdd: EventEmitter<Todo> = new EventEmitter();
  onSubmit(){
    const todo={
      sno:8,
      title:this.title,
      description : this.desc,
      active :true
    }
    console.log(this.title);
    this.todoAdd.emit(todo);
  }

}
