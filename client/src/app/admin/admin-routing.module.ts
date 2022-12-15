import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import {AdminComponent} from "./admin.component";
import {EditProductComponent} from "./edit-product/edit-product.component";
import {BrandTypeComponent} from "./brand-type/brand-type.component";

const routes: Routes = [
  {path: '', component: AdminComponent},

  {path: 'create', component: EditProductComponent, data: {breadcrumb: 'Create'}},
  {path: 'edit/:id', component: EditProductComponent, data: {breadcrumb: 'Edit'}},
  {path :'createExtra', component: BrandTypeComponent},
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
