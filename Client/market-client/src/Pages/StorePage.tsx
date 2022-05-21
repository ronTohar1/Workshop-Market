import * as React from "react";
import {
  DataGrid,
  GridColDef,
  GridValueGetterParams,
  GridSelectionModel,
} from "@mui/x-data-grid";
import {
  alpha,
  Box,
  createTheme,
  Fab,
  IconButton,
  Stack,
  TextField,
  Toolbar,
  Tooltip,
  Typography,
} from "@mui/material";
import Navbar from "../components/Navbar";
import { dummyProducts } from "../services/ProductsService";
import { Store } from "../DTOs/Store";
import * as storeService from "../services/StoreService";
import Product from "../DTOs/Product";
import { AddShoppingCart } from "@mui/icons-material";

const columns: GridColDef[] = [
  {
    field: "name",
    headerName: "Product Name",
    // type: 'number',
    flex: 2,
  },
  {
    field: "price",
    headerName: "Price",
    // type: 'number',
    flex: 1,
  },
  {
    field: "available_quantity",
    headerName: "Available Quantity",
    description: "Product current quantity in store inventory",
    // type: 'number',
    flex: 1,
  },
  {
    field: "category",
    headerName: "Category",
    // type: 'string',
    flex: 1,
    // valueGetter: (params: GridValueGetterParams) =>
    //   `${params.row.firstName || ''} ${params.row.lastName || ''}`,
  },
];

const store: Store = storeService.dummyStore1;
const rows: Product[] = store.products;
const theme = createTheme();

const toolBar = (numSelected: number, handleAddToCart: () => void) => {
  return (
    <Toolbar
      sx={{
        pl: { sm: 2 },
        pr: { xs: 1, sm: 1 },
        ...(numSelected > 0 && {
          bgcolor: (theme) =>
            alpha(
              theme.palette.primary.main,
              theme.palette.action.activatedOpacity
            ),
        }),
      }}
    >
      {numSelected > 0 ? (
        <Typography
          sx={{ flex: "1 1 100%" }}
          color="inherit"
          variant="subtitle1"
          component="div"
        >
          {numSelected} selected
        </Typography>
      ) : (
        <Typography
          sx={{ flex: "1 1 100%" }}
          variant="h6"
          id="tableTitle"
          component="div"
        >
          {store.name}
        </Typography>
      )}
      {numSelected > 0 ? (
        <Tooltip title="Add To Cart">
          <Fab
            size="medium"
            color="primary"
            aria-label="add"
            onClick={() => handleAddToCart()}
          >
            <AddShoppingCart />
          </Fab>
        </Tooltip>
      ) : (
        <Box></Box>
      )}
    </Toolbar>
  );
};

export default function StorePage() {
  const startingPageSize: number = 10;
  const [pageSize, setPageSize] = React.useState<number>(startingPageSize);
  const [numSelected, setNumSelected] = React.useState<number>(0);
  const [selectedProductsIds, setSelectedProductsIds] = React.useState<number[]>([]);

  const handleNewSelection = (newSelectionModel: any) => {
    const chosenIds: number[] = newSelectionModel;
    setNumSelected(chosenIds.length);
    setSelectedProductsIds(chosenIds);
  };

  const handleAddToCart = () => {
    rows.forEach((prod) => {
      if (selectedProductsIds.includes(prod.id)) {
        console.log("Adding to cart: " + prod.name);
      }
    
    });
  };

  return (
    <Box>
      <Navbar />
      {toolBar(numSelected, handleAddToCart)}
      <Stack direction="row">{}</Stack>
      <div style={{ height: 600, width: "100%" }}>
        <DataGrid
          rows={rows}
          columns={columns}
          // Paging:
          pageSize={pageSize}
          onPageSizeChange={(newPageSize) => setPageSize(newPageSize)}
          rowsPerPageOptions={[10, 20, 25]}
          pagination
          // Selection:
          checkboxSelection
          disableSelectionOnClick
          onSelectionModelChange={handleNewSelection}
        />
      </div>
    </Box>
  );
}
