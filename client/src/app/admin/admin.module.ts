import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditProductComponent } from './edit-product/edit-product.component';
import {AdminComponent} from "./admin.component";
import {SharedModule} from "../shared/shared.module";
import {AdminRoutingModule} from "./admin-routing.module";
import {RouterModule} from "@angular/router";
import {FormsModule} from "@angular/forms";



@NgModule({
  declarations: [
    AdminComponent,
    EditProductComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    AdminRoutingModule,
    RouterModule,
    FormsModule
  ],
  exports : [
    AdminComponent
  ]
})
export class AdminModule { }
