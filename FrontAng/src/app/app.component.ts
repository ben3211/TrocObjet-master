import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'FrontAng';

  showMenu=false;
  toogle(){
    this.showMenu=!this.showMenu;
  }
}
