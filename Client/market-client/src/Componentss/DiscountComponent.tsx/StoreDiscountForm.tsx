import * as React from 'react';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Store from '../../DTOs/Store';
import { Box, Button } from '@mui/material';
import Discount from '../../DTOs/DiscountDTOs/Discount';
import StoreDiscount from '../../DTOs/DiscountDTOs/StoreDiscount';

export default function StoreDiscountForm({store,discountAdder}: {store : Store,discountAdder:(discount:Discount)=>void}) {
  const handleAddDiscount = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    const data = new FormData(event.currentTarget)
    discountAdder(new StoreDiscount(Number(data.get("discount"))));
  };
  return ( 
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Store Discount
      </Typography>
      <Grid container spacing={3}>
        <Grid item xs={12} md={12}>
        <form id="myform" onSubmit={handleAddDiscount} >
          <TextField
            autoFocus
            margin="dense"
            id="discount"
            name="discount"
            label="discount percentage"
            type="number"
            fullWidth
            variant="standard"
          />
           <Box textAlign='center'>
             <Button type="submit">Add to discount pool</Button>
             </Box>
          </form>
        </Grid>
      </Grid>
    </React.Fragment>
  );
}