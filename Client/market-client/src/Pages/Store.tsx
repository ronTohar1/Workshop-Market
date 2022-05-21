import * as React from "react";
import { alpha } from "@mui/material/styles";
import Box from "@mui/material/Box";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TablePagination from "@mui/material/TablePagination";
import TableRow from "@mui/material/TableRow";
import TableSortLabel from "@mui/material/TableSortLabel";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Paper from "@mui/material/Paper";
import Checkbox from "@mui/material/Checkbox";
import IconButton from "@mui/material/IconButton";
import Tooltip from "@mui/material/Tooltip";
import FormControlLabel from "@mui/material/FormControlLabel";
import Switch from "@mui/material/Switch";
import DeleteIcon from "@mui/icons-material/Delete";
import FilterListIcon from "@mui/icons-material/FilterList";
import { visuallyHidden } from "@mui/utils";

import { Stack, TextField } from "@mui/material";
import { Fab, makeStyles } from "@mui/material";
import AddShoppingCartIcon from "@mui/icons-material/AddShoppingCart";
import AppBar from "@mui/material/AppBar";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import Navbar from "../components/Navbar";
import Autocomplete from "@mui/material/Autocomplete";
import { Product, createProduct, Store, createStore } from "../Utils";

const storeName = "store 1";

const storeProds = [
  createProduct(1, "Cupcake", 305, 3),
  createProduct(2, "Hamburger", 30, 33),
  createProduct(3, "Salad", 340, 3000),
  createProduct(4, "Cheese", 130, 232),
  createProduct(5, "Banana", 35, 22),
  createProduct(6, "Cooler", 3051, 22),
  createProduct(7, "Sunglasses", 3035, 223),
  createProduct(8, "Elephant", 10, 32),
  createProduct(9, "Zebra", 3, 21),
  createProduct(10, "Hot Dog", 100, 222),
];
const store: Store = createStore(1, "Cool Store", storeProds);

const ProductsRows = store.Products;

function descendingComparator<T>(a: T, b: T, orderBy: keyof T) {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }
  if (b[orderBy] > a[orderBy]) {
    return 1;
  }
  return 0;
}

type Order = "asc" | "desc";

function getComparator<Key extends keyof any>(
  order: Order,
  orderBy: Key
): (
  a: { [key in Key]: number | string },
  b: { [key in Key]: number | string }
) => number {
  return order === "desc"
    ? (a, b) => descendingComparator(a, b, orderBy)
    : (a, b) => -descendingComparator(a, b, orderBy);
}

// This method is created for cross-browser compatibility, if you don't
// need to support IE11, you can use Array.prototype.sort() directly
function stableSort<T>(
  array: readonly T[],
  comparator: (a: T, b: T) => number
) {
  const stabilizedThis = array.map((el, index) => [el, index] as [T, number]);
  stabilizedThis.sort((a, b) => {
    const order = comparator(a[0], b[0]);
    if (order !== 0) {
      return order;
    }
    return a[1] - b[1];
  });
  return stabilizedThis.map((el) => el[0]);
}

interface HeadCell {
  disablePadding: boolean;
  id: keyof Product;
  label: string;
  numeric: boolean;
}

const headCells: readonly HeadCell[] = [
  {
    id: "Name",
    numeric: false,
    disablePadding: true,
    label: "All Products",
  },
  {
    id: "Price",
    numeric: true,
    disablePadding: false,
    label: "Price (NIS)",
  },
  {
    id: "Available_Quantity",
    numeric: true,
    disablePadding: false,
    label: "Available Quantity",
  },
];

interface EnhancedTableProps {
  numSelected: number;
  onRequestSort: (
    event: React.MouseEvent<unknown>,
    property: keyof Product
  ) => void;
  onSelectAllClick: (event: React.ChangeEvent<HTMLInputElement>) => void;
  order: Order;
  orderBy: string;
  rowCount: number;
}

