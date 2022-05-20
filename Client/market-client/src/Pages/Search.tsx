import PropTypes from 'prop-types'
import * as React from 'react';
import {
  DataGrid,
  GridToolbarContainer,
  GridToolbarColumnsButton,
  GridToolbarFilterButton,
  GridToolbarExport,
  GridToolbarDensitySelector,
  GridRowsProp, 
  GridColDef
} from '@mui/x-data-grid';
import { useDemoData } from '@mui/x-data-grid-generator';
import Box from '@mui/material/Box';


// import CustomToolbarGrid  from '../components/ProductsList'
// import * as React from 'react';
import { alpha } from '@mui/material/styles';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';
import TableSortLabel from '@mui/material/TableSortLabel';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';
import Checkbox from '@mui/material/Checkbox';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';
import FormControlLabel from '@mui/material/FormControlLabel';
import Switch from '@mui/material/Switch';
import DeleteIcon from '@mui/icons-material/Delete';
import FilterListIcon from '@mui/icons-material/FilterList';
import { visuallyHidden } from '@mui/utils';

import { Fab, makeStyles } from '@mui/material';
import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart';
import AppBar from '@mui/material/AppBar';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import Navbar from '../Navbar';
import Autocomplete from '@mui/material/Autocomplete';

import Product from '../DTOs/Product'
import {fetchProducts} from '../App'
import DemoTreeDataValue from '@mui/x-data-grid-generator/services/tree-data-generator';


function ProductsTable() {
  return (
    <GridToolbarContainer>
      <GridToolbarFilterButton />
      <GridToolbarDensitySelector />
    </GridToolbarContainer>
  );
}

export default function SearchPage({query} : {query: string}) {

  const columns: GridColDef[] = [
    { field: "name", headerName: "Name", flex: 1 },
    { field: "price", headerName: "Price", flex: 1, /*sortComparator: {}*/ },
    { field: "category", headerName: "Category", flex: 1 },
  ];

  
  // map products to Map indexing items by id
  const productsLst = fetchProducts(query);
  const products : Map<number, Product> = Object.assign({}, ...productsLst.map((p : Product) => ({[p.id]: p})));
  
  type ProductRowType = {
    id: number,
    name: string,
    price: number,
    category: string
  }


  let productsRows : ProductRowType[] = [];
  
  for(const p of productsLst)
  {
    productsRows.push( {
      id: p.id,
      name: p.name,
      price: p.price,
      category: p.category
    });
  }


  return (
    <Box>  
      <Navbar/>
        <div style={{ height: 700, width: '100%' }}>
          
        <DataGrid
          rows={productsRows}
          columns={columns}
          components={{
            Toolbar: ProductsTable,
          }}
        />
    </div>
        </Box>
  );
}

// const SearchPage = ({ query } : {query : string}) => {
//   return (
//     <CustomToolbar />
    // <DataGrid
    //   rows={["rowA", "rowB"]}
    //   columns={["col1", "col2", "col3"]}

    // />
  //   <DataGrid 
  //   {...[5]}
  //   components={{
  //     Toolbar: GridToolbar,
  //   }}
  // />
  // )
// }

SearchPage.defaultProps = {
  query: ""
}

// export default SearchPage

