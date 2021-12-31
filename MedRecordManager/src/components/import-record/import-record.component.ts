import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import * as moment from 'moment';

@Component({
    selector: 'app-import-record',
    templateUrl: './import-record.component.html',
    styleUrls: ['./import-record.component.scss']

})
export class ImportRecordComponent implements OnInit {

    page: any = { CurrentPage : 0};
    rows = new Array<any>();
    rowsQueueOrDone = new Array<any>();
    loading: boolean = false;
    filter = {
        visitStartDate: moment().subtract(3,'months').toDate(),
        visitEndDate: new Date(),
        clinicIds: [],
        finClassIds: [],
        patSearch: ''
    };
    clinics = [];
    finClasses = [];
    generatingAndDownLoadCSV = false;
    currentTabIndex = 0;

    constructor(private apiService: ApiService) {
        this.apiService.getClinics().subscribe(r => {
            this.clinics = r;
        });
        this.apiService.getFinClasses().subscribe(r => {
            this.finClasses = r;
        });
    }

    ngOnInit(): void {
        this.getProcessedIn(1);

      }

    getProcessedIn(pageNum) {
        this.loading = true;

        this.apiService.getProcessedIn(pageNum, this.filter).subscribe(r => {
            console.log(r)
            this.page = r;
            this.rows = r.Results;
            console.log(this.rows)
            this.page.CurrentPage -= 1
            this.loading= false
        })
    }

    getVisitsByStatus(pageNum, status) {
        this.loading = true;

        this.apiService.getVisitsByStatus(pageNum, status,this.filter).subscribe(r => {
            this.page = r;
            this.rowsQueueOrDone = r.Results;
            this.page.CurrentPage -= 1
            this.loading = false
        })
    }



    setPage(pageInfo) {


        switch (this.currentTabIndex) {
            case 0:
                this.getProcessedIn(pageInfo.offset + 1);
                break;
            case 1:
                this.getVisitsByStatus(pageInfo.offset + 1, "QUEUED")
                break;
            case 2:
                this.getVisitsByStatus(pageInfo.offset + 1, "DONE")
                break;
        }
    }

    tabChanged(e) {
        this.currentTabIndex = e.index
        this.page = { CurrentPage: 0 };
        this.rows = [];
        this.rowsQueueOrDone = [];
        console.log(e)
        switch (this.currentTabIndex) {
            case 0:
                this.getProcessedIn(1);
                break;
            case 1:
                this.getVisitsByStatus(1, "QUEUED")
                break;
            case 2:
                this.getVisitsByStatus(1, "DONE")
                break;
        }
    }

    downloadCSV(index) {
        this.generatingAndDownLoadCSV = true;

        switch (index) {
            case 0:
                this.apiService.getProcessedIn4Report(this.filter).subscribe(r => {
                    this.downloadFile(r, 'ProcessedIn');
                    this.generatingAndDownLoadCSV = false;
                }, err => {
                    this.generatingAndDownLoadCSV = false;
                    console.log(err)
                })
                break;
            case 1:
                this.apiService.getVisitsByStatus4Report("QUEUED",this.filter).subscribe(r => {
                    this.downloadFile(r, 'QUEUED');
                    this.generatingAndDownLoadCSV = false;
                }, err => {
                    this.generatingAndDownLoadCSV = false;
                    console.log(err)
                })
                break;
            case 2:
                this.apiService.getVisitsByStatus4Report("DONE",this.filter).subscribe(r => {
                    this.downloadFile(r, 'Done');
                    this.generatingAndDownLoadCSV = false;
                }, err => {
                    this.generatingAndDownLoadCSV = false;
                    console.log(err)
                })
                break;
        }


    }

    downloadFile(data, fileName) {
        //Download type xls
        //const contentType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
        //Download type: CSV
        const contentType2 = 'text/csv';
        const blob = new Blob([data], { type: contentType2 });
        const url = window.URL.createObjectURL(blob);
        //Open a new window to download
        // window.open(url); 

        //Download by dynamically creating a tag
        const a = document.createElement('a');
        a.href = url;
        // a.download = fileName;
        a.download = fileName + '.csv';
        a.click();
        window.URL.revokeObjectURL(url);
    }

}
