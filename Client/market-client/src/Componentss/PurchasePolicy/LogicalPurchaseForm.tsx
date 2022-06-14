import * as React from 'react';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Store from '../../DTOs/Store';
import { Box, Button, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import Discount from '../../DTOs/DiscountDTOs/Discount';
import AndDiscount from '../../DTOs/DiscountDTOs/AndDiscount';
import OrDiscount from '../../DTOs/DiscountDTOs/OrDiscount';
import XorDiscount from '../../DTOs/DiscountDTOs/XorDiscount';
import PurchasePolicy from '../../DTOs/PurchaseDTOs/PurchasePolicy';
import Restriction from '../../DTOs/PurchaseDTOs/Restriction';
import AndPolicy from '../../DTOs/PurchaseDTOs/AndPolicy';
import OrPolicy from '../../DTOs/PurchaseDTOs/OrPolicy';

export default function LogicalPurchaseForm({store,purchasesAdder, restrictions}: {store : Store,purchasesAdder:(purchase:PurchasePolicy)=>void,restrictions:Map<number,Restriction>}) {
  const [selectedId1, setId1] = React.useState(-1);
  const [logOp, setLogOp] = React.useState('');
  const [selectedId2, setId2] = React.useState(-1);
  const logOps = ['AND','OR']
  return ( 
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Logical Discount
      </Typography>
      <Grid container  justifyContent="left" spacing={-100}>
      <Grid item xs={4}>
      <InputLabel id="demo-simple-select-standard-label">First Policy Id</InputLabel>
      <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
        <Select
        labelId="demo-simple-select-label"
        id="demo-simple-select1"
        type="number"
        label="Discount Id"
        onChange={(event: SelectChangeEvent) => {setId1(parseInt(event.target.value))}}
      >

        {Array.from( restrictions ).map(([id, _]) => (
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
       <InputLabel id="demo-simple-select-standard-label">Second Policy Id</InputLabel>
       <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
       <Select
        labelId="demo-simple-select-label"
        id="demo-simple-select"
        type="number"
        label="Discount Id 2"
        onChange={(event: SelectChangeEvent) => {setId2(parseInt(event.target.value))}}
      >

        {Array.from( restrictions ).map(([id, _]) => (
           <MenuItem value={id}>{id}</MenuItem>
        ))}
       </Select>
          </FormControl>
          </Grid>
          </Grid>
          <Box textAlign='center'>
             <Button onClick={()=>{purchasesAdder(
               logOp==='AND'?
             new AndPolicy(restrictions.get(selectedId1)??new Restriction(''),restrictions.get(selectedId2)??new Restriction('')):
             new OrPolicy(restrictions.get(selectedId1)??new Restriction(''),restrictions.get(selectedId2)??new Restriction('')))}} variant="contained"  disabled={selectedId1==-1 ||logOp==''|| selectedId2==-1}>add to policies pool</Button>
             </Box>
    </React.Fragment>
  );
}