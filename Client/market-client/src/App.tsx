import React from "react"

import "./index.css"
import Register from "./Pages/Register"
import Home from "./Pages/Home"
import Login from "./Pages/Login"
import Navbar from "./Componentss/Navbar"
import SearchPage from "./Pages/Search"
import StorePage from "./Pages/StorePage"
import CartPage from "./Pages/Cart"
import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import * as Paths from "./Paths"
import { QueryParamProvider } from "use-query-params"
import StoreManagerPage from "./Pages/StoreManager"
import { clearSession, initSession } from "./services/SessionService"
import StorePageOfManager from "./Pages/StorePageOfManager"

initSession()

const App = () => {
  // window.onunload = () => clearSession()

  return (
    <Router>
      <QueryParamProvider>
        <Routes>
          <Route index element={<Home />} />
          <Route path={Paths.pathRegister} element={<Register />} />
          <Route path={Paths.pathStore} element={<StorePage />} />
          <Route path={Paths.pathCart} element={<CartPage />} />
          <Route path={Paths.pathLogin} element={<Login />} />
          <Route path={Paths.pathSearch} element={<SearchPage />} />
          <Route
            path={Paths.pathStorePageOfManager}
            element={<StorePageOfManager />}
          />
          <Route path={Paths.pathStoreManager} element={<StoreManagerPage />} />
        </Routes>
      </QueryParamProvider>
    </Router>
  )
}

export default App
