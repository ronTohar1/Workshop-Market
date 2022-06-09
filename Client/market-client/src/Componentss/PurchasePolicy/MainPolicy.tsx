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
import { useLocation, useNavigate } from "react-router-dom";
import { getBuyerId } from '../../services/SessionService';
import { fetchResponse } from '../../services/GeneralService';
import Store from '../../DTOs/Store';
import { ButtonGroup, FormControl, Grid, InputLabel, MenuItem, Select, SelectChangeEvent, TextField } from '@mui/material';
import Predicate from '../../DTOs/DiscountDTOs/Predicate';
import PredicateCard from './Predicates/PredicateCard';
import { serverAddDiscountPolicy, serverAddPurchasePolicy } from '../../services/StoreService';
import PurchasePolicy from '../../DTOs/PurchaseDTOs/PurchasePolicy';
import StorePurchaseHourForm from './StorePurchaseHourForm';
import ProductPurchaseHourForm from './ProductPurchaseHourForm';
import MinimumProductForm from './MinimumProductForm';
import MaximumProductForm from './MaximumProductForm';
import DatePurchaseForm from './DatePurchaseForm';
import { Discount } from '@mui/icons-material';
import ImpliesPurchasesForm from './ImpliesPurchasesForm';
import LogicalPurchaseForm from './LogicalPurchaseForm';
import MaximumProductPredicate from './Predicates/MaximumProductPredicate';
import MinimumProductPredicate from './Predicates/MinimumProductPredicate';
import DiscountCard from './PolicyCard';
import PolicyCard from './PolicyCard';
import Restriction from '../../DTOs/PurchaseDTOs/Restriction';
import Navbar from '../Navbar';


//Logical was removed
const PoliciesSteps = ['Store Purchase Hour', 'Product Purchase Hour', 'Product Min Amount', 'Product Max Amount', 'Date', 'Logical', 'Implies'];
const prdicateSteps = ['Product Min Amount', 'Product Max Amount'];
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

