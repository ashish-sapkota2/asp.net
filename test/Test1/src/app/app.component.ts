import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TodosComponent } from './Components/todos/todos.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,TodosComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css',
  '../../node_modules/bootstrap/dist/css/bootstrap.min.css'
]
})
export class AppComponent {
  title = 'ToDO List Test';
}
