import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Guid } from 'guid-typescript';
import { ObjectHttpService } from 'src/app/models/object-http.service';
import { Object } from 'src/app/models/object';

@Component({
  selector: 'app-details-object',
  templateUrl: './details-object.component.html',
  styleUrls: ['./details-object.component.scss']
})
export class DetailsObjectComponent implements OnInit {
  
  constructor(private objectService:ObjectHttpService , private activatedRoute:ActivatedRoute) {
    
    
  }
  object?:Object;
  async ngOnInit() {
    let id= this.activatedRoute.snapshot.params["id"];
    let guid=Guid.parse(id);

    this.object=await this.objectService.getItemAsync(guid);
 
  }

}
