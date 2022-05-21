import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import Register from "./Pages/Register";
import Home from "./Pages/Home";
import Login from "./Pages/Login";
import Navbar from "./components/Navbar";
import SearchPage from "./Pages/Search";
import StorePage from "./Pages/StorePage";
import Cart from "./Pages/Cart";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import * as Paths from "./Paths";
import { QueryParamProvider } from "use-query-params";
import StoreManagerPage from "./Pages/StoreManager";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <Router>
    <QueryParamProvider>
      <Routes>
        <Route index element={<Home />} />
        <Route path={Paths.pathRegister} element={<Register />} />
        <Route path={Paths.pathStore} element={<StorePage />} />
        <Route path={Paths.pathCart} element={<Cart />} />
        <Route path={Paths.pathLogin} element={<Login />} />
        <Route path={Paths.pathSearch} element={<SearchPage />} />
        <Route path={Paths.pathStoreManager} element={<StoreManagerPage />} />
      </Routes>
    </QueryParamProvider>
  </Router>
);
