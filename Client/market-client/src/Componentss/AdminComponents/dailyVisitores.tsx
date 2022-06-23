import * as React from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import Purchase from "../../DTOs/Purchase";
import { Box, Container, FormControl, Grid, IconButton, InputLabel, Slide, Typography } from "@mui/material";
import PurchaseCard from "./PurcaseCard";
import { serverGetBuyerPurchaseHistory } from "../../services/AdminService";
import { getBuyerId } from "../../services/SessionService";
import { fetchResponse } from "../../services/GeneralService";
import FailureSnackbar from "../Forms/FailureSnackbar";
import SuccessSnackbar from "../Forms/SuccessSnackbar";
import Chart from "react-google-charts";
import { TransitionProps } from "@mui/material/transitions";
import CloseIcon from '@mui/icons-material/Close';

// export const purchasesDummy= [
// new Purchase("12/12/2022",34.4, "bought 3 apples", 0),
// new Purchase("10/02/2020",12.4, "bought 2 bananas", 1),
// new Purchase("10/10/2021",10, "bought 3 apples", 0),
// new Purchase("12/12/2022",34.4, "bought 3 apples", 0),
// new Purchase("10/02/2020",12.4, "bought 2 bananas", 1),
// new Purchase("10/10/2021",10, "bought 3 apples", 0),
// new Purchase("12/12/2022",34.4, "bought 3 apples", 0),
// new Purchase("10/02/2020",12.4, "bought 2 bananas", 1),
// ]
export const data = [
    ["Visitor", "Amount"],
    ["Admins", 11],
    ["Store Owners", 2],
    ["Managers(without any stores)", 2],
    ["Members(not managers or store owners)", 2],
    ["Guests", 7],
  ];
  
  export const options = {
    title: "Daily Visitors",
    is3D: true,
  };
  const Transition = React.forwardRef(function Transition(
    props: TransitionProps & {
      children: React.ReactElement;
    },
    ref: React.Ref<unknown>,
  ) {
    return <Slide direction="up" ref={ref} {...props} />;
  });

