import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ImportRecordComponent } from '../components/import-record/import-record.component';

const routes: Routes = [
    {
        path:"record/ImportReport",
        component: ImportRecordComponent
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
