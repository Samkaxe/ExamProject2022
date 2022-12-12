import {Component, OnInit} from '@angular/core';
import {IBrand} from "../../shared/models/brand";
import {IProduct, IProductToCreate, ProductFormValues} from "../../shared/models/product";
import {IType} from "../../shared/models/productType";
import {AdminService} from "../admin.service";
import {ActivatedRoute, Router} from "@angular/router";
import {forkJoin} from "rxjs";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.scss']
})
export class EditProductComponent implements OnInit {

  product: ProductFormValues;
  submitted: boolean = false;
  Brands: IBrand[];
  types: IType[];

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

  uploadChanged(fileName: string) {
    this.product.picturePath =fileName;
    this.product.pictureUrl =fileName;
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

  onSubmit(product: ProductFormValues) {
    if (!product.pictureUrl) {
      console.log(product);
      return;

    }

    if (this.route.snapshot.url[0].path === 'edit') {
      const updatedProduct = {...this.product, ...product, price: +product.price};
      console.log(+this.route.snapshot.paramMap.get('id'));
      //console.log('hey')
      this.adminService.updateProduct(updatedProduct, +this.route.snapshot.paramMap.get('id')).subscribe((response: any) => {
        console.log(updatedProduct)
        this.router.navigate(['/admin']);
      });
    } else {
      const newProduct = {...product, price: +product.price};
      this.adminService.createProduct(newProduct).subscribe((response: any) => {
        console.log(newProduct)
        this.router.navigate(['/admin']);
      });
    }
  }

}
