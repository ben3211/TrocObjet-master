import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AppUser } from 'src/app/models/appUser';
import { userHttpService } from 'src/app/models/appUser-http.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.scss']
})
export class CreateUserComponent {
  constructor(private userService: userHttpService, private router: Router) {

  }
errorMessage?:string;
editValue: {firstName:string, lastName:string, phoneNumber:string, city:string} = {
  firstName :"",
  lastName:"",
  phoneNumber:"",
  city:""
}

ngOnInit(){

}

async createUser(){
  try {
    // Création utilisateur
    var u = new AppUser(this.editValue.firstName);
    u.lastName = this.editValue.lastName;
    u.phoneNumber = this.editValue.phoneNumber;
    u.city=this.editValue.city;

    // sauvegarde du l'utilisateur
    try {
      var id=await this.userService.saveItemAsync(u);
      this.router.navigateByUrl("/user");

    } catch (error) {
      this.errorMessage="L'objet ne peut être sauvegardé maintenant";
    }

  } catch (error) {
    this.errorMessage = "Le métier ne permet pas de créer l'objet";
  }
}

}
