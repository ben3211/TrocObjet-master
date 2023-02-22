import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ObjectComponent } from './pages/object/list-objet/list-object.component';
import { DetailsObjectComponent } from './pages/object/details-object/details-object.component';
import { ListSearchResultComponent } from './controls/list-search-result/list-search-result.component';
import { EditObjectComponent } from './pages/object/edit-object/edit-object.component';
import { CreateObjectComponent } from './pages/object/create-object/create-object.component';
import { ObjectHttpService } from './models/object-http.service';
import { objectService } from './models/object.service';
import { DetailsUserComponent } from './pages/app-user/details-user/details-user.component';
import { CreateUserComponent } from './pages/app-user/create-user/create-user.component';
import { EditUserComponent } from './pages/app-user/edit-user/edit-user.component';
import { ListUserComponent } from './pages/app-user/list-user/list-user.component';


@NgModule({
  declarations: [
    AppComponent,
    ObjectComponent,
    DetailsObjectComponent,
    ListSearchResultComponent,
    EditObjectComponent,
    CreateObjectComponent,
    DetailsUserComponent,
    CreateUserComponent,
    EditUserComponent,
    ListUserComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
