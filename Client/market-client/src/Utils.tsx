export interface Product {
    Id: number,
    Name: string,
    Price: number,
    Available_Quantity: number
}

export const createProduct = (
    Id: number,
    Name: string,
    Price: number,
    Available_Quantity: number
): Product => {
    return {
        Id: Id,
        Name: Name,
        Price: Price,
        Available_Quantity: Available_Quantity,
    };
}

export interface Store {
    Id: number,
    Name: string,
    Products: Product[]
}

export const createStore = (
    Id: number,
    Name: string,
    Products: Product[]
): Store => {
    return {
        Id: Id,
        Name: Name,
        Products: Products
    };
}

export const Currency="NIS"
