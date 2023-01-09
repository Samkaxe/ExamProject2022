import { Component, OnInit } from '@angular/core';
import {IProduct} from "../shared/models/product";
import {ShopService} from "../shop/shop.service";
import {AdminService} from "./admin.service";


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  products: IProduct[];

  constructor(private shopService: ShopService, private adminService: AdminService) {
  }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.adminService.getAll().subscribe(response => {
      this.products = response;
    }, error => {
      console.log(error);
    });
  }

  deleteProduct(id: number) {
    this.adminService.deleteProduct(id).subscribe((makita: any) => {
      this.products.splice(this.products.findIndex(p => p.id === id), 1);
    //method changes the contents of an array by removing or replacing existing elements and/or adding new elements
    });
  }

}
