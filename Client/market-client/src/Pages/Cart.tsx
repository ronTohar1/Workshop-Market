import { ThemeProvider } from "@emotion/react"
import { Box, createTheme, Grid, Stack } from "@mui/material"
import * as React from "react"
import { useNavigate } from "react-router-dom"
import { NumberParam, useQueryParam } from "use-query-params"
import CartSummary from "../Componentss/CartComponents/CartSummary"
import DialogTwoOptions from "../Componentss/CartComponents/DialogTwoOptions"
import ProductCard from "../Componentss/CartComponents/ProductCard"
import SuccessSnackbar from "../Componentss/Forms/SuccessSnackbar"
import LargeMessage from "../Componentss/LargeMessage"
import Navbar from "../Componentss/Navbar"
import Cart from "../DTOs/Cart"
import Product from "../DTOs/Product"
import ShoppingBag from "../DTOs/ShoppingBag"
import { pathHome } from "../Paths"
import {
  serverChangeProductAmount,
  serverGetCart,
  serverRemoveFromCart,
} from "../services/BuyersService"
import { fetchResponse } from "../services/GeneralService"
import {
  fetchProducts,
  getCartProducts,
  serverSearchProducts,
} from "../services/ProductsService"
import { getBuyerId } from "../services/SessionService"

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

export interface CartProduct {
  product: Product
  quantity: number
}

const getProductQuantity = (
  prodId: number,
  quantities: Map<number, number>
): number => {
  const quantity = quantities.get(prodId)
  if (quantity === undefined) {
    alert("Sorry, but an unusual error happened when tried to load your cart")
    return -1 //Not going to happen
  }
  return quantity
}

function convertToCartProduct(
  products: Product[],
  quantities: Map<number, number>
): CartProduct[] {
  return products.map((product: Product) => {
    return {
      product: product,
      quantity: getProductQuantity(product.id, quantities),
    }
  })
}

function MakeProductCard(
  product: Product,
  quantity: number,
  handleRemoveProductClick: (product: Product) => void,
  handleUpdateQuantity: (product: Product, newQuan: number) => void
) {
  return (
    // <Grid
    //   item
    //   sm={6}
    //   md={4}
    //   sx={{
    //     alignItems: "center",
    //   }}
    // >
    <Box sx={{m:2}}>
      {ProductCard(
        product,
        quantity,
        handleRemoveProductClick,
        handleUpdateQuantity
      )}
    </Box>
    // </Grid>
  )
}

export default function CartPage() {
  const navigate = useNavigate()
  const [cartProducts, setCartProducts] = React.useState<CartProduct[]>([])
  const [openRemoveDialog, setOpenRemoveDialog] = React.useState<boolean>(false)
  const [renderProducts, setRenderProducts] = React.useState<boolean>(false)
  const [ProductToRemove, setProductToRemove] = React.useState<Product | null>(
    null
  )
  const [openRemoveProdSnackbar, setOpenRemoveProdSnackbar] =
    React.useState<boolean>(false)
  const [expandSummary, setExpandSummary] = React.useState<boolean>(false)

  // Fetching products from api once when rendered first time.
  React.useEffect(() => {
    const buyerId = getBuyerId()
    fetchResponse(serverGetCart(buyerId))
      .then((cart: Cart) => {
        const [prodsIds, prodsToQuantity] = getCartProducts(cart)
        fetchProducts(
          serverSearchProducts(null, null, null, null, null, prodsIds)
        ).then((products: Product[]) =>
          setCartProducts(convertToCartProduct(products, prodsToQuantity))
        )
      })
      .catch((e) => {
        alert(e)
        navigate(pathHome)
      })
  }, [renderProducts])

  const reloadCartProducts = () => setRenderProducts(!renderProducts)

  const calulateTotal = (): number => {
    return cartProducts.reduce(
      (total: number, cartProduct: CartProduct) =>
        total + cartProduct.product.price,
      0
    )
  }

  const handleUpdateQuantity = (product: Product, newQuan: number) => {
    fetchResponse(
      serverChangeProductAmount(
        getBuyerId(),
        product.id,
        product.storeId,
        newQuan
      )
    )
      .then((success: boolean) => {
        if (success) reloadCartProducts()
      })
      .catch((e) => alert(e))
  }
  const tryRemoveProduct = (product: Product) => {
    fetchResponse(
      serverRemoveFromCart(getBuyerId(), product.id, product.storeId) //Trying to remove from cart in server
    )
      .then((removedSuccess: boolean) => {
        if (removedSuccess) {
          setOpenRemoveProdSnackbar(true)
          reloadCartProducts()
        } // Reload products again from server
      })
      .catch((e) => {
        alert(e)
      })
  }
  const handleCloseRemoveDialog = (remove: boolean, product: Product) => {
    if (remove) tryRemoveProduct(product)
    setOpenRemoveDialog(false)
  }

  const handleRemoveProductCanClick = (product: Product) => {
    setProductToRemove(product)
    setOpenRemoveDialog(true)
  }

  const handlePurchase = () => alert("Purchasing.....")

  return (
    <ThemeProvider theme={theme}>
      <Navbar />
      <Stack direction="row">
        <Box sx={{ width: "80%", ml: 3, mt: 2 }}>
          <Grid
            container
            flex={1}
            spacing={2}
            rowSpacing={1}
            columnSpacing={{ xs: 1, sm: 2, md: 3 }}
          >
            {cartProducts.length > 0
              ? cartProducts.map((cartProduct: CartProduct) =>
                  MakeProductCard(
                    cartProduct.product,
                    cartProduct.quantity,
                    handleRemoveProductCanClick,
                    handleUpdateQuantity
                  )
                )
              : LargeMessage("No Products In Cart....")}
          </Grid>
        </Box>
        <Box sx={{ width: "20%", mt: 2 }}>
          {CartSummary(
            calulateTotal(),
            -1,
            cartProducts,
            expandSummary,
            handleRemoveProductCanClick,
            () => setExpandSummary(!expandSummary),
            handlePurchase
          )}
        </Box>
      </Stack>

      {ProductToRemove !== null
        ? DialogTwoOptions(
            ProductToRemove,
            openRemoveDialog,
            handleCloseRemoveDialog
          )
        : null}

      {SuccessSnackbar(
        "Removed " + ProductToRemove?.name + " Successfully",
        openRemoveProdSnackbar,
        () => setOpenRemoveProdSnackbar(false)
      )}
    </ThemeProvider>
  )
}
