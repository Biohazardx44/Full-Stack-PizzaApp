import { IngredientBE } from "../enums/ingredient.enum";

export interface IOrderBE {
    addressTo: string;
    pizzas: IPizzaBE[];
    orderPrice: number;
}

export interface IPizzaBE {
    name: string;
    price: number;
    ingredients: IngredientBE[];
}