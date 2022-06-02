import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import Purchase from "../../DTOs/Purchase"
import { Container, Grid } from "@mui/material"
import PurchaseCard from "../../Componentss/AdminComponents/PurcaseCard"

let purchases: Purchase[] = [
  new Purchase("12/12/2022",34.4, "bought 3 apples", 0),
  new Purchase("10/02/2020",12.4, "bought 2 bananas", 1),
  new Purchase("10/10/2021",10, "bought 3 apples", 0),
]

export default function FormDialog(
  name:string,
  dialogTitle:string, 
  dialogContextText:string,
  textFieldLabel:string,
  purchaseFetcher:(arg0: number)=>Promise<Purchase[]>) {
  
  const [open, setOpen] = React.useState(false);
  
  const handleClickOpen = () => {
    setOpen(true);
  };
  
  const handleSearch = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    const data = new FormData(event.currentTarget)
    purchaseFetcher(Number(data.get("id")))
   
  };

  const handleClose = () => {
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
      {name}
      </Button>
      <Dialog open={open} onClose={handleClose} >
        <DialogTitle>{dialogTitle}</DialogTitle>
        <DialogContent>
          <DialogContentText>
            {dialogContextText}
          </DialogContentText>
          <form id="myform" onSubmit={handleSearch} >
          <TextField
            autoFocus
            margin="dense"
            id="id"
            label={textFieldLabel}
            type="number"
            fullWidth
            variant="standard"
          />
          </form>
        </DialogContent>
        <Container>
      <Grid container spacing={3}>
        {purchases.map(purchase => (
          <Grid item xs={12} md={6} lg={4}>
            {PurchaseCard(purchase)}
          </Grid>
        ))}
      </Grid>
    </Container>
        <DialogActions>
          <Button onClick={handleClose}>Close</Button>
          <Button type="submit">Search</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
