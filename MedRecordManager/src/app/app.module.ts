import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { ImportRecordComponent } from '../components/import-record/import-record.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTabsModule } from '@angular/material/tabs';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialogModule } from '@angular/material/dialog';
import { ImportLogsComponent } from '../components/import-logs/import-logs.component';

@NgModule({
  declarations: [
        AppComponent,
        ImportRecordComponent,
        ImportLogsComponent
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      AppRoutingModule,
      NgxDatatableModule,
      NoopAnimationsModule,
      MatSelectModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, MatNativeDateModule, MatTabsModule, MatButtonModule, MatIconModule,
      MatCheckboxModule, MatTooltipModule, MatDialogModule
  ],
    providers: [MatDatepickerModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
