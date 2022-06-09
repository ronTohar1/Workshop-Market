import * as React from 'react';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import Store from '../../DTOs/Store';
import { Box, Button, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import Discount from '../../DTOs/DiscountDTOs/Discount';
import Max from '../../DTOs/DiscountDTOs/Max';
import Predicate from '../../DTOs/DiscountDTOs/Predicate';
import ConditionDiscount from '../../DTOs/DiscountDTOs/ConditionDiscount';
import If from '../../DTOs/DiscountDTOs/If';


export default function ConditionalDiscountForm({store,discountAdder,discounts,predicates}: {store : Store,discountAdder:(discount:Discount)=>void,discounts:Map<number, Discount>,predicates:Map<number,Predicate>}) {
  const [selectedId1, setId1] = React.useState(-1);//discount-then
  const [selectedId2, setId2] = React.useState(-1);//pred
  const [selectedId3, setId3] = React.useState(-1);//discount-else
 
  return ( 
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Conditional Discount
      </Typography>
      <Grid container  justifyContent="left" spacing={-100}>
      <Grid item xs={4}>
       <InputLabel id="demo-simple-select-standard-label">Predicate Id</InputLabel>
       <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
       <Select
        required
        labelId="demo-simple-select-label"
        id="demo-simple-select"
        type="number"
        label="Predicate Id"
        onChange={(event: SelectChangeEvent) => {setId2(parseInt(event.target.value))}}
      >

        {Array.from( predicates ).map(([id, _]) => (
           <MenuItem value={id}>{id}</MenuItem>
        ))}
       </Select>
          </FormControl>
        </Grid>
      <Grid item xs={4}>
      <InputLabel id="demo-simple-select-standard-label">Discount Id predicate true</InputLabel>
      <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
        <Select
        required
        labelId="demo-simple-select-label"
        id="demo-simple-select1"
        type="number"
        label="Discount Id predicate true"
        onChange={(event: SelectChangeEvent) => {setId1(parseInt(event.target.value))}}
      >

        {Array.from( discounts ).map(([id, _]) => (
           <MenuItem value={id}>{id}</MenuItem>
        ))}
       </Select>  
       </FormControl>
       </Grid>
       <Grid item xs={4}>
      <InputLabel id="demo-simple-select-standard-label">* Discount Id predicate false </InputLabel>
      <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
        <Select
        labelId="demo-simple-select-label"
        id="demo-simple-select1"
        type="number"
        label="Discount Id"
        onChange={(event: SelectChangeEvent) => {setId3(parseInt(event.target.value))}}
      >

        {Array.from( discounts ).map(([id, _]) => (
           <MenuItem value={id}>{id}</MenuItem>
        ))}
       </Select>  
       </FormControl>
       </Grid>
          </Grid>
          <Box textAlign='center'>
             <Button onClick={()=>{discountAdder(new If(predicates.get(selectedId2)??new Predicate(''),discounts.get(selectedId1)??new Discount(''),selectedId3!=-1?(discounts.get(selectedId3)??null):null))}} disabled={selectedId1==-1}>add to discounts pool</Button>
             </Box>
    </React.Fragment>
  );
}