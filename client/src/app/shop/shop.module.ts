import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SearchPipe, ShopComponent} from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import {FormsModule} from "@angular/forms";
import {BrowserModule} from "@angular/platform-browser";



@NgModule({
    declarations: [
        ShopComponent,
        ProductItemComponent,
      SearchPipe
    ],
    exports: [
        ShopComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
      BrowserModule
    ]
})
export class ShopModule { }
