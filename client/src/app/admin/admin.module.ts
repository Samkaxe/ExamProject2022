import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from '../admin.component';
import { EditProductComponent } from './edit-product/edit-product.component';



@NgModule({
  declarations: [
    AdminComponent,
    EditProductComponent
  ],
  imports: [
    CommonModule
  ]
})
export class AdminModule { }
