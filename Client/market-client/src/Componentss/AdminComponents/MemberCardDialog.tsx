import { Box, Button, Card, CardContent, CardHeader, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Typography } from '@mui/material'
import React from 'react';
import { useNavigate } from 'react-router-dom';
import Member from "../../DTOs/Member";
import { pathAdmin } from '../../Paths';

export default function MemberCardDialog(member:Member) {
  const [open, setOpen] = React.useState(true);
  const handleClose = () => {
    setOpen(false);
  };
  const handleClickOpen = () => {
    setOpen(true);
  };
    return (
        <div>  
             <Dialog
              open={open}
              onClose={handleClose}
              aria-labelledby="alert-dialog-title"
              aria-describedby="alert-dialog-description"
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
                src="https://cdn-icons.flaticon.com/png/512/1144/premium/1144760.png?token=exp=1654254593~hmac=1e29ee840431931025208dadb8271b30"
              />
              </DialogTitle>
              <DialogContent>
                <DialogContentText id="alert-dialog-description">
                { `User name: ${member.userName} `}
                </DialogContentText>
                <DialogContentText id="alert-dialog-description">
                { `user id: ${member.id} `}
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