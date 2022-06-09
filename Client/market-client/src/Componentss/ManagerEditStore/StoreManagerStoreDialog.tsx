import * as React from "react"
import {
  DataGrid,
  GridToolbarContainer,
  GridToolbarFilterButton,
  GridToolbarDensitySelector,
  GridColDef,
} from "@mui/x-data-grid"
import Box from "@mui/material/Box"

// import CustomToolbarGrid  from '../components/ProductsList'
// import * as React from 'react';

import Typography from "@mui/material/Typography"

import {
  AppBar,
  Button,
  Dialog,
  Divider,
  Fab,
  FormControlLabel,
  FormGroup,
  IconButton,
  List,
  ListItem,
  ListItemText,
  makeStyles,
  Paper,
  Stack,
  Switch,
  Tab,
  Tabs,
  TextField,
  Toolbar,
} from "@mui/material"
import { createTheme, ThemeProvider } from "@mui/material/styles"
import Navbar from "../Navbar"

import Product from "../../DTOs/Product"
import { Card, CardActions, CardContent } from "@mui/material"
import { serverGetStore } from "../../services/StoreService"
import ExitToAppIcon from "@mui/icons-material/ExitToApp"
import {
  fetchStoresManagedBy,
} from "../../services/MemberService"
import Grid from "@mui/material/Grid"
import Store from "../../DTOs/Store"

import StoreIcon from "@mui/icons-material/Store"
import SendIcon from "@mui/icons-material/Send"
import CloseIcon from "@mui/icons-material/Close"
import { Label } from "@mui/icons-material"
import StorePage from "../../Pages/StorePage"
import StorePageOfManager from "./StorePageOfManager"
import StoreRoles from "./StoreRoles"
import StorePurchases from "./StorePurchases"
import StorePurchasePolicies from "./StorePurchasePolicies"
import StoreDiscountPolicies from "./StoreDiscountPolicies"

interface TabPanelProps {
  children?: React.ReactNode
  index: number
  value: number
}

function TabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box sx={{ p: 3 }}>
          <Typography>{children}</Typography>
        </Box>
      )}
    </div>
  )
}

function ProductsTable() {
  return (
    <GridToolbarContainer>
      <GridToolbarFilterButton />
      <GridToolbarDensitySelector />
    </GridToolbarContainer>
  )
}

const columns: GridColDef[] = [
  { field: "name", headerName: "Product", flex: 2 },
  { field: "price", headerName: "Price", flex: 1 },
  { field: "category", headerName: "Category", flex: 1 },
  { field: "available_quantity", headerName: "Available Quantity", flex: 1 },
  // { field: "store", headerName: "Store", flex: 1 },
]

export function StoreTabs({
  store,
  handleChangedStore,
}: {
  store: Store
  handleChangedStore: (s: Store) => void
}) {
  const [value, setValue] = React.useState(0)
  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue)
  }

  return (
    <Box sx={{ width: "100%", mr: 3 }}>
      <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
        <Tabs
          value={value}
          onChange={handleChange}
          aria-label="basic tabs example"
        >
          <Tab label="Products" id="0" />
          <Tab label="Roles" id="1" />
          <Tab label="Purchases" id="2" />
          <Tab label="Purchase Policies" id="3" />
          <Tab label="Discount Policies" id="4" />
        </Tabs>
      </Box>
      <TabPanel value={value} index={0}>
        <StorePageOfManager
          store={store}
          handleChangedStore={handleChangedStore}
        />
      </TabPanel>
      <TabPanel value={value} index={1}>
        <StoreRoles
          store={store}
          handleChangedStore={handleChangedStore}
        />
      </TabPanel>
      <TabPanel value={value} index={2}>
        <StorePurchases
          store={store}
          handleChangedStore={handleChangedStore}
        />
      </TabPanel>
      <TabPanel value={value} index={3}>
      <StorePurchasePolicies
          store={store}
          handleChangedStore={handleChangedStore}
        />
      </TabPanel>
      <TabPanel value={value} index={4}>
      <StoreDiscountPolicies
          store={store}
          handleChangedStore={handleChangedStore}
        />
      </TabPanel>
    </Box>
  )
}

export default function StoreDialog({
  store,
  handleCloseDialog,
  handleChangedStore,
}: {
  store: Store
  handleCloseDialog: any
  handleChangedStore: (s: Store) => void
}) {
  return (
    <Dialog fullScreen open={true} onClose={handleCloseDialog}>
      <AppBar sx={{ position: "relative" }}>
        <Toolbar>
          <Typography sx={{ ml: 2, flex: 1 }} variant="h6" component="div">
            Store
          </Typography>
          <IconButton
            edge="start"
            color="inherit"
            onClick={handleCloseDialog}
            aria-label="close"
          >
            <CloseIcon />
          </IconButton>
        </Toolbar>
      </AppBar>
      <StoreTabs store={store} handleChangedStore={handleChangedStore} />
      {/* <List>
        <ListItem button>
          <ListItemText primary='Phone ringtone' secondary='Titania' />
        </ListItem>
        <Divider />
        <ListItem button>
          <ListItemText
            primary='Default notification ringtone'
            secondary='Tethys'
          />
        </ListItem>
      </List> */}
    </Dialog>
  )
}
