import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SearchResult } from 'src/app/models/search-result';

@Component({
  selector: 'app-list-search-result',
  templateUrl: './list-search-result.component.html',
  styleUrls: ['./list-search-result.component.scss']
})
export class ListSearchResultComponent {
  @Output()
  
  searchResultClick=new EventEmitter<SearchResult>();

  @Input()
  searchResults?:SearchResult[];

  onSearchResultClick(r:SearchResult){
    this.searchResultClick.emit(r);
  }


}
