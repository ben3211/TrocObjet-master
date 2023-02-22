import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateUserComponent } from './pages/app-user/create-user/create-user.component';
import { DetailsUserComponent } from './pages/app-user/details-user/details-user.component';
import { EditUserComponent } from './pages/app-user/edit-user/edit-user.component';
import { ListUserComponent } from './pages/app-user/list-user/list-user.component';
import { CreateObjectComponent } from './pages/object/create-object/create-object.component';
import { DetailsObjectComponent } from './pages/object/details-object/details-object.component';
import { EditObjectComponent } from './pages/object/edit-object/edit-object.component';
import { ObjectComponent } from './pages/object/list-objet/list-object.component';


const routes: Routes = [
  {path:"user", component:ListUserComponent},
  {path:"edit-user/:id", component: EditUserComponent},
  {path:"details-user/:id", component:DetailsUserComponent},
  {path:"create-user",component:CreateUserComponent},
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
