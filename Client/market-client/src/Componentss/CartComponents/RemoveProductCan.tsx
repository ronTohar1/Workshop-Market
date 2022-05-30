import { Icon, IconButton } from "@mui/material"
import Product from "../../DTOs/Product"
import DeleteForeverIcon from "@mui/icons-material/DeleteForever"

export default function RemoveProductCan(
  product: Product,
  handleRemoveProduct: (product: Product) => void
) {
  return (
    <div>
      <IconButton
        sx={{ display: "flex" }}
        onClick={() => handleRemoveProduct(product)}
      >
        <Icon sx={{ display: "flex" }}>
          <DeleteForeverIcon fontSize="medium" sx={{ color: "red" }} />
        </Icon>
      </IconButton>
    </div>
  )
}
