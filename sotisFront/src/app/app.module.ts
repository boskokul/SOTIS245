import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home/home.component';
import { AboutComponent } from './about/about/about.component';

import { ApiService } from './services/api.service'; // Import your ApiService
import { AppRoutingModule } from './app-routing.module';
import { SafePipe } from './safe.pipe';
import { TreeNodeComponent } from './tree-node/tree-node.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'about', component: AboutComponent },
];

@NgModule({
  declarations: [AppComponent, HomeComponent, AboutComponent, SafePipe, TreeNodeComponent],
  imports: [
    BrowserModule,
    HttpClientModule, // Add HttpClientModule to imports array
    // RouterModule.forRoot(routes),
    AppRoutingModule,
  ],
  providers: [ApiService],
  bootstrap: [AppComponent],
})
export class AppModule {}
