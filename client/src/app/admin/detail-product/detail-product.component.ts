import { Component, OnInit } from '@angular/core';
import {IProduct, IProductToCreate} from "../../shared/models/product";
import {IBrand} from "../../shared/models/brand";
import {IType} from "../../shared/models/productType";
import {AdminService} from "../admin.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-detail-product',
  templateUrl: './detail-product.component.html',
  styleUrls: ['./detail-product.component.scss']
})
export class DetailProductComponent implements OnInit {
  product : IProduct = {
    id : 0,
    name : '',
    description :'',
    price : 0,
    pictureUrl : '',
    productType: '',
    productBrand: '',
  }

  Brands : IBrand[];
  types : IType[];

  message = '';

  constructor(private http : AdminService,
              private  route : ActivatedRoute,
              private router : Router) { }

  ngOnInit(): void {
  this.message = '';
    this.getProduct(this.route.snapshot.params['id']);
   this.getTypes();
   this.getBrnads();
  }



  updateBox(): void {
    this.message = '';

    this.http.updateProduct(this.product ,this.product.id).subscribe(res => {
      console.log(res);
      this.router.navigate(['/products']);
    },error => {
      console.log(error)
    })
  }

  getProduct(id : string): void {
    this.http.get(id).subscribe( data => {
      this.product = data ;
      console.log(data);
    } , error => {
      console.log(error);
    });
  }

  getBrnads(): void {
    this.http.getAllBrands().subscribe(data => {
      this.Brands = data;
      console.log(data);
    },error => {
      console.log(error)
    });
  }


  getTypes(): void {
    this.http.getAllTypes().subscribe(
      data => {
        this.types = data;
        console.log(data);
      } , error => {
        console.log(error);
      });
  }
}
