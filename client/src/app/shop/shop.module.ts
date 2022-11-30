import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {BrandFilterPipePipe, SearchPipe, ShopComponent, SortPipe, TypeFilterPipe} from './shop.component';import { ProductItemComponent } from './product-item/product-item.component';
import {FormsModule} from "@angular/forms";
import {BrowserModule} from "@angular/platform-browser";
import { ProductDetailsComponent } from './product-details/product-details.component';
import {RouterModule} from "@angular/router";



@NgModule({
    declarations: [
        ShopComponent,
        ProductItemComponent,
      SearchPipe,
      BrandFilterPipePipe,
      TypeFilterPipe,
      SortPipe,
      ProductDetailsComponent
    ],
    exports: [
        ShopComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        BrowserModule,
        RouterModule
    ]
})
export class ShopModule { }
