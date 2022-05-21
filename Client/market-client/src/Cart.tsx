import * as React from "react";
import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";
import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import { createTheme, ThemeProvider, styled } from "@mui/material/styles";
import Navbar from "./Navbar";
import { AppBar } from "@mui/material";
import { Currency } from "./Utils";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import { IconButton } from "@mui/material";
import { Icon } from "@mui/material";
import { TextField } from "@mui/material";
import { Stack } from "@mui/material";
import UpdateIcon from "@mui/icons-material/Update";
import Collapse from "@mui/material/Collapse";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import { IconButtonProps } from "@mui/material/IconButton";

import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import Slide from "@mui/material/Slide";
import { TransitionProps } from "@mui/material/transitions";
import { fontFamily } from "@mui/system";

export interface Product {
  Id: number;
  Name: string;
  Price: number;
  Chosen_Quantity: number;
  Show_Description: boolean;
}

export const createProduct = (
  Id: number,
  Name: string,
  Price: number,
  Chosen_Quantity: number
): Product => {
  return {
    Id: Id,
    Name: Name,
    Price: Price,
    Chosen_Quantity: Chosen_Quantity,
    Show_Description: false,
  };
};

const bull = (
  <Box
    component="span"
    sx={{ display: "inline-block", mx: "2px", transform: "scale(0.8)" }}
  >
    •
  </Box>
);

interface ExpandMoreProps extends IconButtonProps {
  expand: boolean;
}

const ExpandMore = styled((props: ExpandMoreProps) => {
  const { expand, ...other } = props;
  return <IconButton {...other} />;
})(({ theme, expand }) => ({
  transform: !expand ? "rotate(0deg)" : "rotate(180deg)",
  marginLeft: "auto",
  transition: theme.transitions.create("transform", {
    duration: theme.transitions.duration.shortest,
  }),
}));

function BasicCard(
  product: Product,
  handleRemoveProduct: (product: Product) => void,
  handleShowDescription: (product: Product) => void,
  handleUpdateQuantity: (product: Product, newQuan: number) => void
) {
  // const [value, setValue] = React.useState("");
  const handleQuantity = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    handleUpdateQuantity(product, Number(data.get("quantity")));
  };
  return (
    <Card sx={{ ml: 2, mr: 2 }} elevation={6} component={Paper}>
      <CardContent>
        <Typography variant="h3" component="div">
          {product.Name}
        </Typography>
        <Typography sx={{ mb: 1.5 }} color="text.secondary">
          <br></br>
          Product Information
        </Typography>
        <Typography variant="h6">
          Price ({Currency}): {product.Price}
        </Typography>
        <Typography variant="h6">
          Quantity: {product.Chosen_Quantity}
        </Typography>
      </CardContent>

      <Stack direction="row" justifyContent="space-between">
        <Box
          sx={{ display: "flex", mr: 3, mb: 3, ml: 1, justfiyContent: "right" }}
        >
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
                  // value={value}
                  // onChange={(e) => {
                  //     setValue(e.target.value);
                  // }}
                />
                <Button
                  color="primary"
                  variant="contained"
                  // color="inherit"
                  size="small"
                  sx={{ ml: 1 }}
                  startIcon={<UpdateIcon fontSize="small" />}
                  type="submit"
                  // onClick={() => handleUpdateQuantity(product, Number(value))}
                >
                  Update
                </Button>
              </Stack>
            </Box>
          </Stack>
        </Box>
        <CardActions>
          {/* <IconButton onClick={() => handleRemoveProduct(product)}>
                        <Icon>
                            <DeleteForeverIcon fontSize="medium" sx={{ color: 'black' }} />
                        </Icon>
                    </IconButton> */}
          {AlertDialogSlide(product, handleRemoveProduct)}
          <ExpandMore
            expand={product.Show_Description}
            onClick={() => handleShowDescription(product)}
            aria-expanded={product.Show_Description}
            aria-label="show more"
          >
            <ExpandMoreIcon />
          </ExpandMore>
        </CardActions>
      </Stack>
      <Collapse in={product.Show_Description} timeout="auto" unmountOnExit>
        <CardContent>
          <Typography paragraph>Product Description:</Typography>
          <Typography paragraph>
            Thats an amazing product, I really wish I could afford it it's so
            expansive Please let me have one!!!
          </Typography>
        </CardContent>
      </Collapse>
    </Card>
  );
}

let allProducts: Product[] = [
  createProduct(1, "Cupcake", 305, 3),
  createProduct(2, "Hamburger", 30, 33),
  createProduct(3, "Salad", 340, 3000),
  createProduct(4, "Cheese", 130, 232),
  createProduct(5, "Banana", 35, 22),
  createProduct(6, "Cooler", 3051, 22),
  createProduct(7, "Sunglasses", 3035, 223),
  createProduct(8, "Elephant", 10, 32),
  createProduct(9, "Zebra", 3, 21),
  createProduct(10, "Hot Dog", 100, 222),
];

const Item = styled(Paper)(({ theme }) => ({
  ...theme.typography.body2,
  textAlign: "center",
  color: theme.palette.text.secondary,
  height: 60,
  lineHeight: "60px",
}));

const darkTheme = createTheme({ palette: { mode: "dark" } });
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
});
const makeSingleProduct = (
  product: Product,
  handleRemoveProduct: (product: Product) => void,
  handleShowDescription: (product: Product) => void,
  handleUpdateQuantity: (product: Product, newQuan: number) => void
) => {
  return (
    <Grid
      item
      xs={6}
      sm={4}
      sx={{
        my: 2,
        alignItems: "center",
      }}
    >
      {BasicCard(
        product,
        handleRemoveProduct,
        handleShowDescription,
        handleUpdateQuantity
      )}
    </Grid>
  );
};

