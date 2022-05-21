class Product {
  id: number;
  name: string;
  price: number;
  category: string;
  store: string;
  available_quantity: number;

  constructor(
    id: number,
    name: string,
    price: number,
    category: string,
    store: string,
    available_quantity: number

  ) {
    this.id = id;
    this.name = name;
    this.price = price;
    this.category = category;
    this.store = store;
    this.available_quantity= available_quantity;
  }
}

export default Product;
