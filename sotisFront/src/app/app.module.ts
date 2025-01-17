import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'; // Import HTTP_INTERCEPTORS
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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
import { LoginComponent } from './auth/login/login.component';
import { JwtHelperService } from '@auth0/angular-jwt';

import { JwtInterceptor } from './auth/jwt/jwt.interceptor';
import { TestTakingComponent } from './test-taking/test-taking.component';
import { TestExecutionComponent } from './test-execution/test-execution.component';
import { DragDropModule } from '@angular/cdk/drag-drop';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'about', component: AboutComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AboutComponent,
    SafePipe,
    TreeNodeComponent,
    TestComponent,
    TestCreationComponent,
    TestDetailComponent,
    LoginComponent,
    TestTakingComponent,
    TestExecutionComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatDialogModule,
    DragDropModule,
    FormsModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true,
    },
    ApiService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
