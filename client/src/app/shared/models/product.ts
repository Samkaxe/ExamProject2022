export interface IProduct {
  id: number
  name: string
  description: string
  price: number
  pictureUrl: string
  productType: string
  productBrand: string
}

export interface IProductToCreate {
  name: string;
  description: string;
  price: number;
  pictureUrl: string;
  productTypeId: number;
  productBrandId: number;
}

export class ProductFormValues implements IProductToCreate {
  name = '';
  description = '';
  price = 0;
  pictureUrl = '';
  picturePath = '';
  productBrandId: number = 0;
  productTypeId: number = 0;

  constructor(init?: ProductFormValues) {
    Object.assign(this, init);
  }

}
