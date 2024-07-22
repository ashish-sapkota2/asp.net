import { identifierName } from '@angular/compiler';
import { Component, Input, EventEmitter, output, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Todo } from '../../Todos';
import { TodosComponent } from '../todos/todos.component';

@Component({
  selector: 'app-todo-items',
  standalone: true,
  imports: [CommonModule, TodosComponent],
  templateUrl: './todo-items.component.html',
  styleUrls: ['./todo-items.component.css',
              '../../../../node_modules/bootstrap/dist/css/bootstrap.min.css'
]
})
export class TodoItemsComponent {
@Input() todo : Todo | undefined 
@Output() todoDelete: EventEmitter<Todo> = new EventEmitter();

onClick(todo: Todo | undefined){
  // console.log(todo);
  this.todoDelete.emit(todo);
}
}
