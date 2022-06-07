import * as React from "react"
import {
  DataGrid,
  GridCellEditCommitParams,
  GridColDef,
} from "@mui/x-data-grid"
import { Box, Stack, Typography } from "@mui/material"
import Product from "../../DTOs/Product"
import AddProductForm from "../Forms/AddProductForm"
import Store from "../../DTOs/Store"
import {
  Roles,
  serverGetMembersInRoles,
  serverGetStore,
} from "../../services/StoreService"
import { pathHome } from "../../Paths"
import { useNavigate } from "react-router-dom"
import { fetchResponse } from "../../services/GeneralService"
import { getBuyerId } from "../../services/SessionService"
import LoadingCircle from "../LoadingCircle"

export default function StoreRoles(){
    
}