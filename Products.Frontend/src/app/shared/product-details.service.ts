import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductDetails } from './product-details.model';
@Injectable({
  providedIn: 'root'
})
export class ProductDetailsService {
  public GetResult: any;
  public postResult: any;
  list: ProductDetails[] = []
  constructor(private http: HttpClient) { }
  
  ngOnInit():void {
    this.refreshList();
  }

  public refreshList() {
    
    this.http.get('https://localhost:7090/api/products').subscribe((data) => {
      console.log(data);
      //this.list = data as ProductDetails;
    });
    
  }
  public Post() {
    //this.http.post('https://localhost:7090/api/products',).subscribe((data) => {
    //  this.postResult;
    //})
  }
}
