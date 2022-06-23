import * as React from "react"
import Navbar from "../Componentss/Navbar"
import { ProductHeroLayout } from "../Componentss/ProductHeroLayout"
import Button from "@mui/material/Button"
import ButtonGroup from "@mui/material/ButtonGroup"
import Box from "@mui/material/Box"
import { createTheme, ThemeProvider } from "@mui/material/styles"
import { pathLogin, pathRegister } from "../Paths"
import Purchase from "../DTOs/Purchase"
import { getBuyerId } from "../services/SessionService"
import { serverGetDailyProfitOfAllStores } from "../services/AdminService"
import { fetchResponse } from "../services/GeneralService"
import FormDialog from "../Componentss/AdminComponents/BuyerPurchaseHistoryForm"
import BuyerPurchaseHistoryForm from "../Componentss/AdminComponents/BuyerPurchaseHistoryForm"
import StorePurchaseHistoryForm from "../Componentss/AdminComponents/StorePurchaseHistoryForm"
import ShowLoggedInMembers from "../Componentss/AdminComponents/ShowLoggedInMembers"
import DisplayMemberAccount from "../Componentss/AdminComponents/DisplayMemberAccount"
import RemoveAMember from "../Componentss/AdminComponents/RemoveAMember"
import { Card, CardContent, CardHeader, Stack, Typography } from "@mui/material"
import { useNavigate } from "react-router-dom"
import HomeIcon from "@mui/icons-material/Home"
import { pathAdmin, pathHome } from "../Paths"
import LoadingCircle from "../Componentss/LoadingCircle"
import DailyVisitors from "../Componentss/AdminComponents/dailyVisitores"

const backgroundImage =
  "https://images.unsplash.com/photo-1471193945509-9ad0617afabf"

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

export default function Admin() {
   const navigate = useNavigate()
   const [dailyProfit, setDailyProfit] = React.useState(0)
  // React.useEffect(() => {
  //   alert(getBuyerId())
  //   setNoReason(false)
  // }, [])
  React.useEffect(() => {
    const buyerId = getBuyerId()
    fetchResponse(serverGetDailyProfitOfAllStores(buyerId))
      .then((profit) => {
        setDailyProfit(profit)
      })
      .catch((e) => {
        alert(e)
        navigate(pathHome)
      })
  }, [dailyProfit])
  return (
    <ThemeProvider theme={theme}>
      <ProductHeroLayout
        sxBackground={{
          backgroundImage: `url(${backgroundImage})`,
          backgroundSize: "cover",
          backgroundPosition: "center",
          height: "100vh",
          outerHeight: "150vh",
        }}>
        <div style={{ display: "flex", justifyContent: "center" }}>
          
          <Stack>
            {/* Increase the network loading priority of the background image. */}
            <div style={{ display: "flex", justifyContent: "center" }}>
              <Stack spacing={25}>
                <Button
                  variant="contained"
                  // onClick={() => navigate(pathHome)}
                  href={pathHome}
                  color="success"
                   sx={{ position: "absolute", top: "0px", right: "0px" }}
                  endIcon={<HomeIcon />}
                >
                  Home
                </Button>
                <Box
                  component="img"
                  alignItems="justify-end"
                  display="flex"
                  sx={{
                    m: 3,
                    height: 233,
                    width: 175,
                    maxHeight: { xs: 100, md: 167 },
                    maxWidth: { xs: 350, md: 250 },
                  }}
                  alt="Admin setting"
                  src="https://cdn-icons-png.flaticon.com/512/3135/3135715.png"
                />
              </Stack>
            </div>
            <Typography
                  color="inherit"
                  align="center"
                  variant="h5"
                  sx={{ mb: 4, mt: { sx: 4, sm: 2 } }}
                >
                 {`Greetings dear admin, the market daily profit so far is ${dailyProfit} ₪`}
                </Typography>
            {BuyerPurchaseHistoryForm()}
            {StorePurchaseHistoryForm()}
            {ShowLoggedInMembers()}
            {DisplayMemberAccount()}
            {RemoveAMember()}
            {DailyVisitors()}
          {/* <CardContent>
            
          </CardContent> */}
            {/* <BuyerPurchaseHistoryForm />
                <StorePurchaseHistoryForm />
                <ShowLoggedInMembers />
                <DisplayMemberAccount />
                <RemoveAMember /> */}
          </Stack>
        </div>
      </ProductHeroLayout>
    </ThemeProvider>
  )
}