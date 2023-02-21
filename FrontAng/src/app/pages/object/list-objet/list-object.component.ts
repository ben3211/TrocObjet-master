import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ObjectHttpService } from 'src/app/models/object-http.service';
import { SearchResult } from 'src/app/models/search-result';

@Component({
  selector: 'app-object',
  templateUrl: './list-object.component.html',
  styleUrls: ['./list-object.component.scss']
})

export class ObjectComponent {
  results?: SearchResult[];

  constructor(private objectService:ObjectHttpService, private router:Router){

  }
  waiting=false;
  async rechercher(searchText:string){
    this.waiting=true;
    this.results=await this.objectService.searchItemAsync(searchText);
    this.waiting=false;
  }

  showObject(r:SearchResult){
    this.router.navigateByUrl("details-object/"+r.id);
  }
}



