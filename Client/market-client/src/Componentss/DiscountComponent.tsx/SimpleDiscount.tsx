import * as React from 'react';
import CssBaseline from '@mui/material/CssBaseline';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import Toolbar from '@mui/material/Toolbar';
import Paper from '@mui/material/Paper';
import Stepper from '@mui/material/Stepper';
import Step from '@mui/material/Step';
import StepLabel from '@mui/material/StepLabel';
import Button from '@mui/material/Button';
import Link from '@mui/material/Link';
import Typography from '@mui/material/Typography';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { blue, indigo, red } from '@mui/material/colors';
import { useNavigate } from "react-router-dom";
import { pathHome } from '../../Paths';
import { getBuyerId } from '../../services/SessionService';
import { purchaseCart } from '../../services/BuyersService';
import { fetchResponse } from '../../services/GeneralService';
import StoreDiscountForm from './StoreDiscountForm';
import ProductDiscountForm from './ProductDiscountForm';
import CategoryDiscountForm from './CategoryDiscountForm';
import Store from '../../DTOs/Store';
import { ButtonGroup, Grid } from '@mui/material';
import DateDiscount from '../../DTOs/DiscountDTOs/DateDiscount';
import BagValue from '../../DTOs/DiscountDTOs/BagValue';
import ProductDiscount from '../../DTOs/DiscountDTOs/ProductDiscount';
import If from '../../DTOs/DiscountDTOs/If';
import StoreDiscount from '../../DTOs/DiscountDTOs/StoreDiscount';
import Discount from '../../DTOs/DiscountDTOs/Discount';
import DiscountCard from './DiscountCard';

const backgroundImage =
  "https://images.unsplash.com/photo-1471193945509-9ad0617afabf";

function Copyright() {
  return (
    <Typography variant="body2" color="text.secondary" align="center">
      {'Copyright © '}
      <Link color="inherit" href="https://mui.com/">
        Workshop Market
      </Link>{' '}
      {new Date().getFullYear()}
      {'.'}
    </Typography>
  );
}

const steps = ['Store discount', 'Product discount', 'Category discount'];

const theme = createTheme({
  palette: {
    primary: {
      main: indigo[500],
    },
    background: {
      default: "#008394"
    }
    
  },
});

export default function SimpleDiscount({store}: {store : Store}) {
  const navigate = useNavigate();
  const [activeStep, setActiveStep] = React.useState(0);
  const [lastIndex, setLastIndex] = React.useState<number>(3);
  const initDiscounts = new Map<number, Discount>();
  initDiscounts.set(0, new DateDiscount(90, 2001, 4, 18));
  initDiscounts.set(1, new StoreDiscount(68));
  initDiscounts.set(2, new If(new BagValue(2),new StoreDiscount(56),new ProductDiscount(0, 0)));

  const [discounts, setDiscounts] = React.useState<Map<number,Discount>>(initDiscounts);
  function addDiscount (discount:Discount) {
    setLastIndex(lastIndex + 1);
    discounts.set(lastIndex, discount);
    setDiscounts(discounts);
  };
  
  function getStepContent(step: number, store:Store) {
    switch (step) {
      case 0:
        return <StoreDiscountForm store={store} discountAdder={addDiscount}/>;
      case 1:
        return <ProductDiscountForm  store={store}/>;
      case 2:
        return <CategoryDiscountForm  store={store} />;
      default:
        throw new Error('Unknown step');
    }
  }

  return (
    <ThemeProvider theme={theme}>
       
      <CssBaseline />
      <Container component="main" maxWidth={false} sx={{ mb: 4 }}>
        <Paper variant="outlined" sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}>
          <Typography component="h1" variant="h4" align="center">
            Discounts Editor
          </Typography>
          <div className="btn-group" role="group" aria-label="Basic example">
            {/* </Box><Stepper activeStep={activeStep} sx={{ pt: 3, pb: 5 }}> */}
            {steps.map((label) => (
              <Button key={label}  
              color="inherit"
              type="button" 
              className="btn btn-secondary"
              style={{
                backgroundColor: "#21b6ae",
                padding: "18px",
                fontSize: "12px"
            }}
            onClick = {()=>{setActiveStep(steps.indexOf(label))}}
            >
                {label}
              </Button>
            ))}
          </div>
          <React.Fragment>
            {(
              <React.Fragment>
                {getStepContent(activeStep,store)}
              </React.Fragment>
            )}
          </React.Fragment>
        </Paper>
      </Container>
      <CssBaseline />
      <Grid id="top-row" container  alignItems="center">
      <Grid item xs={4}>
      <Paper variant="outlined" sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}> 
      <Typography component="h1" variant="h4" align="center">
          Discounts Pool
          </Typography>
       <Grid
            sx={{ overflowY: "scroll", maxHeight: "250px" }}
            container
            spacing={2}
            >
          {Array.from( discounts ).map(([id, discount]) => (
          <Grid item xs={12} md={6} lg={4}>
            <DiscountCard id={id} discount={discount} store={store}/>
          </Grid>
        ))}
        </Grid>
        </Paper>
        </Grid>
        <Grid item xs={4}>
      <Paper variant="outlined" sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}> 
      <Typography component="h1" variant="h4" align="center">
          Discounts Pool
          </Typography>
       <Grid
            sx={{ overflowY: "scroll", maxHeight: "250px" }}
            container
            spacing={2}
            >
          {Array.from( discounts ).map(([id, discount]) => (
          <Grid item xs={12} md={6} lg={4}>
            <DiscountCard id={id} discount={discount} store={store}/>
          </Grid>
        ))}
        </Grid>
        </Paper>
        </Grid>
        </Grid>
    </ThemeProvider>
  );
}