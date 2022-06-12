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
  const printDate = (date:Date)=>{
    const components = date.toString().substring(0,10).split('-');
    return components[2]+'/'+components[1]+'/'+components[0];
  }
  return (
    <div>
      <Card elevation={10} style={{
                      maxHeight: '8vw',
                      minHeight: '8vw',
                      width: '100%',
                      margin: 'auto',
                      padding: '10px',
                      backgroundColor: '#a2cf6e',
                      }}>
        <CardHeader
          title= { `Total: ${purchase.purchasePrice} ₪`}
          subheader={printDate(purchase.purchaseDate)}
        />
        <CardContent>
          {/* <Typography variant="body2" color="textSecondary">
            { `Description: ${purchase.purchaseDescription}`}
          </Typography> */}
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
          {`Total of:    ${purchase.purchasePrice} ₪`}
          </DialogTitle>
          <DialogContent>
            <DialogContentText id="alert-dialog-description">
              {`Description:    ${purchase.purchaseDescription}`}
            </DialogContentText>
            <DialogContentText id="alert-dialog-description">
              {`Date:    ${printDate(purchase.purchaseDate)}`}
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