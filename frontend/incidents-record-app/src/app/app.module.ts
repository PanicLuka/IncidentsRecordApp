import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './modules/shared/components/navbar/navbar.component';
import { FooterComponent } from './modules/shared/components/footer/footer.component';
import { IncidentService } from './modules/shared/services/incident.service';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { UserService } from './modules/shared/services/user.service';
import { UserDialogComponent } from './dialogs/user-dialog/user-dialog.component';
import { IncidentDialogComponent } from './dialogs/incident-dialog/incident-dialog.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { MatNativeDateModule, MatOptionModule } from '@angular/material/core';
import {
  MatCheckboxDefaultOptions,
  MatCheckboxModule,
  MAT_CHECKBOX_DEFAULT_OPTIONS,
} from '@angular/material/checkbox';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSliderModule } from '@angular/material/slider';
import { CategoryDialogComponent } from './dialogs/category-dialog/category-dialog.component';
import { MatCardModule } from '@angular/material/card';
import { CategoryService } from './modules/shared';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    IncidentDialogComponent,
    CategoryDialogComponent,
    UserDialogComponent,
  ],
  imports: [
    FormsModule,
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatFormFieldModule,
    MatDialogModule,
    MatButtonModule,
    MatDatepickerModule,
    MatSelectModule,
    MatOptionModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatButtonModule,
    MatGridListModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    MatExpansionModule,
    MatTableModule,
    MatToolbarModule,
    MatSnackBarModule,
    MatDialogModule,
    MatDatepickerModule,
    MatSelectModule,
    MatCheckboxModule,
    MatNativeDateModule,
    MatSortModule,
    MatPaginatorModule,
    MatSliderModule,
    MatFormFieldModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatCardModule,
    MatInputModule
  ],
  providers: [
    IncidentService,
    {
      provide: MAT_CHECKBOX_DEFAULT_OPTIONS,
      useValue: { clickAction: 'noop' } as MatCheckboxDefaultOptions,
    },
    IncidentService,
    CategoryService,
    JwtHelperService,
    UserService,
    CategoryService,
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
