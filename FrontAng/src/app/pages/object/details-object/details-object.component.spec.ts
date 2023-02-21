import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsObjectComponent } from './details-object.component';

describe('DetailsObjectComponent', () => {
  let component: DetailsObjectComponent;
  let fixture: ComponentFixture<DetailsObjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetailsObjectComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetailsObjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
