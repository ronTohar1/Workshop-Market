import { Box, Button, Card, CardContent, CardHeader, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Typography } from '@mui/material'
import React from 'react';
import Member from "../../DTOs/Member";

export default function PurchaseCard( {member:Member}) {
  const [open, setOpen] = React.useState(true);
  const handleClose = () => {
    setOpen(false);
  };
    return (
        <div>
              <Dialog
              open={open}
              onClose={handleClose}
              aria-labelledby="alert-dialog-title"
              aria-describedby="alert-dialog-description"
              fullWidth
              maxWidth="sm"
            >
              <DialogTitle id="alert-dialog-title">
              <Box
                component="img"
                sx={{
                  m: 4,
                  height: 233,
                  width: 175,
                  maxHeight: { xs: 233, md: 167 },
                  maxWidth: { xs: 350, md: 250 },
                }}
                alt="Admin setting"
                src="https://cdn-icons.flaticon.com/png/512/666/premium/666201.png?token=exp=1654214272~hmac=42bf1b682d1bba5b4cc43c32d36f2b48"
              />
              </DialogTitle>
              <DialogContent>
                <DialogContentText id="alert-dialog-description">
                { `User name: ${member.username} `}
                </DialogContentText>
                <DialogContentText id="alert-dialog-description">
                { `user id: ${member.loggedIn} `}
                </DialogContentText>
                <DialogContentText id="alert-dialog-description">
                { `user status: ${member.loggedIn?"logged in":"logged out"} `}
                </DialogContentText>
              </DialogContent>
              <DialogActions>
                <Button onClick={handleClose} autoFocus>
                  Close
                </Button>
              </DialogActions>
            </Dialog>
 
        </div>
    )
}