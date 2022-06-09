import { Box, Button, Card, CardContent, CardHeader, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Typography } from '@mui/material'
import React from 'react';
import Discount from '../../../DTOs/DiscountDTOs/Discount';
import Predicate from '../../../DTOs/DiscountDTOs/Predicate';
import Purchase from "../../../DTOs/Purchase"
import Store from '../../../DTOs/Store';

export default function PredicateCard({id,predicate, store}: {id:number, predicate: Predicate, store:Store}) {
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
          title={ `Predicate id: ${id}`}
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
            {`Predicate id:  ${id}`}
          </DialogTitle>
          <DialogContent>
            <DialogContentText id="alert-dialog-description">
              {`Description:    ${predicate.toString(store)}`}
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