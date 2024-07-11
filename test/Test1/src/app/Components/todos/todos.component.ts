import { Component } from '@angular/core';
import { Todo } from '../../Todos';
import { TodoItemsComponent } from '../todo-items/todo-items.component';
import { CommonModule } from '@angular/common';
import { AddTodoComponent } from '../add-todo/add-todo.component';


@Component({
  selector: 'app-todos',
  standalone: true,
  imports: [TodoItemsComponent, CommonModule, AddTodoComponent],
  templateUrl: './todos.component.html',
  styleUrl: './todos.component.css'
})
export class TodosComponent {
  todos: Todo[]=[];
  localItem: string="";
constructor(){

  const savedTodos = localStorage.getItem("todos");
  if(savedTodos){
    this.localItem = savedTodos;
    this.todos = JSON.parse(this.localItem);
  }else{
    this.todos =[]
  }
}

  deleteTodo = (todo: Todo)=>{

    console.log(todo);
    const index = this.todos.indexOf(todo);
    this.todos.splice(index,1);
    localStorage.setItem("todos", JSON.stringify(this.todos));

  }
  Addtodo(todo: Todo){
    this.todos.push(todo);
    localStorage.setItem("todos", JSON.stringify(this.todos))
  }
}
