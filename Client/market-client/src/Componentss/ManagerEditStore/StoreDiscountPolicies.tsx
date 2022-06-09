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
    serverRemoveDiscountPolicy,
    serverRemovePurchasePolicy,
} from "../../services/StoreService"
import { pathDiscount, pathHome, pathPolicy } from "../../Paths"
import { useNavigate } from "react-router-dom"
import { fetchResponse } from "../../services/GeneralService"
import { getBuyerId } from "../../services/SessionService"
import LoadingCircle from "../LoadingCircle"
import FailureSnackbar from "../Forms/FailureSnackbar"
import SuccessSnackbar from "../Forms/SuccessSnackbar"
import { serverGetStorePurchaseHistory } from "../../services/AdminService"
import Purchase from "../../DTOs/Purchase"
import PurchasePolicy from "../../DTOs/PurchasePolicy"
const fields = {
    id: "id",
    description: "description",
}


const columns: GridColDef[] = [
    {
        field: fields.id,
        headerName: "Policy ID",
        type: "number",
        flex: 1,
        align: "left",
        headerAlign: "left",
    },
    {
        field: fields.description,
        headerName: "Policy Description",
        type: "string",
        flex: 3,
        align: "left",
        headerAlign: "left",
    },
]

export default function StorePurchasePolicies({
    store,
    handleChangedStore,
}: {
    store: Store
    handleChangedStore: (s: Store) => void
}) {
    const initSize: number = 5

    const navigate = useNavigate()
    const [pageSize, setPageSize] = React.useState<number>(initSize)
    const [rows, setRows] = React.useState<PurchasePolicy[]>([])
    const [selectionModel, setSelectionModel] = React.useState<number[]>([])
    const [chosenIds, setChosenIds] = React.useState<number[]>([])

    const handleSelectionChanged = (newSelection: any) => {
        const chosenIds: number[] = newSelection.map((id:number)=>rows[id].id)
        setSelectionModel(newSelection)
        setChosenIds(chosenIds)
    }

    const handleRemovePolicy = () => {
        console.log("chosenIds")
        console.log(chosenIds)

        if (chosenIds.length === 0) {
            alert("Please select a policy to remove")
        }
        else {
            fetchResponse(serverRemoveDiscountPolicy(getBuyerId(), store.id, chosenIds[0]))
            .then((success:boolean)=>{
                if (success) handleChangedStore(store)
                else alert("Coludnt remove this policy")
            })
            .catch(alert)
        }
    }

    React.useEffect(() => {
        setRows(store.discountPolicies)
    }, [store])


    const storePolicies = () => {
        return (
            <Box sx={{ mr: 3 }}>
                <Typography
                    sx={{ flex: "1 1 100%" }}
                    variant="h4"
                    id="tableTitle"
                    component="div"
                >
                    {store != null ? store.name + "'s Discount Policy" : "Error- store not exist"}
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
                <Stack direction='row' justifyContent='space-between'>

                    <Box>
                        <Button variant="contained" sx={{ml : 1}} onClick={() => navigate(pathDiscount, { state: store })}>
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
    return <div>{store === null ? LoadingCircle() : storePolicies()}</div> // return  store === null ? LoadingComponent() : storePreview()
}
