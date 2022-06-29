import Bid from "./Bid";
import DiscountPolicy from "./DiscountPolicy";
import Member from "./Member";
import Product from "./Product";
import PurchasePolicy from "./PurchasePolicy";

export default class Store {
  id: number;
  name: string;
  products: Product[];
  founder: Member;
  isOpen: boolean;
  purchasePolicies: PurchasePolicy[];
  discountPolicies: DiscountPolicy[];
  bids: Bid[];
  coOwnersAppointmentsApproving: Map<number, number[]>;

  constructor(Id: number, Name: string, Products: Product[], founder: Member, isOpen: boolean, purchasePolicies: PurchasePolicy[], discountPolicies: DiscountPolicy[], bids: Bid[], coOwnersAppointmentsApproving: Map<number, number[]>) {
    this.id = Id;
    this.name = Name;
    this.products = Products;
    this.isOpen = isOpen;
    this.founder = founder;
    this.purchasePolicies = purchasePolicies;
    this.discountPolicies = discountPolicies
    this.bids = bids
    this.coOwnersAppointmentsApproving = coOwnersAppointmentsApproving
  }
}
