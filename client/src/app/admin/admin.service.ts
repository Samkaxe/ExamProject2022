import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpRequest} from "@angular/common/http";
import {IProduct, IProductToCreate, ProductFormValues} from "../shared/models/product";
import {Observable} from "rxjs";
import {IBrand, IBrandToCreate} from "../shared/models/brand";
import {IType, ITypeToCreate} from "../shared/models/productType";

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
    return this.http.get<IBrand[]>(`${this.baseUrl}Brands`);
  }

  getAllTypes(): Observable<IType[]>{
    return this.http.get<IType[]>(`${this.baseUrl}Types`);
  }

  get(id: any): Observable<IProduct> {
    return this.http.get<IProduct>(`${this.baseUrl}products/${id}`);
  }

  createProduct(product: ProductFormValues) {
    return this.http.post(this.baseUrl + 'products', product);
  }

  createBrand(brand : IBrandToCreate) {
    return this.http.post(this.baseUrl + 'brands' , brand);
  }

  createType(type : ITypeToCreate){
    return this.http.post(this.baseUrl + 'types' , type);
  }

  updateProduct(product: ProductFormValues, id: number) {
    return this.http.put(this.baseUrl + 'products/' + id, product);
  }

  deleteProduct(id: number) {
    return this.http.delete(this.baseUrl + 'products/' + id);
  }

  deleteBrand(id: number) {
    return this.http.delete(this.baseUrl + 'brands/' + id);
  }

  deleteType(id :number){
    return this.http.delete(this.baseUrl + 'types/' + id);
  }

  upload(file: File) {
    const formData: FormData = new FormData();        //for upload we should create a form data and append items to it
    formData.append('image', file);    //appending fields to form data
    formData.append('ImagePath',file.name    )
    const req = new HttpRequest('POST', `${this.baseUrl}upload`, formData, {  //sending upload request with post method
      reportProgress: true,     //determines to get upload progress
      responseType: 'json'    //determines response type
    });
    return this.http.request(req);
  }
}
