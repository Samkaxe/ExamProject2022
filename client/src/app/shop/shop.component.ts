import { Component, OnInit } from '@angular/core';
import {IProduct} from "../shared/models/product";
import {ShopService} from "./shop.service";
import {IBrand} from "../shared/models/brand";
import {IType} from "../shared/models/productType";

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products : IProduct[];
  brands : IBrand[];
  types : IType[];
  constructor(private shopService : ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts().subscribe(res => {
      this.products = res ;
    }, error => {
      console.log(error)
    })
  }

  getBrands(){
    this.shopService.getBrands().subscribe(res => {
      this.brands = res ;
    } ,error => {
      console.log(error)
    });
  }

  getTypes(){
    this.shopService.getType().subscribe(res => {
      this.types = res ;
    } ,error => {
      console.log(error)
    });
  }

}
