import {
  Card,
  Box,
  Button,
  CardContent,
  Paper,
  Stack,
  Typography,
  TextField,
  CardActions,
  Dialog,
} from "@mui/material"
import UpdateIcon from "@mui/icons-material/Update"
import Product from "../../DTOs/Product"
import { Currency } from "../../Utils"
import RemoveProductCan from "./RemoveProductCan"
import ThumbsUpDownIcon from '@mui/icons-material/ThumbsUpDown';
import RateReviewIcon from '@mui/icons-material/RateReview';
import ProductReview from "./ProductReview"
import CurrencyExchangeIcon from '@mui/icons-material/CurrencyExchange';
function UpdateQuantityComponent(
  product: Product,
  handleUpdateQuantity: (product: Product, newQuan: number) => void
) {
  const handleQuantity = (event: React.FormEvent<HTMLFormElement>): void => {
    event.preventDefault()
    const data = new FormData(event.currentTarget)
    handleUpdateQuantity(product, Number(data.get("quantity")))
  }

  return (
    <Stack direction="column">
      <Typography variant="body1">Change Quantity</Typography>
      <Box component="form" noValidate onSubmit={handleQuantity}>
        <Stack direction="row">
          <TextField
            id="quantity"
            name="quantity"
            type="number"
            InputLabelProps={{
              shrink: true,
            }}
            size="small"
          />
          <Button
            color="primary"
            variant="contained"
            // color="inherit"
            size="small"
            sx={{ ml: 1 }}
            startIcon={<UpdateIcon fontSize="small" />}
            type="submit"
          >
            Update
          </Button>
        </Stack>
      </Box>
    </Stack>
  )
}

function ProductContent(product: Product, quantity: number) {
  return (
    <div>
      <Typography variant="h3" component="div">
        {product.name}
      </Typography>
      <Typography sx={{ mb: 1.5 }} color="text.secondary">
        <br></br>
        Product Information
      </Typography>
      <Typography variant="h6">
        Price ({Currency}): {product.price}

      </Typography>
      <Typography variant="h6">
        Quantity: {quantity}
        <br></br>
        Store : {product.storeName}</Typography>
      <br></br>
      <ProductReview product={product} />
      <br />
      <ProductReview product={product} />
    </div>
  )
}

export default function ProductCard(
  product: Product,
  quantity: number,
  handleRemoveProductClick: (product: Product) => void,
  handleUpdateQuantity: (product: Product, newQuan: number) => void
) {
  return (
    <Card sx={{ m: 1 }} elevation={6} component={Paper}>
      <CardContent>{ProductContent(product, quantity)}</CardContent>
      <CardActions disableSpacing>
        {UpdateQuantityComponent(product, handleUpdateQuantity)}

        <Box sx={{ ml: "auto", mt: "auto" }}>
          {RemoveProductCan(product, handleRemoveProductClick)}
        </Box>
      </CardActions>
    </Card>
  )
}
