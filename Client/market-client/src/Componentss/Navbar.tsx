import * as React from "react"
import { styled, alpha } from "@mui/material/styles"
import AppBar from "@mui/material/AppBar"
import Box from "@mui/material/Box"
import Toolbar from "@mui/material/Toolbar"
import IconButton from "@mui/material/IconButton"
import Typography from "@mui/material/Typography"
import InputBase from "@mui/material/InputBase"
import Badge from "@mui/material/Badge"
import MenuItem from "@mui/material/MenuItem"
import Menu from "@mui/material/Menu"
import SearchIcon from "@mui/icons-material/Search"
import AccountCircle from "@mui/icons-material/AccountCircle"
import NotificationsIcon from "@mui/icons-material/Notifications"
import Button from "@mui/material/Button"
import { createTheme, ThemeProvider } from "@mui/material/styles"
import { BadgeProps, List, ListItem, ListItemText } from "@mui/material"
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart"
import Stack from "@mui/material/Stack"
import {
  pathCart,
  pathHome,
  pathLogin,
  pathSearch,
  pathStoreManager,
} from "../Paths"
import { Link, Navigate } from "react-router-dom"
import { useNavigate } from "react-router-dom"
import { Tooltip } from "@mui/material"
import MarketNotification from "../DTOs/MarketNotification"
import { clearNotifications, dummyNotificaitons, noteConn } from "../services/NotificationsService"
import { getCartProducts } from "../services/ProductsService"
import { fetchResponse } from "../services/GeneralService"
import { clearSession, getBuyerId, getIsGuest, getUsername, initSession, storage } from "../services/SessionService"
import { serverGetCart, serverLogout } from "../services/BuyersService"
import Cart from "../DTOs/Cart"
import { addNotificationListener } from '../services/NotificationsService'
import { Logout } from "@mui/icons-material"

const currentNotifications = dummyNotificaitons


const Search = styled("div")(({ theme }) => ({
  position: "relative",
  borderRadius: theme.shape.borderRadius,
  backgroundColor: alpha(theme.palette.common.white, 0.15),
  "&:hover": {
    backgroundColor: alpha(theme.palette.common.white, 0.25),
  },
  marginRight: theme.spacing(2),
  marginLeft: 0,
  width: "100%",
  [theme.breakpoints.up("sm")]: {
    marginLeft: theme.spacing(3),
    width: "auto",
  },
}))

const StyledBadge = styled(Badge)<BadgeProps>(({ theme }) => ({
  "& .MuiBadge-badge": {
    right: -3,
    top: 13,
    border: `2px solid ${theme.palette.background.paper}`,
    padding: "0 4px",
  },
}))

const SearchIconWrapper = styled("div")(({ theme }) => ({
  padding: theme.spacing(0, 2),
  height: "100%",
  position: "absolute",
  pointerEvents: "none",
  display: "flex",
  alignItems: "center",
  justifyContent: "center",
}))

const StyledInputBase = styled(InputBase)(({ theme }) => ({
  color: "inherit",
  "& .MuiInputBase-input": {
    padding: theme.spacing(1, 1, 1, 0),
    // vertical padding + font size from searchIcon
    paddingLeft: `calc(1em + ${theme.spacing(4)})`,
    transition: theme.transitions.create("width"),
    width: "100%",
    [theme.breakpoints.up("md")]: {
      width: "20ch",
    },
  },
}))

const notificationsMenu = (
  open: boolean,
  anchor: any,
  handleClose: any,
  currNotifications: MarketNotification[],
  handleDismissNotifications: (note: MarketNotification) => void
) => {
  return (
    <Menu anchorEl={anchor} open={open} onClose={handleClose}>
      {currNotifications.map((note: MarketNotification) => (
        <Tooltip title="Click To Dismiss">
          <MenuItem onClick={() => handleDismissNotifications(note)}>
            {note.description}
          </MenuItem>
        </Tooltip>
      ))}
    </Menu>
  )
}

