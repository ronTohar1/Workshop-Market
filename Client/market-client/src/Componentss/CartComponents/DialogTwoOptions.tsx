import {
  DialogTitle,
  Dialog,
  DialogContent,
  DialogContentText,
  DialogActions,
  Button,
  Slide,
} from "@mui/material"
import Product from "../../DTOs/Product"
import * as React from "react"
import { TransitionProps } from "@mui/material/transitions"

export default function DialogTwoOptions(
  product: Product,
  open: boolean,
  handleClose: (remove: boolean, product: Product) => void
) {
  return (
    <div>
      <Dialog
        open={open}
        TransitionComponent={Transition}
        keepMounted
        aria-describedby="confirm-remove"
      >
        <DialogTitle>{"Confirm Remove: " + product.name}</DialogTitle>
        <DialogContent>
          <DialogContentText id="confirm-remove">
            Are you sure you want to remove this item from your cart?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => handleClose(false, product)}>Cancel</Button>
          <Button onClick={() => handleClose(true, product)}>Confirm</Button>
        </DialogActions>
      </Dialog>
    </div>
  )
}

const Transition = React.forwardRef(function Transition(
  props: TransitionProps & {
    children: React.ReactElement<any, any>
  },
  ref: React.Ref<unknown>
) {
  return <Slide direction="up" ref={ref} {...props} />
})
