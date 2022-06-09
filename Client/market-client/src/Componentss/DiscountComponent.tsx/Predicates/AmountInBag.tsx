import * as React from 'react';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';
import Store from '../../../DTOs/Store';
import { Box, Button, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import Predicate from '../../../DTOs/DiscountDTOs/Predicate';
import ProductAmount from '../../../DTOs/DiscountDTOs/ProductAmount';

export default function AmountInBag({store,predicateAdder}: {store : Store,predicateAdder:(discount:Predicate)=>void}) {
  const [selectedId, setId] = React.useState(-1);
  const [minimalAmount, setMinimalAmount] = React.useState(-1);
 
  return ( 
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        minimal products in bag
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
       <InputLabel id="demo-simple-select-standard-label">Minimal Amount For Discount</InputLabel>
       <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
       <TextField
            required
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => {setMinimalAmount(parseInt(e.currentTarget.value));}} 
            id="address"
            name="number"
            type="number"
            label="amont"
            fullWidth/>
          </FormControl>
          </Grid>
          </Grid>
          <Box textAlign='center'>
             <Button onClick={()=>{predicateAdder(new ProductAmount(selectedId,minimalAmount))}} disabled={selectedId==-1 || minimalAmount==-1}>add to discount pool</Button>
             </Box>
    </React.Fragment>
  );
}