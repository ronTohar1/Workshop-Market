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

export const Currency="NIS"
