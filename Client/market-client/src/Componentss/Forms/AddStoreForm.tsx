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

export default function AddStoreForm({
  handleAddStore,
  open,
  handleClose,
}: {
  handleAddStore: (storeName: string) => void
  open: boolean
  handleClose: () => void
}) {
  const [name, setName] = React.useState("")

  const resetFields = () => {
    setName("")
  }

  const handleCloseClick = () => {
    resetFields()
    handleClose()
  }

  const handleSubmit = async () => {
    handleAddStore(name)
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
      <Dialog open={open} onClose={handleCloseClick} fullWidth>
        <DialogTitle>Add Product</DialogTitle>
        <DialogContent>
          {makeTextField("storeName", "Store name", name, "text", setName)}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button onClick={handleSubmit}>Submit</Button>
        </DialogActions>
      </Dialog>
    </div>
  )
}
