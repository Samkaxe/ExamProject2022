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
  private basketSource = new BehaviorSubject<IBasket>(null); // A variant of Subject emits its current value whenever it is subscribed to.
  basket$ = this.basketSource.asObservable(); // i want to be able to subscribe using the async pipe in order to get the value

  constructor(private http : HttpClient) { }

  getBasket(id : string){
    return this.http.get(this.baseUrl + 'basket?id=' + id) //concatenate the ID
      .pipe( // so can use if rsjs oprator
        map((basket : IBasket) => { // map the basket
          this.basketSource.next(basket); // next : set the behavior subject to set the value
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

  //
  private addUpdateItem(items: IBaskettem[], itemToAdd: IBaskettem, quantity: number) : IBaskettem[] {
    console.log(items);
    const index = items.findIndex(i => i.id === itemToAdd.id) //
    if(index === -1){ // if the index was not found
      itemToAdd.quantity = quantity ; // add the first itemtoadd
      items.push(itemToAdd); // dds one or more elements to the end of the array
    }
    else {
     // items [index].quantity = ++quantity ;
      items [index].quantity += quantity ;  // if the index already exists we just increment ,
    }
    return items ;
  }

  private createBasket() : IBasket {
  const basket = new Basket();
  localStorage.setItem('basket_id' , basket.id); // give us some level of prisiting as long as the user dont close the broswer

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
