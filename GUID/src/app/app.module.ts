import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login-page/login-page.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from './navbar/navbar.component';
import { ProductPageComponent } from './product-page/product-page.component';
import { ProductPageListComponent } from './product-page-list/product-page-list.component';
import { HomePageComponent } from './home-page/home-page.component';
import { RegisterComponent } from './register/register.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ProductPageComponent } from './components/product-page/product-page.component';
import { ProductPageListComponent } from './components/product-page-list/product-page-list.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { ProductSiteComponent } from './components/product-site/product-site.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavbarComponent,
      ProductPageComponent,
      ProductPageListComponent,
      HomePageComponent,
      ProductSiteComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
