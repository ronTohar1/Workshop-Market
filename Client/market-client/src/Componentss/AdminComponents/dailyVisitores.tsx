import * as React from "react"
import Button from "@mui/material/Button"
import TextField from "@mui/material/TextField"
import Dialog from "@mui/material/Dialog"
import DialogActions from "@mui/material/DialogActions"
import DialogContent from "@mui/material/DialogContent"
import DialogContentText from "@mui/material/DialogContentText"
import DialogTitle from "@mui/material/DialogTitle"
import Purchase from "../../DTOs/Purchase"
import {
  Box,
  Container,
  FormControl,
  Grid,
  IconButton,
  InputLabel,
  Slide,
  Typography,
} from "@mui/material"
import PurchaseCard from "./PurcaseCard"
import {
  serverGetBuyerPurchaseHistory,
  serverGetDailyVisitores,
} from "../../services/AdminService"
import { getBuyerId } from "../../services/SessionService"
import { fetchResponse } from "../../services/GeneralService"
import FailureSnackbar from "../Forms/FailureSnackbar"
import SuccessSnackbar from "../Forms/SuccessSnackbar"
import Chart from "react-google-charts"
import { TransitionProps } from "@mui/material/transitions"
import CloseIcon from "@mui/icons-material/Close"

enum RolesData {
  Admin = 1,
  Owner = 2,
  Manager = 3,
  Member = 4,
  Guest = 5,
}

const convertRoleToIndex = (role: string): number => {
  if (role == "Admin") return RolesData.Admin
  if (role == "Owner") return RolesData.Owner
  if (role == "Manager") return RolesData.Manager
  if (role == "Member") return RolesData.Member
  return RolesData.Guest
}

export const options = {
  title: "Daily Visitors",
  is3D: true,
}

const Transition = React.forwardRef(function Transition(
  props: TransitionProps & {
    children: React.ReactElement
  },
  ref: React.Ref<unknown>
) {
  return <Slide direction="up" ref={ref} {...props} />
})

