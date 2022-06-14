import * as React from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import Purchase from "../../DTOs/Purchase";
import { Box, Container, Grid } from "@mui/material";
import PurchaseCard from "./PurcaseCard";
import {
  serverGetAMemberInfo,
  serverGetBuyerPurchaseHistory,
} from "../../services/AdminService";
import { getBuyerId } from "../../services/SessionService";
import { fetchResponse } from "../../services/GeneralService";
import Member from "../../DTOs/Member";
import MemberCard from "./MemberCard";
import MemberCardDialog from "./MemberCardDialog";

export default function DisplayMemberAccount() {
  const [open, setOpen] = React.useState<boolean>(false);
  const [member, setMember] = React.useState<Member | null>(null);

  const handleClickOpen = () => {
    console.log("opened");
    setOpen(true);
  };
  const handleSearch = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    console.log("in search");
    const buyerId = getBuyerId();
    const responsePromise = serverGetAMemberInfo(
      buyerId,
      Number(data.get("id"))
    );
    console.log(responsePromise);
    fetchResponse(responsePromise)
      .then((member) => {
        setMember(member);
      })
      .catch((e) => {
        alert(e);
        setOpen(false);
      });
  };

  const handleClose = () => {
    console.log("closed");
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
        Display a member account
      </Button>
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Search a member account</DialogTitle>
        <DialogContent style={{ overflow: "hidden" }}>
          <DialogContentText>
            Please enter the member id of the member:
          </DialogContentText>
          <form id='myform' onSubmit={handleSearch}>
            <TextField
              autoFocus
              margin='dense'
              id='id'
              name='id'
              label='member id'
              type='number'
              fullWidth
              variant='standard'
            />
            <Box textAlign='center'>
              <Button type='submit'>Search</Button>
            </Box>
          </form>
        </DialogContent>
        <Container style={{ maxHeight: "5%", overflow: "auto" }}>
          <Grid container spacing={3}>
            {member == null ? (
              <Grid item xs={12} md={6} lg={4}></Grid>
            ) : (
              <Grid item xs={12} md={6} lg={11.3}>
                <MemberCard member={member} />
              </Grid>
            )}
          </Grid>
        </Container>
        <DialogActions>
          <Button onClick={handleClose}>Close</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
