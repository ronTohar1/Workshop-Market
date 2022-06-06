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

export default function AddProductForm({
  handleAddProduct,
  open,
  handleClose,
  handleOpen,
}: {
  handleAddProduct: (
    productName: string,
    price: number,
    category: string
  ) => void
  open: boolean
  handleClose: () => void
  handleOpen: () => void
}) {
  const [name, setName] = React.useState("")
  const [category, setCategory] = React.useState("")
  const [price, setPrice] = React.useState(0)

  const resetFields = () => {
    setName("")
    setCategory("")
    setPrice(0)
  }

  const handleOpenClick = () => {
    resetFields()
    handleOpen()
  }

  const handleSubmit = async () => {
    handleAddProduct(name, price, category)
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
      <Button variant="outlined" onClick={handleOpenClick}>
        Add New Product To Store
      </Button>
      <Dialog open={open} onClose={handleClose} fullWidth>
        <DialogTitle>Add Product</DialogTitle>
        <DialogContent>
          {makeTextField("productName", "Product name", name, "text", setName)}
          {makeTextField(
            "category",
            "Category of the product",
            category,
            "text",
            setCategory
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
          <Button onClick={handleSubmit} >
            Submit
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  )
}
