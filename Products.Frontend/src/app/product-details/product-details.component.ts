import { Component, OnInit } from '@angular/core';
import { ProductDetailsService } from '../shared/product-details.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent implements OnInit {
  constructor(public service: ProductDetailsService) {
    
  }
  ngOnInit(): void {
    this.service.refreshList();
  }
}
