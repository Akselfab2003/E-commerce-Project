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
import { ProductPageListComponent } from './components/product-page-list/product-page-list.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { ProductSiteComponent } from './components/product-site/product-site.component';
import { HttpClientModule } from '@angular/common/http';
import { ProductCardComponent } from './components/product-card/product-card.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavbarComponent,
    ProductPageComponent,
    ProductPageListComponent,
    HomePageComponent,
    ProductSiteComponent,
    ProductCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule
    
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
