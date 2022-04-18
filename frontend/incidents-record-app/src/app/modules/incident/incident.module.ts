import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IncidentRoutingModule } from './incident-routing.module';
import { IncidentComponent } from './incident.component';
import { MaterialModule } from '../shared/material/material.module';


@NgModule({
  declarations: [
    IncidentComponent
  ],
  imports: [
    CommonModule,
    IncidentRoutingModule,
    MaterialModule
  ]
})
export class IncidentModule { }
