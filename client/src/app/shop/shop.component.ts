import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {IProduct} from "../shared/models/product";
import {ShopService} from "./shop.service";
import {IBrand} from "../shared/models/brand";
import {IType} from "../shared/models/productType";
import { Pipe, PipeTransform } from '@angular/core';
import {environment} from "../../environments/environment";


@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: IProduct[];

  brands: IBrand[];
  types: IType[];
  public selectedBrand: IBrand;
  public selectedType: IType;
  public searchInput: string;


  alphabetic = environment.ALPHABETIC;
  priceAscending = environment.PRICE_ASCENDING;
  priceDescending = environment.PRICE_DESCENDING;
  public sortValue: string=this.alphabetic;

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

  selectBrand(brand: IBrand) {
    this.selectedBrand = brand;
  }

  selectType(type: IType) {
    this.selectedType = type;
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

@Pipe({name: 'brandFilter'})
export class BrandFilterPipePipe implements PipeTransform {
  transform(products: IProduct[], selectedBrand: IBrand): IProduct[] {
    if (!selectedBrand) return products
    return products.filter(item => item.productBrand === selectedBrand.name);
  }

}

@Pipe({name: 'typeFilter'})
export class TypeFilterPipe implements PipeTransform {
  transform(products: IProduct[], selectedType: IType): IProduct[] {
    if (!selectedType) return products
    return products.filter(item => item.productType === selectedType.name);
  }

}

@Pipe({name: 'sort'})
export class SortPipe implements PipeTransform {
  transform(products: IProduct[], sort: string): IProduct[] {
    if (!sort) return products
    if (sort === environment.ALPHABETIC) {
      products.sort((a, b) => {
        let aName = a.name.toLowerCase();
        let bName = b.name.toLowerCase();
        if (aName < bName) return -1;
        if (aName > bName) return 1;
        return 0;
      })
    }
    if (sort === environment.PRICE_ASCENDING) {
      products.sort((a,b)=>a.price-b.price);
    }
    if (sort == environment.PRICE_DESCENDING) {
      products.sort((a,b)=>b.price-a.price);
    }
    return products;
  }

}
