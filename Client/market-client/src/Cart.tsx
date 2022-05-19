import * as React from 'react';
import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';
import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { createTheme, ThemeProvider, styled } from '@mui/material/styles';
import Navbar from './Navbar';
import { AppBar } from '@mui/material';
import { Currency } from './Utils';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import { IconButton } from '@mui/material';
import { Icon } from '@mui/material';
import { TextField } from '@mui/material';
import { Stack } from '@mui/material';
import UpdateIcon from '@mui/icons-material/Update';
import Collapse from '@mui/material/Collapse';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { IconButtonProps } from '@mui/material/IconButton';

import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import Slide from '@mui/material/Slide';
import { TransitionProps } from '@mui/material/transitions';

export interface Product {
    Id: number,
    Name: string,
    Price: number,
    Chosen_Quantity: number
}

export const createProduct = (
    Id: number,
    Name: string,
    Price: number,
    Chosen_Quantity: number
): Product => {
    return {
        Id: Id,
        Name: Name,
        Price: Price,
        Chosen_Quantity: Chosen_Quantity,
    };
}


const bull = (
    <Box
        component="span"
        sx={{ display: 'inline-block', mx: '2px', transform: 'scale(0.8)' }}
    >
        •
    </Box>
);

const handleRemoveFromCart = (product: Product) => {
    // products = products.filter((prod) => prod.Id != product.Id);
    alert("removed: " + product.Name)
}



interface ExpandMoreProps extends IconButtonProps {
    expand: boolean;
}

const ExpandMore = styled((props: ExpandMoreProps) => {
    const { expand, ...other } = props;
    return <IconButton {...other} />;
})(({ theme, expand }) => ({
    transform: !expand ? 'rotate(0deg)' : 'rotate(180deg)',
    marginLeft: 'auto',
    transition: theme.transitions.create('transform', {
        duration: theme.transitions.duration.shortest,
    }),
}));

function BasicCard(product: Product) {
    const [expanded, setExpanded] = React.useState(false);

    const handleExpandClick = () => {
        setExpanded(!expanded);
    };
    return (

        <Card sx={{ ml: 3, mr: 3 }}>
            <CardContent>
                <Typography variant="h3" component="div">
                    {product.Name}
                </Typography>
                <Typography sx={{ mb: 1.5 }} color="text.secondary">
                    <br></br>
                    Product Information
                </Typography>
                <Typography variant="h6">
                    Price ({Currency}): {product.Price}
                </Typography>
                <Typography variant="h6">
                    Quantity: {product.Chosen_Quantity}
                </Typography>
            </CardContent>

            <Stack direction="row" justifyContent="space-between">
                <Box sx={{ display: 'flex', mr: 3, mb: 3, ml: 1, justfiyContent: "right" }}>
                    <Stack direction="column">
                        <Typography variant="body1">
                            Change Quantity
                        </Typography>
                        <Stack direction="row">

                            <TextField
                                id="outlined-number"
                                type="number"
                                InputLabelProps={{
                                    shrink: true,
                                }}
                                size="small"
                            />
                            <Button
                                variant="outlined"
                                color="inherit"
                                size="small"
                                sx={{ ml: 1 }}
                                startIcon={<UpdateIcon fontSize='small' />}
                                type="submit"
                            >
                                Update
                            </Button>
                        </Stack>

                    </Stack>
                </Box>
                <CardActions>
                    {/* <IconButton onClick={() => handleRemoveFromCart(product)}>
                        <Icon>
                            <DeleteForeverIcon fontSize="medium" sx={{ color: 'black' }} />
                        </Icon>
                    </IconButton> */}
                    {AlertDialogSlide(product)}
                    <ExpandMore
                        expand={expanded}
                        onClick={handleExpandClick}
                        aria-expanded={expanded}
                        aria-label="show more"
                    >
                        <ExpandMoreIcon />
                    </ExpandMore>
                </CardActions>
            </Stack>
            <Collapse in={expanded} timeout="auto" unmountOnExit>
                <CardContent>
                    <Typography paragraph>Method:</Typography>
                    <Typography paragraph>
                        Heat 1/2 cup of the broth in a pot until simmering, add saffron and set
                        aside for 10 minutes.
                    </Typography>
                    <Typography paragraph>
                        Heat oil in a (14- to 16-inch) paella pan or a large, deep skillet over
                    </Typography>
                    <Typography paragraph>
                        educe heat to medium-low, add ned and rice is just tender, 5 to 7
                        minutes more. (Discard any mussels that don&apos;t open.)
                    </Typography>
                    <Typography>
                        Set aside off of the heat to let rest for 10 minutes, and then serve.
                    </Typography>
                </CardContent>
            </Collapse>
        </Card>
    );
}

let products: Product[] = [
    createProduct(1, 'Cupcake', 305, 3),
    createProduct(2, 'Hamburger', 30, 33),
    createProduct(3, 'Salad', 340, 3000),
    createProduct(4, 'Cheese', 130, 232),
    createProduct(5, 'Banana', 35, 22),
    createProduct(6, 'Cooler', 3051, 22),
    createProduct(7, 'Sunglasses', 3035, 223),
    createProduct(8, 'Elephant', 10, 32),
    createProduct(9, 'Zebra', 3, 21),
    createProduct(10, 'Hot Dog', 100, 222),
]


const Item = styled(Paper)(({ theme }) => ({
    ...theme.typography.body2,
    textAlign: 'center',
    color: theme.palette.text.secondary,
    height: 60,
    lineHeight: '60px',
}));

const darkTheme = createTheme({ palette: { mode: 'dark' } });
const theme = createTheme({ palette: { mode: 'light' } });
const makeSingleProduct = (product: Product) => {
    return (
        <Grid item xs={6} sm={4}>
            {BasicCard(product)}
        </Grid>
    );
};
const makeProducts = (products: Product[]) => {
    return (
        <Grid container spacing={3}>
            {products.map((prod) => {
                return (
                    makeSingleProduct(prod)
                );
            })}
        </Grid>
    );
}

export default function Cart() {
    return (
        <Box >
            <ThemeProvider theme={theme}>
                <Box>
                    <AppBar position="relative">
                        {Navbar()}
                    </AppBar>
                </Box>
                <Box sx={{ mt: 5 }} >
                    <Box >
                        {makeProducts(products)}
                    </Box>
                </Box>
            </ThemeProvider>

        </Box>
    );
}


const Transition = React.forwardRef(function Transition(
    props: TransitionProps & {
        children: React.ReactElement<any, any>;
    },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="up" ref={ref} {...props} />;
});

const AlertDialogSlide = (product: Product) => {
    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = (remove:boolean) => {
        setOpen(false);
        if(remove)
            handleRemoveFromCart(product);
    };

    return (
        <div>
            <IconButton onClick={() => handleClickOpen()}>
                <Icon>
                    <DeleteForeverIcon fontSize="medium" sx={{ color: 'black' }} />
                </Icon>
            </IconButton>
            <Dialog
                open={open}
                TransitionComponent={Transition}
                keepMounted
                onClose={handleClose}
                aria-describedby="alert-dialog-slide-description"
            >
                <DialogTitle>{product.Name + ": Confirm Remove"}</DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-slide-description">
                        Are you sure you want to remove this item from your cart?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => handleClose(false)}>Cancel</Button>
                    <Button onClick={() => handleClose(true)}>Confirm</Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}