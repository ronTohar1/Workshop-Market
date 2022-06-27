import * as React from "react"
import {
  DataGrid,
  GridCellEditCommitParams,
  GridColDef,
  GridRenderCellParams,
} from "@mui/x-data-grid"
import { Box, Button, Dialog, Stack, SvgIcon, Typography } from "@mui/material"
import Product from "../../DTOs/Product"
import Store from "../../DTOs/Store"
import {
  Roles,
  serverAddNewProduct,
  serverApproveBid,
  serverApproveCoOwner,
  serverChangeProductAmountInInventory,
  serverDenyBid,
  serverDenyCoOwner,
  serverGetMembersInRoles,
  serverGetPurchaseHistory,
  serverGetStore,
  serverMakeCounterOffer,
  serverRemovePurchasePolicy,
} from "../../services/StoreService"
import { pathHome, pathPolicy } from "../../Paths"
import { useNavigate } from "react-router-dom"
import { fetchResponse } from "../../services/GeneralService"
import { getBuyerId } from "../../services/SessionService"
import LoadingCircle from "../LoadingCircle"
import FailureSnackbar from "../Forms/FailureSnackbar"
import SuccessSnackbar from "../Forms/SuccessSnackbar"
import { serverGetStorePurchaseHistory } from "../../services/AdminService"
import Purchase from "../../DTOs/Purchase"
import PurchasePolicy from "../../DTOs/PurchasePolicy"
import Bid from "../../DTOs/Bid"
import ThumbDownIcon from "@mui/icons-material/ThumbDown"
import ThumbUpIcon from "@mui/icons-material/ThumbUp"
import CheckCircleOutlineIcon from "@mui/icons-material/CheckCircleOutline"
import CounterOfferForm from "../Forms/CounterOfferForm"

interface AppointmentRow {
  id: number;
  approvingIds: number[]
}

function convertToAppointmentRow(userId:number, approvingIds: number[]): AppointmentRow {
  return {
    id: userId,
    approvingIds: approvingIds
  }
}

const fields = {
  id: "id",
  approvingIds: "approvingIds",
}

