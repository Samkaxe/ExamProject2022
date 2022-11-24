import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {HttpClientModule} from "@angular/common/http";
import {CoreModule} from "./core/core.module";
import { EditProductComponent } from './edit-product/edit-product.component';
import {AdminModule} from "./admin/admin.module";
import {HomeModule} from "./home/home.module";
import {ShopModule} from "./shop/shop.module";

@NgModule({
  declarations: [
    AppComponent,
    EditProductComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CoreModule,
    ShopModule,
    HomeModule,
    AdminModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
