import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PriceListControlComponent } from './price-list-control.component';

describe('PriceListControlComponent', () => {
  let component: PriceListControlComponent;
  let fixture: ComponentFixture<PriceListControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PriceListControlComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PriceListControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
