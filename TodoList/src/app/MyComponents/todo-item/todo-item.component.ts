import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Todo } from '../../Todo';

@Component({
  selector: 'app-todo-item',
  templateUrl: './todo-item.component.html',
  styleUrl: './todo-item.component.css'
})
export class TodoItemComponent implements OnInit{
  
  @Input() todo: Todo | undefined;
  @Output() todoDelete: EventEmitter<Todo>= new EventEmitter();
  
  ngOnInit(): void {
    
  }
onClick(todo: Todo | undefined ){
  this.todoDelete.emit(todo)
  console.log("Onclick has been triggered")
}
}

