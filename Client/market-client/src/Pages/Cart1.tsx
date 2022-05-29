import { ThemeProvider } from "@emotion/react"
import { Box, createTheme, Grid } from "@mui/material"
import * as React from "react"
import DialogTwoOptions from "../Componentss/CartComponents/DialogTwoOptions"
import ProductCard from "../Componentss/CartComponents/ProductCard"
import LargeMessage from "../Componentss/LargeMessage"
import Navbar from "../Componentss/Navbar"
import Product from "../DTOs/Product"

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
const dummyProd = new Product(
  0,
  "prodo",
  123,
  "electronics",
  0,
  "Ronto's",
  1004
)

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
        100,
        handleRemoveProductClick,
        handleUpdateQuantity
      )}
    </Grid>
  )
}

export default function Cart() {
  const [expanded, setExpanded] = React.useState(false)
  const [prods, updateProducts] = React.useState<Product[]>([dummyProd])
  const [openRemoveDialog, setOpenRemoveDialog] = React.useState(false)
  const [chosenProduct, updateChosenProduct] = React.useState<Product | null>(
    dummyProd
  )

  const handleUpdateQuantity = (product: Product, newQuan: number) => {
    // const validQuantity: boolean = newQuan > 0
    // const newProds = validQuantity
    //   ? prods.map((prod: Product) => {
    //       if (prod.Id == product.Id) prod.Chosen_Quantity = newQuan
    //       return prod
    //     })
    //   : prods
    // updateProducts(newProds)
  }

  const handleCloseRemoveDialog = (remove: boolean, product: Product) => {
    setOpenRemoveDialog(false)
    // const newProds = remove
    //   ? prods.filter((prod: Product) => prod.Id != product.Id)
    //   : prods
    // if (remove) updateProducts(newProds)
    // setOpenRemoveDialog(false)
  }

  const handleRemoveProductClick = (product: Product) => {
    // updateChosen(product)
    setOpenRemoveDialog(true)
    // updateProducts(prods => prods.filter((prod) => prod.Id != product.Id));
  }

  return (
    <ThemeProvider theme={theme}>
      <Box>
        <Navbar />
      </Box>
      <Box sx={{ width: "80%", mt: 2 }}>
        <Grid container spacing={0}>
          {prods.length > 0
            ? prods.map((product: Product) =>
                MakeProductCard(
                  product,
                  100,
                  handleRemoveProductClick,
                  handleUpdateQuantity
                )
              )
            : LargeMessage("No Products In Cart....")}
        </Grid>
      </Box>
      {chosenProduct !== null
        ? DialogTwoOptions(
            chosenProduct,
            openRemoveDialog,
            handleCloseRemoveDialog
          )
        : null}
    </ThemeProvider>
  )
}
