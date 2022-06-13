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
import { serverGetBuyerPurchaseHistory } from "../services/AdminService"
import { fetchResponse } from "../services/GeneralService"
import FormDialog from "../Componentss/AdminComponents/BuyerPurchaseHistoryForm"
import BuyerPurchaseHistoryForm from "../Componentss/AdminComponents/BuyerPurchaseHistoryForm"
import StorePurchaseHistoryForm from "../Componentss/AdminComponents/StorePurchaseHistoryForm"
import ShowLoggedInMembers from "../Componentss/AdminComponents/ShowLoggedInMembers"
import DisplayMemberAccount from "../Componentss/AdminComponents/DisplayMemberAccount"
import RemoveAMember from "../Componentss/AdminComponents/RemoveAMember"
import { Stack } from "@mui/material"
import { useNavigate } from "react-router-dom"
import HomeIcon from "@mui/icons-material/Home"
import { pathAdmin, pathHome } from "../Paths"
import LoadingCircle from "../Componentss/LoadingCircle"

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
  // const navigate = useNavigate()
  // const [noReason, setNoReason] = React.useState(true)
  // React.useEffect(() => {
  //   alert(getBuyerId())
  //   setNoReason(false)
  // }, [])
  return (
    <ThemeProvider theme={theme}>
      <ProductHeroLayout
        sxBackground={{
          // backgroundImage: `url(${backgroundImage})`,
          backgroundSize: "cover",
          backgroundPosition: "center",
          height: "100vh",
          outerHeight: "150vh",
        }}>
        <div style={{ display: "flex", justifyContent: "center" }}>
          <Stack>
            {/* Increase the network loading priority of the background image. */}
            <div style={{ display: "flex", justifyContent: "center" }}>
              <Stack >
                <Button
                  variant="contained"
                  // onClick={() => navigate(pathHome)}
                  href={pathHome}
                  color="success"
                  // sx={{ position: "absolute", top: "0px", right: "0px" }}
                  endIcon={<HomeIcon />}
                >
                  Home
                </Button>
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
              </Stack>
            </div>

            {BuyerPurchaseHistoryForm()}
            {StorePurchaseHistoryForm()}
            {ShowLoggedInMembers()}
            {DisplayMemberAccount()}
            {RemoveAMember()}
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