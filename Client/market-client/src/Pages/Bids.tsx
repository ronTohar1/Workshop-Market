import { ThemeProvider } from "@emotion/react"
import { Box, createTheme, Dialog, Grid, Stack } from "@mui/material"
import * as React from "react"
import { useNavigate } from "react-router-dom"
import { NumberParam, useQueryParam } from "use-query-params"
import BidCard from "../Componentss/BidsComponents/BidCard"
import DialogTwoOptionsBids from "../Componentss/BidsComponents/DialogTwoOptionsBids"
import CartSummary from "../Componentss/CartComponents/CartSummary"
import DialogTwoOptions from "../Componentss/CartComponents/DialogTwoOptions"
import ProductCard from "../Componentss/CartComponents/ProductCard"
import FailureSnackbar from "../Componentss/Forms/FailureSnackbar"
import SuccessSnackbar from "../Componentss/Forms/SuccessSnackbar"
import LargeMessage from "../Componentss/LargeMessage"
import LoadingCircle from "../Componentss/LoadingCircle"
import Navbar from "../Componentss/Navbar"
import Bid from "../DTOs/Bid"
import Cart from "../DTOs/Cart"
import Product from "../DTOs/Product"
import ShoppingBag from "../DTOs/ShoppingBag"
import { pathCheckout, pathHome, pathPurchaseBid } from "../Paths"
import {
  serverChangeProductAmountInCart,
  serverGetCart,
  serverRemoveFromCart,
} from "../services/BuyersService"
import { fetchResponse } from "../services/GeneralService"
import { fetchBids } from "../services/MemberService"
import {
  fetchProducts,
  getCartProducts,
  serverSearchProducts,
} from "../services/ProductsService"
import { getBuyerId } from "../services/SessionService"
import {
  serverApproveCounterOffer,
  serverDenyCounterOffer,
  serverRemoveBid,
} from "../services/StoreService"
import { zip } from "../Utils"
import PurchaseBidCheckout from "./PurchaseBidCheckout"

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

export interface BidProduct {
  product: Product
  bid: Bid
}

// function MakeProductCard(
//     product: Product,
//     bid: Bid,
//     handleRemoveProductClick: (product: Product, bid: Bid) => void,
// ) {
//     return (
//         // <Grid
//         //   item
//         //   sm={6}
//         //   md={4}
//         //   sx={{
//         //     alignItems: "center",
//         //   }}
//         // >
//         <Box sx={{ m: 2 }}>
//             {BidCard(
//                 product,
//                 bid,
//                 (product: Product) => handleRemoveProductClick(product, bid),
//             )}
//         </Box>
//         // </Grid>
//     )
// }

