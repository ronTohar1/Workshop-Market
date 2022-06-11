import * as React from "react"
import {
  DataGrid,
  GridCellEditCommitParams,
  GridColDef,
} from "@mui/x-data-grid"
import { Box, Button, Dialog, Stack, Typography } from "@mui/material"
import Product from "../../DTOs/Product"
import Store from "../../DTOs/Store"
import {
  Roles,
  serverAddNewProduct,
  serverChangeProductAmountInInventory,
  serverGetMembersInRoles,
  serverGetPurchaseHistory,
  serverGetStore,
  serverRemovePurchasePolicy,
} from "../../services/StoreService"
import { pathHome, pathPolicy } from "../../Paths"
import { useNavigate } from "react-router-dom"
import { convergePromises, fetchResponse } from "../../services/GeneralService"
import { getBuyerId } from "../../services/SessionService"
import LoadingCircle from "../LoadingCircle"
import FailureSnackbar from "../Forms/FailureSnackbar"
import SuccessSnackbar from "../Forms/SuccessSnackbar"
import { serverGetStorePurchaseHistory } from "../../services/AdminService"
import Purchase from "../../DTOs/Purchase"
import PurchasePolicy from "../../DTOs/PurchasePolicy"
import MemberInRole from "../../DTOs/MemberInRole"
const fields = {
  id: "memberId",
  role: "roleInStore",
}

// interface RoleMember{
//   memberId: number
//   roleInStore:Role
// }

const columns: GridColDef[] = [
  {
    field: fields.id,
    headerName: "member ID",
    type: "number",
    flex: 1,
    align: "left",
    headerAlign: "left",
  },
  {
    field: fields.role,
    headerName: "Role",
    // type: Roles,
    flex: 1,
    align: "left",
    headerAlign: "left",
  },
]

export default function StoreRoles({
  store,
  handleChangedStore,
}: {
  store: Store
  handleChangedStore: (s: Store) => void
}) {
  const initSize: number = 5

  const navigate = useNavigate()
  const [pageSize, setPageSize] = React.useState<number>(initSize)
  const [rows, setRows] = React.useState<MemberInRole[]>([])
  const [selectionModel, setSelectionModel] = React.useState<number[]>([])
  const [chosenIds, setChosenIds] = React.useState<number[]>([])

  const handleSelectionChanged = (newSelection: any) => {
    console.log(newSelection)
    const chosenIds: number[] = newSelection//.map((id:number)=>rows[id].id)
    setSelectionModel(newSelection)
    setChosenIds(chosenIds)
  }

  const handleRemovePolicy = () => {

    if (chosenIds.length === 0) {
      alert("Please select a policy to remove")
    }
    else {
      fetchResponse(serverRemovePurchasePolicy(getBuyerId(), store.id, chosenIds[0]))
        .then((success: boolean) => {
          if (success) handleChangedStore(store)
          else alert("Coludnt remove this policy")
        })
        .catch(alert)
    }
  }

  React.useEffect(() => {
    try {
      const rolesIds: number[] = Object.values(Roles).filter((v: any) => !isNaN(v)).map((v: any) => Number(v)) // taking the ids of the roles in Roles enum
      const rolesNames: string[] = Object.values(Roles).filter((v: any) => isNaN(v)).map((v: any) => String(v)) // Taking the names of the roles in Roles neum

      const membersByRole: Promise<MemberInRole[]>[] = rolesIds.map((roleId: number) => {
        return fetchResponse(serverGetMembersInRoles(getBuyerId(), store.id, roleId))
          .then((membersIds: number[]) => {
            return membersIds.map((memberId): MemberInRole => { return { memberId: memberId, roleInStore: roleId } })
          })
          .catch((e): MemberInRole[] => {
            alert(e)
            return []
          })
      }
      )

      const membersByRoleArr: Promise<MemberInRole[][]> = convergePromises(membersByRole)
      const rows: Promise<MemberInRole[]> = membersByRoleArr.then((mbra: MemberInRole[][]) => mbra.reduce(
        (membersInRoles: MemberInRole[], currArr: MemberInRole[]) => {
          return membersInRoles.concat(currArr)
        }, []
      ))
      console.log(rows)
      rows.then((r) => setRows(r))

    }
    catch (e) {
      alert("Sorry but we couldny load the Roles")
      setRows([])
    }
    // setRows(store.purchasePolicies)
  }, [store])


  const getStoreRoles = () => {
    return (
      <Box sx={{ mr: 3 }}>
        <Typography
          sx={{ flex: "1 1 100%" }}
          variant="h4"
          id="tableTitle"
          component="div"
        >
          {store != null ? store.name + "'s Purchase Policy" : "Error- store not exist"}
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
                getRowId={(row:MemberInRole) => row.memberId}
              />
            </div>
          </div>
        </div>
        <Stack direction='row' justifyContent='space-between'>

          <Box>
            <Button variant="contained" sx={{ ml: 1 }} onClick={() => navigate(pathPolicy, { state: store })}>
              Add New Policies
            </Button>

          </Box>
          <Box>
            <Button sx={{ ml: 'auto', mr: '1vw' }} color="error" variant="contained" onClick={handleRemovePolicy}>
              Remove Selected Policy
            </Button>
          </Box>
        </Stack>
      </Box>

    )
  }
  return <div>{store === null ? LoadingCircle() : getStoreRoles()}</div> // return  store === null ? LoadingComponent() : storePreview()
}
