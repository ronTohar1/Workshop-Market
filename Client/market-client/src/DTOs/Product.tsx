export default class Product {
  id: number;
  name: string;
  price: number;
  category: string;
  storeId: number;
  storeName: string;
  availableQuantity: number;

  constructor(
    id: number,
    name: string,
    price: number,
    category: string,
    store: number,
    storeName: string,
    available_quantity: number
  ) {
    this.id = id;
    this.name = name;
    this.price = price;
    this.category = category;
    this.storeId = store;
    this.storeName = storeName;
    this.availableQuantity = available_quantity;
  }
}
