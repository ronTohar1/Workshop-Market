import { Box, Button, Card, CardContent, CardHeader, Container, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Grid, Typography } from '@mui/material'
import React from 'react';
import { serverGetLoggedInMembers } from '../../services/AdminService';
import { fetchResponse } from '../../services/GeneralService';
import { getBuyerId } from '../../services/SessionService';
import MemberCard from './MemberCard';
import { useNavigate } from 'react-router-dom';
import Member from "../../DTOs/Member";
import { pathAdmin } from '../../Paths';

export const membersDummy= [
    // new Member(0,"Nir",true),
    // new Member(1,"Ronto",true),
    // new Member(2,"Roi",false),
    // new Member(3,"Idan",false),
    // new Member(4,"Zivan",false),
    // new Member(5,"David",true),
    // new Member(6,"Nir",true),
    // new Member(7,"Ronto",true),
    // new Member(8,"Roi",false),
    // new Member(9,"Idan",false),
    // new Member(10,"Zivan",false),
    // new Member(11,"David",true),
    // new Member(12,"Nir",true),
    // new Member(13,"Ronto",true),
    // new Member(14,"Roi",false),
    // new Member(15,"Idan",false),
    // new Member(16,"Zivan",false),
    // new Member(17,"David",true),
  ]

export default function ShowLoggedInMembers() {
    
    const navigate = useNavigate()
    const [open, setOpen] = React.useState<boolean>(false);
    const [wasOpened, setWasOpened] = React.useState<boolean>(false);
    const [members,setMembers] = React.useState<Member[]>(membersDummy);
    
    const handleClickOpen = () => {
      console.log("opened")
      setOpen(true);
    };
  
    const handleClose = () => {
      console.log("closed")
      setOpen(false);
      navigate(pathAdmin);
      setWasOpened(false);
    };

    React.useEffect(() =>{
      if (!wasOpened && open){
        const buyerId = getBuyerId()
        const responsePromise = serverGetLoggedInMembers(buyerId)
        fetchResponse(responsePromise).then((newMembers)=>{
          console.log(newMembers)
          setMembers(newMembers)
          setWasOpened(true);
        })
        .catch((e) => {
          alert(e)
          setOpen(false)
          })
      }
       },[open,members])
  
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
          <DialogTitle>Display logged in members</DialogTitle>
          {/* <DialogContent style={{ overflow: "hidden" }} >
            <DialogContentText>
            Display logged in members
            </DialogContentText>
          </DialogContent> */}
          <Container style={{maxHeight: '5%', overflow: 'auto'}}>
        <Grid container spacing={3} >
          {members.map(currMember => (
             <Grid style={{maxWidth: '100%'}} item xs={20} md={24} lg={11}>
                <MemberCard member={currMember}/>
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
// </DialogContent>
// <Container style={{maxHeight: '5%', overflow: 'auto'}} >
//   {member==memberDummy? 
//   (<Grid item xs={12} md={6} lg={4}>
//     </Grid>):
//   (<Grid item xs={12} md={6} lg={4}>
//   <MemberCard member={member}/>
//   </Grid>)
//   }

// </Container>
// <DialogActions>