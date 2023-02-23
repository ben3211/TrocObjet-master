import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { AppUser } from 'src/app/models/appUser';
import { userHttpService } from 'src/app/models/appUser-http.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent {

  constructor(private userService: userHttpService, private activatedRoute: ActivatedRoute,
    private router: Router) {

  }

  user: AppUser| undefined;
  errorMessage?: string;
  editValue?: { firstName: string, lastName: string, phoneNumber: string,city: string };
  idUser?: Guid;



  ngOnInit() {
    this.activatedRoute.params.subscribe(async params => {
      var id = params["id"];
      this.idUser = Guid.parse(id);
      this.user = await this.userService.getItemAsync(id);
      this.editValue = {
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        phoneNumber:this.user.phoneNumber,
        city:this.user.city,
      }
    });
  }



  async updateUser() {
    try {
      const updatedObject = new AppUser(
        this.editValue?.firstName,
        this.editValue?.lastName!,
        this.editValue?.phoneNumber!,
        this.editValue?.city,

      );
  
      try {
        // Tentative de sauvegarde
        await this.userService.updateItemAsync(this.idUser!, updatedObject);
        this.router.navigateByUrl("/user");
      } catch (error) {
        this.errorMessage = "L'objet ne peut être sauvegardé maintenant";
      }
    } catch (error) {
      this.errorMessage = "Le métier ne permet pas d'éditer l'objet";
    }
  }

}
