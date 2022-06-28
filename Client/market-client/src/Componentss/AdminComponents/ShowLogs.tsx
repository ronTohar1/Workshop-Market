import { BottomNavigation, BottomNavigationAction, Box, Button, Card, CardContent, CardHeader, Container, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Grid, IconButton, Slide, Typography } from '@mui/material'
import React from 'react';
import { serverGetErrorLogs,serverGetEventLogs } from '../../services/AdminService';
import { serverGetLoggedInMembers } from '../../services/AdminService';
import { fetchResponse } from '../../services/GeneralService';
import { getBuyerId } from '../../services/SessionService';
import MemberCard from './MemberCard';
import { useNavigate } from 'react-router-dom';
import Member from "../../DTOs/Member";
import { pathAdmin } from '../../Paths';
import { TransitionProps } from '@mui/material/transitions';
import CloseIcon from '@mui/icons-material/Close';

// export const membersDummy= [
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
  // ]
  const Transition = React.forwardRef(function Transition(
    props: TransitionProps & {
      children: React.ReactElement;
    },
    ref: React.Ref<unknown>,
  ) {
    return <Slide direction="up" ref={ref} {...props} />;
  });
export default function ShowLogs() {
    
    const navigate = useNavigate()
    const [open, setOpen] = React.useState<boolean>(false);
    const [window, setWindow] = React.useState<number>(1);
    const [logs, setLogs] = React.useState<string>('');

    const setLogsError = ()=>{
      const buyerId = getBuyerId()
      fetchResponse(serverGetErrorLogs(buyerId))
        .then((log) => {
          setLogs(log)
        })
        .catch((e) => {
          setLogs(e)
        })
  }

  const setLogsEvent = ()=>{
      const buyerId = getBuyerId()
      fetchResponse(serverGetEventLogs(buyerId))
        .then((log) => {
          setLogs(log)
        })
        .catch((e) => {
          setLogs(e)
        })
  }

    const handleClickOpen = () => {
      setOpen(true);
      setLogsEvent()
    };
  
    const handleClose = () => {
      setOpen(false);
      navigate(pathAdmin);
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
          Display Logs
        </Button>
        </Box>
        <Dialog
        fullScreen
        sx={{marginTop:5}}
        open={open}
        onClose={handleClose}
        TransitionComponent={Transition}
      >
          <IconButton
              edge="end"
              color="inherit"
              onClick={handleClose}
              aria-label="close"
            >
              <CloseIcon />
            </IconButton>
         <BottomNavigation
        showLabels
        value={window}
        onChange={(event, newValue) => {
            setWindow(newValue);
            if (newValue==1)
                setLogsEvent();
            else 
                setLogsError();
        }}
        >
        <BottomNavigationAction label="Events" value={1}/>
        <BottomNavigationAction label="Error"  value={2}/>
        </BottomNavigation>
        <DialogContent>
        <Container style={{ minHeight: '32vw', maxHeight: '32vw', width:'100%',overflow: 'auto' }} >
                 <Grid  sx={{ width: 1}}>
                  {logs}
                </Grid>
        </Container>

        </DialogContent>
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