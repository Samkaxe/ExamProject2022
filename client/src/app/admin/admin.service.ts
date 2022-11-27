import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {IProduct, ProductFormValues} from "../shared/models/product";
import {Observable} from "rxjs";
import {IBrand} from "../shared/models/brand";
import {IType} from "../shared/models/productType";

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;
  constructor(private http : HttpClient) { }

  getAll(): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(`${this.baseUrl}products`);
  }
  getAllBrands(): Observable<IBrand[]> {
    return this.http.get<IBrand[]>(`${this.baseUrl}brands`);
  }

  getAllTypes(): Observable<IType[]>{
    return this.http.get<IType[]>(`${this.baseUrl}types`);
  }

  get(id: any): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(`${this.baseUrl}products/${id}`);
  }

  createProduct(product: ProductFormValues) {
    return this.http.post(this.baseUrl + 'products', product);
  }

  updateProduct(product: ProductFormValues, id: number) {
    return this.http.put(this.baseUrl + 'products/' + id, product);
  }

  deleteProduct(id: number) {
    return this.http.delete(this.baseUrl + 'products/' + id);
  }
}
