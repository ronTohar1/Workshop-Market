import {
  Card,
  Box,
  Button,
  CardContent,
  Stack,
  Typography,
  IconButton,
  styled,
  Collapse,
} from "@mui/material"
import { Currency } from "../../Utils"
import { CartProduct } from "../../Pages/Cart"
import { IconButtonProps } from "@mui/material/IconButton"
import ExpandMoreIcon from "@mui/icons-material/ExpandMore"
import ProductSummary from "./ProductSummary"
import Product from "../../DTOs/Product"

interface ExpandMoreProps extends IconButtonProps {
  expand: boolean
}

const ExpandMore = styled((props: ExpandMoreProps) => {
  const { expand, ...other } = props
  return <IconButton {...other} />
})(({ theme, expand }) => ({
  transform: !expand ? "rotate(0deg)" : "rotate(180deg)",
  marginLeft: "auto",
  transition: theme.transitions.create("transform", {
    duration: theme.transitions.duration.shortest,
  }),
}))

export default function CartSummary(
  totalBeforeDiscount: number,
  totalAfterDiscount: number,
  products: CartProduct[],
  expanded: boolean,
  handleRemoveProduct: (product: Product) => void,
  handleExpandClick: () => void,
  handlePurchase: () => void
) {
  return (
    <Card
      elevation={10}
      sx={{
        border: 0,
        borderRadius: 3,
        mr: 2,
        width: "auto",
        height: "auto",
      }}
    >
      <Typography sx={{ ml: 1 }} variant="h3" component="div">
        Your Cart
      </Typography>
      <Card
        elevation={4}
        sx={{
          border: 0,
          borderRadius: 3,
          m: 1.5,
          width: "auto",
          height: "auto",
        }}
      >
        <Box sx={{ borderBottom: 0 }}>
          <Typography sx={{ mb: 1.5, ml: 1 }} variant="h5">
            <br></br>
            Total: {totalBeforeDiscount} {Currency}
          </Typography>
          {/* <Typography sx={{ ml: "25%" }} variant="h6">
          {totalBeforeDiscount} {Currency}
          </Typography> */}
        </Box>
        {/* <Box sx={{ borderBottom: 0, mb: 3 }}>
          <Typography variant="h5" sx={{ mb: 1.5, mt: 1.5, ml: 1 }}>
            Total After Discount
          </Typography>
          <Typography sx={{ ml: "25%" }} variant="h6">
            {totalAfterDiscount} {Currency}
          </Typography>
        </Box> */}
      </Card>

      <Stack
        direction="row"
        sx={{ display: "flex", justifyContent: "space-between" }}
      >
        <div>
          <Button
            sx={{ m: 1, ml: 2 }}
            onClick={handlePurchase}
            variant="contained"
            color="success"
          >
            Purchase
          </Button>
        </div>
        <div>
          <ExpandMore
            sx={{ m: 1, ml: 2 }}
            expand={expanded}
            onClick={handleExpandClick}
            aria-expanded={expanded}
            aria-label="show more"
          >
            <ExpandMoreIcon />
          </ExpandMore>
        </div>
      </Stack>

      <Collapse in={expanded} timeout="auto" unmountOnExit>
        <CardContent>
          <Typography paragraph>Cart Details:</Typography>
          {products.map((cartProduct: CartProduct) => {
            return ProductSummary(cartProduct.product, cartProduct.quantity, handleRemoveProduct)
          })}
        </CardContent>
      </Collapse>
    </Card>
  )
}
