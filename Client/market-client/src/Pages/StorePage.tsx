import * as React from "react";
import {
  DataGrid,
  GridCellEditCommitParams,
  GridColDef,
} from "@mui/x-data-grid";
import {
  alpha,
  Box,
  createTheme,
  Fab,
  Stack,
  Toolbar,
  Tooltip,
  Typography,
} from "@mui/material";
import Navbar from "../components/Navbar";
import { Store } from "../DTOs/Store";
import * as storeService from "../services/StoreService";
import Product from "../DTOs/Product";
import { AddShoppingCart } from "@mui/icons-material";

const isManager: boolean = true;
const store: Store = storeService.dummyStore1;
const rows: Product[] = store.products;
const theme = createTheme();

const columns: GridColDef[] = [
  {
    field: "name",
    headerName: "Product Name",
    // type: 'string',
    flex: 2,
  },
  {
    field: "price",
    headerName: "Price",
    type: 'number',
    flex: 1,
    editable: isManager,

  },
  {
    field: "available_quantity",
    headerName: "Available Quantity",
    description: "Product current quantity in store inventory",
    type: 'number',
    flex: 1,
    editable: isManager,

  },
  {
    field: "category",
    headerName: "Category",
    // type: 'string',
    flex: 1,
    editable: isManager,

    // valueGetter: (params: GridValueGetterParams) =>
    //   `${params.row.firstName || ''} ${params.row.lastName || ''}`,
  },
];



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
      {numSelected > 0 && isManager ? (
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
      {numSelected > 0 && isManager ? (
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

  const handleCellEdit = (e: GridCellEditCommitParams) => {
    alert(e.field + " Changed into "+ e.value + " id "+ e.id)
  } 

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
          onCellEditCommit={handleCellEdit}
        />
      </div>
    </Box>
  );
}
