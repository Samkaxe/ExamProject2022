export interface IType {
  id : number;
  name: string;
}
export interface ITypeToCreate {
  name: string
}

export class BrandToCreate implements ITypeToCreate{
  name: string;
}
