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
import Navbar from "../Componentss/Navbar";

import Product from "../DTOs/Product";
import { fetchProducts } from "../services/ProductsService";
import { useQueryParam, NumberParam, StringParam } from "use-query-params";
import { serverGetStore } from "../services/StoreService";
import ExitToAppIcon from "@mui/icons-material/ExitToApp";
import { useLocation, useNavigate } from "react-router-dom";
import { pathStore } from "../Paths";
import StoreGrid from "../Componentss/StoreGrid";

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

export default function SearchPage() {
  const startingPageSize = 5;
  const [pageSize, setPageSize] = React.useState<number>(startingPageSize);
  const [query] = useQueryParam("query", StringParam);
  const [productsByStore, setProductsByStore] = React.useState<Product[][]>([]);
  const navigate = useNavigate();

  const HandleGoToStore = (storeId: number) => {
    navigate(`${pathStore}?id=${storeId}`);
  };

  //Send function to navbar that activates the setProductsByStore here when pushin search
  React.useEffect(() => {
    fetchProducts(query || "").then((prods: Product[][]) => {
      setProductsByStore(prods);
    }).catch((e) => alert(e))
  }, [query]);

  return (
    <ThemeProvider theme={theme}>
      <Box>
        <Navbar />
        <Stack>
          {productsByStore.map((prodsOfStore: Product[]) => {
            const storeId = prodsOfStore[0].storeId;
            return StoreGrid(
              prodsOfStore,
              pageSize,
              setPageSize,
              storeId,
              HandleGoToStore
            );
          })}
        </Stack>
      </Box>
    </ThemeProvider>
  );
}

// SearchPage.defaultProps = {
//   query: "",
// };

// export default SearchPage;
