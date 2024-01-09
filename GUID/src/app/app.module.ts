import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login-page/login-page.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from './navbar/navbar.component';
import { ProductPageComponent } from './product-page/product-page.component';
import { ProductPageListComponent } from './product-page-list/product-page-list.component';
import { HomePageComponent } from './home-page/home-page.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { HttpserviceService } from '../Services/httpservice.service';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,

    NavbarComponent,
      ProductPageComponent,
      ProductPageListComponent,
      HomePageComponent,
      
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
