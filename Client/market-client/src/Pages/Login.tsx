import * as React from "react"
import Avatar from "@mui/material/Avatar"
import Button from "@mui/material/Button"
import CssBaseline from "@mui/material/CssBaseline"
import TextField from "@mui/material/TextField"
import FormControlLabel from "@mui/material/FormControlLabel"
import Checkbox from "@mui/material/Checkbox"
import Link from "@mui/material/Link"
import Paper from "@mui/material/Paper"
import Box from "@mui/material/Box"
import Grid from "@mui/material/Grid"
import LockOutlinedIcon from "@mui/icons-material/LockOutlined"
import Typography from "@mui/material/Typography"
import { createTheme, ThemeProvider } from "@mui/material/styles"
import { serverGetPendingMessages, serverLogin } from "../services/BuyersService"
import { useNavigate } from "react-router-dom"
import { pathAdmin, pathHome } from "../Paths"
import * as sessionService from "../services/SessionService"
import HomeIcon from "@mui/icons-material/Home"
import { fetchResponse } from '../services/GeneralService'
import { noteConn, setUpConnection } from "../services/NotificationsService"
import { addEventListener, alertFunc, initWebSocket } from "../App"
// import WebSocket from 'ws'

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

const backgroundImages = [
  "https://images.unsplash.com/photo-1472851294608-062f824d29cc",
  "https://images.unsplash.com/photo-1528698827591-e19ccd7bc23d",
  "https://images.unsplash.com/photo-1578916171728-46686eac8d58",
  "https://images.unsplash.com/photo-1604066867775-43f48e3957d8",
  "https://images.unsplash.com/photo-1542838132-92c53300491e",
  "https://images.unsplash.com/photo-1559631658-9705048d977e",
  "https://images.unsplash.com/photo-1502160348486-995f41fa55b1",
  "https://images.unsplash.com/photo-1601599963565-b7ba29c8e3ff",
]

const randBackgroundImage = () =>
  backgroundImages[Math.floor(Math.random() * backgroundImages.length)]

export default function Login() {
  const navigate = useNavigate()

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    if (!sessionService.getIsGuest()) {
      alert("You are already logged in!\nLog out before you try to log in")
      return
    }

    event.preventDefault()
    const data = new FormData(event.currentTarget)
    const username = data.get("username")?.toString()
    const password = data.get("password")?.toString()
    const result = serverLogin(username, password)
    if (username === undefined) // Not going to happen but
    {
      alert("Please enter username")
      return;
    }

    fetchResponse(result).then((memberId: number) => {

      if (username == "admin" && password == "admin") {
        sessionService.setIsGuest(false)
        sessionService.setBuyerId(memberId)
        //@ts-ignore
        sessionService.setUsername(username)
        sessionService.setIsAdmin(true)
        navigate(pathAdmin)
        return;
      }
      const address = `ws://127.0.0.1:7890/${username}-notifications`
      initWebSocket(address)

      alert("Logged in successfully!")
      sessionService.setIsGuest(false)
      sessionService.setBuyerId(memberId)
      //@ts-ignore
      sessionService.setUsername(username)

      fetchResponse(serverGetPendingMessages(username))
        .then((messages: string[]) => messages.forEach(alertFunc))
        .then(() => {
          navigate(pathHome)

        })
        .catch(alert)
    }).catch((e) => {
      alert(e)
    })

  }

  return (
    <ThemeProvider theme={theme}>
      {/* <IconButton size="large">
        <HomeIcon sx={{ fontsize: 100 }} color="primary" />
      </IconButton> */}

      <Grid container component="main" sx={{ height: "100vh" }}>
        <CssBaseline />
        <Grid
          item
          xs={false}
          sm={4}
          md={7}
          sx={{
            backgroundImage: `url(${randBackgroundImage()})`,
            backgroundRepeat: "no-repeat",
            backgroundColor: (t) =>
              t.palette.mode === "light"
                ? t.palette.grey[50]
                : t.palette.grey[900],
            backgroundSize: "cover",
            backgroundPosition: "center",
          }}
        />
        <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6} square>
          <Box
            sx={{
              my: 8,
              mx: 4,
              display: "flex",
              flexDirection: "column",
              alignItems: "center",
            }}
          >
            <Avatar sx={{ m: 1, bgcolor: "primary.main" }}>
              <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
              Sign in
            </Typography>
            <Box
              component="form"
              noValidate
              onSubmit={handleSubmit}
              sx={{ mt: 1 }}
            >
              <TextField
                margin="normal"
                required
                fullWidth
                id="username"
                label="Username"
                name="username"
                autoComplete="username"
                autoFocus
              />
              <TextField
                margin="normal"
                required
                fullWidth
                name="password"
                label="Password"
                type="password"
                id="password"
                autoComplete="current-password"
              />
              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2 }}
              >
                Sign In
              </Button>
              <Grid container>
                <Grid item xs>
                  <Button
                    variant="contained"
                    href={pathHome}
                    color="primary"
                    // sx={{ position: "absolute", top: "0px", right: "0px" }}
                    endIcon={<HomeIcon />}
                  >
                    Home
                  </Button>
                </Grid>
                <Grid item>
                  <Link href="/register" variant="body2">
                    {"Don't have an account? Sign Up"}
                  </Link>
                </Grid>
              </Grid>
            </Box>
          </Box>
        </Grid>
      </Grid>
    </ThemeProvider>
  )
}
