import Member from "./Member";
import Product from "./Product";

export default class Store {
  id: number;
  name: string;
  products: Product[];
  founder: Member;
  isOpen:boolean;

  constructor(Id: number, Name: string, Products: Product[],founder:Member,isOpen:boolean) {
    this.id = Id;
    this.name = Name;
    this.products = Products;
    this.isOpen = isOpen;
    this.founder = founder;
  }
}
