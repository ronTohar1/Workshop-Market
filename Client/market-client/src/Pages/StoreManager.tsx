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

import { Button, Fab, makeStyles, Paper, Stack } from "@mui/material";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import Navbar from "../Componentss/Navbar";

import Product from "../DTOs/Product";
import { Card, CardActions, CardContent } from "@mui/material";
import { serverGetStore, groupStoresProducts } from "../services/StoreService";
import ExitToAppIcon from "@mui/icons-material/ExitToApp";
import { dummyMember1, getStoresManagedBy } from "../services/MemberService";
import Grid from "@mui/material/Grid";

const currentMember = dummyMember1;

function ProductsTable() {
  return (
    <GridToolbarContainer>
      <GridToolbarFilterButton />
      <GridToolbarDensitySelector />
    </GridToolbarContainer>
  );
}

const columns: GridColDef[] = [
  { field: "name", headerName: "Product", flex: 2 },
  { field: "price", headerName: "Price", flex: 1 },
  { field: "category", headerName: "Category", flex: 1 },
  { field: "available_quantity", headerName: "Available Quantity", flex: 1 },
  // { field: "store", headerName: "Store", flex: 1 },
];

type ProductRowType = {
  id: number;
  name: string;
  price: number;
  category: string;
  store: string;
  available_quantity: number;
};

const createRows = (products: Product[]) => {
  let productRow: ProductRowType[] = [];
  for (const p of products) {
    productRow.push({
      id: p.id,
      name: p.name,
      price: p.price,
      category: p.category,
      store: serverGetStore(p.store).name,
      available_quantity: p.availableQuantity,
    });
  }
  return productRow;
};

function storeGrid(
  productsRows: ProductRowType[],
  pageSize: number,
  setPageSize: any
) {
  const storeName = productsRows[0].store;
  return (
    <Box
      sx={{
        justifyContent: "center",
        width: "100%",
        mt: 3,
        mb: 3,
      }}>
      <Stack justifyContent='space-evenly' alignItems='center' spacing={2}>
        <Box sx={{ m: 1 }}>
          <Typography variant='h4'>{storeName}</Typography>
        </Box>
        <Box style={{ height: 400, width: "90%" }} sx={{ boxShadow: 3, mb: 3 }}>
          <DataGrid
            sx={{ overflow: "auto" }}
            rows={productsRows}
            columns={columns}
            //Paging
            rowsPerPageOptions={[5, 10, 15]}
            pageSize={pageSize}
            onPageSizeChange={(newPageSize) => setPageSize(newPageSize)}
            pagination
            //Components
            components={{
              Toolbar: ProductsTable,
            }}
          />

          <Button
            variant='contained'
            size='large'
            sx={{ mt: 1 }}
            startIcon={<ExitToAppIcon />}>
            {storeName}
          </Button>
        </Box>
      </Stack>
    </Box>
  );
}

const InformationCard = (username: string) => {
  return (
    <Card sx={{ ml: 2, mr: 2 }} elevation={6} component={Paper}>
      <CardContent>
        <Typography sx={{ mb: 3 }} variant='h3' component='div'>
          Account Information
        </Typography>

        <Typography variant='h5'>
          <b>Username</b>: {currentMember.username}
        </Typography>
        <Typography variant='h5'>
          <b>Number Of Managed Stores</b>:{" "}
          {getStoresManagedBy(currentMember).length}
        </Typography>
      </CardContent>
    </Card>
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
  // const products: Map<number, Product> = Object.assign(
  //   {},
  //   ...productsLst.map((p: Product) => ({ [p.id]: p }))
  // );
  const productsByStore: Product[][] = groupStoresProducts(stores);

  return (
    <ThemeProvider theme={theme}>
      <Box>
        <Box>
          <Navbar />
        </Box>
        <Box>
          <Grid container spacing={0}>
            <Grid
              item
              xs={8}
              sm={6}
              sx={{
                my: 2,
                alignItems: "center",
              }}>
              <Stack>
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
                {productsByStore.map((prodsOfStore: Product[]) => {
                  return storeGrid(
                    createRows(prodsOfStore),
                    pageSize,
                    setPageSize
                  );
                })}
              </Stack>
            </Grid>
            <Grid item xs={2} sm={2}></Grid>

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
              {InformationCard("Ronto The User")}
              {/* </Box> */}
            </Grid>
          </Grid>
        </Box>
      </Box>
    </ThemeProvider>
  );
}

// export default SearchPage;
