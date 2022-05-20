import React from 'react';
import { useState, useEffect } from 'react'
import './App.css';
import Product from './DTOs/Product';

const App = () => {

  // Fetch Products
  // should be async
  const fetchProducts = (query : string) => {
    // const res = await fetch('http://localhost:5000/products/query=...')
    // const data = await res.json()

    return [
      new Product(1, "apple", 2.4, "fruits"), 
      new Product(2, "keyboard", 199, "elecronics"), 
      new Product(3, "banana", 3.5, "fruits"), 
      new Product(4, "pineapple", 9, "fruits"), 
      new Product(5, "xiaomi 9", 250, "cellphones"), 
      new Product(6, "xiaomi 10", 300, "cellphones"), 
      new Product(7, "milk", 1.5, "dairy"), 
      new Product(8, "butter", 1.8, "dairy"), 
      new Product(9, "cheese", 1.8, "dairy"), 
    ]
  }

  return (
    <div className="App">
      <header className="App-header">
      </header>
    </div>
  );
}

export default App;
