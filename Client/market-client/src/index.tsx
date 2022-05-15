import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import Register from './Authentication/Register';
import Home from './Home'
import Login from './Authentication/Login';
import {
  BrowserRouter,
  Routes,
  Route,
} from "react-router-dom";
import {
  pathRegister,
  pathLogin,
} from "./Paths";


const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <BrowserRouter>
    <Routes>
      <Route index element={<Home />} />
      <Route path={pathRegister} element={<Register />} />
      <Route path={pathLogin} element={<Login />}>
      </Route>
    </Routes>
  </BrowserRouter>
);

