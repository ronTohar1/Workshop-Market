import * as React from 'react';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import Store from '../../DTOs/Store';
import { Box, Button, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';

import Predicate from '../../DTOs/PurchaseDTOs/Predicate';
import ConditionDiscount from '../../DTOs/DiscountDTOs/ConditionDiscount';
import If from '../../DTOs/DiscountDTOs/If';
import PurchasePolicy from '../../DTOs/PurchaseDTOs/PurchasePolicy';
import Implies from '../../DTOs/PurchaseDTOs/Implies';


export default function ImpliesPurchasesForm({store,purchasesAdder,predicates}: {store : Store,purchasesAdder:(purchase:PurchasePolicy)=>void,predicates:Map<number,Predicate>}) {
  const [selectedId1, setId1] = React.useState(-1);//pred1
  const [selectedId2, setId2] = React.useState(-1);//pred2
 
  return ( 
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Implies Policy
      </Typography>
      <Grid container  justifyContent="left" spacing={-100}>
      <Grid item xs={4}>
       <InputLabel id="demo-simple-select-standard-label">first Predicate Id</InputLabel>
       <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
       <Select
        required
        labelId="demo-simple-select-label"
        id="demo-simple-select"
        type="number"
        label="Predicate Id"
        onChange={(event: SelectChangeEvent) => {setId1(parseInt(event.target.value))}}
      >

        {Array.from( predicates ).map(([id, _]) => (
           <MenuItem value={id}>{id}</MenuItem>
        ))}
       </Select>
          </FormControl>
        </Grid>
      <Grid item xs={4}>
      <InputLabel id="demo-simple-select-standard-label">Second Predicate Id</InputLabel>
      <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
        <Select
        required
        labelId="demo-simple-select-label"
        id="demo-simple-select1"
        type="number"
        label="Predicate"
        onChange={(event: SelectChangeEvent) => {setId2(parseInt(event.target.value))}}
      >

        {Array.from( predicates ).map(([id, _]) => (
           <MenuItem value={id}>{id}</MenuItem>
        ))}
       </Select>  
       </FormControl>
       </Grid>
          </Grid>
          <Box textAlign='center'>
             <Button onClick={()=>{purchasesAdder(new Implies(predicates.get(selectedId1)??new Predicate(''),predicates.get(selectedId2)??new Predicate('')))}} disabled={selectedId1==-1}>add to policies pool</Button>
             </Box>
    </React.Fragment>
  );
}