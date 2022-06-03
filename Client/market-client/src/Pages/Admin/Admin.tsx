import * as React from "react";
import Navbar from "../../Componentss/Navbar";
import { ProductHeroLayout } from "../../Componentss/ProductHeroLayout";
import Button from "@mui/material/Button";
import ButtonGroup from "@mui/material/ButtonGroup";
import Box from "@mui/material/Box";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { pathLogin, pathRegister} from "../../Paths";
import Purchase from "../../DTOs/Purchase";
import { getBuyerId } from "../../services/SessionService";
import { serverGetBuyerPurchaseHistory } from "../../services/AdminService";
import { fetchResponse } from "../../services/GeneralService";
import FormDialog from "../../Componentss/AdminComponents/BuyerPurchaseHistoryForm";
import BuyerPurchaseHistoryForm from "../../Componentss/AdminComponents/BuyerPurchaseHistoryForm";
import StorePurchaseHistoryForm from "../../Componentss/AdminComponents/StorePurchaseHistoryForm";
import ShowLoggedInMembers from "../../Componentss/AdminComponents/ShowLoggedInMembers";


const backgroundImage =
  "https://images.unsplash.com/photo-1471193945509-9ad0617afabf";



const createButton = (name: string, path: string) => {
  return (
    <Button
      href={path}
      style={{ height: 50, width: 500 }}
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
  createButton("Display a member account", pathLogin),
  createButton("Remove a member", pathLogin),
];

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
});

export default function Admin() {
  const [open, setOpen] = React.useState<boolean>(false);
  const [purchases,setPurchases] = React.useState<Purchase[]>([]);
  return (
    <ThemeProvider theme={theme}>
      <main >
        <ProductHeroLayout
          sxBackground={{
            backgroundImage: `url(${backgroundImage})`,
            backgroundColor: "#7fc7d9", // Average color of the background image.
            backgroundSize: "cover",
            backgroundPosition: "center",
            height: "100vh",
            outerHeight: "100vh",
          }}>
          {/* Increase the network loading priority of the background image. */}
          <img
            style={{ display: "none" }}
            src={backgroundImage}
            alt='increase priority'
          />
           <Box
            component="img"
            sx={{
              m: 4,
              height: 233,
              width: 175,
              maxHeight: { xs: 233, md: 167 },
              maxWidth: { xs: 350, md: 250 },
            }}
            alt="Admin setting"
            src="https://cdn-icons-png.flaticon.com/512/3135/3135715.png"
          />
          <BuyerPurchaseHistoryForm/>
          <StorePurchaseHistoryForm/>
          <ShowLoggedInMembers/>
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
          
        </ProductHeroLayout>
      </main>
      {/* Footer */}
    </ThemeProvider>
  );
}
