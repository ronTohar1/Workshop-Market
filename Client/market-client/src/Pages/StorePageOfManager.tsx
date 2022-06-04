import * as React from "react"
import {
  DataGrid,
  GridCellEditCommitParams,
  GridColDef,
} from "@mui/x-data-grid"
import { Box, Stack, Typography } from "@mui/material"
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

export default function StorePageOfManager() {
  const initSize: number = 10

  const navigate = useNavigate()
  const [pageSize, setPageSize] = React.useState<number>(initSize)
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

  function updatePrice(product: Product, price: number) {
    if (price != null) product.price = price
  }
  function updateAvailableQuantity(
    product: Product,
    available_quantity: number
  ) {
    if (available_quantity != null)
      product.availableQuantity = available_quantity
  }
  function updateCategory(product: Product, category: string) {
    if (category != null) product.category = category
  }

  const handleCellEdit = (e: GridCellEditCommitParams) => {
    const newRows = rows.map((row) => {
      if (row.id === e.id) {
        switch (e.field) {
          case fields.price:
            updatePrice(row, e.value)
            break
          case fields.available_quantity:
            updateAvailableQuantity(row, e.value)
            break
          case fields.category:
            updateCategory(row, e.value)
            break
        }
      }
      return row
    })
    setRows(newRows)
    // alert(e.field + " Changed into "+ e.value + " id "+ e.id)
  }

  const handleAddProduct = (productToAdd: Product) => {
    setRows([...rows, productToAdd])
    console.log(rows.map((r) => r.name))
  }

  return (
    <Box>
      <Navbar />
      <Stack direction="row">{}</Stack>
      <Typography
        sx={{ flex: "1 1 100%" }}
        variant="h4"
        id="tableTitle"
        component="div"
      >
        {store != null ? store.name : "Error- store not exist"}
      </Typography>
      <div style={{ height: "50vh", width: "100%" }}>
        <div style={{ display: "flex", height: "100%" }}>
          <div style={{ flexGrow: 1 }}>
            <DataGrid
              rows={rows}
              columns={columns}
              sx={{
                width: "100vw",
                height: "80vh",
                "& .MuiDataGrid-cell:hover": {
                  color: "primary.main",
                  border: 1,
                },
              }}
              // Paging:
              pageSize={pageSize}
              onPageSizeChange={(newPageSize) => setPageSize(newPageSize)}
              rowsPerPageOptions={[10, 20, 25]}
              pagination
              // Selection:
              isCellEditable={(params) => isManager}
              onCellEditCommit={handleCellEdit}
            />
            {isManager ? AddProductForm(handleAddProduct) : null}
          </div>
        </div>
      </div>
    </Box>
  )
}