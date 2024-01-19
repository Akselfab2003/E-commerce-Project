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
import { AdminPageComponent } from './components/admin-page/admin-page.component';
import { adminGuard } from './logic/admin.guard';
import { AdminLoginComponent } from './components/admin-login/admin-login.component';

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
  {path:"admin-page",component:AdminPageComponent,canActivate:[adminGuard]},
  {path:"admin-login",component:AdminLoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
