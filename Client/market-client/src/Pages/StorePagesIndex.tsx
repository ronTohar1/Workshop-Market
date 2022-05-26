import * as React from "react";
import {
  DataGrid,
  GridCellEditCommitParams,
  GridColDef,
} from "@mui/x-data-grid";
import {
  alpha,
  Box,
  Button,
  createTheme,
  Fab,
  Stack,
  Toolbar,
  Tooltip,
  Typography,
} from "@mui/material";
import Navbar from "../Componentss/Navbar";
import Product from "../DTOs/Product";
import { AddShoppingCart } from "@mui/icons-material";
import AddProductForm from "../Componentss/Forms/AddProductForm";
import * as storeService from "../services/StoreService";
import SearchPage from "./Search";
import Store from "../DTOs/Store";
import { useQueryParam, NumberParam, StringParam } from "use-query-params";
import { useNavigate } from "react-router-dom";
import { pathSearch } from "../Paths";
import StorePageById from "./StorePage";

export default function StorePagesdfasdf() {
  // const navigate = useNavigate();
  // const theme = createTheme();
  // const [storeId] = useQueryParam("id", NumberParam);

  // try {
  //   const store: Store = storeService.serverGetStore(
  //     storeId === undefined || storeId === null ? -1 : storeId
  //   );
  //   return StorePageById(store);
  // } catch {
  //   alert("Could not find the requested store");
  //   navigate(pathSearch);
  //   return null;
  // }
}
