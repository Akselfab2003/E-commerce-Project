import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriesControlComponent } from './categories-control.component';

describe('CategoriesControlComponent', () => {
  let component: CategoriesControlComponent;
  let fixture: ComponentFixture<CategoriesControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CategoriesControlComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CategoriesControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
