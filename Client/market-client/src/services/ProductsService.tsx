import Product from "../DTOs/Product";

export const dummyProducts = [
  new Product(1, "apple", 2.4, "fruits", "Rami Levi", 100),
  new Product(2, "keyboard", 199, "elecronics", "Ivory", 100),
  new Product(3, "banana", 3.5, "fruits", "Rami Levi", 100),
  new Product(4, "pineapple", 9, "fruits", "Rami Levi", 100),
  new Product(5, "xiaomi 9", 250, "cellphones", "Ivory", 100),
  new Product(6, "xiaomi 10", 300, "cellphones", "Ivory", 100),
  new Product(7, "milk", 1.5, "dairy", "Rami Levi", 100),
  new Product(8, "butter", 1.8, "dairy", "Rami Levi", 100),
  new Product(9, "cheese", 1.8, "dairy", "Rami Levi", 100),
];
// Fetch Products
// should be async
export const fetchProducts = (query: string) => {
  // const res = await fetch('http://localhost:5000/products/query=...')
  // const data = await res.json()

  return dummyProducts.filter((p) => p.name.includes(query));
};
