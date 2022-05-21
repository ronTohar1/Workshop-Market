class Product {
  id: number;
  name: string;
  price: number;
  category: string;
  store: string;

  constructor(
    id: number,
    name: string,
    price: number,
    category: string,
    store: string
  ) {
    this.id = id;
    this.name = name;
    this.price = price;
    this.category = category;
    this.store = store;
  }
}

export default Product;
