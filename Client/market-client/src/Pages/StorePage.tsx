import * as React from 'react';
import { DataGrid, GridColDef, GridValueGetterParams, GridSelectionModel } from '@mui/x-data-grid';
import { Box, Stack } from '@mui/material';
import Navbar from '../components/Navbar';
import { dummyProducts } from '../services/ProductsService';
import {Store} from '../DTOs/Store'; 
import * as storeService from '../services/StoreService';
import Product from '../DTOs/Product';

const columns: GridColDef[] = [
  {
    field: 'name',
    headerName: 'Product Name',
    // type: 'number',
    flex:2,
  },
  {
    field: 'price',
    headerName: 'Price',
    // type: 'number',
    flex:1,
  },
  {
    field: 'available_quantity',
    headerName: 'Available Quantity',
    description: 'Product current quantity in store inventory',
    // type: 'number',
    flex:1,
  },
  {
    field: 'category',
    headerName: 'Category',
    // type: 'string',
    flex:1,
    // valueGetter: (params: GridValueGetterParams) =>
    //   `${params.row.firstName || ''} ${params.row.lastName || ''}`,
  },
];

const store: Store = storeService.dummyStore1;
const rows : Product[] = store.products;


export default function StorePage() {

  const startingPageSize : number = 10
  const [pageSize, setPageSize] = React.useState<number>(startingPageSize);

  const handleNewSelection = (newSelectionModel:any) => {
    const chosenIds= newSelectionModel;
    console.log(typeof(chosenIds));
    console.log(chosenIds);
    // alert(null)
  }

  return (
    
    <Box>
      <Navbar/>
    <Stack direction="row">
    {}
    </Stack>
    <div  style={{ height: 600, width: '100%' }}>
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
