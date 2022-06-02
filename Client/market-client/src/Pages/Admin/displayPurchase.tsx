import Purchase from "../../DTOs/Purchase"
import React from 'react'
import { Container, Grid, Paper } from "@mui/material"
import { List } from "@mui/icons-material"
import PurchaseCard from "../../Componentss/AdminComponents/PurcaseCard"

export const purchases= [
    new Purchase("12/12/2022",34.4, "bought 3 apples", 0),
    new Purchase("10/02/2020",12.4, "bought 2 bananas", 1),
    new Purchase("10/10/2021",10, "bought 3 apples", 0),

]

function displayPurchase(){
     return (
//     <Paper style={{maxHeight: 200, overflow: 'auto'}}>
//      <List>
//       ...
//      </List>
//    </Paper>
    <Container>
      <Grid container spacing={3}>
        {purchases.map(purchase => (
          <Grid item xs={12} md={6} lg={4}>
            {PurchaseCard(purchase)}
          </Grid>
        ))}
      </Grid>
    </Container>
);
     }