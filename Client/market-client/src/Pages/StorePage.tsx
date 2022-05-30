import * as React from "react"
import {
  DataGrid,
  GridCellEditCommitParams,
  GridColDef,
} from "@mui/x-data-grid"
import { Box, Stack } from "@mui/material"
import Navbar from "../Componentss/Navbar"
import Product from "../DTOs/Product"
import AddProductForm from "../Componentss/Forms/AddProductForm"
import Store from "../DTOs/Store"
import { NumberParam, StringParam, useQueryParam } from "use-query-params"
import { serverGetStore } from "../services/StoreService"
import { pathHome } from "../Paths"
import { useNavigate } from "react-router-dom"
import { fetchResponse } from "../services/GeneralService"
import toolBar from "../Componentss/StorePageToolbar"

const fields = {
  name: "name",
  price: "price",
  available_quantity: "availableQuantity",
  category: "category",
}

export default function StorePage() {
  const startingPageSize: number = 10

  const navigate = useNavigate()
  const [pageSize, setPageSize] = React.useState<number>(startingPageSize)
  const [numSelected, setNumSelected] = React.useState<number>(0)
  const [selectedProductsIds, setSelectedProductsIds] = React.useState<
    number[]
  >([])
  const isManager: boolean = true //TODO: change to real value. storeService.getMemberInRole(...)
  const [rows, setRows] = React.useState<Product[]>([])
  const [storeId] = useQueryParam("id", NumberParam)
  const [store, setStore] = React.useState<Store | null>(null)

  const handleError = (msg: string) => {
    alert(msg)
    navigate(pathHome)
  }

  React.useEffect(() => {
    fetchResponse(serverGetStore(storeId))
      .then((store) => {
        setStore(store)
        setRows(store.products)
      })
      .catch((e) => {
        alert(e)
        navigate(pathHome)
      })
  }, [storeId])

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
      field: fields.price,
      headerName: "Price",
      type: "number",
      flex: 1,
      editable: isManager,
      align: "left",
      headerAlign: "left",
    },
    {
      field: fields.available_quantity,
      headerName: "Available Quantity",
      description: "Product current quantity in store inventory",
      type: "number",
      flex: 1,
      editable: isManager,
      align: "left",
      headerAlign: "left",
    },
    {
      field: fields.category,
      headerName: "Category",
      // type: 'string',
      flex: 1,
      editable: isManager,
      align: "left",
      headerAlign: "left",

      // valueGetter: (params: GridValueGetterParams) =>
      //   `${params.row.firstName || ''} ${params.row.lastName || ''}`,
    },
  ]
  const handleNewSelection = (newSelectionModel: any) => {
    const chosenIds: number[] = newSelectionModel
    setNumSelected(chosenIds.length)
    setSelectedProductsIds(chosenIds)
  }

  const handleAddToCart = () => {
    rows.forEach((prod) => {
      if (selectedProductsIds.includes(prod.id)) {
        console.log("Adding to cart: " + prod.name)
      }
    })
  }
  return (
    <Box>
      <Navbar />
      {toolBar(numSelected, store, handleAddToCart)}
      <Stack direction="row">{}</Stack>
      <div style={{ height: "50vh", width: "100%" }}>
        <div style={{ display: "flex", height: "100%" }}>
          <div style={{ flexGrow: 1 }}>
            <DataGrid
              rows={rows}
              columns={columns}
              sx={{
                width: "100vw",
                // height: "100vh",
              }}
              // Paging:
              pageSize={pageSize}
              onPageSizeChange={(newPageSize) => setPageSize(newPageSize)}
              rowsPerPageOptions={[10, 20, 25]}
              pagination
              // Selection:
              checkboxSelection
              disableSelectionOnClick
              onSelectionModelChange={handleNewSelection}
              isCellEditable={(params) => false}
            />
          </div>
        </div>
      </div>
    </Box>
  )
}
