export interface Product {
  id?: string;
  code: string;
  description: string;
  departmentCode: string;
  price: number;
  active: boolean;
  removed: boolean;
}