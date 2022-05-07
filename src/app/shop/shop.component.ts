import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopService } from './shop.service';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  shopParams = new ShopParams();
  totalCount : number;

  @ViewChild('search', {static : true}) searchTerm : ElementRef;

  sortOptions = [{name:"Alphabetically", value:'name'}, {name: "Price from Low to High", value: 'priceAsc'}, {name: "Price from High to Low", value: "priceDesc"}];

  constructor(private shopService : ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts()
  {
      this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.data;
      this.totalCount = response.count;
    });
  }

  getBrands()
  {
      this.shopService.getBrands().subscribe(response => {
      this.brands = [{id : 0, name : "All"}, ...response];
      });
  }

  getTypes()
  {
      this.shopService.getTypes().subscribe(response => {
      this.types = [{id : 0, name : "All"}, ...response];
      });
  }

  onBrandSelected(brandId : number)
  {
    this.shopParams.brandId = brandId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  onTypeSelected(typeId : number)
  {
    this.shopParams.typeId = typeId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  onSortSelected( sort : string)
  {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(pageNum : number)
  {
    if(this.shopParams.pageIndex !== pageNum)
    {
      this.shopParams.pageIndex = pageNum;
      this.getProducts();
    }
  }

  onSearch()
  {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  onReset()
  {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