export default function BidsPage() {
  const navigate = useNavigate()
  const [bidProducts, setBidProducts] = React.useState<BidProduct[] | null>(
    null
  )
  const [openRemoveDialog, setOpenRemoveDialog] = React.useState<boolean>(false)
  const [renderBids, setRenderBids] = React.useState<boolean>(false)
  const [ProductToRemove, setProductToRemove] = React.useState<Product | null>(
    null
  )
  const [bidToRemove, setBidToRemove] = React.useState<Bid | null>(null)
  const [snackMessage, setSnackMessage] = React.useState<string>("")
  const [openRemoveProdSnackbar, setOpenRemoveProdSnackbar] =
    React.useState<boolean>(false)

  //------------------------------
  const [openFailSnack, setOpenFailSnack] = React.useState<boolean>(false)
  const [failureProductMsg, setFailureProductMsg] = React.useState<string>("")
  const [openSuccSnack, setOpenSuccSnack] = React.useState<boolean>(false)
  const [successProductMsg, setSuccessProductMsg] = React.useState<string>("")
  const showSuccessSnack = (msg: string) => {
    setSuccessProductMsg(msg)
    setOpenSuccSnack(true)
  }

  const showFailureSnack = (msg: string) => {
    setOpenFailSnack(true)
    setFailureProductMsg(msg)
  }
  //------------------------------

  // Fetching products from api once when rendered first time.
  React.useEffect(() => {
    const buyerId = getBuyerId()
    fetchBids(buyerId)
      .then((myBids: [Bid[], Product[]]) => {
        const bidProducts: BidProduct[] = zip(myBids[0], myBids[1]).map(
          (bp) => {
            return { product: bp[1], bid: bp[0] }
          }
        )
        setBidProducts(bidProducts)
        console.log("bidProducts")
        console.log(bidProducts)
      })
      .catch((e) => {
        alert(e)
        navigate(pathHome)
      })
  }, [renderBids])

  const reloadBids = () => setRenderBids(!renderBids)

  const tryRemoveBid = (product: Product, bid: Bid) => {
    fetchResponse(
      serverRemoveBid(product.storeId, getBuyerId(), bid.id) //Trying to remove from cart in server
    )
      .then((removedSuccess: boolean) => {
        if (removedSuccess) {
          reloadBids()
          showSuccessSnack(
            "Successfully removed bid on " + ProductToRemove?.name
          )
        } // Reload products again from server
        else showFailureSnack("Couldn't remove bid")
      })
      .catch((e) => {
        showFailureSnack(e)
      })
  }
  const handleCloseRemoveDialog = (
    remove: boolean,
    product: Product,
    bid: Bid
  ) => {
    if (remove) tryRemoveBid(product, bid)
    setOpenRemoveDialog(false)
  }

  const handleRemoveBidCanClick = (product: Product, bid: Bid) => {
    setProductToRemove(product)
    setBidToRemove(bid)
    setOpenRemoveDialog(true)
  }

  const handlePurchaseBid = (product: Product, bid: Bid) => {
    // navigate(pathCheckout)
    bid.description = product.name+"from "+product.storeName;
    navigate(pathPurchaseBid, { state: bid });
  }

  const handleCounterOffer = (approve: boolean, product: Product, bid: Bid) => {
    const response = approve
      ? serverApproveCounterOffer(product.storeId, getBuyerId(), bid.id)
      : serverDenyCounterOffer(product.storeId, getBuyerId(), bid.id)
    fetchResponse(response)
      .then((success: boolean) => {
        if (approve) showSuccessSnack("Approved Counter Offer")
        else showSuccessSnack("Denyed Counter Offer")
        reloadBids()
      })
      .catch(showFailureSnack)
  }

  return bidProducts === null ? (
    LoadingCircle()
  ) : (
    <ThemeProvider theme={theme}>
      <Navbar />
      <Stack direction="row">
        <Box sx={{ width: "100%", ml: 3, mt: 2 }}>
          <Grid
            container
            flex={1}
            spacing={2}
            rowSpacing={1}
            columnSpacing={{ xs: 1, sm: 2, md: 3 }}
          >
            {bidProducts.length > 0
              ? bidProducts.map((bidProduct: BidProduct) => (
                  <Box sx={{ m: 2 }}>
                    {BidCard(
                      bidProduct.product,
                      bidProduct.bid,
                      (product: Product) =>
                        handleRemoveBidCanClick(product, bidProduct.bid),
                      (product: Product) =>
                        handlePurchaseBid(product, bidProduct.bid),
                      (approve: boolean, product: Product) =>
                        handleCounterOffer(approve, product, bidProduct.bid)
                    )}
                  </Box>
                ))
              : LargeMessage("You have no bids currently")}
          </Grid>
        </Box>
      </Stack>

      {ProductToRemove !== null && bidToRemove !== null
        ? DialogTwoOptionsBids(
            ProductToRemove,
            bidToRemove,
            openRemoveDialog,
            handleCloseRemoveDialog
          )
        : null}

      {SuccessSnackbar(successProductMsg, openSuccSnack, () =>
        setOpenSuccSnack(false)
      )}
      <Dialog open={openFailSnack}>
        {FailureSnackbar(failureProductMsg, openFailSnack, () =>
          setOpenFailSnack(false)
        )}
      </Dialog>
    </ThemeProvider>
  )
}
