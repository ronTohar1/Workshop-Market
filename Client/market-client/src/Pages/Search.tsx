import PropTypes from "prop-types";
import * as React from "react";
import {
  DataGrid,
  GridToolbarContainer,
  GridToolbarColumnsButton,
  GridToolbarFilterButton,
  GridToolbarExport,
  GridToolbarDensitySelector,
  GridRowsProp,
  GridColDef,
} from "@mui/x-data-grid";
import Box from "@mui/material/Box";

// import CustomToolbarGrid  from '../components/ProductsList'
// import * as React from 'react';

import Typography from "@mui/material/Typography";

import { Button, Card, Fab, makeStyles, Stack } from "@mui/material";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import Navbar from "../components/Navbar";

import Product from "../DTOs/Product";
import { fetchProducts, groupByStore } from "../services/ProductsService";
import { useQueryParam, NumberParam, StringParam } from "use-query-params";
import { getStore } from "../services/StoreService";
import ExitToAppIcon from "@mui/icons-material/ExitToApp";
import { useNavigate } from "react-router-dom";
import { pathStore } from "../Paths";

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
  { field: "store", headerName: "Store", flex: 1 },
];

type ProductRowType = {
  id: number;
  name: string;
  price: number;
  category: string;
  store: string;
};

const createRows = (products: Product[]) => {
  let productRow: ProductRowType[] = [];
  for (const p of products) {
    productRow.push({
      id: p.id,
      name: p.name,
      price: p.price,
      category: p.category,
      store: getStore(p.store).name,
    });
  }
  return productRow;
};

export function StoreGrid(
  productsRows: ProductRowType[],
  pageSize: number,
  setPageSize: any,
  storeId: number
) {
  const navigate = useNavigate();
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
            startIcon={<ExitToAppIcon />}
            onClick={() => navigate(`${pathStore}?id=${storeId}`)}>
            {storeName}
          </Button>
        </Box>
      </Stack>
    </Box>
  );
}

export default function SearchPage() {
  const startingPageSize = 5;
  const [pageSize, setPageSize] = React.useState<number>(startingPageSize);
  const [query] = useQueryParam("query", StringParam);

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
  const productsLst = fetchProducts(query || "");
  // const products: Map<number, Product> = Object.assign(
  //   {},
  //   ...productsLst.map((p: Product) => ({ [p.id]: p }))
  // );
  const productsByStore: Product[][] = groupByStore(productsLst);

  return (
    <ThemeProvider theme={theme}>
      <Box>
        <Navbar />
        <Stack>
          {productsByStore.map((prodsOfStore: Product[]) => {
            const storeId = prodsOfStore[0].store;
            return StoreGrid(
              createRows(prodsOfStore),
              pageSize,
              setPageSize,
              storeId
            );
          })}
        </Stack>
      </Box>
    </ThemeProvider>
  );
}

SearchPage.defaultProps = {
  query: "",
};

// export default SearchPage;
