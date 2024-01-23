import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductVariantsControlComponent } from './product-variants-control.component';

describe('ProductVariantsControlComponent', () => {
  let component: ProductVariantsControlComponent;
  let fixture: ComponentFixture<ProductVariantsControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProductVariantsControlComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProductVariantsControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
