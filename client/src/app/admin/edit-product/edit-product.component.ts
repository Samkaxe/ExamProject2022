import { Component, OnInit } from '@angular/core';
import {IBrand} from "../../shared/models/brand";
import {IProduct, IProductToCreate, ProductFormValues} from "../../shared/models/product";
import {IType} from "../../shared/models/productType";
import {AdminService} from "../admin.service";
import {ActivatedRoute, Router} from "@angular/router";
import {forkJoin} from "rxjs";

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.scss']
})
export class EditProductComponent implements OnInit {

  product: ProductFormValues;
  submitted: boolean = false;
  Brands : IBrand[];
  types : IType[];

  constructor(private adminService: AdminService,
              private route: ActivatedRoute,
              private router: Router) {
    this.product = new ProductFormValues();
  }

  ngOnInit(): void {
    const brands = this.getBrands();
    const types = this.getTypes();

    forkJoin([types, brands]).subscribe(results => {
      this.types = results[0];
      this.Brands = results[1];
    }, error => {
      console.log(error);
    }, () => {
      if (this.route.snapshot.url[0].path === 'edit') {
        this.loadProduct();
      }
    });

  }

  updatePrice(event: any) {
    this.product.price = event;
  }

  loadProduct() {
    this.adminService.get(+this.route.snapshot.paramMap.get('id')).subscribe((response: any) => {
      const productBrandId = this.Brands && this.Brands.find(x => x.name === response.productBrand).id;
      const productTypeId = this.types && this.types.find(x => x.name === response.productType).id;
      this.product = {...response, productBrandId, productTypeId};
    });
  }

  getBrands() {
    return this.adminService.getAllBrands();
  }

  getTypes() {
    return this.adminService.getAllTypes();
  }

  // saveBox() {
  //   this.adminService.createProduct(this.product).subscribe( data => {
  //     this.submitted = true ;
  //     console.log(data)
  //   },error => {
  //     console.log(error)
  //   })
  // }

  onSubmit(product: ProductFormValues) {
    if (this.route.snapshot.url[0].path === 'edit') {
      const updatedProduct = {...this.product, ...product, price: +product.price};
      this.adminService.updateProduct(updatedProduct, +this.route.snapshot.paramMap.get('id')).subscribe((response: any) => {
        this.router.navigate(['/admin']);
      });
    } else {
      const newProduct = {...product, price: +product.price};
      this.adminService.createProduct(newProduct).subscribe((response: any) => {
        this.router.navigate(['/admin']);
      });
    }
  }

  // newBox() {
  //   this.submitted = false;
  //   this.product = {
  //     name : '',
  //     description :'',
  //     price : 0,
  //     pictureUrl : '',
  //     productBrandId : 0,
  //     productTypeId :0
  //   }
  // }
}
