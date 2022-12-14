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

  constructor(private  shopService : ShopService , private  activeRoute : ActivatedRoute) { }

  ngOnInit(): void {
  this.loadProduct();
  }

  loadProduct(){
    this.shopService.getProduct(+this.activeRoute.snapshot.paramMap.get('id')).subscribe(product =>{
      this.product = product ;
      console.log(product)
    }, error => {
      console.log(error);
    });
  }

}
