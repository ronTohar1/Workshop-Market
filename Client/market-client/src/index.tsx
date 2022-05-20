import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import Register from './Authentication/Register';
import Home from './Home'
import Login from './Authentication/Login';
import Navbar from './Navbar';
import SearchPage from './Pages/Search'
import EnhancedTableToolbar from './Stores/Store';
import Cart from './Cart';
import {
  BrowserRouter,
  Routes,
  Route,
} from "react-router-dom";
import * as Paths from "./Paths";


const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <BrowserRouter>
    <Routes>
      <Route index element={<Home />} />
      <Route path={Paths.pathRegister} element={<Register />} />
      <Route path={Paths.pathStore} element={<EnhancedTableToolbar />} />
      <Route path={Paths.pathCart} element={<Cart />} />
      <Route path={Paths.pathLogin} element={<Login />}/>
      <Route path={Paths.pathSearch} element={<SearchPage />}/>
    </Routes>
  </BrowserRouter>
);

