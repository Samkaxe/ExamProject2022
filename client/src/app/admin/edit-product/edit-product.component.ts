import { Component, OnInit } from '@angular/core';
import {IBrand} from "../../shared/models/brand";
import {IProduct, IProductToCreate, ProductFormValues} from "../../shared/models/product";
import {IType} from "../../shared/models/productType";
import {AdminService} from "../admin.service";

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.scss']
})
export class EditProductComponent implements OnInit {

  product : IProductToCreate = {
    name : '',
    description :'',
    price : 0,
    pictureUrl : '',
    productBrandId : 0,
    productTypeId :0
  }

  submitted: boolean = false;
  Brands : IBrand[];
  types : IType[];

  constructor(private http : AdminService) { }

  ngOnInit(): void {

  }



  saveBox() {
    this.http.createProduct(this.product).subscribe( data => {
      this.submitted = true ;
      console.log(data)
    },error => {
      console.log(error)
    })
  }

  newBox() {
    this.submitted = false;
    this.product = {
      name : '',
      description :'',
      price : 0,
      pictureUrl : '',
      productBrandId : 0,
      productTypeId :0
    }
  }
}