export default function DailyVisitors() {
  const [open, setOpen] = React.useState<boolean>(false);
  const [purchases, setPurchases] = React.useState<Purchase[]>([]);

  //------------------------------
  const [openFailSnack, setOpenFailSnack] = React.useState<boolean>(false);
  const [failureProductMsg, setFailureProductMsg] = React.useState<string>("");
  const [openSuccSnack, setOpenSuccSnack] = React.useState<boolean>(false);
  const [successProductMsg, setSuccessProductMsg] = React.useState<string>("");

  // const dd = String(today.getDate()).padStart(2, '0');
  // const mm = String(fromSelectedMonth).padStart(2, '0'); //January is 0!
  const today = new Date();
  const dd = today.getDate();
  const mm = today.getMonth() + 1; //January is 0!
  const yyyy = today.getFullYear();
  
  const [fromSelectedDay, setFromDay] = React.useState(dd);
  const [fromSelectedMonth, setFromMonth] = React.useState(mm);
  const [fromSelectedYear, setFromYear] = React.useState(yyyy);

  const [toSelectedDay, setToDay] = React.useState(dd);
  const [toSelectedMonth, setToMonth] = React.useState(mm);
  const [toSelectedYear, setToYear] = React.useState(yyyy);

  const showFailureSnack = (msg: string) => {
    setOpenFailSnack(true);
    setFailureProductMsg(msg);
  };
  //------------------------------

  const handleClickOpen = () => {
    console.log("opened");
    setOpen(true);
  };
  const handleClose = () => {
    console.log("closed");
    setOpen(false);
  };
  const handleSearch = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    console.log("in search");
    const buyerId = getBuyerId();
    const responsePromise = serverGetBuyerPurchaseHistory(
      buyerId,
      Number(data.get("id"))
    );
    console.log(responsePromise);
    fetchResponse(responsePromise)
      .then((newPurchases) => {
        if (newPurchases.length === 0) {
          showFailureSnack("No purchases for this user");
        }
        setPurchases(newPurchases);
      })
      .catch((e) => {
        alert(e);
        setOpen(false);
      });
  };

  return (
    <div>
      <Box textAlign='center'>
      <Button
        onClick={handleClickOpen}
        style={{ height: 50, width: 500 }}
        key='name'
        variant='contained'
        size='large'
        color='primary'
        sx={{
          m: 1,
          "&:hover": {
            borderRadius: 5,
          },
        }}>
        Display Daily Visitors 
      </Button>
      </Box>
      <Dialog open={open} 
              onClose={handleClose}  
              fullScreen
              sx={{marginTop:5}}
              TransitionComponent={Transition}>
            <IconButton
              edge="end"
              color="inherit"
              onClick={handleClose}
              aria-label="close"
            >
              <CloseIcon />
            </IconButton>
        <DialogTitle>Display Daily Visitors</DialogTitle>
        <DialogContent  >
       
        <Grid container  justifyContent="left" >
        <Grid item xs={2.3}>
        </Grid>
        <Grid item xs={0.7}>
            <Typography variant="h6" gutterBottom>
            From:
        </Typography>
        </Grid>
        <Grid item xs={0.75}>
          <FormControl sx={{ m: 1, maxWidth: 100 }} id="myform">
          <TextField
                required
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => {setFromDay(parseInt(e.currentTarget.value));}} 
                value = {String(fromSelectedDay).padStart(2, '0')}
                name="number"
                type="number"
                label="Day "/>
              </FormControl>
        </Grid>
        <Grid item xs={0.85}>
          <FormControl sx={{ m: 1, maxWidth: 100 }} id="myform">
          <TextField
                required
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => {setFromMonth(parseInt(e.currentTarget.value));}} 
                value = {String(fromSelectedMonth).padStart(2, '0')}
                name="number"
                type="number"
                label="Month"
                fullWidth/>
              </FormControl>
        </Grid>
        <Grid item xs={2}>
          <FormControl sx={{ m: 1, maxWidth: 100 }} id="myform">
          <TextField
                required
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => {setFromYear(parseInt(e.currentTarget.value));}} 
                value={fromSelectedYear}
                name="number"
                type="number"
                label="Year"
                fullWidth/>
              </FormControl>
        </Grid>
        <Grid item xs={0.5}>
            <Typography variant="h6" gutterBottom>
            To:
        </Typography>
        </Grid>
        <Grid item xs={0.75}>
          <FormControl sx={{ m: 1, maxWidth: 100 }} id="myform">
          <TextField
                required
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => {setToDay(parseInt(e.currentTarget.value));}} 
                value = {String(toSelectedDay).padStart(2, '0')}
                name="number"
                type="number"
                label="Day "/>
              </FormControl>
        </Grid>
        <Grid item xs={0.85}>
          <FormControl sx={{ m: 1, maxWidth: 100 }} id="myform">
          <TextField
                required
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => {setToMonth(parseInt(e.currentTarget.value));}} 
                value = {String(toSelectedMonth).padStart(2, '0')}
                name="number"
                type="number"
                label="Month"
                fullWidth/>
              </FormControl>
        </Grid>
        <Grid item xs={2}>
          <FormControl sx={{ m: 1, maxWidth: 100 }} id="myform">
          <TextField
                required
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => {setToYear(parseInt(e.currentTarget.value));}} 
                value={toSelectedYear}
                name="number"
                type="number"
                label="Year"
                fullWidth/>
              </FormControl>
        </Grid>
        </Grid>
        <Box textAlign='center'>
             <Button onClick={()=>{}} >search</Button>
             </Box>
        </DialogContent>
        <Container
          style={{ maxHeight: "100%", maxWidth: "100%", overflow: "auto" }}>
            <Chart
            chartType="PieChart"
            data={data}
            options={options}
            width={"100%"}
            height={"400px"}
            />
        </Container>
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
