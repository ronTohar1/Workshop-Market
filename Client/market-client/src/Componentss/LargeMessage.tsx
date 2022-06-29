import { Box, Typography } from "@mui/material"

export default function LargeMessage(msg: string) {
    return(
  <Box sx={{ display: "flex", justifyContent: "center" }}>
    <Typography variant="h1" component="div" gutterBottom>
      {msg}
    </Typography>
  </Box>
    );
}
