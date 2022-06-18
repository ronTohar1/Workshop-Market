import * as React from 'react';
import Typography from '@mui/material/Typography';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import Grid from '@mui/material/Grid';
import CheckoutDTO from '../../DTOs/CheckoutDTO';
import Product from '../../DTOs/Product';
import CheckoutDTOBid from '../../DTOs/CheckoutDTOBid';
import { FormControl, InputLabel, MenuItem, Paper, Select, SelectChangeEvent } from '@mui/material';
import Bid from '../../DTOs/Bid';
import { getBuyerId } from '../../services/SessionService';
import { fetchResponse } from '../../services/GeneralService';
import { serverGetAllMemberBids } from '../../services/StoreService';
import BidCard from './BidCard';



export default function PurchaseBid({checkout}: {checkout:CheckoutDTOBid}) {
  const [selectedBid, setSelectedBid] = React.useState<number>(-1);
  const [bids, setBids] = React.useState<Bid[]>([]);

  React.useEffect(() => {
    // Set ProductsAmount map

    const buyerId = getBuyerId();
    fetchResponse(serverGetAllMemberBids(buyerId))
      .then((bids: Bid[]) => {
        setBids(bids);
      })
      .catch((e) => {
        alert(e);
      });
  }, []);

  return (
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Bidding Options
      </Typography>
      <Grid item xs={4}>
      <InputLabel id="demo-simple-select-standard-label">Discount Id predicate true</InputLabel>
      <FormControl sx={{ m: 1, minWidth: 240 }} id="myform">
        <Select
        required
        labelId="demo-simple-select-label"
        id="demo-simple-select1"
        type="number"
        label="Discount Id predicate true"
        onChange={(event: SelectChangeEvent) => {
            setSelectedBid(parseInt(event.target.value));
            checkout.bid= bids.find((bid)=>bid.id==selectedBid);
        }}
      >

        {Array.from( bids ).map((bid) => (
           <MenuItem value={bid.id}>{bid.id}</MenuItem>
        ))}
       </Select>  
       </FormControl>
       </Grid>
       <Grid item xs={4}>
      <Paper variant="outlined" sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}> 
      <Typography component="h1" variant="h4" align="center">
          Bids Pool
          </Typography>
       <Grid
            sx={{ overflowY: "scroll",minHeight: "200px" , maxHeight: "200px" }}
            container
            spacing={2}
            >
          {Array.from( bids ).map((bid) => (
          <Grid item xs={12} md={6} lg={4}>
            <BidCard bid={bid}/>
          </Grid>
        ))}
        </Grid>
        </Paper>
        </Grid>
    </React.Fragment>
  );
}