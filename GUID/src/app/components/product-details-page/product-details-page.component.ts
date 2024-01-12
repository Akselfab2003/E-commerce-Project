import { Component, Input } from '@angular/core';
import { Products } from '../../models/Products';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { HttpserviceService } from '../../../Services/httpservice.service';

@Component({
  selector: 'app-product-details-page',
  templateUrl: './product-details-page.component.html',
  styleUrl: './product-details-page.component.css'
})
export class ProductDetailsPageComponent<T> {
  constructor(private route: ActivatedRoute, private service: HttpserviceService<T>) {}

  @Input() product: Products = new Products();

  GetProduct<T>(id:Number){
    this.service.GetRequest<Products>(`Products/${id}`).subscribe((data)=>{
      this.product = data;
      console.log(data)
    });
  };

  selectedId: number = 0;
  ngOnInit() {
    this.route.paramMap.subscribe((data)=>{
      this.selectedId = Number(data.get('id'));
      this.GetProduct(this.selectedId)
      console.log("ProductDetails Object:");
    })
  }
}
