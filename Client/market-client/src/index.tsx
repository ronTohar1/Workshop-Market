// import React, { useEffect } from "react"
// // import ReactDOM from "react-dom/client"
// import ReactDOM from "react-dom"
// import "./index.css"
// import App from "./App"
// import Register from "./Pages/Register"
// import Home from "./Pages/Home"
// import Login from "./Pages/Login"
// import Navbar from "./Componentss/Navbar"
// import SearchPage from "./Pages/Search"
// import StorePage from "./Pages/StorePage"
// import CartPage from "./Pages/Cart"
// import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
// import * as Paths from "./Paths"
// import { QueryParamProvider } from "use-query-params"
// import StoreManagerPage from "./Pages/StoreManager"
// import { initSession } from "./services/SessionService"
// import StorePageOfManager from "./Pages/StorePageOfManager"
// import { LocalSeeOutlined } from "@mui/icons-material"

import React, { useEffect } from "react"
import { createRoot } from "react-dom/client"
import "./index.css"
import App from "./App"
import { getBuyerId, initSession } from "./services/SessionService"

// initSession()
// window.onunload = () => localStorage.clear()
initSession()



const container = document.getElementById("root")
//@ts-ignore
const root = createRoot(container) // createRoot(container!) if you use TypeScript
root.render(<App />)

// const root = ReactDOM.createRoot(document.getElementById("root") as HTMLElement)
// root.render(
//   <Router>
//     <QueryParamProvider>
//       <Routes>
//         <Route index element={<Home />} />
//         <Route path={Paths.pathRegister} element={<Register />} />
//         <Route path={Paths.pathStore} element={<StorePage />} />
//         <Route path={Paths.pathCart} element={<CartPage />} />
//         <Route path={Paths.pathLogin} element={<Login />} />
//         <Route path={Paths.pathSearch} element={<SearchPage />} />
//         <Route
//           path={Paths.pathStorePageOfManager}
//           element={<StorePageOfManager />}
//         />
//         <Route path={Paths.pathStoreManager} element={<StoreManagerPage />} />
//       </Routes>
//     </QueryParamProvider>
//   </Router>
// )
