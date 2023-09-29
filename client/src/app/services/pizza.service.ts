import { Injectable } from '@angular/core'
import { IPizza } from '../types/interfaces/pizza.interface'
import { Ingredient } from '../types/enums/ingredient.enum'
import { BehaviorSubject, Observable } from 'rxjs'
import { PizzaSize } from '../types/enums/pizza-size.enum'

@Injectable({
    providedIn: 'root',
})
export class PizzaService {
    private activeOrder: BehaviorSubject<IPizza[]> = new BehaviorSubject<
        IPizza[]
    >([])

    activeOrder$: Observable<IPizza[]> = this.activeOrder.asObservable()

    updateActiveOrder(order: IPizza[]): void {
        this.activeOrder.next(order)
    }

    // The same logic from above is applied to the selected ingredients.
    private selectedIngredients: BehaviorSubject<Ingredient[]> =
        new BehaviorSubject<Ingredient[]>([])

    selectedIngredients$: Observable<Ingredient[]> =
        this.selectedIngredients.asObservable()

    updateSelectedIngredients(ingredients: Ingredient[]): void {
        this.selectedIngredients.next(ingredients)
    }

    constructor() {}

    updatePizzaTitle(id: number, name: string) {
        // We get the current active order from the BehaviourSubject by using the getValue() method.
        const order = this.activeOrder.getValue()
        // changed to filter to avoid mutating the array (filter returns a new array, unlike splice which was mutating the original array and causing issues)
        const index = order.findIndex((p) => p.id === id)
        order[index].name = name
        this.updateActiveOrder(order)
    }

    submitOrder() {
        // TODO: Send the order to the backend
        console.log('Submitting order', this.activeOrder.getValue())
    }

    deletePizzaFromOrder(index: number) {
        const updatedOrder = this.activeOrder
            .getValue()
            .filter((_, i) => i !== index)

        this.updateActiveOrder(updatedOrder)
    }

    // Default data for pizzas
    defaultPizzas: IPizza[] = [
        {
            id: 1,
            name: 'Margherita',
            price: 5,
            size: PizzaSize.LARGE,
            image: '/assets/margherita.png',
            ingredients: [Ingredient.TOMATO_SAUCE, Ingredient.MOZZARELLA],
        },
        {
            id: 2,
            name: 'Neapolitan',
            price: 5,
            size: PizzaSize.MEDIUM,
            image: '/assets/neapolitan.png',
            ingredients: [
                Ingredient.TOMATO_SAUCE,
                Ingredient.MOZZARELLA,
                Ingredient.HAM,
            ],
        },
        {
            id: 3,
            name: 'Quatro Formagi',
            price: 6,
            size: PizzaSize.MEDIUM,
            image: '/assets/quatro-formagi.png',
            ingredients: [
                Ingredient.PARMESAN,
                Ingredient.MOZZARELLA,
                Ingredient.BLUE_CHEESE,
                Ingredient.GORGONZOLA,
            ],
        },
        {
            id: 4,
            name: 'Bacon',
            price: 6,
            size: PizzaSize.SMALL,
            image: '/assets/bacon.png',
            ingredients: [
                Ingredient.BACON,
                Ingredient.TOMATO_SAUCE,
                Ingredient.MOZZARELLA,
            ],
        },
        {
            id: 5,
            name: 'Bianca',
            price: 6,
            size: PizzaSize.LARGE,
            image: '/assets/bianca.png',
            ingredients: [Ingredient.SOUR_CREAM],
        },
        {
            id: 6,
            name: 'Capricciosa',
            price: 6,
            size: PizzaSize.MEDIUM,
            image: '/assets/capri.png',
            ingredients: [
                Ingredient.HAM,
                Ingredient.TOMATO_SAUCE,
                Ingredient.MUSHROOMS,
                Ingredient.MOZZARELLA,
            ],
        },
        {
            id: 7,
            name: 'Mexicana',
            price: 6,
            size: PizzaSize.SMALL,
            image: '/assets/mexicana.png',
            ingredients: [
                Ingredient.TOMATO_SAUCE,
                Ingredient.GORGONZOLA,
                Ingredient.OLIVES,
                Ingredient.PEPPERONI,
                Ingredient.CHILLI_PEPPER,
            ],
        },
        {
            id: 8,
            name: 'Pepperoni',
            price: 6,
            size: PizzaSize.MEDIUM,
            image: '/assets/pepperoni.png',
            ingredients: [
                Ingredient.TOMATO_SAUCE,
                Ingredient.GORGONZOLA,
                Ingredient.PEPPERONI,
            ],
        },
        {
            id: 9,
            name: 'Tuna',
            price: 6,
            size: PizzaSize.SMALL,
            image: '/assets/tuna.png',
            ingredients: [
                Ingredient.TOMATO_SAUCE,
                Ingredient.TUNA,
                Ingredient.MOZZARELLA,
                Ingredient.ONION,
            ],
        },
        {
            id: 10,
            name: 'Vegetariana',
            price: 6,
            size: PizzaSize.SMALL,
            image: '/assets/vegetariana.png',
            ingredients: [
                Ingredient.TOMATO_SAUCE,
                Ingredient.MOZZARELLA,
                Ingredient.OLIVES,
                Ingredient.MUSHROOMS,
            ],
        },
    ]
}
