import Product from "./Product";

export class Store{
    Id: number;
    Name: string;
    Products: Product[];

    constructor(
        Id: number,
        Name: string,
        Products: Product[]
    ){
        this.Id = Id;
        this.Name = Name;
        this.Products = Products;
    }
}