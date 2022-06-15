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
import { fetchResponse } from "../../services/GeneralService"
import { getBuyerId } from "../../services/SessionService"
import LoadingCircle from "../LoadingCircle"
import FailureSnackbar from "../Forms/FailureSnackbar"
import SuccessSnackbar from "../Forms/SuccessSnackbar"
import { serverGetStorePurchaseHistory } from "../../services/AdminService"
import Purchase from "../../DTOs/Purchase"
import PurchasePolicy from "../../DTOs/PurchasePolicy"
import Bid from "../../DTOs/Bid"

interface BidRow {
    id: number,
    storeId: number,
    productId: number,
    memberId: number,
    bid: number,
    approvingIds: number[],
    counterOffer: boolean,
    productName: string,
}

function convertToBidRow(bid: Bid, store: Store): BidRow {
    return {
        id: bid.id,
        storeId: bid.storeId,
        productId: bid.productId,
        memberId: bid.memberId,
        bid: bid.bid,
        approvingIds: bid.approvingIds,
        counterOffer: bid.counterOffer,
        productName: store.products.filter(p => p.id === bid.productId)[0].name
    }
}

const fields = {
    name: "productName",
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
        renderCell: ({ value }) => (
            <span style={{ overflow: "scroll" }}>
                {value}
            </span>
        ),
        headerAlign: "left",
    },
]

export default function StoreBids({
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
        console.log(newSelection)
        const chosenIds: number[] = newSelection//.map((id:number)=>rows[id].id)
        setSelectionModel(newSelection)
        setChosenIds(chosenIds)
    }

    const handleRemovePolicy = () => {
        console.log("chosenIds")
        console.log(chosenIds)

        if (chosenIds.length === 0) {
            showFailureSnack("Please select a policy to remove")
        }
        else {
            fetchResponse(serverRemovePurchasePolicy(getBuyerId(), store.id, chosenIds[0]))
                .then((success: boolean) => {
                    if (success) {
                        handleChangedStore(store)
                        showSuccessSnack("Removed Policy Successfully")
                    }
                    else showFailureSnack("Coludn't remove this policy")
                })
                .catch(showFailureSnack)
        }
    }

    React.useEffect(() => {
        setRows(store.bids)
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
                            />
                            <Stack direction='row' justifyContent='space-between' width={'95vw'}>

                                <Box>
                                    <Button variant="contained" sx={{ mt: 1 }} onClick={() => navigate(pathPolicy, { state: store })}>
                                        Add New Policies
                                    </Button>

                                </Box>
                                <Box>
                                    <Button sx={{ mt: 1 }} color="error" variant="contained" disabled={chosenIds.length === 0} onClick={handleRemovePolicy}>
                                        Remove Selected Policy
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
    return <div>{store === null ? LoadingCircle() : storePolicies()}</div> // return  store === null ? LoadingComponent() : storePreview()
}
