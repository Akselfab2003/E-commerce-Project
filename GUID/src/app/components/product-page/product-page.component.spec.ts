import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProductDetailsPageComponent } from '../product-details-page/product-details-page.component';

describe('ProductPageComponent', () => {
  let component: ProductDetailsPageComponent;
  let fixture: ComponentFixture<ProductDetailsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProductDetailsPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProductDetailsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
