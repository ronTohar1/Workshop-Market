import React, { useEffect } from "react"

import "./index.css"
import Register from "./Pages/Register"
import Home from "./Pages/Home"
import Login from "./Pages/Login"
import SearchPage from "./Pages/Search"
import StorePage from "./Pages/StorePage"
import CartPage from "./Pages/Cart"
import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import * as Paths from "./Paths"
import { QueryParamProvider } from "use-query-params"
import StoreManagerPage from "./Pages/StoreManager"
import StorePageOfManager from "./Componentss/ManagerEditStore/StorePageOfManager"
import Checkout from "./Pages/Checkout"
import Admin from "./Pages/Admin"
import Product from "./DTOs/Product"
import MainDiscount from "./Componentss/DiscountComponent.tsx/MainDiscount"
import Store from "./DTOs/Store"
import Member from "./DTOs/Member"
import { createTheme, ThemeProvider } from "@mui/material"
import MainPolicy from "./Componentss/PurchasePolicy/MainPolicy"

const App = () => {
  // window.onunload = () => clearSession()
  useEffect(() => {
    const handleTabClose = (event: any) => {
      event.preventDefault()

      console.log("beforeunload event triggered")
    }

    window.addEventListener("beforeunload", handleTabClose)

    return () => {
      window.removeEventListener("beforeunload", handleTabClose)
    }
  }, [])
  const products = new Map([
    [new Product(0, "Milk", 12.9, "Dairy", 1, "Kaldo", 10), 1],
    [new Product(1, "Bread", 5, "Bakery", 1, "Kaldo", 10), 2],
    [new Product(0,"cheese", 12.3, "dairy", 0,"David's",5), 3],
    [new Product(3, "Apple", 4, "Fruits", 1, "Shufersal", 10), 1],
    [new Product(4, "Cheese", 13.9, "Dairy", 1, "Shufersal", 10), 5],
    [new Product(5, "Tommato", 1.9, "Vegtables", 1, "Shufersal", 10), 1],
  ])

  const theme = createTheme({
    typography: {
      fontFamily: [
        "-apple-system",
        "BlinkMacSystemFont",
        '"Segoe UI"',
        "Roboto",
        '"Helvetica Neue"',
        "Arial",
        "sans-serif",
        '"Apple Color Emoji"',
        '"Segoe UI Emoji"',
        '"Segoe UI Symbol"',
      ].join(","),
    },
  })
  return (
    <ThemeProvider theme={theme}>
      <Router>
        <QueryParamProvider>
          <Routes>
            <Route index element={<Home />} />
            <Route path={Paths.pathRegister} element={<Register />} />
            <Route path={Paths.pathStore} element={<StorePage />} />
            <Route path={Paths.pathCart} element={<CartPage />} />
            <Route path={Paths.pathLogin} element={<Login />} />
            <Route path={Paths.pathSearch} element={<SearchPage />} />
            <Route path={Paths.pathDiscount} element={<MainDiscount  />} />
            <Route path={Paths.pathPolicy} element={<MainPolicy/>} />

            {/* <Route
            path={Paths.pathStorePageOfManager}
            element={<StorePageOfManager />}
          /> */}
            <Route
              path={Paths.pathStoreManager}
              element={<StoreManagerPage />}
            />
            <Route path={Paths.pathAdmin} element={<Admin />} />
            <Route
              path={Paths.pathCheckout}
              element={<Checkout />}
            />
          </Routes>
        </QueryParamProvider>
      </Router>
    </ThemeProvider>
  )
}

export default App
