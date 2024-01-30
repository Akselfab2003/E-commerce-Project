import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyControlComponent } from './company-control.component';

describe('CompanyControlComponent', () => {
  let component: CompanyControlComponent;
  let fixture: ComponentFixture<CompanyControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CompanyControlComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CompanyControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
