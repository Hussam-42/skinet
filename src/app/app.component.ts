import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { IProduct } from './models/product';

import { IPagination } from './models/pagination'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  products: IProduct[];

  constructor(private http: HttpClient)
  {

  }

  ngOnInit(): void {

      this.http.get('http://localhost:7095/api/Products').subscribe((response:IPagination) => {
        this.products = response.data;
      });

  }

  title = 'Skinet';

}
