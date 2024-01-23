import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login-page/login-page.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { RegisterComponent } from './components/register/register.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ProductPageComponent } from './components/product-page/product-page.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { HttpClientModule } from '@angular/common/http';
import { FiltersComponent } from './components/filters/filters.component';
import { ProductCardComponent } from './components/product-card/product-card.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ProductDetailsPageComponent } from './components/product-details-page/product-details-page.component';
import { BasketComponent } from './components/basket/basket.component';
import { BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { CheckoutPageComponent } from './components/checkout-page/checkout-page.component';
import { AdminPageComponent } from './components/admin-page/admin-page.component';
import { AdminLoginComponent } from './components/admin-login/admin-login.component';
import { CarouselComponent } from './components/carousel/carousel.component';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { AdminControlPanelComponent } from './components/admin-control-panel/admin-control-panel.component';
import { UserControlComponent } from './components/AdminControlsModelComponents/user-control/user-control.component';
import { AdminControlComponent } from './components/AdminControlsModelComponents/admin-control/admin-control.component';
import { ProductVariantsControlComponent } from './components/AdminControlsModelComponents/product-variants-control/product-variants-control.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavbarComponent,
    ProductPageComponent,
    HomePageComponent,
    ProductCardComponent,
    ProductDetailsPageComponent,
    FiltersComponent,
    ProfileComponent,
    ProductDetailsPageComponent,
    BasketComponent,
    CheckoutPageComponent,
    AdminPageComponent,
    AdminLoginComponent,
    CarouselComponent,
    AdminControlPanelComponent,
    UserControlComponent,
    AdminControlComponent,
    ProductVariantsControlComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    SlickCarouselModule
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
