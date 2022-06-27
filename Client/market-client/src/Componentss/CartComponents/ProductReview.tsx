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
import Product from '../../DTOs/Product';
import Member from '../../DTOs/Member';
import { Container, Grid, InputLabel, TextField } from '@mui/material';
import { serverAddProductReview, serverGetProductReview } from '../../services/StoreService';
import { fetchResponse } from "../../services/GeneralService"
import { getBuyerId } from '../../services/SessionService';
import RateReviewIcon from '@mui/icons-material/RateReview';


const Transition = React.forwardRef(function Transition(
  props: TransitionProps & {
    children: React.ReactElement;
  },
  ref: React.Ref<unknown>,
) {
  return <Slide direction="up" ref={ref} {...props} />;
});
// const dummy = new Map<string, string[]>();
// dummy.set("ronto", ["Great quality", "I like cats", "Shawarma"])
// dummy.set("David", ["Abale", "Shawarma","Abale", "Shawarma","Abale", "Shawarma"])

export default function ProductReview({ product }: { product: Product }) {
  const [open, setOpen] = React.useState(false);
  const [newComment, setNewComment] = React.useState('');
  const [comments, setComments] = React.useState<Map<string, string[]>>(new Map<string, string[]>());

  const refreshReviews = () => {
    const responsePromise = serverGetProductReview(product.storeId, product.id)
    console.log(responsePromise)
    fetchResponse(responsePromise).then((succeed: any) => {
      const commentsMap = new Map<string, string[]>();
      Object.keys(succeed).forEach(name => commentsMap.set(name, succeed[name]))
      setComments(commentsMap)
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
  const handleSubmit = () => {
    const buyerId = getBuyerId()
    const responsePromise = serverAddProductReview(product.storeId, buyerId, product.id, newComment)
    fetchResponse(responsePromise).then((succedded) => {
      refreshReviews()
    })
      .catch((e) => {
        alert(e)
        setOpen(false)
      })
  }

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <div>
      <Button onClick={handleClickOpen} variant="contained" color="success" endIcon={<RateReviewIcon />}>
        View Reviews
      </Button>
      <Dialog
        fullScreen
        sx={{marginTop:5}}
        open={open}
        onClose={handleClose}
        TransitionComponent={Transition}
      >
        <AppBar sx={{ position: "relative" }}>
          <Toolbar>
            <Typography sx={{ ml: 2, flex: 1 }} variant="h6" component="div">
              {`${product.name} Reviews`}
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
        <Container style={{ minHeight: '32vw', maxHeight: '32vw',overflow: 'auto' }} >
          <Grid container spacing={3} sx={{mt:5}} >
            {Array.from(comments).map(([member, reviews]) =>
              reviews.map(review => (
                <Grid item xs={12} md={11.7} sx={{borderBottom:0.5}}>
                  <ListItemText primary={`Member: ${member}`} secondary={`Review:       ${review}`} />
                </Grid>))
            )}
          </Grid>
        </Container>
        <Container style={{ maxHeight: '100%', maxWidth: '100%', overflow: 'auto' }} >

          <InputLabel id="demo-simple-select-standard-label">Give us your review!</InputLabel>
          <TextField
            style={{ textAlign: "left" }}
            multiline
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => { setNewComment(e.currentTarget.value); }}
            id="comment"
            margin="normal"
            fullWidth // this may override your custom width
            rows={3}
          />
          <Button onClick={handleSubmit} variant="contained" disabled={newComment == ''}>submit</Button>
        </Container>
      </Dialog>
    </div>
  );
}