function EnhancedTableHead(props: EnhancedTableProps) {
  const {
    onSelectAllClick,
    order,
    orderBy,
    numSelected,
    rowCount,
    onRequestSort,
  } = props;
  const createSortHandler =
    (property: keyof Product) => (event: React.MouseEvent<unknown>) => {
      onRequestSort(event, property);
    };

  return (
    <TableHead>
      <TableRow>
        <TableCell padding='checkbox'>
          <Checkbox
            color='primary'
            indeterminate={numSelected > 0 && numSelected < rowCount}
            checked={rowCount > 0 && numSelected === rowCount}
            onChange={onSelectAllClick}
            inputProps={{
              "aria-label": "select all desserts",
            }}
          />
        </TableCell>
        {headCells.map((headCell) => (
          <TableCell
            key={headCell.id}
            align={headCell.numeric ? "right" : "left"}
            padding={headCell.disablePadding ? "none" : "normal"}
            sortDirection={orderBy === headCell.id ? order : false}>
            <TableSortLabel
              active={orderBy === headCell.id}
              direction={orderBy === headCell.id ? order : "asc"}
              onClick={createSortHandler(headCell.id)}>
              {headCell.label}
              {orderBy === headCell.id ? (
                <Box component='span' sx={visuallyHidden}>
                  {order === "desc" ? "sorted descending" : "sorted ascending"}
                </Box>
              ) : null}
            </TableSortLabel>
          </TableCell>
        ))}
      </TableRow>
    </TableHead>
  );
}

interface EnhancedTableToolbarProps {
  numSelected: number;
  selected: readonly number[];
  handleAddToCart: () => void;
  rows: Product[];
  handleFilter: (s1: string) => void;
}

const EnhancedTableToolbar = (props: EnhancedTableToolbarProps) => {
  const { numSelected, selected, handleAddToCart, rows, handleFilter } = props;
  const [value, setValue] = React.useState("");

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
      }}>
      {numSelected > 0 ? (
        <Typography
          sx={{ flex: "1 1 100%" }}
          color='inherit'
          variant='subtitle1'
          component='div'>
          {numSelected} selected
        </Typography>
      ) : (
        <Typography
          sx={{ flex: "1 1 100%" }}
          variant='h6'
          id='tableTitle'
          component='div'>
          {storeName}
        </Typography>
      )}
      {numSelected > 0 ? (
        <Tooltip title='Add To Cart'>
          <Fab
            size='medium'
            color='primary'
            aria-label='add'
            onClick={handleAddToCart}>
            <AddShoppingCartIcon />
          </Fab>
        </Tooltip>
      ) : (
        <Stack direction='row' spacing={2} sx={{ width: 300 }}>
          <TextField
            id='contained'
            InputLabelProps={{
              shrink: true,
            }}
            placeholder='Find Products in store'
            size='small'
            value={value}
            onChange={(e) => {
              setValue(e.target.value);
            }}
          />
          <Tooltip title='Filter list'>
            <IconButton
              onClick={() => {
                handleFilter(value);
              }}>
              <FilterListIcon />
            </IconButton>
          </Tooltip>
        </Stack>
      )}
    </Toolbar>
  );
};

