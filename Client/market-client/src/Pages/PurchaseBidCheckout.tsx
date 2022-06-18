import * as React from "react";
import CssBaseline from "@mui/material/CssBaseline";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import Toolbar from "@mui/material/Toolbar";
import Paper from "@mui/material/Paper";
import Stepper from "@mui/material/Stepper";
import Step from "@mui/material/Step";
import StepLabel from "@mui/material/StepLabel";
import Button from "@mui/material/Button";
import Link from "@mui/material/Link";
import Typography from "@mui/material/Typography";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import AddressForm from "../Componentss/PurchaseBid/AddressForm";
import PaymentForm from "../Componentss/PurchaseBid/PaymentForm";
import Review from "../Componentss/PurchaseBid/Review";
import PurchaseBid from "../Componentss/PurchaseBid/PurchaseBid";
import CheckoutDTO from "../DTOs/CheckoutDTO";
import Product from "../DTOs/Product";
import { pathCart, pathHome, pathStore } from "../Paths";
import { getBuyerId } from "../services/SessionService";
import { serverPurchaseCart, serverGetCart, serverPurchaseBid } from "../services/BuyersService";
import { fetchResponse } from "../services/GeneralService";
import Purchase from "../DTOs/Purchase";
import { blue, indigo, red } from "@mui/material/colors";
import { useLocation, useNavigate } from "react-router-dom";
import {
  fetchProducts,
  getCartProducts,
  serverSearchProducts,
} from "../services/ProductsService";
import { CartProduct, convertToCartProduct } from "./Cart";
import LoadingCircle from "../Componentss/LoadingCircle";
import Cart from "../DTOs/Cart";
import CheckoutDTOBid from "../DTOs/CheckoutDTOBid";

const backgroundImage =
  "https://images.unsplash.com/photo-1471193945509-9ad0617afabf";

function Copyright() {
  return (
    <Typography variant='body2' color='text.secondary' align='center'>
      {"Copyright © "}
      <Link color='inherit' href='https://mui.com/'>
        Workshop Market
      </Link>{" "}
      {new Date().getFullYear()}
      {"."}
    </Typography>
  );
}

const steps = ["Bidding Options","Shipping address", "Payment details", "Review your order"];

function getStepContent(
  step: number,
  checkout: CheckoutDTOBid,
) {
  switch (step) {
    case 0:
      return <PurchaseBid checkout={checkout} />;
    case 1:
      return <AddressForm checkout={checkout} />;
    case 2:
      return <PaymentForm checkout={checkout} />;
    case 3:
      return <Review checkout={checkout} />;
    default:
      throw new Error("Unknown step");
  }
}

const theme = createTheme({
  palette: {
    primary: {
      main: indigo[500],
    },
    background: {
      default: "#008394",
    },
  },
});
const checkout = new CheckoutDTOBid();
// {productsAmount}:{productsAmount:Map<Product,number>}
export default function PurchaseBidCheckout() {
  const navigate = useNavigate();
  const [activeStep, setActiveStep] = React.useState(0);
  const [succeeded, setSucceeded] = React.useState<boolean>(false);
  const [purchaseDesc, setPurchase] = React.useState<string>("");
  const [errorMessage, setErrorMessage] = React.useState<string>("");
  const [orderPlaced, setOrderPlaced] = React.useState(false);
  const handleNext = () => {
    setActiveStep(activeStep + 1);
  };



  React.useEffect(() => {
    if (activeStep === steps.length) placeOrder();
  }, [activeStep]);

  const handleBack = () => {
    setActiveStep(activeStep - 1);
  };

  console.log(checkout);
  const handleClickHome = () => {
    navigate(`${pathHome}`);
  };

  function placeOrder() {
    const buyerId = getBuyerId();
    const responsePromise = serverPurchaseBid(buyerId, checkout);
    // console.log(responsePromise)

    fetchResponse(responsePromise)
      .then((_) => {
        setPurchase('Bid purchase was done successfuly!')
        setSucceeded(true);
        return true;
      })
      .then(setOrderPlaced)
      .catch((e) => {
        setErrorMessage(e);
        setSucceeded(false);
        setOrderPlaced(true);
      });
  }

  function thankForOrder() {
    return (
      <React.Fragment>
        <Typography variant='h5' gutterBottom>
          Thank you for your order.
        </Typography>
        <Typography variant='subtitle1'>
          {purchaseDesc}
        </Typography>
        <Box textAlign='center'>
          <Button href={pathHome} variant='contained' sx={{ mt: 3, ml: 1 }}>
            Back To home page
          </Button>
        </Box>
      </React.Fragment>
    );
  }

  function orderError() {
    return (
      <React.Fragment>
        <Typography variant='h5' gutterBottom>
          Couldn't complete the order.
        </Typography>
        <Typography variant='subtitle1'>
          <p>{errorMessage}</p>
        </Typography>
        <Box textAlign='center'>
          <Button href={pathCart} variant='contained' sx={{ mt: 3, ml: 1 }}>
            Back To my cart
          </Button>
        </Box>
      </React.Fragment>
    );
  }

  function getCurrentStep(
    activeStep: number,
    checkout: CheckoutDTOBid
  ) {
    return (
      <React.Fragment>
        {getStepContent(activeStep, checkout)}
        <Box sx={{ display: "flex", justifyContent: "flex-end" }}>
          {activeStep !== 0 && (
            <Button onClick={handleBack} sx={{ mt: 3, ml: 1 }}>
              Back
            </Button>
          )}
          <Button
            variant='contained'
            onClick={handleNext}
            sx={{ mt: 3, ml: 1 }}>
            {activeStep === steps.length - 1 ? "Place order" : "Next"}
          </Button>
        </Box>
      </React.Fragment>
    );
  }

  // return (purchaseDesc == "" && errorMessage=="") ? (
  //   LoadingCircle()
  // ) : (
    return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <AppBar
        position='sticky'
        color='default'
        elevation={0}
        sx={{
          position: "relative",
          borderBottom: (t) => `1px solid ${t.palette.divider}`,
        }}>
        <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
          <Typography
            onClick={handleClickHome}
            variant='h6'
            noWrap
            component='div'
            sx={{
              display: { xs: "none", sm: "block" },
              "&:hover": {
                cursor: "pointer",
              },
            }}>
            Workshop Market
          </Typography>
        </Toolbar>
      </AppBar>
      <Container component='main' maxWidth='sm' sx={{ mb: 4 }}>
        <Paper
          variant='outlined'
          sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}>
          <Typography component='h1' variant='h4' align='center'>
            Checkout
          </Typography>
          <Stepper activeStep={activeStep} sx={{ pt: 3, pb: 5 }}>
            {steps.map((label) => (
              <Step key={label}>
                <StepLabel>{label}</StepLabel>
              </Step>
            ))}
          </Stepper>
          <React.Fragment>
            {orderPlaced
              ? succeeded
                ? thankForOrder()
                : orderError()
              : activeStep === steps.length // If its last step
              ? LoadingCircle()
              : getCurrentStep(activeStep, checkout)}
          </React.Fragment>
        </Paper>
        <Copyright />
      </Container>
    </ThemeProvider>
  );
}
