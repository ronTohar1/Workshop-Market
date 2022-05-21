import Product from "../DTOs/Product";

// Fetch Products
// should be async
export const fetchProducts = (query: string) => {
  // const res = await fetch('http://localhost:5000/products/query=...')
  // const data = await res.json()

  return [
    new Product(1, "apple", 2.4, "fruits", "Rami Levi"),
    new Product(2, "keyboard", 199, "elecronics", "Ivory"),
    new Product(3, "banana", 3.5, "fruits", "Rami Levi"),
    new Product(4, "pineapple", 9, "fruits", "Rami Levi"),
    new Product(5, "xiaomi 9", 250, "cellphones", "Ivory"),
    new Product(6, "xiaomi 10", 300, "cellphones", "Ivory"),
    new Product(7, "milk", 1.5, "dairy", "Rami Levi"),
    new Product(8, "butter", 1.8, "dairy", "Rami Levi"),
    new Product(9, "cheese", 1.8, "dairy", "Rami Levi"),
  ].filter((p) => p.name.includes(query));
};
