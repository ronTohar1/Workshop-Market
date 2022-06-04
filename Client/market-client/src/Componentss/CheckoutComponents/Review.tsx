import * as React from 'react';
import Typography from '@mui/material/Typography';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import Grid from '@mui/material/Grid';
import CheckoutDTO from '../../DTOs/CheckoutDTO';
import Product from '../../DTOs/Product';



export default function Review({checkout, productsAmounts}: {checkout:CheckoutDTO,productsAmounts:Map<Product,number> }) {
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
        {Array.from( productsAmounts ).map(([product, amount]) => (
          <ListItem key={product.name} sx={{ py: 1, px: 0 }}>
            <ListItemText primary={`${product.name} ${amount>1?` X ${amount}`:""}`} secondary={product.storeName} />
            <Typography variant="body2">{`${product.price*amount} ₪`}</Typography>
          </ListItem>
        ))}
         <ListItem sx={{ py: 1, px: 0 }}>
          <ListItemText primary="Shipping" />
          <Typography variant="subtitle1" sx={{ fontWeight: 700 }}>
          Free
          </Typography>
        </ListItem>
        <ListItem sx={{ py: 1, px: 0 }}>
          <ListItemText primary="Total" />
          <Typography variant="subtitle1" sx={{ fontWeight: 700 }}>
          {`${Array.from( productsAmounts ).reduce((sum, [product, amount])=>sum+(product.price*amount),0)} ₪`}
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