import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Object } from 'src/app/models/object';
import { ObjectHttpService } from 'src/app/models/object-http.service';
import { objectService } from 'src/app/models/object.service';

@Component({
  selector: 'app-create-object',
  templateUrl: './create-object.component.html',
  styleUrls: ['./create-object.component.scss']
})
export class CreateObjectComponent implements OnInit {
  constructor(private objectService: ObjectHttpService,
    private router: Router) {

  }

  // valeurs initiales du formulaire
  Date = Date;
  errorMessage?: string;
  editValue: { label: string, description: string, estimatedPrice:string, photoUrl:[] } = {
    label: "",
    description: "",
    estimatedPrice:"",
    photoUrl:[]
  }

  ngOnInit() {
    
  }

  async createObject() {
    try {
      // Création objet
      var o = new Object(this.editValue.label);
      o.description = this.editValue.description;
      o.estimatedPrice= +this.editValue.estimatedPrice;
      try {
        var id=await this.objectService.saveItemAsync(o);
        this.router.navigateByUrl("/object");

      } catch (error) {
        this.errorMessage="L'objet ne peut être sauvegardé maintenant";
      }

    } catch (error) {
      this.errorMessage = "Le métier ne permet pas de créer l'objet";
    }
  }
}
