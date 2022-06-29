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
import RemoveProductCan from "../CartComponents/RemoveProductCan"
import ThumbsUpDownIcon from "@mui/icons-material/ThumbsUpDown"
import RateReviewIcon from "@mui/icons-material/RateReview"
import ProductReview from "../CartComponents/ProductReview"
import Bid from "../../DTOs/Bid"
import DoneIcon from "@mui/icons-material/Done"
import ClearIcon from "@mui/icons-material/Clear"

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

function ProductContent(product: Product, bid: Bid) {
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
        Bid Price: {bid.bid}
        <br></br>
        Store : {product.storeName}
      </Typography>
      <br></br>
      <ProductReview product={product} />
    </div>
  )
}

export default function BidCard(
  product: Product,
  bid: Bid,
  handleRemoveProductClick: (product: Product) => void,
  handlePurchase: (product: Product) => void,
  handleCounterOffer: (accept: boolean, product: Product) => void
) {
  function CounterOfferButtons() {
    return bid.counterOffer ? (
      <Box sx={{ border: 1, padding: 1, m: 1 }}>
        <Typography variant="h5">Counter Offer: {bid.offer}</Typography>

        <Stack direction="row">
          <Button
            sx={{ m: 1 }}
            variant="contained"
            color="success"
            endIcon={<DoneIcon />}
            onClick={() => handleCounterOffer(true, product)}
          >
            Accept
          </Button>
          <Button
            sx={{ m: 1 }}
            variant="contained"
            color="error"
            endIcon={<ClearIcon />}
            onClick={() => handleCounterOffer(false, product)}
          >
            Deny
          </Button>
        </Stack>
      </Box>
    ) : null
  }
  return (
    <Card sx={{ m: 1 }} elevation={6} component={Paper}>
      <CardContent>{ProductContent(product, bid)}</CardContent>
      <CardActions>
        <div>{CounterOfferButtons()}</div>
      </CardActions>
      <CardActions>
        <Button
          variant="contained"
          color="primary"
          onClick={() => handlePurchase(product)}
        >
          Purchase
        </Button>
        <Box sx={{ ml: "auto" }}>
          {RemoveProductCan(product, handleRemoveProductClick)}
        </Box>
      </CardActions>
    </Card>
  )
}
