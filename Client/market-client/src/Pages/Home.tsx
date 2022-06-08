import * as React from "react"
import Navbar from "../Componentss/Navbar"
import { Typography } from "@mui/material"
import { ProductHeroLayout } from "../Componentss/ProductHeroLayout"
import { styled } from "@mui/material/styles"
import Button from "@mui/material/Button"
import AppBar from "@mui/material/AppBar"
import Grid from "@mui/material/Grid"
import ButtonGroup from "@mui/material/ButtonGroup"
import Box from "@mui/material/Box"
import { createTheme, ThemeProvider } from "@mui/material/styles"
import { pathLogin, pathRegister, pathSearch } from "../Paths"

const cards = [1, 2, 3]

// const backgroundImage =
//   "https://images.unsplash.com/photo-1471193945509-9ad0617afabf"

const createButton = (name: string, path: string) => {
  return (
    <Button
      href={path}
      style={{ height: 100, width: 500 }}
      key="name"
      variant="contained"
      size="large"
      color="primary"
      sx={{
        m: 1,
        "&:hover": {
          borderRadius: 5,
        },
      }}
    >
      {name}
    </Button>
  )
  //   <Button
  //   color="secondary"
  //   variant="contained"
  //   size="large"
  //   component="a"
  //   href="/store"
  //   sx={{ minWidth: 200 }}
  // >
}
const buttons = [
  createButton("Continue To Website", pathSearch),
  createButton("Create An Account", pathRegister),
  createButton("Log In To Your Account", pathLogin),
]

const theme = createTheme({
  palette: {
    mode: "light",
  },
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
const styles = {
  paperContainer: {
    height: 1356,
    backgroundImage: `url(${"https://images.unsplash.com/photo-1630326120534-9619533ba0d3?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80"})`,
  },
}
export default function Home() {
  return (
    <ThemeProvider theme={theme}>
      <main>
        <div style={styles.paperContainer}>
        {/* Increase the network loading priority of the background image. */}
        {/* <img
          style={{ display: "none" }}
          src={backgroundImage}
          alt="increase priority"
        /> */}
        <Typography color="primary" align="center" variant="h2">
          Buy smart and cheap
        </Typography>
        <Typography
          color="inherit"
          align="center"
          variant="h5"
          sx={{ mb: 4, mt: { sx: 4, sm: 2 } }}
        >
          Enjoy a variety of products, and mostly the suffering of 6 naive
          students
        </Typography>
        <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            justifyContent: "center",
          }}
        >
          <ButtonGroup
            orientation="vertical"
            aria-label="vertical contained button group"
            variant="text"
          >
            {buttons}
          </ButtonGroup>
        </Box>
        </div>
      </main>
    </ThemeProvider>
  )
}
