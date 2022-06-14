import * as React from "react"
import {
  DataGrid,
  GridCellEditCommitParams,
  GridColDef,
} from "@mui/x-data-grid"
import { Box, Dialog, Stack, Typography } from "@mui/material"
import Product from "../../DTOs/Product"
import Store from "../../DTOs/Store"
import {
  Roles,
  serverAddNewProduct,
  serverChangeProductAmountInInventory,
  serverGetMembersInRoles,
  serverGetPurchaseHistory,
  serverGetDailyStoreProfit,
  serverGetStore,
} from "../../services/StoreService"
import { pathHome } from "../../Paths"
import { useNavigate } from "react-router-dom"
import { fetchResponse } from "../../services/GeneralService"
import { getBuyerId } from "../../services/SessionService"
import LoadingCircle from "../LoadingCircle"
import FailureSnackbar from "../Forms/FailureSnackbar"
import SuccessSnackbar from "../Forms/SuccessSnackbar"
import { serverGetStorePurchaseHistory } from "../../services/AdminService"
import Purchase from "../../DTOs/Purchase"
const fields = {
  date: "purchaseDate",
  price: "purchasePrice",
  description: "purchaseDescription",
  buyerId: "buyerId",
}


const columns: GridColDef[] = [

  {
    field: fields.date,
    headerName: "Purchase Date",
    type: "date",
    flex: 2,
    align: "left",
    headerAlign: "left",
  },
  {
    field: fields.price,
    headerName: "Price",
    type: "number",
    flex: 1,
    align: "left",
    headerAlign: "left",
  },
  {
    field: fields.description,
    headerName: "Purchase Description",
    type: "string",
    flex: 1,
    align: "left",
    headerAlign: "left",
    renderCell: ({ value }) => (
      <span style={{ overflow: "scroll" }}>
        {value}
      </span>
    )
  },
  {
    field: fields.buyerId,
    headerName: "Buyer ID",
    type: 'number',
    flex: 1,
    align: "left",
    headerAlign: "left",

    // valueGetter: (params: GridValueGetterParams) =>
    //   `${params.row.firstName || ''} ${params.row.lastName || ''}`,
  },
]

export default function StorePurchases({
  store,
  handleChangedStore,
}: {
  store: Store
  handleChangedStore: (s: Store) => void
}) {
  let rowId = 0;
  const initSize: number = 5

  const [pageSize, setPageSize] = React.useState<number>(initSize)
  const [rows, setRows] = React.useState<Purchase[]>([])
  const [dailyProfit, setDailyProfit] = React.useState<number>(-1)

  React.useEffect(() => {
    fetchResponse(serverGetPurchaseHistory(getBuyerId(), store.id))
      .then((purchases: Purchase[]) => {
        console.log("purchases")
        console.log(purchases)
        purchases.forEach(
          (p: Purchase) => {
            p.purchaseDate = new Date("12/12/2020")
          })
        setRows(purchases)
      })
      .catch(alert)
  }, [])

  React.useEffect(() => {
    fetchResponse(serverGetDailyStoreProfit(store.id,getBuyerId()))
      .then((profit) => {
        setDailyProfit(profit)
      })
      .catch((e)=>{})//it could be a manager, so we don't want to alert unnecesserly, we just won't display it
  }, [])


  const storePurchases = () => {
    return (
      <Box sx={{ mr: 3 }}>
        <Stack direction="row">{ }</Stack>
        <Typography
          sx={{ flex: "1 1 100%" }}
          variant="h4"
          id="tableTitle"
          component="div"
        >
          {store != null ? store.name + "'s Purchases" : "Error- store not exist"}
        </Typography>
        <Stack direction="column" spacing={17}>
        <div style={{ height: "40vh", width: "100%" }}>
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
                disableSelectionOnClick
                getRowId={() => rowId++}
              />
            </div>
          </div>
        </div>
        <Typography
                  color="inherit"
                  align="center"
                  variant="h5"
                  sx={{ mb: 4, mt: { sx: 4, sm: 2 } }}
                >
                 {dailyProfit != -1 ? `the store's daily profit so far is ${dailyProfit} ₪` : ""}
        </Typography>
        </Stack>
      </Box>
      
    )
  }
  return <div>{store === null ? LoadingCircle() : storePurchases()}</div> // return  store === null ? LoadingComponent() : storePreview()
}
