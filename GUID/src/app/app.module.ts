import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
<<<<<<< HEAD
import { LoginComponent } from './login-page/login-page.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from './navbar/navbar.component';

=======
import { LoginComponent } from './components/login-page/login-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { ProductControlComponent } from './components/AdminControlsModelComponents/product-control/product-control.component';
import { CompanyControlComponent } from './components/AdminControlsModelComponents/company-control/company-control.component';
import { CategoriesControlComponent } from './components/AdminControlsModelComponents/categories-control/categories-control.component';
import { SearchResultComponent } from './components/search-result/search-result.component';
import { ImagesControlComponent } from './components/AdminControlsModelComponents/images-control/images-control.component';
import { CarouselReviewsComponent } from './components/carousel-reviews/carousel-reviews.component';
import { ReviewsPageComponent } from './components/reviews-page/reviews-page.component';
import { PriceListControlComponent } from './components/AdminControlsModelComponents/price-list-control/price-list-control.component';
import { OrderConfirmationPageComponent } from './components/order-confirmation-page/order-confirmation-page.component';
>>>>>>> develop

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
<<<<<<< HEAD

    NavbarComponent
=======
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
    ProductControlComponent,
    ProductVariantsControlComponent,
    CompanyControlComponent,
    CategoriesControlComponent,
    SearchResultComponent,
    CarouselReviewsComponent,
    ReviewsPageComponent,
    ImagesControlComponent,
    PriceListControlComponent,
    OrderConfirmationPageComponent
>>>>>>> develop
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
<<<<<<< HEAD
=======
    HttpClientModule,
    BrowserAnimationsModule,
    SlickCarouselModule
>>>>>>> develop
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
