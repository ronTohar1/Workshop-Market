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
import { Container, DialogActions, DialogContent, DialogTitle, Grid, InputLabel, TextField } from '@mui/material';
import { serverAddBid, serverAddProductReview, serverGetProductReview } from '../../services/StoreService';
import { fetchResponse } from "../../services/GeneralService"
import { getBuyerId } from '../../services/SessionService';
import RateReviewIcon from '@mui/icons-material/RateReview';
import Bid from '../../DTOs/Bid';
import { Currency } from '../../Utils';
import CurrencyExchangeIcon from '@mui/icons-material/CurrencyExchange';
import FailureSnackbar from '../Forms/FailureSnackbar';
import SuccessSnackbar from '../Forms/SuccessSnackbar';

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

export default function ProductBidForm({ product }: { product: Product }) {
  const [open, setOpen] = React.useState(false);
  const [price, setPrice] = React.useState<number | null>(0);

  //------------------------------
  const [openFailSnack, setOpenFailSnack] = React.useState<boolean>(false)
  const [failureProductMsg, setFailureProductMsg] = React.useState<string>("")
  const [openSuccSnack, setOpenSuccSnack] = React.useState<boolean>(false)
  const [successProductMsg, setSuccessProductMsg] = React.useState<string>("")
  const showSuccessSnack = (msg: string) => {
    setOpenSuccSnack(true)
    setSuccessProductMsg(msg)
  }

  const showFailureSnack = (msg: string) => {
    setOpenFailSnack(true)
    setFailureProductMsg(msg)
  }
  //------------------------------


  const handleClickOpen = () => {
    setOpen(true);
  }
  const handleSubmit = () => {
    if (price === undefined || price === null) {
      showFailureSnack("Please enter price")
      return;
    }
    const buyerId = getBuyerId()
    const responsePromise = serverAddBid(product.storeId, product.id, buyerId, price)
    fetchResponse(responsePromise).then((succedded) => {
      showSuccessSnack(`${product.name} bid successfully made. (${price} ${Currency})`)
      setOpen(false)
    })
      .catch((e) => {
        showFailureSnack(e)
        setOpen(false)
      })
  }

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <div>
      <Button onClick={handleClickOpen} variant="contained" color="warning" endIcon={<CurrencyExchangeIcon />}>
        Bid
      </Button>
      <Dialog open={open} onClose={handleClose} fullWidth>
        <DialogTitle>Add Product</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            id="bid-price"
            label="Bid Price"
            type="number"
            value={price}
            onChange={(e: any) => setPrice(e.target.value)}
            fullWidth
            variant="standard"
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button onClick={handleSubmit} >
            Submit
          </Button>
        </DialogActions>
      </Dialog>

      <Dialog open={openFailSnack}>
        {FailureSnackbar(failureProductMsg, openFailSnack, () =>
          setOpenFailSnack(false)
        )}
      </Dialog>
      {SuccessSnackbar(successProductMsg, openSuccSnack, () =>
        setOpenSuccSnack(false)
      )}
    </div>
  );
}
