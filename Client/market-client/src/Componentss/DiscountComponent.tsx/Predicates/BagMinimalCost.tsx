import * as React from 'react';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';
import Store from '../../../DTOs/Store';
import { Box, Button, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import Predicate from '../../../DTOs/DiscountDTOs/Predicate';
import ProductAmount from '../../../DTOs/DiscountDTOs/ProductAmount';
import BagValue from '../../../DTOs/DiscountDTOs/BagValue';

export default function BagMinimalCost({store,predicateAdder}: {store : Store,predicateAdder:(discount:Predicate)=>void}) {
  const [cost, setCost] = React.useState(-1);
  const handleCost = (e: React.ChangeEvent<HTMLInputElement>) => {
    setCost(parseInt(e.currentTarget.value));
  }
  const handleAddPredicate = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    const data = new FormData(event.currentTarget)
    predicateAdder(new BagValue(cost));
  };
  return ( 
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Minimal Bag Cost
      </Typography>
      <Grid container spacing={3}>
        <Grid item xs={12} md={12}>
        <form id="myform" onSubmit={handleAddPredicate} >
          <TextField
            autoFocus
            margin="dense"
            id="cost"
            name="cost"
            label="cost"
            type="number"
            onChange={handleCost}
            fullWidth
            variant="standard"
          />
           <Box textAlign='center'>
             <Button type="submit"  disabled={cost==-1}>Add to Predicate pool</Button>
             </Box>
          </form>
        </Grid>
      </Grid>
    </React.Fragment>
  );
}