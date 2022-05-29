import * as React from "react"
import { alpha, Box, Fab, Toolbar, Tooltip, Typography } from "@mui/material"
import { AddShoppingCart } from "@mui/icons-material"
import Store from "../DTOs/Store"

export default function toolBar(
  numSelected: number,
  store: Store | null,
  isManager: boolean,
  handleAddToCart: () => void
) {
  return (
    <Toolbar
      sx={{
        pl: { sm: 2 },
        pr: { xs: 1, sm: 1 },
        ...(numSelected > 0 && {
          bgcolor: (theme) =>
            alpha(
              theme.palette.primary.main,
              theme.palette.action.activatedOpacity
            ),
        }),
      }}
    >
      {numSelected > 0 && isManager ? (
        <Typography
          sx={{ flex: "1 1 100%" }}
          color="inherit"
          variant="subtitle1"
          component="div"
        >
          {numSelected} selected
        </Typography>
      ) : (
        <Typography
          sx={{ flex: "1 1 100%" }}
          variant="h4"
          id="tableTitle"
          component="div"
        >
          {store != null ? store.name : "Error- store not exist"}
        </Typography>
      )}
      {numSelected > 0 && isManager ? (
        <Tooltip title="Add To Cart">
          <Fab
            size="medium"
            color="primary"
            aria-label="add"
            onClick={handleAddToCart}
          >
            <AddShoppingCart />
          </Fab>
        </Tooltip>
      ) : (
        <Box></Box>
      )}
    </Toolbar>
  )
}