export default function DailyVisitors() {
  const [open, setOpen] = React.useState<boolean>(false)
  const [chartData, setChartData] = React.useState<(string | number)[][]>([])
  const chartDataRef = React.useRef<(string | number)[][]>([])
  const updateChartData = (newData: (string | number)[][]) => {
    chartDataRef.current = newData
    setChartData(newData)
  }
  const [ignoreWS, setIgnoreWS] = React.useState<boolean>(false)
  const ignoreWSref = React.useRef<boolean>(false)

  //------------------------------
  const [openFailSnack, setOpenFailSnack] = React.useState<boolean>(false)
  const [failureProductMsg, setFailureProductMsg] = React.useState<string>("")
  const [openSuccSnack, setOpenSuccSnack] = React.useState<boolean>(false)
  const [successProductMsg, setSuccessProductMsg] = React.useState<string>("")

  // const dd = String(today.getDate()).padStart(2, '0');
  // const mm = String(fromSelectedMonth).padStart(2, '0'); //January is 0!
  const today = new Date()
  const dd = today.getDate()
  const mm = today.getMonth() + 1 //January is 0!
  const yyyy = today.getFullYear()

  const [fromSelectedDay, setFromDay] = React.useState(dd)
  const [fromSelectedMonth, setFromMonth] = React.useState(mm)
  const [fromSelectedYear, setFromYear] = React.useState(yyyy)

  const [toSelectedDay, setToDay] = React.useState(dd)
  const [toSelectedMonth, setToMonth] = React.useState(mm)
  const [toSelectedYear, setToYear] = React.useState(yyyy)

  const showFailureSnack = (msg: string) => {
    setOpenFailSnack(true)
    setFailureProductMsg(msg)
  }
  //------------------------------

  React.useEffect(() => {
    const buyerId = getBuyerId()

    const responsePromise = serverGetDailyVisitores(
      buyerId,
      fromSelectedDay,
      fromSelectedMonth,
      fromSelectedYear,
      toSelectedDay,
      toSelectedMonth,
      toSelectedYear
    )
    fetchResponse(responsePromise)
      .then((dailyVisits) => {
        // checkIfIgnoreWebsocket() - No need to ignore for sure because its today's date

        updateChartData([
          ["Visitor", "Amount"],
          ["Admins", dailyVisits[0]],
          ["Store Owners", dailyVisits[1]],
          ["Managers", dailyVisits[2]],
          ["Members", dailyVisits[3]],
          ["Guests", dailyVisits[4]],
        ])
      })
      .catch((e) => {
        showFailureSnack(e)
        setOpen(false)
      })
  }, [])

  React.useEffect(() => {
    const ws = new WebSocket("ws://127.0.0.1:4560/logs")

    if (!ignoreWSref.current) ws.addEventListener("message", addOrDecrease)

    return () => {
      ws.removeEventListener("message", addOrDecrease)
    }
  }, [])

  const checkIfIgnoreWebsocket = () => {
    const today = new Date()
    let ignore = true
    console.log(fromSelectedYear)
    console.log(fromSelectedMonth)
    console.log(fromSelectedDay)
    console.log("------------------")
    console.log(toSelectedYear)
    console.log(toSelectedMonth)
    console.log(toSelectedDay)
    console.log("------------------")

    console.log(today.getFullYear())
    console.log(today.getMonth())
    console.log(today.getDate())
    if (
      fromSelectedYear <= today.getFullYear() &&
      today.getFullYear() <= toSelectedYear
    )
      if (
        fromSelectedMonth <= today.getMonth() + 1 &&
        today.getMonth() + 1 <= toSelectedMonth
      )
        if (
          fromSelectedDay <= today.getDate() &&
          today.getDate() <= toSelectedDay
        )
          ignore = false

    ignoreWSref.current = ignore
    setIgnoreWS(ignore)
  }

  //@ts-ignore
  const CreateNewChartData = (roleIndex: number, newAmount: number) => {
    const newStats = [
      ["Visitor", "Amount"],
      //@ts-ignore
      ["Admins", chartDataRef.current[1][1]],
      //@ts-ignore
      ["Store Owners", chartDataRef.current[2][1]],
      //@ts-ignore
      ["Managers", chartDataRef.current[3][1]],
      //@ts-ignore
      ["Members", chartDataRef.current[4][1]],
      //@ts-ignore
      ["Guests", chartDataRef.current[5][1]],
    ]
    newStats[roleIndex][1] = newAmount
    return newStats
  }

  const addOrDecrease = (event: any) => {
    if (!ignoreWSref.current) {
      const msg: string = event.data
      const add: boolean = msg[0] === "+"
      const role: string = msg.slice(1)
      add ? addOneToRole(role) : DecOneOfRole(role)
    }
  }

  const addOneToRole = (role: string) => {
    const index = convertRoleToIndex(role)
    const currChartData = chartDataRef.current
    // @ts-ignore
    const newAmount: number = currChartData[index][1] + 1
    const newChartData = CreateNewChartData(index, newAmount)
    updateChartData(newChartData)
  }
  const DecOneOfRole = (role: string) => {
    const index = convertRoleToIndex(role)
    const currChartData = chartDataRef.current
    // @ts-ignore
    const newAmount: number = currChartData[index][1] - 1
    const newChartData = CreateNewChartData(index, newAmount)
    updateChartData(newChartData)
  }
  const isThereInformation = () => {
    return (
      chartData.filter((tup, index) => index !== 0 && tup[1] > 0).length > 0
    )
  }

  const handleSearch = () => {
    const buyerId = getBuyerId()
    const responsePromise = serverGetDailyVisitores(
      buyerId,
      fromSelectedDay,
      fromSelectedMonth,
      fromSelectedYear,
      toSelectedDay,
      toSelectedMonth,
      toSelectedYear
    )
    fetchResponse(responsePromise)
      .then((dailyVisits) => {
        checkIfIgnoreWebsocket()

        updateChartData([
          ["Visitor", "Amount"],
          ["Admins", dailyVisits[0]],
          ["Store Owners", dailyVisits[1]],
          ["Managers", dailyVisits[2]],
          ["Members", dailyVisits[3]],
          ["Guests", dailyVisits[4]],
        ])
      })
      .catch((e) => {
        showFailureSnack(e)
        setOpen(false)
      })
  }

  const handleClickOpen = () => {
    console.log("opened")
    setOpen(true)
  }
  const handleClose = () => {
    console.log("closed")
    setOpen(false)
  }

  return (
    <div>
      <Box textAlign="center">
        <Button
          onClick={handleClickOpen}
          style={{ height: 50, width: 500 }}
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
          Display Daily Visitors
        </Button>
      </Box>
      <Dialog
        open={open}
        onClose={handleClose}
        fullScreen
        sx={{ marginTop: 5 }}
        TransitionComponent={Transition}
      >
        <IconButton
          edge="end"
          color="inherit"
          onClick={handleClose}
          aria-label="close"
        >
          <CloseIcon />
        </IconButton>
        <DialogTitle>Display Daily Visitors</DialogTitle>
        <DialogContent>
          <Grid container justifyContent="left">
            <Grid item xs={2.3}></Grid>
            <Grid item xs={0.7}>
              <Typography variant="h6" gutterBottom>
                From:
              </Typography>
            </Grid>
            <Grid item xs={0.75}>
              <FormControl sx={{ m: 1, maxWidth: 100 }} id="myform">
                <TextField
                  required
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
                    setFromDay(parseInt(e.currentTarget.value))
                  }}
                  value={String(fromSelectedDay).padStart(2, "0")}
                  name="number"
                  type="number"
                  label="Day "
                />
              </FormControl>
            </Grid>
            <Grid item xs={0.85}>
              <FormControl sx={{ m: 1, maxWidth: 100 }} id="myform">
                <TextField
                  required
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
                    setFromMonth(parseInt(e.currentTarget.value))
                  }}
                  value={String(fromSelectedMonth).padStart(2, "0")}
                  name="number"
                  type="number"
                  label="Month"
                  fullWidth
                />
              </FormControl>
            </Grid>
            <Grid item xs={2}>
              <FormControl sx={{ m: 1, maxWidth: 100 }} id="myform">
                <TextField
                  required
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
                    setFromYear(parseInt(e.currentTarget.value))
                  }}
                  value={fromSelectedYear}
                  name="number"
                  type="number"
                  label="Year"
                  fullWidth
                />
              </FormControl>
            </Grid>
            <Grid item xs={0.5}>
              <Typography variant="h6" gutterBottom>
                To:
              </Typography>
            </Grid>
            <Grid item xs={0.75}>
              <FormControl sx={{ m: 1, maxWidth: 100 }} id="myform">
                <TextField
                  required
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
                    setToDay(parseInt(e.currentTarget.value))
                  }}
                  value={String(toSelectedDay).padStart(2, "0")}
                  name="number"
                  type="number"
                  label="Day "
                />
              </FormControl>
            </Grid>
            <Grid item xs={0.85}>
              <FormControl sx={{ m: 1, maxWidth: 100 }} id="myform">
                <TextField
                  required
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
                    setToMonth(parseInt(e.currentTarget.value))
                  }}
                  value={String(toSelectedMonth).padStart(2, "0")}
                  name="number"
                  type="number"
                  label="Month"
                  fullWidth
                />
              </FormControl>
            </Grid>
            <Grid item xs={2}>
              <FormControl sx={{ m: 1, maxWidth: 100 }} id="myform">
                <TextField
                  required
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
                    setToYear(parseInt(e.currentTarget.value))
                  }}
                  value={toSelectedYear}
                  name="number"
                  type="number"
                  label="Year"
                  fullWidth
                />
              </FormControl>
            </Grid>
          </Grid>
          <Box textAlign="center">
            <Button onClick={handleSearch}>search</Button>
          </Box>
        </DialogContent>
        <Box style={{ maxHeight: "100%", maxWidth: "100%", overflow: "auto" }}>
          <Chart
            chartType="PieChart"
            data={chartData}
            options={options}
            width={"100%"}
            height={"400px"}
          />
          {!isThereInformation() && (
            <Typography sx={{ m: 5 }} variant="h3">
              {" "}
              No One Visited The Site
            </Typography>
          )}
        </Box>
      </Dialog>
      <Dialog open={openFailSnack}>
        {FailureSnackbar(failureProductMsg, openFailSnack, () =>
          setOpenFailSnack(false)
        )}
      </Dialog>
      {SuccessSnackbar(successProductMsg, openSuccSnack, () =>
        setOpenSuccSnack(false)
      )}
    </div>
  )
}
