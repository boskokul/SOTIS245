import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home/home.component';
import { AboutComponent } from './about/about/about.component';

import { ApiService } from './services/api.service'; // Import your ApiService
import { AppRoutingModule } from './app-routing.module';
import { SafePipe } from './safe.pipe';
import { TreeNodeComponent } from './tree-node/tree-node.component';
import { TestComponent } from './test/test.component';
import { TestCreationComponent } from './test-creation/test-creation.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { TestDetailComponent } from './test-detail/test-detail.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'about', component: AboutComponent },
];

@NgModule({
  declarations: [AppComponent, HomeComponent, AboutComponent, SafePipe, TreeNodeComponent, TestComponent, TestCreationComponent, TestDetailComponent],
  imports: [
    BrowserModule,
    HttpClientModule, // Add HttpClientModule to imports array
    // RouterModule.forRoot(routes),
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatDialogModule,
  ],
  providers: [ApiService],
  bootstrap: [AppComponent],
})
export class AppModule {}
