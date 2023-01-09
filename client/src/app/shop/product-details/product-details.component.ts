import { Component, OnInit } from '@angular/core';
import {IProduct} from "../../shared/models/product";
import {ShopService} from "../shop.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product : IProduct;

  constructor(private  shopService : ShopService , private  activeRoute : ActivatedRoute) { } // Provides access to information about a route associated with a component that is loaded in an outlet

  ngOnInit(): void {
  this.loadProduct();
  }

  loadProduct(){                // get  the param from the Url + convert from string to integer
    this.shopService.getProduct(+this.activeRoute.snapshot.paramMap.get('id')).subscribe(product =>{
      this.product = product ;
      console.log(product)
    }, error => {
      console.log(error);
    });
  }

}
