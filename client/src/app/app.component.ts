import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IProduct} from "./shared/models/product";
import {BasketService} from "./basket/basket.service";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Exam Project';
  products: IProduct[];


  constructor(private service : BasketService) {}

  ngOnInit(): void {
    const basketId = localStorage.getItem('basket_id');
    if(basketId){
      this.service.getBasket(basketId).subscribe( () => {
        console.log('Basket Created ');
      } , error => {
        console.log(error);
      });
    }
  }
}
