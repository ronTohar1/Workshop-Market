import * as React from "react"
import Button from "@mui/material/Button"
import Dialog from "@mui/material/Dialog"
import ListItemText from "@mui/material/ListItemText"
import ListItem from "@mui/material/ListItem"
import List from "@mui/material/List"
import Divider from "@mui/material/Divider"
import AppBar from "@mui/material/AppBar"
import Toolbar from "@mui/material/Toolbar"
import IconButton from "@mui/material/IconButton"
import Typography from "@mui/material/Typography"
import CloseIcon from "@mui/icons-material/Close"
import Slide from "@mui/material/Slide"
import { TransitionProps } from "@mui/material/transitions"
import Product from "../../DTOs/Product"
import Member from "../../DTOs/Member"
import {
  Container,
  DialogActions,
  DialogContent,
  DialogTitle,
  Grid,
  InputLabel,
  TextField,
} from "@mui/material"
import {
  serverAddProductReview,
  serverGetProductReview,
} from "../../services/StoreService"
import { fetchResponse } from "../../services/GeneralService"
import { getBuyerId } from "../../services/SessionService"
import RateReviewIcon from "@mui/icons-material/RateReview"
import { Currency, makeSetStateFromEvent } from "../../Utils"

const Transition = React.forwardRef(function Transition(
  props: TransitionProps & {
    children: React.ReactElement
  },
  ref: React.Ref<unknown>
) {
  return <Slide direction="up" ref={ref} {...props} />
})

export default function CounterOfferForm({
  isDisabled,
  handleCounterOffer,
}: {
  handleCounterOffer: (offer: number) => void
  isDisabled: boolean

}) {
  const [open, setOpen] = React.useState(false)
  const [offer, setOffer] = React.useState<number>(0)

  const handleClickOpen = () => {
    setOpen(true)
  }
  const handleSubmit = () => {
    handleCounterOffer(offer)
    setOpen(false)
  }

  const handleClose = () => {
    setOpen(false)
  }

  const makeTextField = (
    id: string,
    label: string,
    value: string | number,
    type: "text" | "number",
    setValue: any
  ) => {
    return (
      <TextField
        autoFocus
        margin="dense"
        id={id}
        label={label}
        type={type}
        value={value}
        onChange={makeSetStateFromEvent(setValue)}
        fullWidth
        variant="standard"
      />
    )
  }

  return (
    <div>
      <Button
        onClick={handleClickOpen}
        variant="contained"
        color="success"
        disabled={isDisabled}
        endIcon={<RateReviewIcon />}
      >
        Make Counter Offer
      </Button>
      <Dialog open={open} onClose={handleClose} fullWidth>
        <DialogTitle>Add Product</DialogTitle>
        <DialogContent>
          {makeTextField(
            "What is your offer?",
            "What is your offer?",
            offer,
            "number",
            setOffer
          )}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button onClick={handleSubmit}>Submit</Button>
        </DialogActions>
      </Dialog>
    </div>
  )
}
