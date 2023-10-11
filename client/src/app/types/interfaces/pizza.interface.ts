import { Ingredient } from '../enums/ingredient.enum';
import { PizzaSize } from '../enums/pizza-size.enum';

export interface IPizza {
    id: number
    name: string
    price: number
    size: PizzaSize
    image: string
    ingredients: Ingredient[]
}