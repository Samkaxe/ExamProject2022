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
  loadChildren: () => import('./admin/admin.module') //An object specifying lazy-loaded child routes.
  .then(mod => mod.AdminModule), data: { breadcrumb: 'Admin' }
   },
  {path: 'basket',
    loadChildren: () => import('./basket/basket.module')
      .then(mod => mod.BasketModule), data: { breadcrumb: 'Basket'}
  },
  {path : '**' , redirectTo : 'full'} //Full matched against the entire URL
];

@NgModule({
  imports: [RouterModule.forRoot(routes)], // this means its added to our root modules which is our app modules where those routes are contained
  exports: [RouterModule]
})
export class AppRoutingModule { }
