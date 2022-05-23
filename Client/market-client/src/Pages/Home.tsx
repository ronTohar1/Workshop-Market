import * as React from "react";
import Navbar from "../components/Navbar";
import { Typography } from "@mui/material";
import { ProductHeroLayout } from "../components/ProductHeroLayout";
import { styled } from "@mui/material/styles";
import Button from "@mui/material/Button";
import AppBar from "@mui/material/AppBar";
import Grid from "@mui/material/Grid";
import ButtonGroup from "@mui/material/ButtonGroup";
import Box from "@mui/material/Box";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { pathLogin, pathRegister, pathSearch } from "../Paths";

const cards = [1, 2, 3];

const backgroundImage =
  "https://images.unsplash.com/photo-1471193945509-9ad0617afabf";

const theme = createTheme();

const createButton = (name: string, path: string) => {
  return (
    <Button
      href={path}
      style={{ height: 100, width: 500 }}
      key='name'
      variant='contained'
      size='large'
      color='primary'
      sx={{
        m: 1,
        "&:hover": {
          borderRadius: 5,
        },
      }}>
      {name}
    </Button>
  );
  //   <Button
  //   color="secondary"
  //   variant="contained"
  //   size="large"
  //   component="a"
  //   href="/store"
  //   sx={{ minWidth: 200 }}
  // >
};
const buttons = [
  createButton("Continue To Website", pathSearch),
  createButton("Create An Account", pathRegister),
  createButton("Log In To Your Account", pathLogin),
];

export default function Home() {
  const bar = Navbar();
  return (
    <ThemeProvider theme={theme}>
      <main>
        <ProductHeroLayout
          sxBackground={{
            backgroundImage: `url(${backgroundImage})`,
            backgroundColor: "#7fc7d9", // Average color of the background image.
            backgroundSize: "cover",
            backgroundPosition: "center",
          }}>
          {/* Increase the network loading priority of the background image. */}
          <img
            style={{ display: "none" }}
            src={backgroundImage}
            alt='increase priority'
          />
          <Typography color='primary' align='center' variant='h2'>
            Buy smart and cheap
          </Typography>
          <Typography
            color='inherit'
            align='center'
            variant='h5'
            sx={{ mb: 4, mt: { sx: 4, sm: 10 } }}>
            Enjoy a variety of products, and mostly the suffering of 6 naive
            students
          </Typography>
          <Box
            sx={{
              display: "flex",
              flexDirection: "column",
              alignItems: "center",
              justifyContent: "center",
            }}>
            <ButtonGroup
              orientation='vertical'
              aria-label='vertical contained button group'
              variant='text'>
              {buttons}
            </ButtonGroup>
          </Box>
          <Typography variant='body2' color='inherit' sx={{ mt: 2 }}>
            Enjoy The Market
          </Typography>
        </ProductHeroLayout>
      </main>
      {/* Footer */}
    </ThemeProvider>
  );
}
