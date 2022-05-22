import * as React from 'react';
import Button from '@mui/material/Button';
import AppBar from '@mui/material/AppBar';
import Grid from '@mui/material/Grid';
import ButtonGroup from '@mui/material/ButtonGroup';
import Box from '@mui/material/Box';
import {createTheme, ThemeProvider } from '@mui/material/styles';
import Navbar from './Navbar';
import {pathLogin, pathRegister} from './Paths'
import {Typography}  from '@mui/material';
import ProductHeroLayout from './ProductHeroLayout';
import { styled } from '@mui/material/styles';

const cards = [1, 2, 3];

const backgroundImage =
  'https://images.unsplash.com/photo-1471193945509-9ad0617afabf?crop=entropy&cs=tinysrgb&fm=jpg&ixlib=rb-1.2.1&q=80&raw_url=true&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170';
  

const theme = createTheme();

const createButton = (name: string, path: string) => {
  return <Button href={path} style={{height: 100, width: 500}}                  
                  key="name" variant="contained" size='large' color='primary' >{name}</Button> 
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
  createButton("Continue To Website", pathLogin),
  createButton("Create An Account", pathRegister),
  createButton("Log In To Your Account",pathLogin)
];

export default function Home() {
  const bar = Navbar();
  return (
    <ThemeProvider theme={theme}>
      <main>
    
            <ProductHeroLayout
      sxBackground={{
        backgroundImage: `url(${backgroundImage})`,
        backgroundColor: '#7fc7d9', // Average color of the background image.
        backgroundSize: 'cover',
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
      color="primary"
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
      <Box sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center'
              }}>
              <ButtonGroup 
                orientation="vertical"
                aria-label="vertical contained button group"
                variant="text"
              >
                {buttons}
              </ButtonGroup>
            </Box>
      <Typography variant="body2" color="inherit" sx={{ mt: 2 }}>
        Discover the experience
      </Typography>
    </ProductHeroLayout>
  
      

        {/* <Box
          sx={{
            bgcolor: 'background.paper',
            pt: 8,
            pb: 6,
          }}
        >
          <Container maxWidth="sm">
            <Typography
              component="h1"
              variant="h2"
              align="center"
              color="text.primary"
              gutterBottom
            >
              Album layout
            </Typography>
            <Typography variant="h5" align="center" color="text.secondary" paragraph>
              Something short and leading about the collection below—its contents,
              the creator, etc. Make it short and sweet, but not too short so folks
              don&apos;t simply skip over it entirely.
            </Typography>
            <Stack
              sx={{ pt: 4 }}
              direction="row"
              spacing={2}
              justifyContent="center"
            >
              <Button variant="contained">Main call to action</Button>
              <Button variant="outlined">Secondary action</Button>
            </Stack>
          </Container>
        </Box>
        <Container sx={{ py: 8 }} maxWidth="md">
          <Grid container spacing={4}>
            {cards.map((card) => (
              <Grid item key={card} xs={12} sm={6} md={4}>
                <Card
                  sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}
                >
                  <CardMedia
                    component="img"
                    sx={{
                      // 16:9
                      pt: '56.25%',
                    }}
                    image="https://source.unsplash.com/random"
                    alt="random"
                  />
                  <CardContent sx={{ flexGrow: 1 }}>
                    <Typography gutterBottom variant="h5" component="h2">
                      Heading
                    </Typography>
                    <Typography>
                      This is a media card. You can use this section to describe the
                      content.
                    </Typography>
                  </CardContent>
                  <CardActions>
                    <Button size="small">View</Button>
                    <Button size="small">Edit</Button>
                  </CardActions>
                </Card>
              </Grid>
            ))}
          </Grid>
        </Container> */}
      </main>
      {/* Footer */}

    </ThemeProvider>
  );
}