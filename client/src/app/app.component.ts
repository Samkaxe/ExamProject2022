import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IProduct} from "./shared/models/product";
import {BasketService} from "./basket/basket.service";


@Component({ // identifies it as angular component that marks
  selector: 'app-root', //CSS selector that identifies this directive in a template and triggers instantiation of the directive.
  templateUrl: './app.component.html', //path or absolute URL of a template file for an Angular component.
  styleUrls: ['./app.component.scss'] //absolute URLs for files containing CSS stylesheets to use in this component.
})
export class AppComponent implements OnInit{ //A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive to handle any additional initialization tasks..
  title = 'Exam Project';
  products: IProduct[];


  constructor(private service : BasketService) {} // beacuse we want to get the basket on startup

  ngOnInit(): void {
    const basketId = localStorage.getItem('basket_id');
    if(basketId){ // if we have an id
      this.service.getBasket(basketId).subscribe( () => {
        console.log('Basket Created ');
      } , error => {
        console.log(error);
      });
    }
  }
}
