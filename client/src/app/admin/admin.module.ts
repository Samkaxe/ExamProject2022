import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditProductComponent } from './edit-product/edit-product.component';
import {AdminComponent} from "./admin.component";
import {SharedModule} from "../shared/shared.module";
import {AdminRoutingModule} from "./admin-routing.module";
import {FormsModule} from "@angular/forms";
import {ImageUploadComponent} from "./image-upload/image-upload.component";


@NgModule({
  declarations: [
    AdminComponent,
    EditProductComponent,
    ImageUploadComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    AdminRoutingModule,
    FormsModule,
  ],
  exports : [
    AdminComponent
  ]
})
export class AdminModule { }