function EnhancedTable() {
  const [order, setOrder] = React.useState<Order>("asc");
  const [orderBy, setOrderBy] = React.useState<keyof Product>("Name");
  const [selected, setSelected] = React.useState<readonly number[]>([]);
  const [page, setPage] = React.useState(0);
  const [dense, setDense] = React.useState(false);
  const [rowsPerPage, setRowsPerPage] = React.useState(5);
  const [rows, setRows] = React.useState(ProductsRows);

  const handleAddToCart = () => {
    rows.forEach((row) => {
      if (isSelected(row.Id)) alert(row.Name + " is selected");
    });
  };

  const handleRequestSort = (
    event: React.MouseEvent<unknown>,
    property: keyof Product
  ) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleFilter = (searchString: string) => {
    const newRows = ProductsRows.filter((prod) =>
      prod.Name.toLowerCase().includes(searchString.toLowerCase())
    );
    setRows(newRows);
  };

  const handleSelectAllClick = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.checked && selected.length == 0) {
      const newSelecteds = rows.map((n) => n.Id);
      setSelected(newSelecteds);
      return;
    }
    setSelected([]);
  };

  const handleClick = (event: React.MouseEvent<unknown>, Id: number) => {
    const selectedIndex = selected.indexOf(Id);
    let newSelected: readonly number[] = [];

    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, Id);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1)
      );
    }

    setSelected(newSelected);
  };

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const handleChangeDense = (event: React.ChangeEvent<HTMLInputElement>) => {
    setDense(event.target.checked);
  };

  const isSelected = (Id: number) => selected.indexOf(Id) !== -1;

  // Avoid a layout jump when reaching the last page with empty rows.
  const emptyRows =
    page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

  const theme = createTheme({
    typography: {
      fontFamily: [
        "Nunito",
        "Roboto",
        "Helvetica Neue",
        "Arial",
        "sans-serif",
      ].join(","),
    },
  });

  return (
    <ThemeProvider theme={theme}>
      <Box sx={{ width: "100%" }}>
        <Paper sx={{ width: "100%", mb: 2 }}>
          <EnhancedTableToolbar
            selected={selected}
            numSelected={selected.length}
            handleAddToCart={handleAddToCart}
            rows={rows}
            handleFilter={handleFilter}
          />
          <TableContainer sx={{ display: "flex", justifyContent: "center" }}>
            <Table
              aria-labelledby='tableTitle'
              size={dense ? "small" : "medium"}>
              <EnhancedTableHead
                numSelected={selected.length}
                order={order}
                orderBy={orderBy}
                onSelectAllClick={handleSelectAllClick}
                onRequestSort={handleRequestSort}
                rowCount={rows.length}
              />
              <TableBody>
                {/* if you don't need to support IE11, you can replace the `stableSort` call with:
              rows.slice().sort(getComparator(order, orderBy)) */}
                {stableSort(rows, getComparator(order, orderBy))
                  .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                  .map((row, index) => {
                    const isItemSelected = isSelected(row.Id);
                    const labelId = `enhanced-table-checkbox-${index}`;

                    return (
                      <TableRow
                        hover
                        onClick={(event) => handleClick(event, row.Id)}
                        role='checkbox'
                        aria-checked={isItemSelected}
                        tabIndex={-1}
                        key={row.Id}
                        selected={isItemSelected}>
                        <TableCell padding='checkbox'>
                          <Checkbox
                            color='primary'
                            checked={isItemSelected}
                            inputProps={{
                              "aria-labelledby": labelId,
                            }}
                          />
                        </TableCell>
                        <TableCell
                          component='th'
                          id={labelId}
                          scope='row'
                          padding='none'>
                          {row.Name}
                        </TableCell>
                        <TableCell align='right'>{row.Price}</TableCell>
                        <TableCell align='right'>
                          {row.Available_Quantity}
                        </TableCell>
                      </TableRow>
                    );
                  })}
                {emptyRows > 0 && (
                  <TableRow
                    style={{
                      height: (dense ? 33 : 53) * emptyRows,
                    }}>
                    <TableCell colSpan={6} />
                  </TableRow>
                )}
              </TableBody>
            </Table>
          </TableContainer>
          <TablePagination
            rowsPerPageOptions={[5, 10, 25]}
            component='div'
            count={rows.length}
            rowsPerPage={rowsPerPage}
            page={page}
            onPageChange={handleChangePage}
            onRowsPerPageChange={handleChangeRowsPerPage}
          />
        </Paper>
        <FormControlLabel
          control={<Switch checked={dense} onChange={handleChangeDense} />}
          label='Dense padding'
        />
      </Box>
    </ThemeProvider>
  );
}

export default function Store() {
  return (
    <div>
      <Navbar />
      <EnhancedTableToolbar />
    </div>
  );
}
