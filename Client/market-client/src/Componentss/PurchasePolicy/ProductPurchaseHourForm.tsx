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
import Purchase from '../../DTOs/PurchaseDTOs/PurchasePolicy';
import AfterHourProduct from '../../DTOs/PurchaseDTOs/AfterHourProduct';
import BeforeHourProduct from '../../DTOs/PurchaseDTOs/BeforeHourProduct';

export default function ProductPurchaseHourForm({store,purchasesAdder}: {store : Store,purchasesAdder:(purchase:Purchase)=>void}) {
  const [logOp, setLogOp] = React.useState('');
  const [hour, setHour] = React.useState(-1);
  const [selectedId, setId] = React.useState(-1);
  const [amount, setAmount] = React.useState(-1);
 
  const logOps = ['Before','After']
  return ( 
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Product Max Amount By Hour Restrictions 
      </Typography>
      <Grid container  justifyContent="left" spacing={-100}>
       <Grid item xs={4}>
       <InputLabel id="demo-simple-select-standard-label">Before/After</InputLabel>
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
       <InputLabel id="demo-simple-select-standard-label">Hour</InputLabel>
       <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
       <Select
        labelId="demo-simple-select-label"
        id="demo-simple-select"
        label="Discount Id 2"
        onChange={(event: SelectChangeEvent) => {setHour(parseInt(event.target.value))}}
      >
        <MenuItem value={'08'}>08:00</MenuItem>
        <MenuItem value={'09'}>09:00</MenuItem>
        <MenuItem value={'10'}>10:00</MenuItem>
        <MenuItem value={'11'}>11:00</MenuItem>
        <MenuItem value={'12'}>12:00</MenuItem>
        <MenuItem value={'13'}>13:00</MenuItem>
        <MenuItem value={'14'}>14:00</MenuItem>
        <MenuItem value={'15'}>15:00</MenuItem>
        <MenuItem value={'16'}>16:00</MenuItem>
        <MenuItem value={'17'}>17:00</MenuItem>
        <MenuItem value={'18'}>18:00</MenuItem>
        <MenuItem value={'19'}>19:00</MenuItem>
        <MenuItem value={'20'}>20:00</MenuItem>
        <MenuItem value={'21'}>21:00</MenuItem>
        <MenuItem value={'22'}>22:00</MenuItem>
        <MenuItem value={'23'}>23:00</MenuItem>
        <MenuItem value={'00'}>00:00</MenuItem>
        <MenuItem value={'01'}>01:00</MenuItem>
        <MenuItem value={'02'}>02:00</MenuItem>
        <MenuItem value={'03'}>03:00</MenuItem>
        <MenuItem value={'04'}>04:00</MenuItem>
        <MenuItem value={'05'}>05:00</MenuItem>
        <MenuItem value={'06'}>06:00</MenuItem>
        <MenuItem value={'07'}>07:00</MenuItem>
       
       </Select>
          </FormControl>
          </Grid>
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
       <InputLabel id="demo-simple-select-standard-label">Max Amount</InputLabel>
       <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
       <TextField
            required
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => {setAmount(parseInt(e.currentTarget.value));}} 
            id="address"
            name="number"
            type="number"
            label="max amount"
            fullWidth/>
          </FormControl>
          </Grid>
          </Grid>
          <Box textAlign='center'>
             <Button onClick={()=>{purchasesAdder(
               logOp==='Before'?
             new BeforeHourProduct(hour,store.products.find(p=>p.id==selectedId)?.name,selectedId,amount):
             new AfterHourProduct(hour,store.products.find(p=>p.id==selectedId)?.name,selectedId,amount))}} disabled={hour==-1 || amount==-1 || selectedId==-1|| logOp==''}>add to policies pool</Button>
             </Box>
    </React.Fragment>
  );
}