export default function Navbar() {
  // const navigate = (x: any) => {};

  const navigate = useNavigate()
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null)
  const [searchValue, setSearchValue] = React.useState("")
  const [anchorElNotifications, setAnchorElNotifications] =
    React.useState<null | HTMLElement>(null)
  const [openNotifications, setOpenNotifications] =
    React.useState<boolean>(false)
  const [notifications, setNotifications] =
    React.useState<MarketNotification[]>(currentNotifications)
  const [numItemsInCart, setNumItemsInCart] = React.useState<number>(0)

  // React.useEffect(() => {
  //   fetchResponse(serverGetCart(getBuyerId()))
  //     .then((cart: Cart) => {
  //       const [prodsIds, prodsToQuantity] = getCartProducts(cart)
  //       setNumItemsInCart(prodsIds.length)
  //     })
  //     .catch((e) => {
  //       alert("Couldn't load some of your information")
  //     })
  // })

  // React.useEffect(()=>{
  //   console.log(noteConn.notifications)
  //   if (noteConn.notifications.length > 0)
  //     alert("new message!")
  //   clearNotifications()
  // },[noteConn.notifications])


  const handleClickHome = () => {
    navigate(`${pathHome}`)
  }
  const handleProfileMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget)
  }

  const handleOpenNotifications = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNotifications(event.currentTarget)
    setOpenNotifications(!openNotifications && notifications.length > 0)
  }

  const handleMenuClose = () => {
    setAnchorEl(null)
  }

  const handleDismissNotification = (noteToDis: MarketNotification) => {
    const newNotifications = notifications.filter(
      (note: MarketNotification) => note.id != noteToDis.id
    )
    setNotifications(newNotifications)
    if (newNotifications.length === 0) setOpenNotifications(false)
  }

  const handleSearch = () => {
    // navigate(pathSearch)
    navigate(`${pathSearch}?query=${searchValue}`)
  }

  const handleMyAccountClick = () => {
    setAnchorEl(null)
    if (getIsGuest()) navigate(`${pathLogin}`)
    else navigate(`${pathStoreManager}`)
  }

  const menuId = "primary-search-account-menu"
  const renderMenuAccount = (
    <Menu
      anchorEl={anchorEl}
      anchorOrigin={{
        vertical: "top",
        horizontal: "right",
      }}
      id={menuId}
      keepMounted
      transformOrigin={{
        vertical: "top",
        horizontal: "right",
      }}
      open={Boolean(anchorEl)}
      onClose={handleMenuClose}
    >
      <MenuItem onClick={handleMyAccountClick}>My account</MenuItem>
    </Menu>
  )

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

  const handleLogout = () => {
    if (getIsGuest()) {
      alert("First Login Before Logging Out!")
    }
    else {
      fetchResponse(serverLogout(getBuyerId()))
        .then(
          (success: boolean) => {
            alert("Good Bye " + getUsername())

            clearSession()
            initSession().then(() => navigate(pathHome))
          }
        )
        .catch(alert)
    }
  }
  return (
    <ThemeProvider theme={theme}>
      <AppBar position="sticky">
        <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
          <div>
            <Stack direction='row' spacing={8}>
              <Typography
                onClick={handleClickHome}
                variant="h6"
                noWrap
                component="div"
                sx={{
                  display: { xs: "none", sm: "block" },
                  "&:hover": {
                    cursor: "pointer",
                  },
                }}
              >
                Ronto's Market
              </Typography>

              <Typography
                onClick={handleMyAccountClick}
                variant="h6"
                noWrap
                component="div"
                sx={{
                  display: { xs: "none", sm: "block" },
                  "&:hover": {
                    cursor: "pointer",
                  },
                  ml: '10'
                }}
              >
                {getIsGuest() ? "Hello Guest" : "Hello " + getUsername()}
              </Typography>
            </Stack>

            <Box sx={{}} />
          </div>
          <div>
            <Box
              component="form"
              noValidate
              onSubmit={(e: any) => {
                handleSearch()
              }}
            >
              <Stack direction="row" spacing={2}>
                <Search sx={{ flexGrow: 1 }}>
                  <SearchIconWrapper>
                    <SearchIcon />
                  </SearchIconWrapper>
                  <StyledInputBase
                    id="query"
                    name="query"
                    onChange={(e) => setSearchValue(e.target.value)}
                    sx={{ flexGrow: 1, width: "30vw" }}
                    placeholder="Search Products..."
                    inputProps={{ "aria-label": "search", width: "auto" }}
                  />
                </Search>
                <Button
                  variant="outlined"
                  color="inherit"
                  startIcon={<SearchIcon />}
                  type="submit"
                >
                  Search
                </Button>
              </Stack>
            </Box>
            {/* <Box sx={{ display: { xs: 'none', sm: 'block' }, direction: 'row' }}>

                        </Box> */}
          </div>
          <div>
            <Box sx={{ display: { xs: "none", md: "flex" } }}>
              <Tooltip
                title={`You have ${notifications.length} new notifications`}
              >
                <IconButton
                  size="large"
                  aria-label="show new notifications"
                  color="inherit"
                  onClick={handleOpenNotifications}
                >
                  <Badge badgeContent={notifications.length} color="error">
                    <NotificationsIcon />
                  </Badge>
                </IconButton>
              </Tooltip>
              {notificationsMenu(
                openNotifications,
                anchorElNotifications,
                () => setOpenNotifications(false),
                notifications,
                handleDismissNotification
              )}
              <IconButton
                aria-label="cart"
                size="large"
                color="inherit"
                component={Link}
                to={pathCart}
              >
                <StyledBadge badgeContent={numItemsInCart} color="secondary">
                  <ShoppingCartIcon />
                </StyledBadge>
              </IconButton>
              <Tooltip title="Logout from your account">
                <IconButton
                  size="large"
                  edge="end"
                  aria-label="account of current user"
                  aria-controls={menuId}
                  aria-haspopup="true"
                  onClick={handleLogout}
                  color="inherit"
                >
                  <Logout />
                </IconButton>
              </Tooltip>
              <IconButton
                size="large"
                edge="end"
                aria-label="account of current user"
                aria-controls={menuId}
                aria-haspopup="true"
                onClick={handleProfileMenuOpen}
                color="inherit"
              >
                <AccountCircle />
              </IconButton>
            </Box>
          </div>
        </Toolbar>
      </AppBar>
      {renderMenuAccount}
    </ThemeProvider>
  )
}
