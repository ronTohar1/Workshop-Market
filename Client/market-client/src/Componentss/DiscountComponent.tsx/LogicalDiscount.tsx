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
import Addative from '../../DTOs/DiscountDTOs/Addative';
import AndDiscount from '../../DTOs/DiscountDTOs/AndDiscount';
import OrDiscount from '../../DTOs/DiscountDTOs/OrDiscount';
import XorDiscount from '../../DTOs/DiscountDTOs/XorDiscount';

export default function AddativeDiscountForm({store,discountAdder,discounts}: {store : Store,discountAdder:(discount:Discount)=>void,discounts:Map<number,Discount>}) {
  const [selectedId1, setId1] = React.useState(-1);
  const [logOp, setLogOp] = React.useState('');
  const [selectedId2, setId2] = React.useState(-1);
  const logOps = ['AND','OR','XOR']
  return ( 
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Logical Discount
      </Typography>
      <Grid container  justifyContent="left" spacing={-100}>
      <Grid item xs={4}>
      <InputLabel id="demo-simple-select-standard-label">First Discount Id</InputLabel>
      <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
        <Select
        labelId="demo-simple-select-label"
        id="demo-simple-select1"
        type="number"
        label="Discount Id"
        onChange={(event: SelectChangeEvent) => {setId1(parseInt(event.target.value))}}
      >

        {Array.from( discounts ).map(([id, _]) => (
           <MenuItem value={id}>{id}</MenuItem>
        ))}
       </Select>  
       </FormControl>
       </Grid>
       <Grid item xs={4}>
       <InputLabel id="demo-simple-select-standard-label">Logical operator</InputLabel>
       <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
       <Select
        labelId="demo-simple-select-label"
        id="demo-simple-select"
        label="Discount Id 2"
        onChange={(event: SelectChangeEvent) => {setLogOp(event.target.value)}}
      >

        {Array.from( logOps ).map(str => (
           <MenuItem value={str}>{str}</MenuItem>
        ))}
       </Select>
          </FormControl>
          </Grid>
          <Grid item xs={4}>
       <InputLabel id="demo-simple-select-standard-label">Second Discount Id</InputLabel>
       <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
       <Select
        labelId="demo-simple-select-label"
        id="demo-simple-select"
        type="number"
        label="Discount Id 2"
        onChange={(event: SelectChangeEvent) => {setId2(parseInt(event.target.value))}}
      >

        {Array.from( discounts ).map(([id, _]) => (
           <MenuItem value={id}>{id}</MenuItem>
        ))}
       </Select>
          </FormControl>
          </Grid>
          </Grid>
          <Box textAlign='center'>
             <Button onClick={()=>{discountAdder(
               logOp==='AND'?
             new AndDiscount(discounts.get(selectedId1)??new Discount(),discounts.get(selectedId2)??new Discount()):
             (logOp==='OR'? new OrDiscount(discounts.get(selectedId1)??new Discount(),discounts.get(selectedId2)??new Discount()):
             new XorDiscount(discounts.get(selectedId1)??new Discount(),discounts.get(selectedId2)??new Discount())))}} disabled={selectedId1==-1 ||logOp==''|| selectedId2==-1}>add to discount pool</Button>
             </Box>
    </React.Fragment>
  );
}