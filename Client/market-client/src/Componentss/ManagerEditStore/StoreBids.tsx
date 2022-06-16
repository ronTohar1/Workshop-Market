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
    serverChangeProductAmountInInventory,
    serverDenyBid,
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
import ThumbDownIcon from '@mui/icons-material/ThumbDown'
import ThumbUpIcon from '@mui/icons-material/ThumbUp'
import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline'

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
    id: "id",
    name: "productName",
    bid: "bid",
    counterOffer: "counterOffer",
}



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
    const [rows, setRows] = React.useState<Bid[]>([])
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
        const chosenIds: number[] = newSelection//.map((id:number)=>rows[id].id)
        setSelectionModel(newSelection)
        setChosenIds(chosenIds)
    }

    const handleRemovePolicy = () => {
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
        setRows(store.bids.map(b => convertToBidRow(b, store)))
    }, [store])

    const handleApproveBid = (bidId: number) => {
        fetchResponse(serverApproveBid(store.id, getBuyerId(), bidId))
            .then((success) => {
                showSuccessSnack("Bid Approved")
                handleChangedStore(store)
            })
            .catch(showFailureSnack)
    }

    const handleDenyBid = (bidId: number) => {
        alert("the id is "+bidId)
        fetchResponse(serverDenyBid(store.id, getBuyerId(), bidId))
            .then((success) => {
                showSuccessSnack("Bid Denyed")
                handleChangedStore(store)
            })
            .catch(showFailureSnack)
    }

    const columns: GridColDef[] = [
        {
            field: fields.name,
            headerName: "Product Name",
            type: "string",
            flex: 2,
            align: "left",
            headerAlign: "left",
        },
        {
            field: fields.bid,
            headerName: "Bid Price",
            type: "number",
            flex: 1,
            align: "left",
            headerAlign: "left",
        },
        {
            field: fields.counterOffer,
            headerName: "Counter Offer?",
            type: "boolean",
            flex: 1,
            align: "left",
            headerAlign: "left",
        },
        {
            field: fields.id,
            headerName: "Approve Bid",
            type: "boolean",
            flex: 1,
            align: "left",
            headerAlign: "left",
            renderCell: (params: GridRenderCellParams<BidRow>) => {
                const approved = params.row.approvingIds.includes(getBuyerId())
                console.log(params.row)
                return approved ? (<CheckCircleOutlineIcon />) : (
                    <strong>
                        <Button sx={{ mr: 1 }} variant="contained" onClick={() => handleApproveBid(params.row.id)} startIcon={<ThumbUpIcon />} color="success">
                        </Button>
                        <Button variant="contained" onClick={() => handleDenyBid(params.row.id)} startIcon={<ThumbDownIcon />} color="error"> </Button>

                    </strong>
                )
            }
        },

    ]

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
