import { Component, OnInit } from '@angular/core';
import {IBrand, IBrandToCreate} from "../../shared/models/brand";
import {IProduct} from "../../shared/models/product";
import {AdminService} from "../admin.service";
import {IType, ITypeToCreate} from "../../shared/models/productType";

@Component({
  selector: 'app-brand-type',
  templateUrl: './brand-type.component.html',
  styleUrls: ['./brand-type.component.scss']
})
export class BrandTypeComponent implements OnInit {
  brands: IBrand[];
  types : IType[];
  brand: IBrandToCreate = {name: ''};
  type : ITypeToCreate = { name : ''};

  constructor(private service : AdminService) { }

  ngOnInit(): void {
    this.getBrands();
    this.getTypes();

  }

  createBrand(): void {
    this.service.createBrand(this.brand)
      .subscribe(
        response => {
          console.log(response);
          this.getBrands();
        },
        error => {
          console.log(error);
        });
  }

  createType(): void {
    this.service.createType(this.type)
      .subscribe(
        response => {
          console.log(response);
          this.getTypes();
        },
        error => {
          console.log(error);
        });
  }

  getBrands(){
    this.service.getAllBrands().subscribe(res => {
      this.brands = res ;
      console.log(res)
      }, error => {
      console.log(error)
    });
  }

  getTypes(){
    this.service.getAllTypes().subscribe( res => {
      this.types = res ;
      console.log(res)
    } , error => {
      console.log(error)
    })
  }

  deleteBrands(id : number){
    this.service.deleteBrand(id).subscribe( (m : any) => {
      this.brands.splice(this.brands.findIndex(p => p.id === id), 1 )
    });
  }

  deleteTypes(id : number){
    this.service.deleteType(id).subscribe( (m : any) => {
      this.types.splice(this.types.findIndex(p => p.id === id), 1 )
    });
  }

}
