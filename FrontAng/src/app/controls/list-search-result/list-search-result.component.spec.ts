import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListSearchResultComponent } from './list-search-result.component';

describe('ListSearchResultComponent', () => {
  let component: ListSearchResultComponent;
  let fixture: ComponentFixture<ListSearchResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListSearchResultComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListSearchResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
