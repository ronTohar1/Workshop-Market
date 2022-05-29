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
} from "@mui/material"
import UpdateIcon from "@mui/icons-material/Update"
import Product from "../../DTOs/Product"
import { Currency } from "../../Utils"
import RemoveProductCan from "./RemoveProductCan"

function ProductActions(
  handleUpdateQuantity: (event: React.FormEvent<HTMLFormElement>) => void
) {
  return (
    <Stack direction="column">
      <Typography variant="body1">Change Quantity</Typography>
      <Box component="form" noValidate onSubmit={handleUpdateQuantity}>
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
      <Typography variant="h6">Quantity: {quantity}</Typography>
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
      <CardActions>
        {RemoveProductCan(product, handleRemoveProductClick)}
      </CardActions>
    </Card>
  )
}

