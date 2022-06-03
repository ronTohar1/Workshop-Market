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
    <Grid
      item
      sm={6}
      md={4}
      sx={{
        alignItems: "center",
      }}
    >
      {ProductCard(
        product,
        quantity,
        handleRemoveProductClick,
        handleUpdateQuantity
      )}
    </Grid>
  )
}

export default function CartPage() {
  const navigate = useNavigate()
  const [expanded, setExpanded] = React.useState(false)
  const [cartProducts, setCartProducts] = React.useState<CartProduct[]>([])
  const [openRemoveDialog, setOpenRemoveDialog] = React.useState<boolean>(false)
  const [renderProducts, setRenderProducts] = React.useState<boolean>(false)
  const [chosenProduct, updateChosenProduct] = React.useState<Product | null>(
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
        console.log("Loaded cart")
        console.log(prodsToQuantity)
      })
      .catch((e) => {
        alert(e)
        navigate(pathHome)
      })
  }, [renderProducts])

  const reloadCartProducts = () => setRenderProducts(!renderProducts)

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
  const RemoveProduct = (product: Product) => {
    fetchResponse(
      serverRemoveFromCart(getBuyerId(), product.id, product.storeId) //Trying to remove from cart in server
    ).then((removedSuccess: boolean) => {
      if (removedSuccess) {
        setOpenRemoveProdSnackbar(true)

        reloadCartProducts()
      } // Reload products again from server
    })
  }
  const handleCloseRemoveDialog = (remove: boolean, product: Product) => {
    if (remove) RemoveProduct(product)
    setOpenRemoveDialog(false)
  }

  const handleRemoveProductCanClick = (product: Product) => {
    updateChosenProduct(product)
    setOpenRemoveDialog(true)
  }

  const handlePurchase = () => alert("Purchasing.....")

  return (
    <ThemeProvider theme={theme}>
      <Navbar />
      <Stack direction="row">
        <Box sx={{ width: "80%", mt: 2 }}>
          <Grid container spacing={0}>
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
            -1,
            -1,
            cartProducts,
            expandSummary,
            handleRemoveProductCanClick,
            () => setExpandSummary(!expandSummary),
            handlePurchase
          )}
        </Box>
      </Stack>
      {chosenProduct !== null
        ? DialogTwoOptions(
            chosenProduct,
            openRemoveDialog,
            handleCloseRemoveDialog
          )
        : null}

      {SuccessSnackbar(
        "Removed " + chosenProduct?.name + " Successfully",
        openRemoveProdSnackbar,
        () => setOpenRemoveProdSnackbar(false)
      )}
    </ThemeProvider>
  )
}
