import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {HttpClientModule} from "@angular/common/http";
import {CoreModule} from "./core/core.module";
import {AdminModule} from "./admin/admin.module";
import {ShopModule} from "./shop/shop.module";
import {HomeModule} from "./home/home.module";

// what is ng module used for
//Declares which components, directives, and pipes belong to the module
// Makes some of those components, directives, and pipes public so that other module's component templates can use them
// Imports other modules with the components, directives, and pipes that components in the current module need
// Provides services that other application components can use

@NgModule({
  declarations: [
    AppComponent,
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
