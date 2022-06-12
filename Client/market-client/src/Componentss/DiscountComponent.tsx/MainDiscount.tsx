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
import { pathHome } from '../../Paths';
import { getBuyerId } from '../../services/SessionService';
import { fetchResponse } from '../../services/GeneralService';
import StoreDiscountForm from './StoreDiscountForm';
import ProductDiscountForm from './ProductDiscountForm';
import CategoryDiscountForm from './MaxDiscountForm';
import Store from '../../DTOs/Store';
import { ButtonGroup, FormControl, Grid, InputLabel, MenuItem, Select, SelectChangeEvent, TextField } from '@mui/material';
import DateDiscount from '../../DTOs/DiscountDTOs/DateDiscount';
import BagValue from '../../DTOs/DiscountDTOs/BagValue';
import ProductDiscount from '../../DTOs/DiscountDTOs/ProductDiscount';
import If from '../../DTOs/DiscountDTOs/If';
import StoreDiscount from '../../DTOs/DiscountDTOs/StoreDiscount';
import Discount from '../../DTOs/DiscountDTOs/Discount';
import DiscountCard from './DiscountCard';
import AddativeDiscountForm from './AddativeDiscountForm';
import MaxDiscountForm from './MaxDiscountForm';
import DateDiscountForm from './DateDiscountForm';
import ConditionalDiscount from './ConditionalDiscountForm';
import LogicalDiscount from './LogicalDiscount';
import AmountInBag from './Predicates/AmountInBag';
import BagMinimalCost from './Predicates/BagMinimalCost';
import LogicalPredicate from './Predicates/LogicalPredicate';
import Predicate from '../../DTOs/DiscountDTOs/Predicate';
import PredicateCard from './Predicates/PredicateCard';
import { serverAddDiscountPolicy } from '../../services/StoreService';
import Navbar from '../Navbar';


//Logical was removed
const discountSteps = ['Store', 'Product', 'Max','Date','Conditional'];
const prdicateSteps = ['Amount in bag', 'Bag minimal cost','Logical'];
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

