import { Box } from "@mui/material"
import CircularProgress from "@mui/material/CircularProgress"


export default function LoadingCircle() {
    return (
      <Box sx={{ display: "flex", justifyContent: "center" }}>
        <CircularProgress />
      </Box>
    )
  }