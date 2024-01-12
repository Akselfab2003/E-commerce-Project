import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login-page/login-page.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './components/register/register.component';
import { ProductPageComponent } from './components/product-page/product-page.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { ProductSiteComponent } from './components/product-site/product-site.component';
import { FiltersComponent } from './components/filters/filters.component';
import { ProductCardComponent } from './components/product-card/product-card.component';
import { authenticatorGuard } from '../app/logic/authenticator.guard'
import { ProfileComponent } from './components/profile/profile.component';

const routes: Routes = [
  {path:"",component:HomePageComponent},
  {path:"Login", component:LoginComponent},
  {path:"Register", component:RegisterComponent},
  {path:"product-page", component:ProductPageComponent},
  {path:"home-page", component:HomePageComponent},
  {path:"product-Site/:id", component:ProductSiteComponent},
  {path:"filter",component:FiltersComponent},
  {path:"product-Site/:id", component:ProductSiteComponent},
  {path:"product-card", component:ProductCardComponent},
  {path:"profile",component:ProfileComponent,canActivate:[authenticatorGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
