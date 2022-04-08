import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IncidentComponent } from './components/incident/incident.component';
import { LoginComponent } from './components/login/login.component';
import { ReportComponent } from './components/report/report.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'incident', component: IncidentComponent },
  { path: 'report', component: ReportComponent },
  { path: '', pathMatch: 'full', redirectTo: '' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
