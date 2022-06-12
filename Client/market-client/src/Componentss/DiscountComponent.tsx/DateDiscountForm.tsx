import * as React from 'react';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Store from '../../DTOs/Store';
import { Box, Button, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import Discount from '../../DTOs/DiscountDTOs/Discount';
import StoreDiscount from '../../DTOs/DiscountDTOs/StoreDiscount';
import Max from '../../DTOs/DiscountDTOs/Max';
import ProductDiscount from '../../DTOs/DiscountDTOs/ProductDiscount';
import DateDiscount from '../../DTOs/DiscountDTOs/DateDiscount';

export default function DateDiscountForm({store,discountAdder}: {store : Store,discountAdder:(discount:Discount)=>void}) {
  const [selectedDay, setDay] = React.useState(-1);
  const [selectedMonth, setMonth] = React.useState(-1);
  const [selectedYear, setYear] = React.useState(-1);
  const [discount, setDiscount] = React.useState(-1);
 
  return ( 
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Date Discount
      </Typography>
      <Grid container  justifyContent="left" spacing={-100}>
        <Grid item xs={4}>
          <InputLabel id="demo-simple-select-standard-label">Day</InputLabel>
          <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
          <TextField
                required
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => {setDay(parseInt(e.currentTarget.value));}} 
                id="address"
                name="number"
                type="number"
                label="Day "
                fullWidth/>
              </FormControl>
        </Grid>
        <Grid item xs={4}>
          <InputLabel id="demo-simple-select-standard-label">Month</InputLabel>
          <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
          <TextField
                required
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => {setMonth(parseInt(e.currentTarget.value));}} 
                id="address"
                name="number"
                type="number"
                label="Month"
                fullWidth/>
              </FormControl>
        </Grid>
        <Grid item xs={4}>
          <InputLabel id="demo-simple-select-standard-label">Year</InputLabel>
          <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
          <TextField
                required
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => {setYear(parseInt(e.currentTarget.value));}} 
                id="address"
                name="number"
                type="number"
                label="Year"
                fullWidth/>
              </FormControl>
        </Grid>
        <Grid item xs={4}>
          <InputLabel id="demo-simple-select-standard-label">Discount Percentage</InputLabel>
          <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
          <TextField
                required
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => {setDiscount(parseInt(e.currentTarget.value));}} 
                id="address"
                name="number"
                type="number"
                label="discount percentage"
                fullWidth/>
              </FormControl>
        </Grid>
      </Grid>
          <Box textAlign='center'>
             <Button onClick={()=>{discountAdder(new DateDiscount(discount,selectedYear,selectedMonth,selectedDay))}} disabled={discount==-1 && (selectedYear==-1 || selectedMonth==-1 ||selectedDay==-1 )}>add to discount pool</Button>
             </Box>
    </React.Fragment>
  );
}