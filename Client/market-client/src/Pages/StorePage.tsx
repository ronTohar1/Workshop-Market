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
const storeProducts: Product[] = store.products;
const theme = createTheme();

const fields = {
  name: "name",
  price: "price",
  available_quantity: "available_quantity",
  category: "category",
};

const columns: GridColDef[] = [
  {
    field: fields.name,
    headerName: "Product Name",
    type: "string",
    flex: 2,
    align: "left",
    headerAlign: "left",
  },
  {
    field: fields.price,
    headerName: "Price",
    type: "number",
    flex: 1,
    editable: isManager,
    align: "left",
    headerAlign: "left",
  },
  {
    field: fields.available_quantity,
    headerName: "Available Quantity",
    description: "Product current quantity in store inventory",
    type: "number",
    flex: 1,
    editable: isManager,
    align: "left",
    headerAlign: "left",
  },
  {
    field: fields.category,
    headerName: "Category",
    // type: 'string',
    flex: 1,
    editable: isManager,
    align: "left",
    headerAlign: "left",

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

function updatePrice(product: Product, price: number) {
  if (price != null) product.price = price;
}
function updateAvailableQuantity(product: Product, available_quantity: number) {
  if (available_quantity != null)
    product.available_quantity = available_quantity;
}
function updateCategory(product: Product, category: string){
  if ( category != null)
    product.category = category;
}

export default function StorePage() {
  const startingPageSize: number = 10;

  const [pageSize, setPageSize] = React.useState<number>(startingPageSize);
  const [numSelected, setNumSelected] = React.useState<number>(0);
  const [selectedProductsIds, setSelectedProductsIds] = React.useState<
    number[]
  >([]);
  const [rows, setRows] = React.useState<Product[]>(storeProducts);

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
    const newRows = rows.map((row) => {
      if (row.id === e.id) {
        switch (e.field) {
          case fields.price:
            updatePrice(row, e.value);
            break;
          case fields.available_quantity:
            updateAvailableQuantity(row, e.value);
            break;
          case fields.category:
            updateCategory(row, e.value);
            break;
        }
      }
      return row;
    });
    setRows(newRows);
    // alert(e.field + " Changed into "+ e.value + " id "+ e.id)
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
          isCellEditable={(params) => isManager}
          onCellEditCommit={handleCellEdit}
        />
      </div>
    </Box>
  );
}