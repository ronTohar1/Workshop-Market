import React, { useEffect } from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import Register from "./Pages/Register";
import Home from "./Pages/Home";
import Login from "./Pages/Login";
import Navbar from "./Componentss/Navbar";
import SearchPage from "./Pages/Search";
import StorePage from "./Pages/StorePage";
import CartPage from "./Pages/Cart1";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import * as Paths from "./Paths";
import { QueryParamProvider } from "use-query-params";
import StoreManagerPage from "./Pages/StoreManager";
import { initSession } from "./services/SessionService";
import StorePageOfManager from "./Pages/StorePageOfManager"
import Admin from "./Pages/Admin"
import ShowLoggedInMembers from "./Componentss/AdminComponents/ShowLoggedInMembers";
import Checkout from "./Pages/Checkout";
import Product from "./DTOs/Product";

initSession();

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
const products = new Map(
  [
    [new Product(0,'Milk',12.9,'Dairy',1,'Kaldo',10), 1],
    [new Product(1,'Bread',5,'Bakery',1,'Kaldo',10), 2],
    [new Product(2,'Wine',50.5,'Alchohol',1,'Kaldo',10), 3],
    [new Product(3,'Apple',4,'Fruits',1,'Shufersal',10), 1],
    [new Product(4,'Cheese',13.9,'Dairy',1,'Shufersal',10), 5],
    [new Product(5,'Tommato',1.9,'Vegtables',1,'Shufersal',10), 1]
  ]
)
//   {
//     name: 'Product 1',
//     desc: 'A nice thing',
//     price: '$9.99',
//   },
//   {
//     name: 'Product 2',
//     desc: 'Another thing',
//     price: '$3.45',
//   },
//   {
//     name: 'Product 3',
//     desc: 'Something else',
//     price: '$6.51',
//   },
//   {
//     name: 'Product 4',
//     desc: 'Best thing of all',
//     price: '$14.11',
//   },
// ];
root.render(
  <Router>
    <QueryParamProvider>
      <Routes>
        <Route index element={<Home />} />
        <Route path={Paths.pathRegister} element={<Register />} />
        <Route path={Paths.pathStore} element={<StorePage />} />
        <Route path={Paths.pathCart} element={<CartPage />} />
        <Route path={Paths.pathLogin} element={<Login />} />
        <Route path={Paths.pathSearch} element={<SearchPage />} />
        <Route path={Paths.pathStorePageOfManager} element={<StorePageOfManager />} />
        <Route path={Paths.pathStoreManager} element={<StoreManagerPage />} />
        <Route path={Paths.pathAdmin} element={<Admin/>} />
        <Route path={Paths.pathCheckout} element={<Checkout productsAmount={products}/>} />
      </Routes>
    </QueryParamProvider>
  </Router>
);
