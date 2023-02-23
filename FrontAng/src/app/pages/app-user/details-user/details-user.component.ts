import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { AppUser } from 'src/app/models/appUser';
import { userHttpService } from 'src/app/models/appUser-http.service';
import { Object} from '../../../models/object';

@Component({
  selector: 'app-details-user',
  templateUrl: './details-user.component.html',
  styleUrls: ['./details-user.component.scss']
})
export class DetailsUserComponent implements OnInit {
  
  constructor(private userService:userHttpService , private activatedRoute:ActivatedRoute, private router:Router) {
  
  }

  user?:AppUser;
  objects?:Object[];
  async ngOnInit() {
    let id= this.activatedRoute.snapshot.params["id"];
    let guid=Guid.parse(id);

    // Promise.all => prend un tableau de promesses et ça les ecécute toutes en même temps
    [this.user,this.objects]=await Promise.all([this.userService.getItemAsync(guid),this.userService.getObjectsAsync(id)]);
  }
  async deleteItemAsync() {
    let id= this.activatedRoute.snapshot.params["id"];
    let guid=Guid.parse(id);
    await this.userService.deleteItemAsync(guid);
   
    this.router.navigate(['/user']);
  }

  async editPage(){
    let id= this.activatedRoute.snapshot.params["id"];
    let guid=Guid.parse(id);
    this.router.navigate(['/edit-user/'+guid]);
  }

  async detailObjectPage(){

  }
}
