import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import Purchase from "../../DTOs/Purchase"
import { Box, Container, Grid } from "@mui/material"
import PurchaseCard from "./PurcaseCard"
import { serverGetBuyerPurchaseHistory } from '../../services/AdminService';
import { getBuyerId } from '../../services/SessionService';
import { fetchResponse } from '../../services/GeneralService';

export const purchasesDummy= [
  new Purchase("12/12/2022",34.4, "bought 3 apples", 0),
  new Purchase("10/02/2020",12.4, "bought 2 bananas", 1),
  new Purchase("10/10/2021",10, "bought 3 apples", 0),
  new Purchase("12/12/2022",34.4, "bought 3 apples", 0),
  new Purchase("10/02/2020",12.4, "bought 2 bananas", 1),
  new Purchase("10/10/2021",10, "bought 3 apples", 0),
  new Purchase("12/12/2022",34.4, "bought 3 apples", 0),
  new Purchase("10/02/2020",12.4, "bought 2 bananas", 1),

]

export default function StorePurchaseHistoryForm() {
  
  const [open, setOpen] = React.useState<boolean>(false);
  const [purchases,setPurchases] = React.useState<Purchase[]>(purchasesDummy);
  
  React.useEffect(() =>{
    setPurchases(purchases)
     },[purchases])

  const handleClickOpen = () => {
    
    console.log("opened")
    setOpen(true);
  };
  const handleSearch = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    const data = new FormData(event.currentTarget)
    console.log("in search")
    const buyerId = getBuyerId()
    const responsePromise = serverGetBuyerPurchaseHistory(buyerId,Number(data.get("id")))
    console.log(responsePromise)
    fetchResponse(responsePromise).then((newPurchases)=>{
     setPurchases(newPurchases)
    })
    .catch((e) => {
      alert(e)
      setOpen(false)
     })
   
  };

  const handleClose = () => {
    console.log("closed")
    setOpen(false);
  };
  return (
    <div>
      <Button
        onClick={handleClickOpen}
        style={{ height: 50, width: 500 }}
        key='name'
        variant='contained'
        size='large'
        color='primary'
        sx={{
          m: 1,
          "&:hover": {
            borderRadius: 5,
          },
      }}>
      Display A store purchases
      </Button>
      <Dialog open={open} onClose={handleClose} >
        <DialogTitle>Search a store purchases</DialogTitle>
        <DialogContent style={{ overflow: "hidden" }} >
          <DialogContentText>
            Please enter the store id of the store:
          </DialogContentText>
          <form id="myform" onSubmit={handleSearch} >
          <TextField
            autoFocus
            margin="dense"
            id="id"
            label="Store id"
            type="number"
            fullWidth
            variant="standard"
          />
           <Box textAlign='center'>
             <Button type="submit">Search</Button>
             </Box>
          </form>
        </DialogContent>
        <Container style={{maxHeight: '5%', overflow: 'auto'}} >
      <Grid container spacing={3} >
        {purchases.map(purchase => (
          <Grid item xs={12} md={6} lg={4}>
            {PurchaseCard(purchase)}
          </Grid>
        ))}
      </Grid>
    </Container>
        <DialogActions>
          <Button onClick={handleClose}>Close</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
