import { Card, CardContent, CardHeader, Typography } from '@mui/material'
import Purchase from "../../DTOs/Purchase"

export default function PurchaseCard( purchase: Purchase) {
  return (
    <div>
      <Card elevation={1}>
        <CardHeader
          title={ `The buyer: ${purchase.buyerId}`}
          subheader={purchase.purchaseDate}
        />
        <CardContent>
          <Typography variant="body2" color="textSecondary">
            { `Description: ${purchase.purchaseDescription}`}
            { `Total: ${purchase.purchasePrice}`}
          </Typography>
        </CardContent>
      </Card>
    </div>
  )
}