const makeSingleProductDetails = (
  product: Product,
  handleRemoveProduct: (product: Product) => void
) => {
  return (
    <Box sx={{ borderRadius: 2, boxShadow: 2 }}>
      <Box sx={{ ml: 2, mb: 2 }}>
        <Typography sx={{ mb: 1.5 }} variant="h5">
          {product.Name} x {product.Chosen_Quantity}
        </Typography>
        <Stack
          direction="row"
          sx={{ display: "flex", justifyContent: "space-between" }}
        >
          <Typography sx={{ mb: 1.5 }} variant="h6">
            Total : {product.Price * product.Chosen_Quantity}
          </Typography>
          {AlertDialogSlide(product, handleRemoveProduct)}
        </Stack>
      </Box>
    </Box>
  );
};

const width = 80;
const widthCart: string = width + "%";
const widthDash: string = 100 - width + "%";
const x: number[] = [];

const createDialog = (
  product: Product,
  open: boolean,
  handleClose: (remove: boolean, product: Product) => void
) => {
  return (
    <div>
      <Dialog
        open={open}
        TransitionComponent={Transition}
        keepMounted
        // onClose={() => handleClose(false,product)}
        aria-describedby="alert-dialog-slide-description"
      >
        <DialogTitle>{product.Name + ": Confirm Remove"}</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-slide-description">
            Are you sure you want to remove this item from your cart?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => handleClose(false, product)}>Cancel</Button>
          <Button onClick={() => handleClose(true, product)}>Confirm</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
};

const MakeProducts = (products1: Product[]) => {
  const [expanded, setExpanded] = React.useState(false);
  const [prods, updateProducts] = React.useState(products1);
  const [open, setOpen] = React.useState(false);
  const [chosenProd, updateChosen] = React.useState(
    createProduct(-1, "", 1, 1)
  );

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleUpdateQuantity = (product: Product, newQuan: number) => {
    const validQuantity: boolean = newQuan > 0;
    const newProds = validQuantity
      ? prods.map((prod: Product) => {
          if (prod.Id == product.Id) prod.Chosen_Quantity = newQuan;
          return prod;
        })
      : prods;

    updateProducts(newProds);
  };

  const handleClose = (remove: boolean, product: Product) => {
    const newProds = remove
      ? prods.filter((prod: Product) => prod.Id != product.Id)
      : prods;
    updateProducts(newProds);
    setOpen(false);
  };

  const handleRemoveProduct = (product: Product) => {
    updateChosen(product);
    handleClickOpen();

    // updateProducts(prods => prods.filter((prod) => prod.Id != product.Id));
  };

  const handleShowDescription = (product: Product) => {
    updateProducts((prods) =>
      prods.map((prod) => {
        if (product.Id == prod.Id)
          prod.Show_Description = !prod.Show_Description;
        return prod;
      })
    );
  };

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  const handlePurchase = (products: Product[]) => {
    alert("buynig");
  };

  return (
    <Stack direction="row">
      <Box sx={{ width: widthCart }}>
        <Grid container spacing={0}>
          {prods.map((prod) => {
            return makeSingleProduct(
              prod,
              handleRemoveProduct,
              handleShowDescription,
              handleUpdateQuantity
            );
          })}
        </Grid>
      </Box>
      <Box sx={{ width: widthDash }}>
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
                return makeSingleProductDetails(prod, handleRemoveProduct);
              })}
            </CardContent>
          </Collapse>
        </Card>
      </Box>
      {open ? createDialog(chosenProd, open, handleClose) : <div></div>}
    </Stack>
  );
};

export default function Cart() {
  return (
    <Box>
      <ThemeProvider theme={theme}>
        <Box>
            <Navbar/>
        </Box>
        <Box sx={{ mt: 5 }}>
          <Box>{MakeProducts(allProducts)}</Box>
        </Box>
      </ThemeProvider>
    </Box>
  );
}

const Transition = React.forwardRef(function Transition(
  props: TransitionProps & {
    children: React.ReactElement<any, any>;
  },
  ref: React.Ref<unknown>
) {
  return <Slide direction="up" ref={ref} {...props} />;
});

const AlertDialogSlide = (
  product: Product,
  handleRemoveProduct: (product: Product) => void
) => {
  // const [open, setOpen] = React.useState(false);

  // const handleClickOpen = () => {
  //     setOpen(true);
  // };

  // const handleClose = (remove: boolean) => {
  //     handleRemoveProduct(product);
  //     setOpen(false);
  // };

  return (
    <div>
      <IconButton onClick={() => handleRemoveProduct(product)}>
        <Icon>
          <DeleteForeverIcon fontSize="medium" sx={{ color: "red" }} />
        </Icon>
      </IconButton>
      {/* <Dialog
                open={open}
                TransitionComponent={Transition}
                keepMounted
                onClose={handleClose}
                aria-describedby="alert-dialog-slide-description"
            >
                <DialogTitle>{product.Name + ": Confirm Remove"}</DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-slide-description">
                        Are you sure you want to remove this item from your cart?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                     <Button onClick={() => handleClose(false)}>Cancel</Button>
                    <Button onClick={() => handleClose(true)}>Confirm</Button>
                </DialogActions>
            </Dialog> */}
    </div>
  );
};
