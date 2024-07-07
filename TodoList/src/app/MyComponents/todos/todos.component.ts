import { Component, OnInit } from '@angular/core';
import { Todo } from '../../Todo';
import { Action } from 'rxjs/internal/scheduler/Action';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrl: './todos.component.css'
})
export class TodosComponent implements OnInit{
  localItem:string="";
  todos: Todo[] | undefined
  constructor(){
    const savedTodos = localStorage.getItem("todos");
    if (savedTodos) { // Check if savedTodos is not null
      this.localItem = savedTodos; // Assign only if not null
      this.todos = JSON.parse(this.localItem);
    }else{

      this.todos =[]
    }

  }
  ngOnInit(): void {
   
  }

  deleteTodo = (todo: Todo) => {
    console.log(todo);
    
    // Check if `todos` is defined
    if (this.todos) {
      // Find the index of the todo to delete
      const index = this.todos.indexOf(todo);
  
      // Ensure the index is valid
      if (index !== -1) {
        this.todos.splice(index, 1);
        localStorage.setItem("todos",JSON.stringify(this.todos));
      } else {
        console.error("Todo not found in the list.");
      }
    } else {
      console.error("Todos list is undefined.");
    }
  };
  Addtodo(todo: Todo){
    console.log(todo);
    
    if(this.todos){
      this.todos.push(todo);
      localStorage.setItem("todos",JSON.stringify(this.todos));
      // if(index!==-1){
      //   this.todos.push(todo);
      // }else{
      //   console.error("error");
      // }
    }else{
      console.error("error");
    }
  }
  

}
