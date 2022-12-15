import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IProduct} from "../shared/models/product";
import {IBrand} from "../shared/models/brand";
import {IType} from "../shared/models/productType";

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl ='https://localhost:5001/api/';
  constructor(private http : HttpClient) { }


  getProducts(){
    return this.http.get<IProduct[]>(this.baseUrl + 'products');
  }

  getBrands(){
    return this.http.get<IBrand[]>(this.baseUrl + 'brands');
  }

  getType(){
    return this.http.get<IType[]>(this.baseUrl + 'types');
  }
  getProduct(id : number){
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }
}