export default function MainDiscount() {
  const store: Store = useLocation().state as Store
  const navigate = useNavigate();
  const [activeStep, setActiveStep] = React.useState(0);
  const [lastDiscountIndex, setLastDiscountIndex] = React.useState<number>(0);
  const [lastPredicateIndex, setLastPredicateIndex] = React.useState<number>(0);
  const [selectedId, setId] = React.useState(-1);
  //const initDiscounts = new Map<number, Discount>();
  // initDiscounts.set(0, new DateDiscount(90, 2001, 4, 18));
  // initDiscounts.set(1, new StoreDiscount(68));
  // initDiscounts.set(2, new If(new BagValue(2),new StoreDiscount(56),new ProductDiscount(0, 0)));

  const [discounts, setDiscounts] = React.useState<Map<number,Discount>>(new Map<number,Discount>());
  function addDiscount (discount:Discount) {
    setLastDiscountIndex(lastDiscountIndex + 1);
    discounts.set(lastDiscountIndex, discount);
    setDiscounts(discounts);
  };

  const [predicates, setPredicates] = React.useState<Map<number,Predicate>>(new Map<number,Predicate>());
  function addPredicate (predicate:Predicate) {
    predicates.set(lastPredicateIndex, predicate);
    setLastPredicateIndex(lastPredicateIndex + 1);
    setPredicates(predicates);
  };

  function getStepContent(step: number, store:Store) {
    switch (step) {
      case 0:
        return <StoreDiscountForm store={store} discountAdder={addDiscount}/>;
      case 1:
        return <ProductDiscountForm  store={store} discountAdder={addDiscount}/>;
      case 2:
        return <MaxDiscountForm  store={store}  discountAdder={addDiscount} discounts={discounts}/>;
      // case 3:
      //   return <AddativeDiscountForm  store={store}  discountAdder={addDiscount} discounts={discounts}/>;
      case 3:
        return <DateDiscountForm  store={store}  discountAdder={addDiscount}/>;
      // case 5:
      //     return <LogicalDiscount  store={store}  discountAdder={addDiscount}  discounts={discounts}/>;
      case 4:
          return <ConditionalDiscount  store={store}  discountAdder={addDiscount} discounts={discounts} predicates={predicates} />;
      case 5:
          return <AmountInBag  store={store}  predicateAdder={addPredicate}/>;
      case 6:
          return <BagMinimalCost  store={store}  predicateAdder={addPredicate}/>;
      case 7:
          return <LogicalPredicate  store={store}  predicateAdder={addPredicate} predicates={predicates}/>;
      default:
        throw new Error('Unknown step');
    }
  }

  const handleChangeId = (event: SelectChangeEvent) => {
    setId(parseInt(event.target.value));
    console.log(selectedId)
  };
  const handleSubmit = () =>{
    const buyerId = getBuyerId()
    const responsePromise = serverAddDiscountPolicy(buyerId,store, discounts.get(selectedId)??new Discount(''))
    console.log(responsePromise)
    fetchResponse(responsePromise).then((id:number)=>{
      alert('Discount policy was added successfully to the store!')
    })
    .catch((e) => {
      alert(e)
     })
  }
  return (
    <ThemeProvider theme={theme}>
       <Navbar/>
      <CssBaseline />
      <Container component="main" maxWidth={false} sx={{ mb: 4 }}>
        <Paper variant="outlined" sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}>
          <Typography component="h1" variant="h4" align="center">
            Discounts Editor
          </Typography>
          <div className="btn-group" role="group" aria-label="Basic example">
          <Grid container  justifyContent="center">
            {/* </Box><Stepper activeStep={activeStep} sx={{ pt: 3, pb: 5 }}> */}
            {discountSteps.map((label) => (
              <Box m={1} pt={3}>
              <Button key={label}  
              color="inherit"
              type="button" 
              className="btn btn-secondary"
              style={{
                backgroundColor: "#21b6ae",
                padding:"16px",
                fontSize: "12px",
                maxWidth: '120px',
                maxHeight: '50px',
                minWidth: '120px',
                minHeight: '50px'
            }}
            onClick = {()=>{setActiveStep(discountSteps.indexOf(label))}}
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
                padding:"16px",
                fontSize: "12px",
                maxWidth: '120px',
                maxHeight: '50px',
                minWidth: '120px',
                minHeight: '50px'
            }}
            onClick = {()=>{setActiveStep(discountSteps.length+prdicateSteps.indexOf(label))}}
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
                {getStepContent(activeStep,store)}
              </React.Fragment>
            )}
          </React.Fragment>
        </Paper>
      </Container>
      <CssBaseline />
      <Container component="main" maxWidth={false} sx={{ mb: 4 }}>
      <Grid container  justifyContent="center" spacing={4}>
      <Grid item xs={4}>
      <Paper variant="outlined" sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}> 
      <Typography component="h1" variant="h4" align="center">
          Discounts Pool
          </Typography>
       <Grid
            sx={{ overflowY: "scroll",minHeight: "200px" , maxHeight: "200px" }}
            container
            spacing={2}
            >
          {Array.from( discounts ).map(([id, discount]) => (
          <Grid item xs={6} md={6} lg={4}>
            <DiscountCard id={id} discount={discount} store={store}/>
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
            sx={{ overflowY: "scroll",minHeight: "200px" , maxHeight: "200px" }}
            container
            spacing={2}
            >
          {Array.from( predicates ).map(([id, predicate]) => (
          <Grid item xs={12} md={6} lg={4}>
            <PredicateCard id={id} predicate={predicate} store={store}/>
          </Grid>
        ))}
        </Grid>
        </Paper>
        </Grid>
        <Grid item xs={4}>
        <Container component="main" maxWidth="sm" sx={{ mb: 4 }}>
      <Paper variant="outlined" sx={{ my: { xs: 3, md: 6 ,sm:6}, p: { xs: 2, md: 3 } }}> 
      <Typography variant="h6" gutterBottom>
       Submit Discount
      </Typography>
        <Grid item xs={12} sm={12}>
        <FormControl fullWidth id="myform" onSubmit={()=>{}} >
        <InputLabel id="demo-simple-select-standard-label">Discount Id</InputLabel>
        <Select
        labelId="demo-simple-select-label"
        id="demo-simple-select"
        type="number"
        label="Discount Id"
        onChange={handleChangeId}
      >

        {Array.from( discounts ).map(([id, discount]) => (
           <MenuItem value={id}>{id}</MenuItem>
        ))}
       </Select>
           <Box textAlign='center'>
             <Button type="submit" onClick={handleSubmit} disabled={selectedId==-1}>Submit </Button>
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