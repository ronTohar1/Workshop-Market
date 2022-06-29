import * as React from "react"
import Navbar from "../Componentss/Navbar"
import { ProductHeroLayout } from "../Componentss/ProductHeroLayout"
import Button from "@mui/material/Button"
import ButtonGroup from "@mui/material/ButtonGroup"
import Box from "@mui/material/Box"
import { createTheme, ThemeProvider } from "@mui/material/styles"
import { pathLogin, pathRegister } from "../Paths"
import Purchase from "../DTOs/Purchase"
import { getBuyerId } from "../services/SessionService"
import { serverGetBuyerPurchaseHistory } from "../services/AdminService"
import { fetchResponse } from "../services/GeneralService"
import FormDialog from "../Componentss/AdminComponents/BuyerPurchaseHistoryForm"
import BuyerPurchaseHistoryForm from "../Componentss/AdminComponents/BuyerPurchaseHistoryForm"
import StorePurchaseHistoryForm from "../Componentss/AdminComponents/StorePurchaseHistoryForm"
import ShowLoggedInMembers from "../Componentss/AdminComponents/ShowLoggedInMembers"
import DisplayMemberAccount from "../Componentss/AdminComponents/DisplayMemberAccount"
import RemoveAMember from "../Componentss/AdminComponents/RemoveAMember"
import { Stack } from "@mui/material"
import Discount from "../DTOs/DiscountDTOs/Discount"
import DateDiscount from "../DTOs/DiscountDTOs/DateDiscount"
import ProductDiscount from "../DTOs/DiscountDTOs/ProductDiscount"
import StoreDiscount from "../DTOs/DiscountDTOs/StoreDiscount"
import BagValue from "../DTOs/DiscountDTOs/BagValue"
import If from "../DTOs/DiscountDTOs/If"
import Store from "../DTOs/Store"

const backgroundImage =
  "https://images.unsplash.com/photo-1471193945509-9ad0617afabf"

export const discount = [
    new DateDiscount(90, 2001, 4, 18),
    new StoreDiscount(68),
    new If(new BagValue(2),new StoreDiscount(56),new ProductDiscount(0, 0)),
]
const theme = createTheme({
  palette: {
    mode: "light",
  },
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

export default function DiscountsPage(store: Store) {
   
}
