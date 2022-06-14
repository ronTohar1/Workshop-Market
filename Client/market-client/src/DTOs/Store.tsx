import DiscountPolicy from "./DiscountPolicy";
import Member from "./Member";
import Product from "./Product";
import PurchasePolicy from "./PurchasePolicy";

export default class Store {
  id: number;
  name: string;
  products: Product[];
  founder: Member;
  isOpen:boolean;
  purchasePolicies: PurchasePolicy[];
  discountPolicies: DiscountPolicy[];

  constructor(Id: number, Name: string, Products: Product[],founder:Member,isOpen:boolean, purchasePolicies:PurchasePolicy[], discountPolicies:DiscountPolicy[]) {
    this.id = Id;
    this.name = Name;
    this.products = Products;
    this.isOpen = isOpen;
    this.founder = founder;
    this.purchasePolicies= purchasePolicies;
    this.discountPolicies = discountPolicies
  }
}
