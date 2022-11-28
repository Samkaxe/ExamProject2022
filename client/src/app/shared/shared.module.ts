import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {CurrencyMaskModule} from "ng2-currency-mask";



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    CurrencyMaskModule
  ],
  exports : [CurrencyMaskModule]
})
export class SharedModule { }
