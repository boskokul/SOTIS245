import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home/home.component';
import { AboutComponent } from './about/about/about.component';
import { TestComponent } from './test/test.component';
import { TestTakingComponent } from './test-taking/test-taking.component';

const routes: Routes = [
  { path: '', redirectTo: '/start', pathMatch: 'full' },
  { path: 'home/:id', component: HomeComponent },
  { path: 'start', component: AboutComponent },
  { path: 'tests-creation', component: TestComponent },
  { path: 'tests-taking', component: TestTakingComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
