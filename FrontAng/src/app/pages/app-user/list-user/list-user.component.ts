import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { userHttpService } from 'src/app/models/appUser-http.service';
import { SearchResult } from 'src/app/models/search-result';

@Component({
  selector: 'app-list-user',
  templateUrl: './list-user.component.html',
  styleUrls: ['./list-user.component.scss']
})
export class ListUserComponent {
  results?: SearchResult[];
  constructor(private userService: userHttpService, private router: Router) {

  }
  waiting = false;
  async rechercherUser(searchText: string) {
    this.waiting = true;
    this.results = await this.userService.searchItemAsync(searchText);
    this.waiting = false;
  }

  showUser(r: SearchResult) {
    this.router.navigateByUrl("details-user/" + r.id);
  }
}
