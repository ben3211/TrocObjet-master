import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppUserComponent } from './pages/app-user/app-user.component';
import { CreateObjectComponent } from './pages/object/create-object/create-object.component';
import { DetailsObjectComponent } from './pages/object/details-object/details-object.component';
import { EditObjectComponent } from './pages/object/edit-object/edit-object.component';
import { ObjectComponent } from './pages/object/list-objet/list-object.component';


const routes: Routes = [
  {path:"user", component:AppUserComponent},
  {path:"object", component:ObjectComponent},
  {path:"details-object/:id", component:DetailsObjectComponent},
  {path:"create-object",component:CreateObjectComponent},
  {path:"edit-object/:id",component:EditObjectComponent},
  {path:"**", redirectTo:"user"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