export default function MainPolicy() {
  const store: Store = useLocation().state as Store
  console.log(store)
  const navigate = useNavigate();
  const [activeStep, setActiveStep] = React.useState(0);
  const [lastPurchaseIndex, setLastPurchaseIndex] = React.useState<number>(0);
  const [lastPredicateIndex, setLastPredicateIndex] = React.useState<number>(0);
  const [selectedId, setId] = React.useState(-1);
  //const initDiscounts = new Map<number, Discount>();
  // initDiscounts.set(0, new DateDiscount(90, 2001, 4, 18));
  // initDiscounts.set(1, new StoreDiscount(68));
  // initDiscounts.set(2, new If(new BagValue(2),new StoreDiscount(56),new ProductDiscount(0, 0)));

  const [purchases, setPurchases] = React.useState<Map<number, PurchasePolicy>>(new Map<number, PurchasePolicy>());
  const [restrictions, setRestriction] = React.useState<Map<number, Restriction>>(new Map<number, Restriction>());
  function addPurchasePolicy(policy: PurchasePolicy) {
    setLastPurchaseIndex(lastPurchaseIndex + 1);
    purchases.set(lastPurchaseIndex, policy);
    setPurchases(purchases);
    if (policy.tag.includes('Restriction')) {
      restrictions.set(lastPurchaseIndex, policy);
      setRestriction(restrictions);
    }
  };

  const [predicates, setPredicates] = React.useState<Map<number, Predicate>>(new Map<number, Predicate>());
  function addPredicate(predicate: Predicate) {
    predicates.set(lastPredicateIndex, predicate);
    setLastPredicateIndex(lastPredicateIndex + 1);
    setPredicates(predicates);
  };

  function getStepContent(step: number, store: Store) {
    switch (step) {
      case 0:
        return <StorePurchaseHourForm store={store} purchasesAdder={addPurchasePolicy} />;
      case 1:
        return <ProductPurchaseHourForm store={store} purchasesAdder={addPurchasePolicy} />;
      case 2:
        return <MinimumProductForm store={store} purchasesAdder={addPurchasePolicy} />;
      case 3:
        return <MaximumProductForm store={store} purchasesAdder={addPurchasePolicy} />;
      case 4:
        return <DatePurchaseForm store={store} purchasesAdder={addPurchasePolicy} />
      case 5:
        return <LogicalPurchaseForm store={store} purchasesAdder={addPurchasePolicy} restrictions={restrictions} />;
      case 6:
        return <ImpliesPurchasesForm store={store} purchasesAdder={addPurchasePolicy} predicates={predicates} />;
      case 7:
        return <MinimumProductPredicate store={store} predicateAdder={addPredicate} />;
      case 8:
        return <MaximumProductPredicate store={store} predicateAdder={addPredicate} />;
      default:
        throw new Error('Unknown step');
    }
  }

  const handleChangeId = (event: SelectChangeEvent) => {
    setId(parseInt(event.target.value));
    console.log(selectedId)
  };
  const handleSubmit = () => {
    const buyerId = getBuyerId()
    const responsePromise = serverAddPurchasePolicy(buyerId, store, purchases.get(selectedId) ?? new PurchasePolicy(''))
    console.log(responsePromise)
    fetchResponse(responsePromise).then((id: number) => {
      alert('Purchase policy was added successfully to the store!')
    })
      .catch((e) => {
        alert(e)
      })
  }
  return (
    <ThemeProvider theme={theme}>
      <Navbar />
      <CssBaseline />
      <Container component="main" maxWidth={false} sx={{ mb: 4 }}>
        <Paper variant="outlined" sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}>
          <Typography component="h1" variant="h4" align="center">
            Purchase policies Editor
          </Typography>
          <div className="btn-group" role="group" aria-label="Basic example">
            <Grid container justifyContent="center">
              {/* </Box><Stepper activeStep={activeStep} sx={{ pt: 3, pb: 5 }}> */}
              {PoliciesSteps.map((label) => (
                <Box m={1} pt={3}>
                  <Button key={label}
                    color="inherit"
                    type="button"
                    className="btn btn-secondary"
                    style={{
                      backgroundColor: "#21b6ae",
                      padding: "16px",
                      fontSize: "12px",
                      maxWidth: '160px',
                      maxHeight: '60px',
                      minWidth: '160px',
                      minHeight: '60px'
                    }}
                    onClick={() => { setActiveStep(PoliciesSteps.indexOf(label)) }}
                  >
                    {label}
                  </Button>
                </Box>
              ))}
              {prdicateSteps.map((label) => (
                <Box m={1} pt={3}>
                  <Button key={label}
                    color="inherit"
                    type="button"
                    className="btn btn-secondary"
                    style={{
                      backgroundColor: "#00e676",
                      padding: "16px",
                      fontSize: "12px",
                      maxWidth: '160px',
                      maxHeight: '60px',
                      minWidth: '160px',
                      minHeight: '60px'
                    }}
                    onClick={() => { setActiveStep(PoliciesSteps.length + prdicateSteps.indexOf(label)) }}
                  >
                    {label}
                  </Button>
                </Box>
              ))}
            </Grid>
          </div>
          <React.Fragment>
            {(
              <React.Fragment>
                {getStepContent(activeStep, store)}
              </React.Fragment>
            )}
          </React.Fragment>
        </Paper>
      </Container>
      <CssBaseline />
      <Container component="main" maxWidth={false} sx={{ mb: 4 }}>
        <Grid container justifyContent="center" spacing={4}>
          <Grid item xs={4}>
            <Paper variant="outlined" sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}>
              <Typography component="h1" variant="h4" align="center">
                Policies Pool
              </Typography>
              <Grid
                sx={{ overflowY: "scroll", minHeight: "200px", maxHeight: "200px" }}
                container
                spacing={2}
              >
                {Array.from(purchases).map(([id, purchase]) => (
                  <Grid item xs={6} md={6} lg={4}>
                    <PolicyCard id={id} purchase={purchase} />
                  </Grid>
                ))}
              </Grid>
            </Paper>
          </Grid>
          <Grid item xs={4}>
            <Paper variant="outlined" sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}>
              <Typography component="h1" variant="h4" align="center">
                Predicate Pool
              </Typography>
              <Grid
                sx={{ overflowY: "scroll", minHeight: "200px", maxHeight: "200px" }}
                container
                spacing={2}
              >
                {Array.from(predicates).map(([id, predicate]) => (
                  <Grid item xs={12} md={6} lg={4}>
                    <PredicateCard id={id} predicate={predicate} store={store} />
                  </Grid>
                ))}
              </Grid>
            </Paper>
          </Grid>
          <Grid item xs={4}>
            <Container component="main" maxWidth="sm" sx={{ mb: 4 }}>
              <Paper variant="outlined" sx={{ my: { xs: 3, md: 6, sm: 6 }, p: { xs: 2, md: 3 } }}>
                <Typography variant="h6" gutterBottom>
                  Submit Policy
                </Typography>
                <Grid item xs={12} sm={12}>
                  <FormControl fullWidth id="myform" onSubmit={() => { }} >
                    <InputLabel id="demo-simple-select-standard-label">Policy Id</InputLabel>
                    <Select
                      labelId="demo-simple-select-label"
                      id="demo-simple-select"
                      type="number"
                      label="Discount Id"
                      onChange={handleChangeId}
                    >

                      {Array.from(purchases).map(([id, purchase]) => (
                        <MenuItem value={id}>{id}</MenuItem>
                      ))}
                    </Select>
                    <Box textAlign='center'>
                      <Button type="submit" variant="contained" sx={{ mt: 1 }} onClick={handleSubmit} disabled={selectedId == -1}>Submit </Button>
                    </Box>
                  </FormControl>
                </Grid>
              </Paper>
            </Container>
          </Grid>
        </Grid>
      </Container>

    </ThemeProvider>
  );
}