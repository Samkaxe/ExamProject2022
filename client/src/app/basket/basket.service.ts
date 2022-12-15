import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, map} from "rxjs";
import {Basket, IBasket, IBaskettem} from "../shared/models/basket";
import {IProduct} from "../shared/models/product";

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();

  constructor(private http : HttpClient) { }

  getBasket(id : string){
    return this.http.get(this.baseUrl + 'basket?id=' + id)
      .pipe(
        map((basket : IBasket) => {
          this.basketSource.next(basket);
          console.log(this.getCurrentBasketValue());
        })
      );
  }

  setBasket(basket : IBasket){
    return this.http.post(this.baseUrl+ 'basket' , basket).subscribe((res : IBasket) => {
      this.basketSource.next(res);
      console.log(res)
    }, error => {
      console.log(error)
    })
  }

  getCurrentBasketValue(){
    return this.basketSource.value ;
  }

  addItemToBasket(item : IProduct , quantity = 1){
    const itemToAdd : IBaskettem = this.mapper(item , quantity);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    //basket.items.push(itemToAdd);
  basket.items = this.addUpdateItem(basket.items , itemToAdd , quantity);
  this.setBasket(basket);
  }

  private addUpdateItem(items: IBaskettem[], itemToAdd: IBaskettem, quantity: number) : IBaskettem[] {
    console.log(items);
    const index = items.findIndex(i => i.id === itemToAdd.id)
    if(index === -1){
      itemToAdd.quantity = quantity ;
      items.push(itemToAdd);
    }
    else {
     // items [index].quantity = ++quantity ;
      items [index].quantity += quantity ;
    }
    return items ;
  }

  private createBasket() : IBasket {
  const basket = new Basket();
  localStorage.setItem('basket_id' , basket.id);

  return basket;
  }

  private mapper(item: IProduct, quantity: number):IBaskettem {
    return {
      id : item.id,
      productName : item.name ,
      price : item.price ,
      pictureUrl : item.pictureUrl,
      quantity,
      brand : item.productBrand,
      type : item.productType
    }
  }

}