export default function StoreCoOwnerAppointment({
  store,
  handleChangedStore,
}: {
  store: Store
  handleChangedStore: (s: Store) => void
}) {
  const initSize: number = 5

  const navigate = useNavigate()
  const [pageSize, setPageSize] = React.useState<number>(initSize)
  const [rows, setRows] = React.useState<AppointmentRow[]>([])
  const [selectionModel, setSelectionModel] = React.useState<number[]>([])
  const [chosenIds, setChosenIds] = React.useState<number[]>([])
  const [isDisabled, setIsDisabled] = React.useState<boolean>(true)

  //------------------------------
  const [openFailSnack, setOpenFailSnack] = React.useState<boolean>(false)
  const [failureProductMsg, setFailureProductMsg] = React.useState<string>("")
  const [openSuccSnack, setOpenSuccSnack] = React.useState<boolean>(false)
  const [successProductMsg, setSuccessProductMsg] = React.useState<string>("")
  const showSuccessSnack = (msg: string) => {
    setOpenSuccSnack(true)
    setSuccessProductMsg(msg)
  }

  const showFailureSnack = (msg: string) => {
    setOpenFailSnack(true)
    setFailureProductMsg(msg)
  }
  //------------------------------

  const handleSelectionChanged = (newSelection: any) => {
    const chosenIds: number[] = newSelection //.map((id:number)=>rows[id].id)
    setSelectionModel(newSelection)
    // const bid: AppointmentRow = rows.filter((r) => r.id == chosenIds[0])[0]
    // if (chosenIds.length > 0 && !bid.counterOffer) setIsDisabled(false)
    // else setIsDisabled(true)
    setChosenIds(chosenIds)
  }

  React.useEffect(() => {

    const buyerId = getBuyerId()
    const appointmentsMap : any = store.coOwnersAppointmentsApproving
    const appointees = Object.keys(appointmentsMap).map((apointeeId:string) => Number(apointeeId))
    const appointersArray : number[][] = appointees.map((appointeeId:number)=> appointmentsMap[appointeeId])
    const didntVoteTo: number[] = appointees.reduce(
        (didntVoteTo: number[],appointee:number,index:number)=>
        {
            if(appointersArray[index].includes(buyerId)) // means I approved already
                return didntVoteTo
            return didntVoteTo.concat(appointee)
        }
        ,[])
    console.log("didntVoteTo")
    console.log(didntVoteTo)
    //@
    const appointmentsRows : AppointmentRow[] = didntVoteTo.map((didntVoteUserId) => convertToAppointmentRow(didntVoteUserId, appointersArray[appointees.indexOf(didntVoteUserId)]))
    console.log("appointmentsRows")
    console.log(appointmentsRows)
    setRows(appointmentsRows)

    setChosenIds([])
    setIsDisabled(true)
  }, [store])

  const handleApproveCoOwner = (targetUserId: number) => {
    fetchResponse(serverApproveCoOwner( getBuyerId(),store.id, targetUserId))
      .then((success) => {
        showSuccessSnack("Co-Owner Approved")
        handleChangedStore(store)
      })
      .catch(showFailureSnack)
  }

  const handleDenyCoOwner = (targetUserId: number) => {
    fetchResponse(serverDenyCoOwner(getBuyerId(), store.id,  targetUserId))
      .then((success) => {
        showSuccessSnack("Co-Owner Appointment Denyed")
        handleChangedStore(store)
      })
      .catch(showFailureSnack)
  }

  
  const columns: GridColDef[] = [
    {
      field: fields.id,
      headerName: "User Id",
      type: "number",
      flex: 2,
      align: "left",
      headerAlign: "left",
    },
    
    {
      field: fields.approvingIds,
      headerName: "Approve Co-Owner",
    //   type: "number",
      flex: 1,
      align: "left",
      headerAlign: "left",
      renderCell: (params: GridRenderCellParams<AppointmentRow>) => {
        const approved = params.row.approvingIds.includes(getBuyerId())
        
        return approved ? (
          <CheckCircleOutlineIcon />
         ) : (
          <strong>
            <Button
              sx={{ mr: 1 }}
              variant="contained"
              onClick={() => handleApproveCoOwner(params.row.id)}
              startIcon={<ThumbUpIcon />}
              color="success"
            ></Button>
            <Button
              variant="contained"
              onClick={() => handleDenyCoOwner(params.row.id)}
              startIcon={<ThumbDownIcon />}
              color="error"
            >
            </Button>
          </strong>
        )
      },
    },
  ]

  const storeAppointments = (rows: AppointmentRow[]) => {
    console.log(rows)
    console.log("rows")
    return (
      <Box sx={{ mr: 3 }}>
        <Typography
          sx={{ flex: "1 1 100%" }}
          variant="h4"
          id="tableTitle"
          component="div"
        >
          {store != null
            ? store.name + "'s Co-Owner Appointments"
            : "Error- store not exist"}
        </Typography>
        <div style={{ height: "40vh", width: "100%", marginRight: 3 }}>
          <div style={{ display: "flex", height: "100%" }}>
            <div style={{ flexGrow: 1 }}>
              <DataGrid
                rows={rows}
                columns={columns}
                sx={{
                  width: "95vw",
                  height: "50vh",
                  "& .MuiDataGrid-cell:hover": {
                    color: "primary.main",
                    border: 1,
                  },
                }}
                // Paging:
                pageSize={pageSize}
                onPageSizeChange={(newPageSize) => setPageSize(newPageSize)}
                rowsPerPageOptions={[initSize, initSize + 5, initSize + 10]}
                pagination
                // Selection:
                onSelectionModelChange={handleSelectionChanged}
              />
             
            </div>
          </div>
        </div>
        <Dialog open={openFailSnack}>
          {FailureSnackbar(failureProductMsg, openFailSnack, () =>
            setOpenFailSnack(false)
          )}
        </Dialog>
        {SuccessSnackbar(successProductMsg, openSuccSnack, () =>
          setOpenSuccSnack(false)
        )}
      </Box>
    )
  }
  return <div>{store === null ? LoadingCircle() : storeAppointments(rows)}</div> // return  store === null ? LoadingComponent() : storePreview()
}
