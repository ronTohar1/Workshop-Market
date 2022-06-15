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
import ThumbsUpDownIcon from '@mui/icons-material/ThumbsUpDown';
import RateReviewIcon from '@mui/icons-material/RateReview';
import ProductReview from "../CartComponents/ProductReview"
import Bid from "../../DTOs/Bid"
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
                {bid.counterOffer
                    ? (
                        <><br /><p> -- Counter Offer</p></>
                    )
                    : null}

                Store : {product.storeName}</Typography>
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
) {
    return (
        <Card sx={{ m: 1, minWidth: "20vw" }} elevation={6} component={Paper}>
            <CardContent>{ProductContent(product, bid)}</CardContent>
            <CardActions disableSpacing>
                <Button variant="contained" color="primary" onClick={() => handlePurchase(product)}> Purchase </Button>
                <Box sx={{ ml: "auto" }}>
                    {RemoveProductCan(product, handleRemoveProductClick)}
                </Box>
            </CardActions>
        </Card>
    )
}
