import * as React from 'react';
import Typography from '@mui/material/Typography';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import Grid from '@mui/material/Grid';
import CheckoutDTO from '../../DTOs/CheckoutDTO';
import Product from '../../DTOs/Product';
import CheckoutDTOBid from '../../DTOs/CheckoutDTOBid';
import { Currency } from '../../Utils';



export default function Review({checkout}: {checkout:CheckoutDTOBid}) {
  const payments = [
    { name: 'Card type', detail: 'Visa' },
    { name: 'Card holder', detail: checkout.nameOnCard },
    { name: 'Card number', detail: checkout.cardNumber },
    { name: 'Expiry date', detail: checkout.month+'/'+checkout.year },
  ];
  const addresses = [checkout.address, checkout.city, checkout.zip, checkout.country];
  return (
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Order summary
      </Typography>
      <List disablePadding>
        <ListItem sx={{ py: 1, px: 0 }}>
          <ListItemText primary={checkout.bid?.description} />
          <Typography variant="subtitle1" sx={{ fontWeight: 700 }}>
          {`${checkout.bid?.bid} (${Currency})`}
          </Typography>
        </ListItem>
         <ListItem sx={{ py: 1, px: 0 }}>
          <ListItemText primary="Shipping" />
          <Typography variant="subtitle1" sx={{ fontWeight: 700 }}>
          Free
          </Typography>
        </ListItem>
        <ListItem sx={{ py: 1, px: 0 }}>
          <ListItemText primary="Total" />
          <Typography variant="subtitle1" sx={{ fontWeight: 700 }}>
          {`${checkout.bid?.bid} (${Currency})`}
          </Typography>
        </ListItem>
      </List>
      <Grid container spacing={2}>
        <Grid item xs={12} sm={6}>
          <Typography variant="h6" gutterBottom sx={{ mt: 2 }}>
            Shipping
          </Typography>
          <Typography gutterBottom>{checkout.firstName+" "+checkout.lastName}</Typography>
          <Typography gutterBottom>{addresses.join(', ')}</Typography>
        </Grid>
        <Grid item container direction="column" xs={12} sm={6}>
          <Typography variant="h6" gutterBottom sx={{ mt: 2 }}>
            Payment details
          </Typography>
          <Grid container>
            {payments.map((payment) => (
              <React.Fragment key={payment.name}>
                <Grid item xs={6}>
                  <Typography gutterBottom>{payment.name}</Typography>
                </Grid>
                <Grid item xs={6}>
                  <Typography gutterBottom>{payment.detail}</Typography>
                </Grid>
              </React.Fragment>
            ))}
          </Grid>
        </Grid>
      </Grid>
    </React.Fragment>
  );
}