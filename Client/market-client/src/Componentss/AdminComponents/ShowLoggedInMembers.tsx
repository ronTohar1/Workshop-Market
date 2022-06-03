import { Box, Button, Card, CardContent, CardHeader, Container, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Grid, Typography } from '@mui/material'
import React from 'react';
import Member from "../../DTOs/Member";
import { serverGetLoggedInMembers } from '../../services/AdminService';
import { fetchResponse } from '../../services/GeneralService';
import { getBuyerId } from '../../services/SessionService';
import MemberCard from './MemberCard';

export const membersDummy= [
    new Member(0,"Nir",true),
    new Member(1,"Ronto",true),
    new Member(2,"Roi",false),
    new Member(3,"Idan",false),
    new Member(4,"Zivan",false),
    new Member(5,"David",true),
  ]

export default function ShowLoggedInMembers() {
    
    
    const [open, setOpen] = React.useState<boolean>(false);
    const [members,setMembers] = React.useState<Member[]>(membersDummy);
    
    React.useEffect(() =>{
      const buyerId = getBuyerId()
      const responsePromise = serverGetLoggedInMembers(buyerId)
      fetchResponse(responsePromise).then((newMembers)=>{
        setMembers(newMembers)
       })
       .catch((e) => {
         alert(e)
         setOpen(false)
        })
       },[open, members])
  
    const handleClickOpen = () => {
      
      console.log("opened")
      setOpen(true);
    };
  
    const handleClose = () => {
      console.log("closed")
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
        Display logged in members
        </Button>
        <Dialog open={open} onClose={handleClose} >
          <DialogTitle>"Display logged in members</DialogTitle>
          {/* <DialogContent style={{ overflow: "hidden" }} >
            <DialogContentText>
            Display logged in members
            </DialogContentText>
          </DialogContent> */}
          <Container style={{maxHeight: '100%', overflow: 'auto'}} >
        <Grid container spacing={3} >
          {members.map(member => (
            <Grid item xs={12} md={6} lg={4}>
                 <Card elevation={1}>
                    <CardHeader
                    title={ `User name: ${member.username}`}
                    />
                    <CardContent>
                    <Box textAlign='center'>
                    <Button  href=<MemberCard member= }}>
                        member info
                    </Button>
                    </Box>  
                    </CardContent>
                </Card>
            </Grid>
          ))}
        </Grid>
      </Container>
          <DialogActions>
            <Button onClick={handleClose}>Close</Button>
          </DialogActions>
        </Dialog>
      </div>
    );
}
 