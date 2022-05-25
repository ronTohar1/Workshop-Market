
class Product {
  id: number;
  name: string;
  price: number;
  category: string;
  store: number;
  availableQuantity: number;

  constructor(
    id: number = 0,
    name: string = "",
    price: number = 0,
    category: string = "",
    store: number = 0,
    available_quantity: number = 0
  ) {
    this.id = id;
    this.name = name;
    this.price = price;
    this.category = category;
    this.store = store;
    this.availableQuantity = available_quantity;
  }
}

export default Product;
