import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home/home.component';

const routes: Routes = [
  {path:'', component:HomeComponent,  data: {breadcrumb: "Home"}},
  {path:'test-error', component:TestErrorComponent, data: {breadcrumb: "Test Errors"}},
  {path:'server-error', component:ServerErrorComponent, data: {breadcrumb: "Server Errors"}},
  {path:'notfound-error', component:NotFoundComponent,  data: {breadcrumb: "Not Found"}},
  {path:'home', component:HomeComponent,  data: {breadcrumb: "Home"}},
  {path:'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule)},
  {path:'**', redirectTo:'notfound-error', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
