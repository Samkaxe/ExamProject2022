export interface IBrand {
  id : number;
  name: string;
}

export interface IBrandToCreate {
  name: string
}

export class BrandToCreate implements IBrandToCreate{
  name: string;
}
