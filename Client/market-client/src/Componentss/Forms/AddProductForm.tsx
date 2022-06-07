import * as React from "react"
import Button from "@mui/material/Button"
import TextField from "@mui/material/TextField"
import Dialog from "@mui/material/Dialog"
import DialogActions from "@mui/material/DialogActions"
import DialogContent from "@mui/material/DialogContent"
import DialogContentText from "@mui/material/DialogContentText"
import DialogTitle from "@mui/material/DialogTitle"
import Product from "../../DTOs/Product"
import { Currency, makeSetStateFromEvent } from "../../Utils"
import { addNewProduct } from "../../services/StoreService"
import SuccessSnackbar from "./SuccessSnackbar"

export default function AddProductForm({
  handleAddProduct,
}: {
  handleAddProduct: (product: Product) => void
}) {
  const [open, setOpen] = React.useState(false)
  const [name, setName] = React.useState("")
  const [category, setCategory] = React.useState("")
  const [quantity, setQuantity] = React.useState(0)
  const [price, setPrice] = React.useState(0)
  const [openSnack, setOpenSnack] = React.useState(false)
  const [x, setx] = React.useState<number>(100) // TODO : DELETE THIS X

  const resetFields = () => {
    setOpen(false)
    setName("")
    setCategory("")
    setQuantity(0)
    setPrice(0)
  }

  const handleCloseSnack = () => {
    setOpenSnack(false)
  }

  const handleClickOpen = () => {
    setOpen(true)
  }
  const handleClose = () => {
    setOpen(false)
  }

  const handleSubmit = async () => {
    let product: Product = new Product(
      x,
      name,
      price,
      category,
      0,
      "Cool store yea",
      quantity
    ) //TODO: fill real storeid
    setx(x + 1) // TODO: Delete This x
    // alert(`sending to the server the product: ${JSON.stringify(product)}`);

    try {
      // const result = addNewProduct(-1, product);
      // TODO: Check if good promise
      handleClose()
      resetFields()
      setOpenSnack(true)
      handleAddProduct(product)
    } catch {}
    // TODO: if not succeed!
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
      <Button variant="outlined" onClick={handleClickOpen}>
        Add New Product To Store
      </Button>
      <Dialog open={open} onClose={handleClose} fullWidth>
        <DialogTitle>Add Product</DialogTitle>
        <DialogContent>
          {/* <DialogContentText>
            To subscribe to this website, please enter your email address here.
            We will send updates occasionally.
          </DialogContentText> */}
          {makeTextField("productName", "Product name", name, "text", setName)}
          {makeTextField(
            "category",
            "Category of the product",
            category,
            "text",
            setCategory
          )}
          {makeTextField(
            "quantity",
            "Available quantity in store",
            quantity,
            "number",
            setQuantity
          )}
          {makeTextField(
            "price",
            `Price (in ${Currency})`,
            price,
            "number",
            setPrice
          )}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button onClick={handleSubmit}>Submit</Button>
        </DialogActions>
      </Dialog>

      {SuccessSnackbar(
        "Product Added Successfully!",
        openSnack,
        handleCloseSnack
      )}
    </div>
  )
}
