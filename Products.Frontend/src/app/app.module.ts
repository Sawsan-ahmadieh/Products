import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ProductDetailsFormComponent } from './product-details/product-details-form/product-details-form.component';
import { HttpClientModule } from '@angular/common/http';
import { ProductDetails } from './shared/product-details.model';

@NgModule({
  declarations: [
    AppComponent,
    ProductDetailsComponent,
    ProductDetailsFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
    
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
