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

export default function AddToRoleForm({
    handleAddCoOwner,
    open,
    handleClose,
    handleOpen,
    displayText,
}: {
    handleAddCoOwner: (
        userId: number
    ) => void,
    open: boolean
    handleClose: () => void
    handleOpen: () => void
    displayText: string
}) {
    const [userId, setUserId] = React.useState<number>(-1)

    const resetFields = () => {
        setUserId(-1)
    }

    const handleOpenClick = () => {
        resetFields()
        handleOpen()
    }

    const handleSubmit = async () => {
        handleAddCoOwner(userId)
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
            <Button variant="contained" sx={{ mt: 1 }} onClick={handleOpenClick}>
                {displayText}
            </Button>
            <Dialog open={open} onClose={handleClose} fullWidth>
                <DialogTitle>Add Product</DialogTitle>
                <DialogContent>
                    {makeTextField("userId", "User Website ID", userId, "number", setUserId)}
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
