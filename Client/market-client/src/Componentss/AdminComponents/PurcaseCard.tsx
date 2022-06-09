import { Box, Button, Card, CardContent, CardHeader, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Typography } from '@mui/material'
import React from 'react';
import Purchase from "../../DTOs/Purchase"

export default function PurchaseCard({purchase}: {purchase: Purchase}) {
  const [open, setOpen] = React.useState(false);
  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };
  return (
    <div>
      <Card elevation={1}>
        <CardHeader
          title={ `Buyer id: ${purchase.buyerId}`}
          subheader={purchase.purchaseDate.toDateString()}
        />
        <CardContent>
          {/* <Typography variant="body2" color="textSecondary">
            { `Description: ${purchase.purchaseDescription}`}
          </Typography> */}
          <Typography variant="body2" color="textSecondary">
          { `Total: ${purchase.purchasePrice} ₪`}
          </Typography>
          <Box textAlign='center'>
          <Button  onClick={handleClickOpen}>
              detailes
          </Button>
          </Box>
          <Dialog
          open={open}
          onClose={handleClose}
          aria-labelledby="alert-dialog-title"
          aria-describedby="alert-dialog-description"
          fullWidth
          maxWidth="sm"
        >
          <DialogTitle id="alert-dialog-title">
            {`purchase made by buyer with id:  ${purchase.buyerId}`}
          </DialogTitle>
          <DialogContent>
            <DialogContentText id="alert-dialog-description">
              {`Description:    ${purchase.purchaseDescription}`}
            </DialogContentText>
            <DialogContentText id="alert-dialog-description">
              {`Date:    ${purchase.purchaseDate}`}
            </DialogContentText>
            <DialogContentText id="alert-dialog-description">
              {`Total of:    ${purchase.purchasePrice} ₪`}
            </DialogContentText>
          </DialogContent>
          <DialogActions>
            <Button onClick={handleClose} autoFocus>
              Close
            </Button>
          </DialogActions>
        </Dialog>
        </CardContent>
      </Card>
    </div>
  )
}