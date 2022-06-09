import * as React from 'react';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';
import Store from '../../../DTOs/Store';
import { Box, Button, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import Predicate from '../../../DTOs/DiscountDTOs/Predicate';
import ProductAmount from '../../../DTOs/DiscountDTOs/ProductAmount';
import BagValue from '../../../DTOs/DiscountDTOs/BagValue';
import CheckProductMore from '../../../DTOs/PurchaseDTOs/CheckProductMore';

export default function MinimumProductPredicate({store,predicateAdder}: {store : Store,predicateAdder:(discount:Predicate)=>void}) {
  const [selectedId, setId] = React.useState(-1);
  const [amount, setAmount] = React.useState(-1);
 
  return ( 
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Minimun Product In Bag Predicate
      </Typography>
      <Grid container  justifyContent="left" spacing={-100}>
      <Grid item xs={4}>
      <InputLabel id="demo-simple-select-standard-label">Product</InputLabel>
      <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
        <Select
        labelId="demo-simple-select-label"
        id="demo-simple-select1"
        type="number"
        label="Discount Id"
        onChange={(event: SelectChangeEvent) => {setId(parseInt(event.target.value))}}
      >

        {store.products .map((product) => (
           <MenuItem value={product.id}>{product.name}</MenuItem>
        ))}
       </Select>  
       </FormControl>
       </Grid>
       <Grid item xs={4}>
       <InputLabel id="demo-simple-select-standard-label">Min Amount</InputLabel>
       <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
       <TextField
            required
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => {setAmount(parseInt(e.currentTarget.value));}} 
            id="address"
            name="number"
            type="number"
            label="minimum amount"
            fullWidth/>
          </FormControl>
          </Grid>
          </Grid>
          <Box textAlign='center'>
             <Button onClick={()=>{predicateAdder(new CheckProductMore(selectedId,store.products.find(p=>p.id==selectedId)?.name,amount))}} disabled={selectedId==-1 || amount==-1}>add to policies pool</Button>
             </Box>
    </React.Fragment>
  );
}