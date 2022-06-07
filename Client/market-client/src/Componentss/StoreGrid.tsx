import { Box, Button, Stack, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { pathStore } from "../Paths";
import ExitToAppIcon from "@mui/icons-material/ExitToApp";
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
import Product from "../DTOs/Product";
import { serverGetStore } from "../services/StoreService";

function ProductsTable() {
  return (
    <GridToolbarContainer>
      <GridToolbarFilterButton />
      <GridToolbarDensitySelector />
    </GridToolbarContainer>
  );
}
;

const columns: GridColDef[] = [
  { field: "name", headerName: "Product", flex: 2 },
  { field: "price", headerName: "Price", flex: 1 },
  { field: "category", headerName: "Category", flex: 1 },
  { field: "storeName", headerName: "Store", flex: 1 },
];



export default function StoreGrid(
  products: Product[],
  pageSize: number,
  setPageSize: any,
  storeId: number,
  handleGoToStore: (storeId: number) => void
) {
  const productsRows: Product[] = products;

  const storeName = productsRows[0].storeName; // We know its not empty
  return (
    <Box
      sx={{
        justifyContent: "center",
        width: "100%",
        mt: 3,
        mb: 3,
      }}
    >
      <Stack justifyContent="space-evenly" alignItems="center" spacing={2}>
        <Box sx={{ m: 1 }}>
          <Typography variant="h4">{storeName}</Typography>
        </Box>
        <Box style={{ height: 400, width: "90%" }} sx={{ boxShadow: 3, mb: 3 }}>
          <DataGrid
            rows={productsRows}
            columns={columns}
            //Paging
            rowsPerPageOptions={[5, 10, 15]}
            pageSize={pageSize}
            onPageSizeChange={(newPageSize: number) => setPageSize(newPageSize)}
            pagination
            disableSelectionOnClick
            //Components
            components={{
              Toolbar: ProductsTable,
            }}
          />

          <Button
            variant="contained"
            size="large"
            sx={{ mt: 1 }}
            startIcon={<ExitToAppIcon />}
            onClick={() => handleGoToStore(storeId)}
          >
            {storeName}
          </Button>
        </Box>
      </Stack>
    </Box>
  );
}
