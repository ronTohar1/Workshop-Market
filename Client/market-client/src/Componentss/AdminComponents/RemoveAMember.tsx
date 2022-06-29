import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import { Box } from '@mui/material';
import { fetchResponse } from '../../services/GeneralService';
import { getBuyerId } from '../../services/SessionService';
import { serverRemoveMember } from '../../services/AdminService';

export default function RemoveAMember() {
  const [open, setOpen] = React.useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };
  
  const handleSearch = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    const data = new FormData(event.currentTarget)
    console.log("in search id")
    const buyerId = getBuyerId()
    const responsePromise = serverRemoveMember(buyerId,Number(data.get("id")))
    console.log(responsePromise)
    fetchResponse(responsePromise).then((succedded)=>{
      console.log(succedded)
      alert("Member was removed successfully!")
    })
    .catch((e) => {
      alert(e)
      setOpen(false)
     })
   
  };
  return (
    <div>
       <Box textAlign='center'>
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
          Remove a member
        </Button>
        </Box>
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Remove a member</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Please enter the id of the member you wish to remove:
          </DialogContentText>
           <form id="myform" onSubmit={handleSearch} >
          <TextField
            autoFocus
            margin="dense"
            id="id"
            name="id"
            label="member id"
            type="number"
            fullWidth
            variant="standard"
          />
           <Box textAlign='center'>
             <Button type="submit">Remove</Button>
             </Box>
          </form>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
