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

export default function CartSummary() {
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
        elevation={2}
        sx={{
          border: 0,
          borderRadius: 3,
          m: 1.5,
          width: "auto",
          height: "auto",
        }}
      >
        <Box sx={{ borderBottom: 1 }}>
          <Typography sx={{ mb: 1.5, ml: 1 }} variant="h5">
            <br></br>
            Total
          </Typography>
          <Typography sx={{ ml: "25%" }} variant="h6">
            300 {Currency}
          </Typography>
        </Box>
        <Box sx={{ borderBottom: 0, mb: 3 }}>
          <Typography variant="h5" sx={{ mb: 1.5, mt: 1.5, ml: 1 }}>
            Total After Discount
          </Typography>
          <Typography sx={{ ml: "25%" }} variant="h6">
            300 {Currency}
          </Typography>
        </Box>
      </Card>

      <Stack
        direction="row"
        sx={{ display: "flex", justifyContent: "space-between" }}
      >
        <div>
          <Button
            sx={{ m: 1, ml: 2 }}
            onClick={() => handlePurchase(prods)}
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
          {prods.map((prod) => {
            return makeSingleProductDetails(prod, handleRemoveProduct)
          })}
        </CardContent>
      </Collapse>
    </Card>
  )
}
