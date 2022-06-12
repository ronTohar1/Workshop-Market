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
  serverMakeCoManager,
  serverMakeCoOwner,
  serverRemoveCoOwner,
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
import AddToRoleForm from "../Forms/AddToRoleForm"
const fields = {
  id: "memberId",
  // role: "roleInStore",
  roleName: "roleName"
}

interface RoleMember {
  memberId: number
  roleInStore: Roles
  roleName: string
}

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
    field: fields.roleName,
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
  const [rows, setRows] = React.useState<RoleMember[]>([])
  const [selectionModel, setSelectionModel] = React.useState<number[]>([])
  const [chosenIds, setChosenIds] = React.useState<number[]>([])
  const [removeMsg, setRemoveMsg] = React.useState<string | null>(null)
  const [openAddCoOwnerDialog, setOpenAddCoOwnerDialog] = React.useState<boolean>(false)
  const [openAddManagerDialog, setOpenAddManagerDialog] = React.useState<boolean>(false)

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

  const updateRemoveMessage = (chosenIds: number[]) => {
    if (chosenIds.length === 0)
      setRemoveMsg(null)
    else {
      const chosenMember: RoleMember[] = rows.filter((member: RoleMember) => member.memberId === chosenIds[0])
      if (chosenMember[0].roleInStore === Roles.Manager)
        setRemoveMsg(null)
      else
        setRemoveMsg("Remove " + chosenMember[0].roleName)
    }
  }
  const handleSelectionChanged = (newSelection: any) => {
    console.log(newSelection)
    const chosenIds: number[] = newSelection//.map((id:number)=>rows[id].id)
    updateRemoveMessage(chosenIds)
    setSelectionModel(newSelection)
    setChosenIds(chosenIds)
  }

  const handleRemovePosition = () => {

    if (chosenIds.length === 0) {
      alert("Please select a co-owner to remove")
    }
    else {
      const chosenMember: RoleMember[] = rows.filter((member: RoleMember) => member.memberId === chosenIds[0])
      if (!(chosenMember[0].roleInStore === Roles.Owner))
        alert("Please pick an ~OWNER~ to remove")
      else
        removeCoOwner(chosenIds[0])
    }
  }

  function removeCoOwner(ownerId: number) {
    fetchResponse(serverRemoveCoOwner(getBuyerId(), store.id, ownerId))
      .then((success: boolean) => {
        handleChangedStore(store)
        showSuccessSnack("Successfully removed " + ownerId + " from owner position")
      })
      .catch(showFailureSnack)
  }

  const handleAddCoOwnerClick = () => {
    setOpenAddCoOwnerDialog(true)
  }
  function addCoOwner(userId: number) {
    fetchResponse(serverMakeCoOwner(getBuyerId(), store.id, userId))
      .then((success: boolean) => {
        handleChangedStore(store)
        setOpenAddCoOwnerDialog(false)
        showSuccessSnack("Added user " + userId + " to the co-owners")
      })
      .catch(showFailureSnack)
  }

  function addManager(userId: number) {
    fetchResponse(serverMakeCoManager(getBuyerId(), store.id, userId))
      .then((success: boolean) => {
        handleChangedStore(store)
        setOpenAddManagerDialog(false)
        showSuccessSnack("Added user " + userId + " to the managers")
      })
      .catch(showFailureSnack)
  }

  React.useEffect(() => {
    try {
      const rolesIds: number[] = Object.values(Roles).filter((v: any) => !isNaN(v)).map((v: any) => Number(v)) // taking the ids of the roles in Roles enum
      const rolesNames: string[] = Object.values(Roles).filter((v: any) => isNaN(v)).map((v: any) => String(v)) // Taking the names of the roles in Roles neum
      const membersByRole: Promise<RoleMember[]>[] = rolesIds.map((roleId: number, index: number) => {
        return fetchResponse(serverGetMembersInRoles(getBuyerId(), store.id, roleId))
          .then((membersIds: number[]) => {
            return membersIds.map((memberId): RoleMember => { return { memberId: memberId, roleInStore: roleId, roleName: rolesNames[index] } })
          })
          .catch((e): RoleMember[] => {
            alert("this is")
            alert(e)
            return []
          })
      }
      )

      const membersByRoleArr: Promise<RoleMember[][]> = convergePromises(membersByRole)
      const rows: Promise<RoleMember[]> = membersByRoleArr.then((mbra: RoleMember[][]) => mbra.reduce(
        (membersInRoles: RoleMember[], currArr: RoleMember[]) => {
          return membersInRoles.concat(currArr)
        }, []
      ))
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
                getRowId={(row: MemberInRole) => row.memberId}
              />
              <Stack direction="row" justifyContent="space-between" width={'95vw'}>
                <Stack>
                  <AddToRoleForm
                    handleAddCoOwner={addCoOwner}
                    open={openAddCoOwnerDialog}
                    handleClose={() => setOpenAddCoOwnerDialog(false)}
                    handleOpen={() => setOpenAddCoOwnerDialog(true)}
                    displayText={"Add A Co-Owner"} />
                  <AddToRoleForm
                    handleAddCoOwner={addManager}
                    open={openAddManagerDialog}
                    handleClose={() => setOpenAddManagerDialog(false)}
                    handleOpen={() => setOpenAddManagerDialog(true)}
                    displayText={"Add A Manager"} />
                </Stack>
                <Box>
                  <Button sx={{ mt: 1 }} disabled={removeMsg === null} color="error" variant="contained" onClick={handleRemovePosition}>
                    Remove Co-Owner
                  </Button>
                </Box>
              </Stack>
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
  return <div>{store === null ? LoadingCircle() : getStoreRoles()}</div> // return  store === null ? LoadingComponent() : storePreview()
}
