import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, map} from "rxjs";
import {Basket, IBasket} from "../shared/models/basket";

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();

  constructor(private http : HttpClient) { }

  getBasket(id : string){
    return this.http.get(this.baseUrl + 'basket?id' + id)
      .pipe(
        map((basket : IBasket) => {
          this.basketSource.next(basket);
        })
      );
  }

  setBasket(basket : IBasket){
    return this.http.post(this.baseUrl+ 'basket' , basket).subscribe((res : IBasket) => {
      this.basketSource.next(res);
    }, error => {
      console.log(error)
    })
  }

  getCurrentBasketValue(){
    return this.basketSource.value ;
  }
}
