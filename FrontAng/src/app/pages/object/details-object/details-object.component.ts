import { Component, NgModule, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { ObjectHttpService } from 'src/app/models/object-http.service';
import { Object } from 'src/app/models/object';
import { SearchResult } from 'src/app/models/search-result';


@Component({
  selector: 'app-details-object',
  templateUrl: './details-object.component.html',
  styleUrls: ['./details-object.component.scss']
})


export class DetailsObjectComponent implements OnInit {
  
  
  constructor(private objectService:ObjectHttpService , private activatedRoute:ActivatedRoute, private router:Router) {
    
    
  }

  object?:Object;
  async ngOnInit() {
    let id= this.activatedRoute.snapshot.params["id"];
    let guid=Guid.parse(id);

    this.object=await this.objectService.getItemAsync(guid);
    
  }
  async deleteItemAsync() {
    let id= this.activatedRoute.snapshot.params["id"];
    let guid=Guid.parse(id);
    await this.objectService.deleteItemAsync(guid);
    this.router.navigate(['/object']);
  }

  async editPage(){
    let id= this.activatedRoute.snapshot.params["id"];
    let guid=Guid.parse(id);
    this.router.navigate(['/edit-object/'+guid]);
  }
}
