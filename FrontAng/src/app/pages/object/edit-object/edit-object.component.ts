//à terminer

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Object } from 'src/app/models/object';
import { objectService } from 'src/app/models/object.service';
import { ObjectHttpService } from 'src/app/models/object-http.service';

@Component({
  selector: 'app-edit-object',
  templateUrl: './edit-object.component.html',
  styleUrls: ['./edit-object.component.scss']
})
export class EditObjectComponent implements OnInit {
  constructor(private objectService: ObjectHttpService, private activatedRoute: ActivatedRoute,
    private router: Router) {

  }

  object: Object | undefined;
  errorMessage?: string;
  editValue?: { label: string, description: string, estimatedPrice: number };
  idObject?: Guid;

  async updateObject() {
    try {
      // Tentative de modification des valeurs de l'object
      this.object!.label = this.editValue?.label!;
      this.object!.description = this.editValue?.description!;
      this.object!.estimatedPrice = this.editValue?.estimatedPrice!;
      try {
        // Tentative de sauvegarde
        var id = await this.objectService.updateItemAsync(this.idObject!, this.object!);
        this.router.navigateByUrl("/object");
      } catch (error) {
        this.errorMessage = "L'objet ne peut être sauvegardé maintenant";
      }
    } catch (error) {
      this.errorMessage = "Le métier ne permet pas d'éditer l'objet";
    }
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(async params => {
      var id = params["id"];
      this.idObject = Guid.parse(id);
      this.object = await this.objectService.getItemAsync(id);
      this.editValue = {
        label: this.object.label,
        description: this.object.description,
        estimatedPrice:this.object.estimatedPrice
      }

    })
  }
}
