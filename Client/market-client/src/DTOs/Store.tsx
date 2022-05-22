import Product from "./Product";

export class Store{
    id: number;
    name: string;
    products: Product[];

    constructor(
        Id: number,
        Name: string,
        Products: Product[]
    ){
        this.id = Id;
        this.name = Name;
        this.products = Products;
    }
}