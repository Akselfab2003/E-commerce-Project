import { Component, Input } from '@angular/core';
import { Products } from '../../models/Products';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { basketLogic } from '../../logic/basketLogic';

@Component({
  selector: 'app-product-details-page',
  templateUrl: './product-details-page.component.html',
  styleUrl: './product-details-page.component.css'
})
export class ProductDetailsPageComponent<T> {
  constructor(private route: ActivatedRoute, private service: HttpserviceService<T>, private basketTest:basketLogic<T>) {}

  @Input() product: Products = new Products();

  GetProduct<T>(id:Number){
    this.service.GetRequest<Products>(`Products/${id}`).subscribe((data)=>{
      this.product = data;
      console.log(data)
    });
  };

  AddToBasket(event: MouseEvent){
    event.stopPropagation()
    this.basketTest.AddToBasket(this.product)
  }

  selectedId: number = 0;
  ngOnInit() {
    this.route.paramMap.subscribe((data)=>{
      this.selectedId = Number(data.get('id'));
      this.GetProduct(this.selectedId)
      console.log("ProductDetails Object:");
    })
  }
}
