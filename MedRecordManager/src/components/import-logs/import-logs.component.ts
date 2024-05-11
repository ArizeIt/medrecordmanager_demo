import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'app-import-logs',
    templateUrl:"./import-logs.component.html",

})
export class ImportLogsComponent implements OnInit {
    logs =[]
    constructor(@Inject(MAT_DIALOG_DATA) private data: any,) {
        console.log(data)
        this.logs = data.logs
    }

  ngOnInit(): void {
  }

}
