import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent,  } from './modules/shared/components/navbar/navbar.component';
import { FooterComponent } from './modules/shared/components/footer/footer.component';
import { IncidentService } from './modules/shared/services/incident.service';
import { IncidentDialogComponent } from './dialogs/incident-dialog/incident-dialog.component';
import {  MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule } from '@angular/material/dialog';
import {  MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [AppComponent, NavbarComponent, FooterComponent, IncidentDialogComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatFormFieldModule,
    MatDialogModule,
    MatInputModule,
    MatButtonModule,

  ],
  providers: [IncidentService],
  bootstrap: [AppComponent],
})
export class AppModule {}
