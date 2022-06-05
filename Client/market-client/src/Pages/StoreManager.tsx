import * as React from "react";
import {
  DataGrid,
  GridToolbarContainer,
  GridToolbarFilterButton,
  GridToolbarDensitySelector,
  GridColDef,
} from "@mui/x-data-grid";
import Box from "@mui/material/Box";

// import CustomToolbarGrid  from '../components/ProductsList'
// import * as React from 'react';

import Typography from "@mui/material/Typography";

import {
  AppBar,
  Button,
  Dialog,
  Divider,
  FormGroup,
  IconButton,
  List,
  ListItem,
  ListItemText,
  makeStyles,
  Paper,
  Stack,
  styled,
  Switch,
  TextField,
  Toolbar,
} from "@mui/material";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import Navbar from "../Componentss/Navbar";

import Product from "../DTOs/Product";
import { Card, CardActions, CardContent } from "@mui/material";
import { serverGetStore } from "../services/StoreService";
import ExitToAppIcon from "@mui/icons-material/ExitToApp";
import { dummyMember1, getStoresManagedBy } from "../services/MemberService";
import Grid from "@mui/material/Grid";
import Store from "../DTOs/Store";

import StoreIcon from "@mui/icons-material/Store";
import CloseIcon from "@mui/icons-material/Close";
import StoreDialog from "../Componentss/ManagerEditStore/StoreManagerStoreDialog";
import StorePage from "./StorePage";

const currentMember = dummyMember1;



const UserInfoCard = (username: string) => {
  return (
    <Card>
      <CardContent>
        <Typography variant='h4' component='div'>
          Account Information
        </Typography>

        <Typography variant='h6'>
          <b>Username</b>: {currentMember.username}
        </Typography>
        <Typography variant='h6'>
          <b>Number Of Managed Stores</b>:{" "}
          {getStoresManagedBy(currentMember).length}
        </Typography>
      </CardContent>
    </Card>
  );
};

const StoreCard = ({ store }: { store: Store }) => {
  const [openDialog, setOpenDialog] = React.useState(false);
  const [isStoreOpen, setIsStoreOpen] = React.useState(false);
  // TODO: open and close store

  const handleClickOpenDialog = () => {
    setOpenDialog(true);
  };

  const handleCloseDialog = () => {
    setOpenDialog(false);
  };

  const handleChangeStoreOpen = (event: React.ChangeEvent<HTMLInputElement>) => {
    setIsStoreOpen(event.target.checked);
    // TODO: service update
  };

  return (
    <div>
      {openDialog && <StoreDialog store={store} handleCloseDialog={handleCloseDialog}/>}
      <Card sx={{ display: "flex" }} elevation={6} component={Paper}>
        <CardContent sx={{ display: "flex", flexDirection: "column" }}>
          <Typography sx={{ mb: 3 }} variant='h3' component='div'>
            Store "{store.name}"
          </Typography>

          <Button
            variant='contained'
            endIcon={<StoreIcon />}
            sx={{ mb: 3 }}
            onClick={handleClickOpenDialog}>
            To the store
          </Button>
          <FormGroup>
            <Stack direction='row' spacing={1} alignItems='center'>
              <Typography>Store is now {isStoreOpen ? "Open" : "Closed"}</Typography>
              <Switch checked={isStoreOpen} onChange={handleChangeStoreOpen} />
            </Stack>
          </FormGroup>
        </CardContent>
      </Card>
    </div>
  );
};

export default function StoreManagerPage() {
  const startingPageSize = 5;
  const [pageSize, setPageSize] = React.useState<number>(startingPageSize);

  const theme = createTheme({
    typography: {
      fontFamily: [
        "-apple-system",
        "BlinkMacSystemFont",
        '"Segoe UI"',
        "Roboto",
        '"Helvetica Neue"',
        "Arial",
        "sans-serif",
        '"Apple Color Emoji"',
        '"Segoe UI Emoji"',
        '"Segoe UI Symbol"',
      ].join(","),
    },
  });

  // map products to Map indexing items by id
  const stores = getStoresManagedBy(currentMember);
  
  const productsByStore: Product[][] = [];

  const Item = styled("div")(({ theme }) => ({
    ...theme.typography.body2,
    padding: theme.spacing(3),
    textAlign: "center",
    color: theme.palette.text.secondary,
  }));

  return (
    <ThemeProvider theme={theme}>
      <Box>
        <Box>
          <Navbar />
        </Box>
        <Box>
          <Grid
            item
            xs={2}
            sm={4}
            sx={{
              justifyContent: "cemter",
              mt: 3,
              alignItems: "center",
            }}>
            {/* <Box
                sx={{
                  justifyContent: "center",
                  display: "flex",
                  width: "100%",
                }}
              > */}
            {UserInfoCard("Ronto The User")}
            {/* </Box> */}
          </Grid>
          <Grid>
            <Box
              sx={{
                justifyContent: "center",
                display: "flex",
                width: "100%",
                mt: 3,
                mb: 3,
              }}>
              <Typography variant='h3' component='div'>
                Stores You Manage
              </Typography>
            </Box>
            {/* {productsByStore.map((prodsOfStore: Product[]) => {
                  return storeGrid(prodsOfStore, pageSize, setPageSize);
                })} */}
            <Grid
              container
              flex={1}
              rowSpacing={1}
              columnSpacing={{ xs: 1, sm: 2, md: 3 }}>
              {stores.map((s) => (
                <Item>
                  <StoreCard store={s} />
                </Item>
              ))}
            </Grid>
          </Grid>
        </Box>
      </Box>
    </ThemeProvider>
  );
}

// export default SearchPage;
