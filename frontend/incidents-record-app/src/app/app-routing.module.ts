import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'login', pathMatch: 'full', redirectTo: 'login' },
  { path: 'report', loadChildren: () => import('./modules/report/report.module').then((m) => m.ReportModule), },
  { path: 'incident', loadChildren: () => import('./modules/incident/incident.module').then((m) => m.IncidentModule), },
  { path: 'login', loadChildren: () => import('./modules/login/login.module').then(m => m.LoginModule) },
  { path: 'category', loadChildren: () => import('./modules/category/category.module').then(m => m.CategoryModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
