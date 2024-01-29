import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login-page/login-page.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './components/register/register.component';
import { ProductPageComponent } from './components/product-page/product-page.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { ProductDetailsPageComponent } from './components/product-details-page/product-details-page.component';
import { FiltersComponent } from './components/filters/filters.component';
import { ProductCardComponent } from './components/product-card/product-card.component';
import { authenticatorGuard } from '../app/logic/authenticator.guard'
import { ProfileComponent } from './components/profile/profile.component';
import { BasketComponent } from './components/basket/basket.component';
import { CheckoutPageComponent } from './components/checkout-page/checkout-page.component';
import { adminGuard } from './logic/admin.guard';
import { AdminLoginComponent } from './components/admin-login/admin-login.component';
import { CarouselComponent } from './components/carousel/carousel.component';
import { AdminControlPanelComponent } from './components/admin-control-panel/admin-control-panel.component';
import { UserControlComponent } from './components/AdminControlsModelComponents/user-control/user-control.component';
import { AdminControlComponent } from './components/AdminControlsModelComponents/admin-control/admin-control.component';
import { CompanyControlComponent } from './components/AdminControlsModelComponents/company-control/company-control.component';
import { ProductVariantsControlComponent } from './components/AdminControlsModelComponents/product-variants-control/product-variants-control.component';
import { ProductControlComponent } from './components/AdminControlsModelComponents/product-control/product-control.component';
import { CategoriesControlComponent } from './components/AdminControlsModelComponents/categories-control/categories-control.component';
import { SearchResultComponent } from './components/search-result/search-result.component';
import { ImagesControlComponent } from './components/AdminControlsModelComponents/images-control/images-control.component';

const routes: Routes = [
  {path:"",component:HomePageComponent},
  {path:"Login", component:LoginComponent},
  {path:"Register", component:RegisterComponent},
  {path:"product-page", component:ProductPageComponent},
  {path:"home-page", component:HomePageComponent},
  {path:"product-details/:id", component:ProductDetailsPageComponent},
  {path:"filter",component:FiltersComponent},
  {path:"product-card", component:ProductCardComponent},
  {path:"profile",component:ProfileComponent,canActivate:[authenticatorGuard]},
  {path:"basket",component:BasketComponent},
  {path:"checkout-page",component:CheckoutPageComponent},
  {path:"admin-page",component:AdminControlPanelComponent},
  {path:"admin-login",component:AdminLoginComponent},
  {path:"carousel",component:CarouselComponent},
  {
    path:"AdminControl",
    children:[
      {path:"",component:AdminControlPanelComponent},
      {path:"UserControl",component:UserControlComponent,},
      {path:"CompanyControlComponent", component:CompanyControlComponent},
      {path:"admin-user-control",component:AdminControlComponent},
      {path:"product-control",component:ProductControlComponent},
      {path:"product-variants-control",component:ProductVariantsControlComponent},
      {path:"categories-control",component:CategoriesControlComponent},
      {path:"images-control",component:ImagesControlComponent},

    ],
    canActivate:[adminGuard],
  },

  
  {path:"Search/:q",component:SearchResultComponent},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
