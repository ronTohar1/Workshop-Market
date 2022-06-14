import * as React from "react"
import {
  DataGrid,
  GridToolbarContainer,
  GridToolbarFilterButton,
  GridToolbarDensitySelector,
  GridColDef,
} from "@mui/x-data-grid"
import Box from "@mui/material/Box"
import AddBusinessIcon from "@mui/icons-material/AddBusiness"
// import CustomToolbarGrid  from '../components/ProductsList'
// import * as React from 'react';

import Typography from "@mui/material/Typography"

import {
  AppBar,
  Button,
  Dialog,
  Divider,
  FormGroup,
  IconButton,
  List,
  ListItem,
  ListItemText,
  makeStyles,
  Paper,
  Stack,
  styled,
  Switch,
  TextField,
  Toolbar,
} from "@mui/material"
import { createTheme, ThemeProvider } from "@mui/material/styles"
import Navbar from "../Componentss/Navbar"

import Product from "../DTOs/Product"
import { Card, CardActions, CardContent } from "@mui/material"
import {
  serverGetStore,
  serverCloseStore,
  serverOpenNewStore,
} from "../services/StoreService"
import { fetchStoresManagedBy } from "../services/MemberService"
import Grid from "@mui/material/Grid"
import Store from "../DTOs/Store"

import StoreIcon from "@mui/icons-material/Store"
import CloseIcon from "@mui/icons-material/Close"
import StoreDialog from "../Componentss/ManagerEditStore/StoreManagerStoreDialog"
import StorePage from "./StorePage"
import { fetchResponse } from "../services/GeneralService"
import { getBuyerId, getIsGuest, getUsername } from "../services/SessionService"
import { useNavigate } from "react-router-dom"
import { pathHome } from "../Paths"
import LoadingCircle from "../Componentss/LoadingCircle"
import AddStoreForm from "../Componentss/Forms/AddStoreForm"
import SuccessSnackbar from "../Componentss/Forms/SuccessSnackbar"
import FailureSnackbar from "../Componentss/Forms/FailureSnackbar"

const UserInfoCard = (numOfManagedStores: number,username:string) => {
  return (
    <Card elevation={5}>
      <CardContent>
        <Typography variant="h4" component="div">
          Account Information
        </Typography>

        <Typography variant="h6">Hello {username}</Typography>
        <Typography variant="h6">website-ID: {getBuyerId()}</Typography>

        <Typography variant="h6">
          <b>Number Of Managed Stores</b>: {numOfManagedStores}
        </Typography>
      </CardContent>
    </Card>
  )
}

const StoreCard = ({
  store,
  handleChangedStore,
  showSuccess 
}: {
  store: Store
  handleChangedStore: (s: Store) => void
  showSuccess: (m:string)=>void 

}) => {
  const [openDialog, setOpenDialog] = React.useState(false)
  const [isStoreOpen, setIsStoreOpen] = React.useState(store.isOpen)

  const handleClickOpenDialog = () => {
    setOpenDialog(true)
  }

  const handleCloseDialog = () => {
    setOpenDialog(false)
  }

  const handleChangeStoreOpen = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const openStore: boolean = event.target.checked
    if (openStore) alert("Sorry, closed stores cannot be re-opened")
    else
      fetchResponse(serverCloseStore(getBuyerId(), store.id))
        .then((closed: boolean) => {
          if (closed) showSuccess("Store "+ store.name +" Closed Successfully")
          setIsStoreOpen(false)
          handleChangedStore(store)
        })
        .catch((e) => {
          alert(e)
        })
  }

  return (
    <div>
      {openDialog && (
        <StoreDialog
          store={store}
          handleCloseDialog={handleCloseDialog}
          handleChangedStore={handleChangedStore}
        />
      )}
      <Card sx={{ display: "flex" }} elevation={6} component={Paper}>
        <CardContent sx={{ display: "flex", flexDirection: "column" }}>
          <Typography sx={{ mb: 3 }} variant="h3" component="div">
            Store "{store.name}"
          </Typography>

          <Button
            variant="contained"
            endIcon={<StoreIcon />}
            sx={{ mb: 3 }}
            onClick={handleClickOpenDialog}
            disabled={!isStoreOpen}
          >
            To the store
          </Button>
          <FormGroup>
            <Stack direction="row" spacing={1} alignItems="center">
              <Typography>
                Store is now {isStoreOpen ? "Open" : "Closed"}
              </Typography>
              <Switch checked={isStoreOpen} disabled={!isStoreOpen} onChange={handleChangeStoreOpen} />
            </Stack>
          </FormGroup>
        </CardContent>
      </Card>
    </div>
  )
}

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
})

const Item = styled("div")(({ theme }) => ({
  ...theme.typography.body2,
  padding: theme.spacing(3),
  textAlign: "center",
  color: theme.palette.text.secondary,
}))

