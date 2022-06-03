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
import { serverAddToCart } from "../services/BuyersService"
import { getBuyerId } from "../services/SessionService"

const fields = {
  name: "name",
  price: "price",
  available_quantity: "availableQuantity",
  category: "category",
}

const handleFailToAdd = (products: Product[]) => {
  const failString: string =
    "Failed to add the following products to your cart:\n" +
    products.map((product: Product) => product.name)
  alert(failString)
}

const handleSucceedToAdd = (products: Product[]) => {
  const success: string =
    "Successfully added the following products to your cart:\n" +
    products.map((product: Product) => product.name)
}

export default function StorePage() {
  const startingPageSize: number = 10

  const navigate = useNavigate()
  const [pageSize, setPageSize] = React.useState<number>(startingPageSize)
  const [selectionModel, setSelectionModel] = React.useState<number[]>([])
  const [selectedProductsIds, setSelectedProductsIds] = React.useState<
    number[]
  >([])
  const isManager: boolean = true //TODO: change to real value. storeService.getMemberInRole(...)
  const [products, setProducts] = React.useState<Product[]>([])
  const [storeId] = useQueryParam("id", NumberParam)
  const [store, setStore] = React.useState<Store | null>(null)

  React.useEffect(() => {
    fetchResponse(serverGetStore(storeId))
      .then((store) => {
        setStore(store)
        setProducts(store.products)
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

  const updateSelection = (newSelection: any) => {
    const chosenIds: number[] = newSelection
    setSelectionModel(newSelection)
    setSelectedProductsIds(chosenIds)
  }

  const handleNewSelection = (newSelectionModel: any) => {
    updateSelection(newSelectionModel)
  }

  const handleAddToCart = () => {
    const failedToAdd: Product[] = []
    const succeedToAdd: Product[] = []
    products.forEach((prod: Product) => {
      if (selectedProductsIds.includes(prod.id)) {
        fetchResponse(serverAddToCart(getBuyerId(), prod.id, prod.storeId, 0))
          .then((success: boolean) => {
            if(success) succeedToAdd.push(prod)
          })
          .catch((e) => {
            failedToAdd.push(prod)
            alert(e)
          })
      }
    })

    if (failedToAdd.length === 0)
      alert("Successfully added all requested products to your cart")
    else
      alert("Managed to add only "+succeedToAdd.length+" products")
    updateSelection([])
  }

  return (
    <Box>
      <Navbar />
      {toolBar(selectedProductsIds.length, store, handleAddToCart)}
      <Stack direction="row">{}</Stack>
      <div style={{ height: "50vh", width: "100%" }}>
        <div style={{ display: "flex", height: "100%" }}>
          <div style={{ flexGrow: 1 }}>
            <DataGrid
              rows={products}
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
              selectionModel={selectionModel}
              isCellEditable={(params) => false}
            />
          </div>
        </div>
      </div>
    </Box>
  )
}
