import * as React from "react"
import {
  DataGrid,
  GridCellEditCommitParams,
  GridColDef,
} from "@mui/x-data-grid"
import { Box, Dialog, Stack, Typography } from "@mui/material"
import Product from "../../DTOs/Product"
import AddProductForm from "../Forms/AddProductForm"
import Store from "../../DTOs/Store"
import {
  Roles,
  serverAddNewProduct,
  serverChangeProductAmountInInventory,
  serverGetMembersInRoles,
  serverGetStore,
} from "../../services/StoreService"
import { pathHome } from "../../Paths"
import { useNavigate } from "react-router-dom"
import { fetchResponse } from "../../services/GeneralService"
import { getBuyerId } from "../../services/SessionService"
import LoadingCircle from "../LoadingCircle"
import FailureSnackbar from "../Forms/FailureSnackbar"
import SuccessSnackbar from "../Forms/SuccessSnackbar"
const fields = {
  name: "name",
  price: "price",
  available_quantity: "availableQuantity",
  category: "category",
}

const isEditableField = (field: string) => {
  return field == fields.available_quantity
}

const nameEnding = (field: string) => {
  return field == fields.available_quantity ? " (Editable)" : ""
}

const columns: GridColDef[] = [
  {
    field: fields.name,
    headerName: "Product Name" + nameEnding(fields.name),
    type: "string",
    flex: 2,
    align: "left",
    editable: isEditableField(fields.name),
    headerAlign: "left",
  },
  {
    field: fields.price,
    headerName: "Price" + nameEnding(fields.price),
    type: "number",
    flex: 1,
    editable: isEditableField(fields.price),
    align: "left",
    headerAlign: "left",
  },
  {
    field: fields.available_quantity,
    headerName: "Available Quantity" + nameEnding(fields.available_quantity),
    description: "Product current quantity in store inventory",
    type: "number",
    flex: 1,
    editable: isEditableField(fields.available_quantity),
    align: "left",
    headerAlign: "left",
  },
  {
    field: fields.category,
    headerName: "Category" + nameEnding(fields.category),
    // type: 'string',
    flex: 1,
    editable: isEditableField(fields.category),
    align: "left",
    headerAlign: "left",

    // valueGetter: (params: GridValueGetterParams) =>
    //   `${params.row.firstName || ''} ${params.row.lastName || ''}`,
  },
]

export default function StorePageOfManager({
  store,
  handleChangedStore,
}: {
  store: Store
  handleChangedStore: (s: Store) => void
}) {
  const initSize: number = 5

  const navigate = useNavigate()
  const [pageSize, setPageSize] = React.useState<number>(initSize)
  const [rows, setRows] = React.useState<Product[]>([])
  const [openProductForm, setOpenProductForm] = React.useState<boolean>(false)
  const [openFailSnack, setOpenFailSnack] = React.useState<boolean>(false)
  const [failureProductMsg, setFailureProductMsg] = React.useState<string>("")
  const [openSuccSnack, setOpenSuccSnack] = React.useState<boolean>(false)
  const [successProductMsg, setSuccessProductMsg] = React.useState<string>("")

  const showSuccessSnack = (msg: string) => {
    setOpenSuccSnack(true)
    setSuccessProductMsg(msg)
    setOpenProductForm(false)
  }

  const showFailureSnack = (msg: string) => {
    setOpenFailSnack(true)
    setFailureProductMsg(msg)
  }

  // const [storeId] = useQueryParam("id", NumberParam)

  const verifyIsManagerOrOwner = (store: Store) => {
    fetchResponse(
      serverGetMembersInRoles(getBuyerId(), store.id, Roles.Manager)
    )
      .then((managerIds: number[]) => {
        fetchResponse(
          serverGetMembersInRoles(getBuyerId(), store.id, Roles.Owner)
        ).then((ownersIds: number[]) => {
          const buyerId = getBuyerId()
          if (!(managerIds.includes(buyerId) || ownersIds.includes(buyerId))) {
            alert("You dont have permission to watch this page!!!!")
            navigate(pathHome)
          }
        })
      })
      .catch((e) => {
        alert(e)
        navigate(pathHome)
      })
  }

  React.useEffect(() => {
    verifyIsManagerOrOwner(store) // Verifying this is user is allowed to watch the page
    setRows(store.products)
  })

  function updateAvailableQuantity(product: Product, newQuantity: number) {
    fetchResponse(
      serverChangeProductAmountInInventory(
        getBuyerId(),
        store.id,
        product.id,
        newQuantity,
        product.availableQuantity
      )
    )
      .then((updated: boolean) => {
        if (updated) handleChangedStore(store)
        showSuccessSnack(
          `Changed ${product.name} amount in inventory to ${newQuantity}`
        )
      })
      .catch((e) => {
        const newRows = rows.map((row) => row)
        setRows(newRows) //Re-rendering for the changed fields to stay the same as before
        showFailureSnack(e)
      })
  }

  const handleCellEdit = (e: GridCellEditCommitParams) => {
    const newRows = rows.map((row) => {
      if (row.id === e.id) {
        switch (e.field) {
          case fields.available_quantity:
            updateAvailableQuantity(row, e.value)
            break
        }
      }
      return row
    })
  }

  const handleAddProduct = (
    productName: string,
    price: number,
    category: string
  ) => {
    fetchResponse(
      serverAddNewProduct(getBuyerId(), store.id, productName, price, category)
    )
      .then((prodId: number) => {
        handleChangedStore(store)
        showSuccessSnack("Added " + productName + " Successfully")
      })
      .catch(showFailureSnack)
  }

  const storePreview = () => {
    return (
      <Box sx={{ mr: 3 }}>
        <Stack direction="row">{}</Stack>
        <Typography
          sx={{ flex: "1 1 100%" }}
          variant="h4"
          id="tableTitle"
          component="div"
        >
          {store != null ? store.name : "Error- store not exist"}
        </Typography>
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
                isCellEditable={(params) => isEditableField(params.field)}
                onCellEditCommit={handleCellEdit}
                disableSelectionOnClick
              />
              <AddProductForm
                handleAddProduct={handleAddProduct}
                open={openProductForm}
                handleClose={() => setOpenProductForm(false)}
                handleOpen={() => setOpenProductForm(true)}
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
  return <div>{store === null ? LoadingCircle() : storePreview()}</div> // return  store === null ? LoadingComponent() : storePreview()
}
