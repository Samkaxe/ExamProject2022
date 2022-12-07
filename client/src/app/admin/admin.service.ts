import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpRequest} from "@angular/common/http";
import {IProduct, IProductToCreate, ProductFormValues} from "../shared/models/product";
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

  get(id: any): Observable<IProduct> {
    return this.http.get<IProduct>(`${this.baseUrl}products/${id}`);
  }

  createProduct(product: ProductFormValues) {
    //product.pictureUrl = 'tested';
    return this.http.post(this.baseUrl + 'products', product);
  }

  updateProduct(product: ProductFormValues, id: number) {
   // product.pictureUrl = 'tested';
    return this.http.put(this.baseUrl + 'products/' + id, product);
  }

  deleteProduct(id: number) {
    return this.http.delete(this.baseUrl + 'products/' + id);
  }

  upload(file: File) {
    const formData: FormData = new FormData();
    formData.append('image', file);
    formData.append('ImagePath',file.name    )
    const req = new HttpRequest('POST', `${this.baseUrl}upload`, formData, {
      reportProgress: true,
      responseType: 'json'
    });
    return this.http.request(req);
  }
}
