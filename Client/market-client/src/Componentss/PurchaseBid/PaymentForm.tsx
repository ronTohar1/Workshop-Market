import * as React from 'react';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import CheckoutDTO from '../../DTOs/CheckoutDTO';
import CheckoutDTOBid from '../../DTOs/CheckoutDTOBid';

export default function PaymentForm({checkout}: {checkout:CheckoutDTOBid}) {
  const handleNameOnCard = (e: React.ChangeEvent<HTMLInputElement>) => {
    checkout.nameOnCard = e.currentTarget.value;
  }
  const handleCardNumber = (e: React.ChangeEvent<HTMLInputElement>) => {
    checkout.cardNumber = e.currentTarget.value;
  }
  const handleMonth = (e: React.ChangeEvent<HTMLInputElement>) => {
    checkout.month = e.currentTarget.value;
  }
  const handleYear = (e: React.ChangeEvent<HTMLInputElement>) => {
    checkout.year = e.currentTarget.value;
  }
  const handleCvv = (e: React.ChangeEvent<HTMLInputElement>) => {
    checkout.ccv = e.currentTarget.value;
  }
  const handleOwnerId = (e: React.ChangeEvent<HTMLInputElement>) => {
    checkout.id = e.currentTarget.value;
  }
  return ( 
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Payment method
      </Typography>
      <Grid container spacing={3}>
        <Grid item xs={12} md={6}>
          <TextField
            required
            onChange = {handleNameOnCard}
            id="cardName"
            label="Name on card"
            fullWidth
            autoComplete="cc-name"
            variant="standard"
          />
        </Grid>
        <Grid item xs={12} md={6}>
          <TextField
            required
            onChange = {handleCardNumber}
            id="cardNumber"
            label="Card number"
            fullWidth
            autoComplete="cc-number"
            variant="standard"
          />
        </Grid>
        <Grid item xs={12} md={3}>
          <TextField
            required
            onChange = {handleMonth}
            id="month"
            label="Expiry month"
            fullWidth
            autoComplete="cc-exp"
            variant="standard"
          />
        </Grid>
        <Grid item xs={12} md={3}>
          <TextField
            required
            onChange = {handleYear}
            id="year"
            label="Expiry year"
            fullWidth
            autoComplete="cc-exp"
            variant="standard"
          />
        </Grid>
        <Grid item xs={12} md={6}>
          <TextField
            required
            onChange = {handleCvv}
            id="cvv"
            label="CVV"
            helperText="Last three digits on signature strip"
            fullWidth
            autoComplete="cc-csc"
            variant="standard"
          />
        </Grid>
        <Grid item xs={16} >
          <TextField
            required
            onChange = {handleOwnerId}
            id="ownerId"
            label="owner id"
            fullWidth
            autoComplete="cc-csc"
            variant="standard"
          />
        </Grid>
      </Grid>
    </React.Fragment>
  );
}