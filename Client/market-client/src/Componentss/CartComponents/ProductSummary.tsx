import Box from "@mui/material/Box"
import Typography from "@mui/material/Typography"
import { Stack } from "@mui/material"
import Product from "../../DTOs/Product"
import RemoveProductCan from "./RemoveProductCan"

export default function ProductSummary(
  product: Product,
  quantity: number,
  handleRemoveProduct: (product: Product) => void
) {
  return (
    <Box sx={{ borderRadius: 2, boxShadow: 4 }}>
      <Box sx={{ ml: 2, mb: 2 }}>
        <Typography sx={{ mb: 1.5 }} variant="h5">
          {product.name} x {quantity}
        </Typography>
        <Stack
          direction="row"
          sx={{ display: "flex", justifyContent: "space-between" }}
        >
          <Typography sx={{ mb: 1.5 }} variant="h6">
            Total : {product.price * quantity}
          </Typography>
          {RemoveProductCan(product, handleRemoveProduct)}
        </Stack>
      </Box>
    </Box>
  )
}