const ManagedStores = (
  stores: Store[],
  handleChangedStore: (store: Store) => void,
  handleOpenStoreForm: () => void,
  showSuccessSnack: (m:string)=>void
) => {
  return (
    <Grid>
      <Box
        sx={{
          justifyContent: "center",
          display: "flex",
          width: "100%",
          mt: 3,
          mb: 3,
          ml: 3,
          mr: 3,
        }}
      >
        <Stack>
          <Typography variant="h3" component="div">
            Stores You Manage
          </Typography>
          <Button
            variant="contained"
            sx={{
              width: "100%",
              mt: 2,
              display: "flex",
              justifyContent: "center",
            }}
            endIcon={<AddBusinessIcon />}
            onClick={handleOpenStoreForm}
          >
            Add A Store
          </Button>
        </Stack>
      </Box>
      <Grid
        sx={{ ml: 2 }}
        container
        flex={1}
        rowSpacing={1}
        columnSpacing={{ xs: 1, sm: 2, md: 3 }}
      >
        {stores.map((s) => (
          <Item>
            <StoreCard store={s} handleChangedStore={handleChangedStore} showSuccess={showSuccessSnack} />
          </Item>
        ))}
      </Grid>
    </Grid>
  )
}

export default function StoreManagerPage() {
  const startingPageSize = 5
  const [pageSize, setPageSize] = React.useState<number>(startingPageSize)
  const [stores, setStores] = React.useState<Store[] | null>(null) // sotres managed by current member

  const [openStoreForm, setOpenStoreForm] = React.useState<boolean>(false)
  const [openSuccSnack, setOpenSuccSnack] = React.useState<boolean>(false)
  const [successProductMsg, setSuccessProductMsg] = React.useState<string>("")
  const [openFailSnack, setOpenFailSnack] = React.useState<boolean>(false)
  const [failureProductMsg, setFailureProductMsg] = React.useState<string>("")
  const [username,setUsername] = React.useState<string>("");
  const showFailureSnack = (msg: string) => {
    setOpenFailSnack(true)
    setFailureProductMsg(msg)
  }

  const navigate = useNavigate()
  const showSuccessSnack = (msg: string) => {
    setOpenSuccSnack(true)
    setSuccessProductMsg(msg)
    setOpenStoreForm(false)
  }
  const handleAddStore = (storeName: string) => {
    fetchResponse(serverOpenNewStore(getBuyerId(), storeName))
      .then((storeId: number) => fetchResponse(serverGetStore(storeId)))
      .then((newStore: Store) =>
        setStores(stores === null ? [newStore] : stores.concat(newStore))
      )
      .then(() => showSuccessSnack("Added " + storeName + " Successfully"))
      .catch(showFailureSnack)
  }

  if (getIsGuest()) {
    alert("You are not allowed to visit this page!")
    navigate(`${pathHome}`)
  }

  React.useEffect(() => {
    // alert(getUsername())
    setUsername(getUsername())
    fetchStoresManagedBy(getBuyerId()).then((managedStores: Store[]) => {
      setStores(managedStores)
    })
  }, [])

  const handleChangedStore = (changedStore: Store) => {
    fetchResponse(serverGetStore(changedStore.id))
      .then((loadedStore: Store) => {
        const newStores = stores?.map((currStore) => {
          if (currStore.id === loadedStore.id) return loadedStore
          return currStore
        })
        console.log(newStores)
        setStores(newStores || null)
      })
      .catch((e) => {
        alert(e)
        setStores([])
      })
  }

  return stores !== null ? (
    <ThemeProvider theme={theme}>
      <Box>
        <Box>
          <Navbar />
        </Box>
        <Box sx={{ ml: 3, mr: 3 }}>
          <Grid
            item
            xs={2}
            sm={4}
            sx={{
              justifyContent: "cemter",
              mt: 3,
              alignItems: "center",
            }}
          >
            {/* <Box
                sx={{
                  justifyContent: "center",
                  display: "flex",
                  width: "100%",
                }}
              > */}
            {UserInfoCard(stores.length, username)}
            {/* </Box> */}
          </Grid>
          {ManagedStores(stores, handleChangedStore, () =>
            setOpenStoreForm(true),showSuccessSnack
          )}
        </Box>
      </Box>

      {/* Unrelated dialogs: */}
      <AddStoreForm
        open={openStoreForm}
        handleAddStore={handleAddStore}
        handleClose={() => setOpenStoreForm(false)}
      />
      <Dialog open={openFailSnack}>
        {FailureSnackbar(failureProductMsg, openFailSnack, () =>
          setOpenFailSnack(false)
        )}
      </Dialog>
      {SuccessSnackbar(successProductMsg, openSuccSnack, () =>
        setOpenSuccSnack(false)
      )}
    </ThemeProvider>
  ) : (
    LoadingCircle()
  )
}

// export default SearchPage;
