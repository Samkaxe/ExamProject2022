import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {IProduct} from "../shared/models/product";
import {ShopService} from "./shop.service";
import {IBrand} from "../shared/models/brand";
import {IType} from "../shared/models/productType";
import { Pipe, PipeTransform } from '@angular/core';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products : IProduct[];
  brands : IBrand[];
  types : IType[];
  public searchInput: string;

  @Output()
  searchTextChanged : EventEmitter<string> = new EventEmitter<string>();

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



  toggleExpand(product : any) {
    product.expanded = !product.expanded;
  }
}

@Pipe({
  name:'search'
})
export class SearchPipe implements PipeTransform {
  transform(products: IProduct[], searchInput: string): IProduct[]{
    if(!searchInput) {
      return  products;
    }

    searchInput = searchInput.toLowerCase();

    return products.filter(item => {
      if (item) {
        return (item["name"].toLowerCase().includes(searchInput));
      }
      return false;
    });
  }
}
