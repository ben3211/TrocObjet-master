import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  showMenu=false;
  toogle(){
    this.showMenu=!this.showMenu;
  }
  title = 'FrontAng';
}

