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


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,

    NavbarComponent,
      ProductPageComponent,
      ProductPageListComponent
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
