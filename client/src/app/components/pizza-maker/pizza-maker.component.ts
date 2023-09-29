import { Component, OnDestroy, OnInit } from '@angular/core'
import { PizzaService } from 'src/app/services/pizza.service'
import { IPizza } from '../../types/interfaces/pizza.interface'
import { CommonModule } from '@angular/common'
import { MatStepperModule } from '@angular/material/stepper'
import { IngredientsComponent } from '../ingredients/ingredients.component'
import { PreviewOrderComponent } from '../preview-order/preview-order.component'
import { CheckoutComponent } from '../checkout/checkout.component'
import { Subscription } from 'rxjs'

@Component({
    selector: 'app-pizza-maker',
    standalone: true,
    imports: [
        CommonModule,
        MatStepperModule,
        IngredientsComponent,
        PreviewOrderComponent,
        CheckoutComponent,
    ],
    templateUrl: './pizza-maker.component.html',
    styleUrls: ['./pizza-maker.component.scss'],
})
export class PizzaMakerComponent implements OnInit, OnDestroy {
    hasOrders: boolean = false // flag to enable/disable going to the next step
    subscription: Subscription = new Subscription() // we subscribe to the activeOrder$ observable to know if we have orders

    constructor(private pizzaService: PizzaService) {}

    ngOnInit() {
        this.subscription = this.pizzaService.activeOrder$.subscribe(
            (order: IPizza[]) => {
                this.hasOrders = !!order?.length
            }
        )
    }

    ngOnDestroy(): void {
        // unsubscribe to avoid memory leaks
        this.subscription.unsubscribe()
    }
}
