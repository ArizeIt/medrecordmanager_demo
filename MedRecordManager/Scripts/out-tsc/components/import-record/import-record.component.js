"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.ImportRecordComponent = void 0;
var core_1 = require("@angular/core");
var api_service_1 = require("../../services/api.service");
var moment = require("moment");
var _ = require("lodash");
var dialog_1 = require("@angular/material/dialog");
var import_logs_component_1 = require("../import-logs/import-logs.component");
var ImportRecordComponent = /** @class */ (function () {
    function ImportRecordComponent(apiService, matDialog) {
        var _this = this;
        this.apiService = apiService;
        this.matDialog = matDialog;
        this.page = { CurrentPage: 0 };
        this.rows = new Array();
        this.rowsQueueOrDone = new Array();
        this.loading = false;
        this.filter = {
            visitStartDate: moment().subtract(3, 'months').toDate(),
            visitEndDate: new Date(),
            clinicIds: [],
            finClassIds: [],
            patSearch: ''
        };
        this.clinics = [];
        this.finClasses = [];
        this.generatingAndDownLoadCSV = false;
        this.unqueuing = false;
        this.reruning = false;
        this.currentTabIndex = 0;
        this.apiService.getClinics().subscribe(function (r) {
            _this.clinics = r;
        });
        this.apiService.getFinClasses().subscribe(function (r) {
            _this.finClasses = r;
        });
    }
    ImportRecordComponent.prototype.ngOnInit = function () {
        this.getProcessedIn(1);
    };
    ImportRecordComponent.prototype.getProcessedIn = function (pageNum) {
        var _this = this;
        this.loading = true;
        this.apiService.getProcessedIn(pageNum, this.filter).subscribe(function (r) {
            console.log(r);
            _this.page = r;
            _this.rows = r.Results;
            console.log(_this.rows);
            _this.page.CurrentPage -= 1;
            _this.loading = false;
        });
    };
    ImportRecordComponent.prototype.getVisitsByStatus = function (pageNum, status) {
        var _this = this;
        this.loading = true;
        this.apiService.getVisitsByStatus(pageNum, status, this.filter).subscribe(function (r) {
            _this.page = r;
            _this.rowsQueueOrDone = r.Results;
            _this.page.CurrentPage -= 1;
            _this.loading = false;
        });
    };
    ImportRecordComponent.prototype.setPage = function (pageInfo) {
        switch (this.currentTabIndex) {
            case 0:
                this.getProcessedIn(pageInfo.offset + 1);
                break;
            case 1:
                this.getVisitsByStatus(pageInfo.offset + 1, "QUEUED");
                break;
            case 2:
                this.getVisitsByStatus(pageInfo.offset + 1, "DONE");
                break;
        }
    };
    ImportRecordComponent.prototype.tabChanged = function (e) {
        this.currentTabIndex = e.index;
        this.page = { CurrentPage: 0 };
        this.rows = [];
        this.rowsQueueOrDone = [];
        console.log(e);
        switch (this.currentTabIndex) {
            case 0:
                this.getProcessedIn(1);
                break;
            case 1:
                this.getVisitsByStatus(1, "QUEUED");
                break;
            case 2:
                this.getVisitsByStatus(1, "DONE");
                break;
        }
    };
    ImportRecordComponent.prototype.downloadCSV = function (index) {
        var _this = this;
        this.generatingAndDownLoadCSV = true;
        switch (index) {
            case 0:
                this.apiService.getProcessedIn4Report(this.filter).subscribe(function (r) {
                    _this.downloadFile(r, 'ProcessedIn');
                    _this.generatingAndDownLoadCSV = false;
                }, function (err) {
                    _this.generatingAndDownLoadCSV = false;
                    console.log(err);
                });
                break;
            case 1:
                this.apiService.getVisitsByStatus4Report("QUEUED", this.filter).subscribe(function (r) {
                    _this.downloadFile(r, 'QUEUED');
                    _this.generatingAndDownLoadCSV = false;
                }, function (err) {
                    _this.generatingAndDownLoadCSV = false;
                    console.log(err);
                });
                break;
            case 2:
                this.apiService.getVisitsByStatus4Report("DONE", this.filter).subscribe(function (r) {
                    _this.downloadFile(r, 'Done');
                    _this.generatingAndDownLoadCSV = false;
                }, function (err) {
                    _this.generatingAndDownLoadCSV = false;
                    console.log(err);
                });
                break;
        }
    };
    ImportRecordComponent.prototype.downloadFile = function (data, fileName) {
        //Download type xls
        //const contentType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
        //Download type: CSV
        var contentType2 = 'text/csv';
        var blob = new Blob([data], { type: contentType2 });
        var url = window.URL.createObjectURL(blob);
        //Open a new window to download
        // window.open(url); 
        //Download by dynamically creating a tag
        var a = document.createElement('a');
        a.href = url;
        // a.download = fileName;
        a.download = fileName + '.csv';
        a.click();
        window.URL.revokeObjectURL(url);
    };
    ImportRecordComponent.prototype.getSelected = function () {
        return _.filter(this.rowsQueueOrDone, { checked: true });
    };
    ImportRecordComponent.prototype.unqueue = function () {
        var _this = this;
        var ids = _.map(this.getSelected(), function (r) {
            return r.VisitId;
        });
        this.unqueuing = true;
        this.apiService.unqueue(ids).subscribe(function (r) {
            _this.unqueuing = false;
            _this.getVisitsByStatus(1, "QUEUED");
        });
    };
    ImportRecordComponent.prototype.rerunImport = function () {
        var _this = this;
        var ids = _.map(this.getSelected(), function (r) {
            return r.VisitId;
        });
        this.reruning = true;
        this.apiService.rerunImport(ids).subscribe(function (r) {
            _this.reruning = false;
            _this.getVisitsByStatus(1, "QUEUED");
        });
    };
    ImportRecordComponent.prototype.viewLogs = function (logs) {
        this.matDialog.open(import_logs_component_1.ImportLogsComponent, {
            data: {
                logs: logs
            }
        });
    };
    ImportRecordComponent = __decorate([
        core_1.Component({
            selector: 'app-import-record',
            templateUrl: './import-record.component.html',
            styleUrls: ['./import-record.component.scss']
        }),
        __metadata("design:paramtypes", [api_service_1.ApiService, dialog_1.MatDialog])
    ], ImportRecordComponent);
    return ImportRecordComponent;
}());
exports.ImportRecordComponent = ImportRecordComponent;
//# sourceMappingURL=import-record.component.js.map