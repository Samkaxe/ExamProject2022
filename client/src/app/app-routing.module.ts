import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {ShopComponent} from "./shop/shop.component";
import {ProductDetailsComponent} from "./shop/product-details/product-details.component";
import {AdminComponent} from "./admin/admin.component";

const routes: Routes = [
  {path : '' , component : HomeComponent},
  {path : 'shop' , component : ShopComponent},
  {path : 'shop/:id' , component : ProductDetailsComponent},
  {path: 'admin',
  loadChildren: () => import('./admin/admin.module')
  .then(mod => mod.AdminModule), data: { breadcrumb: 'Admin' }
   },
  {path: 'basket',
    loadChildren: () => import('./basket/basket.module')
      .then(mod => mod.BasketModule), data: { breadcrumb: 'Basket'}
  },
  {path : '**' , redirectTo : 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
