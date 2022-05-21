import * as React from 'react';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import ButtonGroup from '@mui/material/ButtonGroup';
import {createTheme, ThemeProvider } from '@mui/material/styles';
import Navbar from './Navbar';
import {pathLogin, pathRegister} from './Paths'
import Box from '@mui/material/Box';
import Link from '@mui/material/Link';
import {Typography}  from '@mui/material';
import ProductHeroLayout from './ProductHeroLayout';
import { styled } from '@mui/material/styles';
import Toolbar from './Components/ToolBar';
import AppBar from './Components/AppBar';


const cards = [1, 2, 3];

const theme = createTheme();
const backgroundImage =
  'https://images.unsplash.com/photo-1471193945509-9ad0617afabf?crop=entropy&cs=tinysrgb&fm=jpg&ixlib=rb-1.2.1&q=80&raw_url=true&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170';
  
const createButton = (name: string, path: string) => {
  return <Button href={path} style={{height: 100, width: 500}}                  
                  key="name" size='large' color='primary' >{name}</Button> 
};
const buttons = [
  createButton("Continue To Website", pathLogin),
  createButton("Create An Account", pathRegister),
  createButton("Log In To Your Account",pathLogin)
];
const rightLink = {
    fontSize: 16,
    color: 'common.white',
    ml: 3,
  };
const ProductHeroLayoutRoot = styled('section')(({ theme }) => ({
    color: theme.palette.common.white,
    position: 'relative',
    display: 'flex',
    alignItems: 'center',
    [theme.breakpoints.up('sm')]: {
      height: '80vh',
      minHeight: 500,
      maxHeight: 1300,
    },
  }));
const Background = styled(Box)({
    position: 'absolute',
    left: 0,
    right: 0,
    top: 0,
    bottom: 0,
    backgroundSize: 'cover',
    backgroundRepeat: 'no-repeat',
    zIndex: -2,
  });
export default function Home2() {
  const bar = Navbar();
  return (
    <div>
      <AppBar position="fixed">
        <Toolbar sx={{ justifyContent: 'space-between' }}>
          <Box sx={{ flex: 1 }} />
          <Link
            variant="h6"
            underline="none"
            color="inherit"
            align="center"
            href="/home2"
            sx={{ fontSize: 24 }}
          >
            {'Market'}
          </Link>
          <Box sx={{ flex: 1, display: 'flex', justifyContent: 'flex-end' }}>
            <Link
              variant="h6"
              underline="none"
              href="/login"
              sx={{...rightLink}}
            >
              {'Sign In'}
            </Link>
            <Link
              variant="h6"
              underline="none"
              href="/register"
              sx={{ ...rightLink}}
            >
              {'Sign Up'}
            </Link>
          </Box>
        </Toolbar>
      </AppBar>
      <Toolbar />
      <ProductHeroLayout
      sxBackground={{
        backgroundImage: `url(${backgroundImage})`,
        backgroundColor: '#7fc7d9', // Average color of the background image.
        backgroundPosition: 'center',
      }}
    >
      {/* Increase the network loading priority of the background image. */}
      <img
        style={{ display: 'none' }}
        src={backgroundImage}
        alt="increase priority"
      />
      <Typography 
      color="inherit"
      align="center"
      variant="h2">
       
        Buy smart and cheap
      </Typography>
      <Typography
        color="inherit"
        align="center"
        variant="h5"
        sx={{ mb: 4, mt: { sx: 4, sm: 10 } }}
      >
        Enjoy a variety of products, and mostly the suffering of 6 naive students
      </Typography>
      <Button
        color="secondary"
        variant="contained"
        size="large"
        component="a"
        href="/store"
        sx={{ minWidth: 200 }}
      >
        store
      </Button>
      <Typography variant="body2" color="inherit" sx={{ mt: 2 }}>
        Discover the experience
      </Typography>
    </ProductHeroLayout>
    </div>
  );
}