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
import { initSession, storage } from "./services/SessionService"
import ProductReview from "./Componentss/CartComponents/ProductReview"
import BidsPage from "./Pages/Bids"


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

const f = () => {
  const x = storage.getItem("address")

  if (x != null) {

    const newWs = new WebSocket(x)
    addEventListener(newWs, 'message', function (event: any) {
      alertFunc("Message from server:\n" + event.data);
    })

    // addEventListener(newWs, 'open', function (event: any) {
    //   alertFunc("Opened");
    // })

    // addEventListener(newWs, 'close', function (event: any) {
    //   alertFunc("Closed");
    // })
    return newWs
  }

  return null
}
const conn: { ws: WebSocket | null } = { ws: f() }
export function alertFunc(m: string) { alert(m) }
export function initWebSocket(address: string) {
  storage.setItem("address", address)
  conn.ws = f()
}
export function addEventListener(ws: WebSocket, listenTo: string, funcToExec: any) {
  ws.addEventListener(listenTo, function (event) { funcToExec(event) })
}



const App = () => {
  // window.onunload = () => clearSession()

  initSession()
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
            <Route path={Paths.pathDiscount} element={<MainDiscount />} />
            <Route path={Paths.pathPolicy} element={<MainPolicy />} />
            <Route path={Paths.pathBids} element={<BidsPage/>} />
            {/* <Route path={Paths.pathProductReview} element={<ProductReview product={new Product(0,"apple",0,"apple",0,"apple",0)}/>} /> */}

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
