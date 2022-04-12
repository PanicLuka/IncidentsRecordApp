import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IncidentRoutingModule } from './incident-routing.module';
import { IncidentComponent } from './incident.component';


@NgModule({
  declarations: [
    IncidentComponent
  ],
  imports: [
    CommonModule,
    IncidentRoutingModule
  ]
})
export class IncidentModule { }
