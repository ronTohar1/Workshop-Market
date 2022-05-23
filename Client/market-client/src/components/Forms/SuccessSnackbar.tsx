import * as React from "react";
import MuiAlert, { AlertProps } from "@mui/material/Alert";
import Snackbar from "@mui/material/Snackbar";
import { SnackbarOrigin } from "@mui/material/Snackbar";

const Alert = React.forwardRef<HTMLDivElement, AlertProps>(function Alert(
  props,
  ref
) {
  return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

export default function SuccessSnackbar(message: String, isOpen:boolean, handleCloseSnack: () => void) {
  const [openSnack, setOpenSnack] = React.useState(false);
  const snackBarPosition: SnackbarOrigin = {
    vertical: "top",
    horizontal: "center",
  };

  return (
    <Snackbar
      anchorOrigin={snackBarPosition}
      open={isOpen}
      autoHideDuration={6000}
      onClose={handleCloseSnack}
    >
      <Alert
        onClose={handleCloseSnack}
        severity="success"
        sx={{ width: "100%" }}
      >
        {message}
      </Alert>
    </Snackbar>
  );
}
