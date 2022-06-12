import * as React from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import ListItemText from '@mui/material/ListItemText';
import ListItem from '@mui/material/ListItem';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import CloseIcon from '@mui/icons-material/Close';
import Slide from '@mui/material/Slide';
import { TransitionProps } from '@mui/material/transitions';
import Product from '../DTOs/Product';
import Member from '../DTOs/Member';
import { Container, InputLabel, TextField } from '@mui/material';
import { serverAddProductReview, serverGetProductReview } from '../services/StoreService';
import { fetchResponse } from "../services/GeneralService"
import { getBuyerId } from '../services/SessionService';

const Transition = React.forwardRef(function Transition(
  props: TransitionProps & {
    children: React.ReactElement;
  },
  ref: React.Ref<unknown>,
) {
  return <Slide direction="up" ref={ref} {...props} />;
});
const dummy = new Map<Member, string[]>();
dummy.set(new Member(0, "ronto", true), ["Great quality", "I like cats", "Shawarma"])
dummy.set(new Member(0, "David", true), ["Abale", "Shawarma","Abale", "Shawarma","Abale", "Shawarma"])

export default function ProductReview({product}: {product:Product}) {
  const [open, setOpen] = React.useState(false);
  const [newComment, setNewComment] = React.useState('');
  const [comments, setComments] = React.useState<Map<Member, string[]>>(dummy);

  const refreshReviews = ()=>{
    const responsePromise = serverGetProductReview(product.storeId,product.id)
    console.log(responsePromise)
    fetchResponse(responsePromise).then((succedded)=>{
      console.log(succedded)
      setComments(succedded)
    })
    .catch((e) => {
      alert(e)
      setOpen(false)
     })
  }
  const handleClickOpen = () => {
    setOpen(true);
    refreshReviews();
  };
  const handleSubmit = ()=>{
    const buyerId = getBuyerId()
    const responsePromise = serverAddProductReview(product.storeId,buyerId,product.id,newComment)
    console.log(responsePromise)
    fetchResponse(responsePromise).then((succedded)=>{
      console.log(succedded)
    })
    .catch((e) => {
      alert(e)
      setOpen(false)
     })
    refreshReviews();
  }

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <div>
       <Button variant="outlined" onClick={handleClickOpen}>
        products Reviews
      </Button>
      <Dialog
        fullScreen
        open={open}
        onClose={handleClose}
        TransitionComponent={Transition}
      >
       <AppBar sx={{ position: "relative" }}>
          <Toolbar>
            <Typography sx={{ ml: 2, flex: 1 }} variant="h6" component="div">
              {`Product: ${product.name}`}
            </Typography>
            <IconButton
              edge="end"
              color="inherit"
              onClick={handleClose}
              aria-label="close"
            >
              <CloseIcon />
            </IconButton>
          </Toolbar>
        </AppBar>
        <Container style={{minHeight: '32vw',maxHeight: '32vw', overflow: 'auto'}} >
        <List>
        {Array.from( comments ).map(([member, reviews]) => 
           reviews.map(review=>(  
           <ListItem >
            <ListItemText primary={`Member: ${member.userName}`} secondary={`Review:       ${review}`} />
          </ListItem>))
          )}
        </List>
        </Container>
        <Container style={{maxHeight: '100%',maxWidth: '100%', overflow: 'auto'}} >

        <InputLabel id="demo-simple-select-standard-label">Give us your review!</InputLabel>
        <TextField
          style={{ textAlign: "left" }}
          multiline
          onChange={(e: React.ChangeEvent<HTMLInputElement>) => {setNewComment(e.currentTarget.value);}} 
          id="comment"
          margin="normal"
          fullWidth // this may override your custom width
          rows={3}
        />
        <Button onClick={handleSubmit} disabled={newComment==''}>submit</Button>
        </Container>
      </Dialog>
    </div>
  );
}
