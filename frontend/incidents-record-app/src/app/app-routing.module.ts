import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '' },
  { path: '', loadChildren: () => import('./modules/home/home.module').then(m => m.HomeModule), },
  { path: 'about', loadChildren: () => import('./modules/about/about.module').then((m) => m.AboutModule), },
  { path: 'report',loadChildren: () => import('./modules/report/report.module').then((m) => m.ReportModule), canActivate: [AuthGuard]},
  { path: 'incident', loadChildren: () => import('./modules/incident/incident.module').then((m) => m.IncidentModule), canActivate: [AuthGuard] },
  { path: 'login', loadChildren: () => import('./modules/login/login.module').then(m => m.LoginModule) },
  { path: 'register', loadChildren: () => import('./modules/register/register.module').then(m => m.RegisterModule) },
  { path: 'user', loadChildren: () => import('./modules/user/user.module').then(m => m.UserModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
