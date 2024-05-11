(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["main"],{

/***/ 0:
/*!***************************!*\
  !*** multi ./src/main.ts ***!
  \***************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! D:\Workspace\MedRecordManager\MedRecordManager\src\main.ts */"zUnb");


/***/ }),

/***/ "AytR":
/*!*****************************************!*\
  !*** ./src/environments/environment.ts ***!
  \*****************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
Object.defineProperty(exports, "__esModule", { value: true });
exports.environment = void 0;
exports.environment = {
    production: false
};
/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.


/***/ }),

/***/ "LpmO":
/*!*****************************************************************!*\
  !*** ./src/components/import-record/import-record.component.ts ***!
  \*****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
exports.ImportRecordComponent = void 0;
var api_service_1 = __webpack_require__(/*! ../../services/api.service */ "hD8V");
var moment = __webpack_require__(/*! moment */ "wd/R");
var _ = __webpack_require__(/*! lodash */ "LvDl");
var dialog_1 = __webpack_require__(/*! @angular/material/dialog */ "0IaG");
var import_logs_component_1 = __webpack_require__(/*! ../import-logs/import-logs.component */ "b5bQ");
var i0 = __webpack_require__(/*! @angular/core */ "fXoL");
var i1 = __webpack_require__(/*! ../../services/api.service */ "hD8V");
var i2 = __webpack_require__(/*! @angular/material/dialog */ "0IaG");
var i3 = __webpack_require__(/*! @angular/material/tabs */ "wZkO");
var i4 = __webpack_require__(/*! @angular/material/form-field */ "kmnG");
var i5 = __webpack_require__(/*! @angular/material/input */ "qFsG");
var i6 = __webpack_require__(/*! @angular/material/datepicker */ "iadO");
var i7 = __webpack_require__(/*! @angular/forms */ "3Pt+");
var i8 = __webpack_require__(/*! @angular/material/select */ "d3UM");
var i9 = __webpack_require__(/*! @angular/common */ "ofXK");
var i10 = __webpack_require__(/*! @angular/material/button */ "bTqV");
var i11 = __webpack_require__(/*! @swimlane/ngx-datatable */ "lDzL");
var i12 = __webpack_require__(/*! @angular/material/core */ "FKr1");
var i13 = __webpack_require__(/*! @angular/material/icon */ "NFeN");
var i14 = __webpack_require__(/*! @angular/material/checkbox */ "bSwM");
var i15 = __webpack_require__(/*! @angular/material/tooltip */ "Qu3c");
function ImportRecordComponent_mat_option_21_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-option", 31);
    i0.ɵɵtext(1);
    i0.ɵɵelementEnd();
} if (rf & 2) {
    var c_r40 = ctx.$implicit;
    i0.ɵɵproperty("value", c_r40.id);
    i0.ɵɵadvance(1);
    i0.ɵɵtextInterpolate(c_r40.text);
} }
function ImportRecordComponent_mat_option_26_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-option", 31);
    i0.ɵɵtext(1);
    i0.ɵɵelementEnd();
} if (rf & 2) {
    var c_r41 = ctx.$implicit;
    i0.ɵɵproperty("value", c_r41.text);
    i0.ɵɵadvance(1);
    i0.ɵɵtextInterpolate(c_r41.text);
} }
function ImportRecordComponent_button_34_Template(rf, ctx) { if (rf & 1) {
    var _r43 = i0.ɵɵgetCurrentView();
    i0.ɵɵelementStart(0, "button", 10);
    i0.ɵɵlistener("click", function ImportRecordComponent_button_34_Template_button_click_0_listener() { i0.ɵɵrestoreView(_r43); var ctx_r42 = i0.ɵɵnextContext(); return ctx_r42.downloadCSV(0); });
    i0.ɵɵelementStart(1, "mat-icon");
    i0.ɵɵtext(2, "arrow_circle_down");
    i0.ɵɵelementEnd();
    i0.ɵɵtext(3, "Download csv");
    i0.ɵɵelementEnd();
} }
function ImportRecordComponent_span_35_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "span", 32);
    i0.ɵɵtext(1, "generating and downloading your csv..");
    i0.ɵɵelementEnd();
} }
function ImportRecordComponent_ng_template_39_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
    i0.ɵɵpipe(1, "date");
} if (rf & 2) {
    var row_r44 = ctx.row;
    i0.ɵɵtextInterpolate1(" ", i0.ɵɵpipeBind2(1, 1, row_r44.TimeIn, "MMM dd yyyy, HH:mm"), " ");
} }
function ImportRecordComponent_ng_template_41_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
} if (rf & 2) {
    var row_r45 = ctx.row;
    i0.ɵɵtextInterpolate1(" ", row_r45.ClinicId, " ");
} }
function ImportRecordComponent_ng_template_43_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
} if (rf & 2) {
    var row_r46 = ctx.row;
    i0.ɵɵtextInterpolate1(" ", row_r46.FinClass, " ");
} }
function ImportRecordComponent_ng_template_45_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
} if (rf & 2) {
    var row_r47 = ctx.row;
    i0.ɵɵtextInterpolate1(" ", row_r47.OfficeKey, " ");
} }
function ImportRecordComponent_ng_template_47_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
    i0.ɵɵelement(1, "br");
    i0.ɵɵtext(2);
    i0.ɵɵelement(3, "br");
} if (rf & 2) {
    var row_r48 = ctx.row;
    i0.ɵɵtextInterpolate2(" ", row_r48.PatientInformation.FirstName, " ", row_r48.PatientInformation.LastName, " ");
    i0.ɵɵadvance(2);
    i0.ɵɵtextInterpolate2(" ", row_r48.PatientInformation.Address1, ", ", row_r48.PatientInformation.City, " ");
} }
function ImportRecordComponent_mat_option_68_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-option", 31);
    i0.ɵɵtext(1);
    i0.ɵɵelementEnd();
} if (rf & 2) {
    var c_r49 = ctx.$implicit;
    i0.ɵɵproperty("value", c_r49.id);
    i0.ɵɵadvance(1);
    i0.ɵɵtextInterpolate(c_r49.text);
} }
function ImportRecordComponent_mat_option_73_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-option", 31);
    i0.ɵɵtext(1);
    i0.ɵɵelementEnd();
} if (rf & 2) {
    var c_r50 = ctx.$implicit;
    i0.ɵɵproperty("value", c_r50.text);
    i0.ɵɵadvance(1);
    i0.ɵɵtextInterpolate(c_r50.text);
} }
function ImportRecordComponent_button_81_Template(rf, ctx) { if (rf & 1) {
    var _r52 = i0.ɵɵgetCurrentView();
    i0.ɵɵelementStart(0, "button", 10);
    i0.ɵɵlistener("click", function ImportRecordComponent_button_81_Template_button_click_0_listener() { i0.ɵɵrestoreView(_r52); var ctx_r51 = i0.ɵɵnextContext(); return ctx_r51.downloadCSV(1); });
    i0.ɵɵelementStart(1, "mat-icon");
    i0.ɵɵtext(2, "arrow_circle_down");
    i0.ɵɵelementEnd();
    i0.ɵɵtext(3, "Download csv");
    i0.ɵɵelementEnd();
} }
function ImportRecordComponent_span_82_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "span", 32);
    i0.ɵɵtext(1, "generating and downloading your csv..");
    i0.ɵɵelementEnd();
} }
function ImportRecordComponent_button_84_Template(rf, ctx) { if (rf & 1) {
    var _r54 = i0.ɵɵgetCurrentView();
    i0.ɵɵelementStart(0, "button", 10);
    i0.ɵɵlistener("click", function ImportRecordComponent_button_84_Template_button_click_0_listener() { i0.ɵɵrestoreView(_r54); var ctx_r53 = i0.ɵɵnextContext(); return ctx_r53.unqueue(); });
    i0.ɵɵelementStart(1, "mat-icon");
    i0.ɵɵtext(2, "warning");
    i0.ɵɵelementEnd();
    i0.ɵɵtext(3, "Unqueue selected");
    i0.ɵɵelementEnd();
} }
function ImportRecordComponent_span_85_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "span", 32);
    i0.ɵɵtext(1, "unqueuing..");
    i0.ɵɵelementEnd();
} }
function ImportRecordComponent_button_87_Template(rf, ctx) { if (rf & 1) {
    var _r56 = i0.ɵɵgetCurrentView();
    i0.ɵɵelementStart(0, "button", 10);
    i0.ɵɵlistener("click", function ImportRecordComponent_button_87_Template_button_click_0_listener() { i0.ɵɵrestoreView(_r56); var ctx_r55 = i0.ɵɵnextContext(); return ctx_r55.rerunImport(); });
    i0.ɵɵelementStart(1, "mat-icon");
    i0.ɵɵtext(2, "warning");
    i0.ɵɵelementEnd();
    i0.ɵɵtext(3, "Rerun import selected");
    i0.ɵɵelementEnd();
} }
function ImportRecordComponent_span_88_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "span", 32);
    i0.ɵɵtext(1, "reruning..");
    i0.ɵɵelementEnd();
} }
function ImportRecordComponent_ng_template_91_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-checkbox", 33);
    i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_ng_template_91_Template_mat_checkbox_ngModelChange_0_listener($event) { var row_r57 = ctx.row; return row_r57.checked = $event; });
    i0.ɵɵelementEnd();
    i0.ɵɵtext(1);
    i0.ɵɵpipe(2, "date");
} if (rf & 2) {
    var row_r57 = ctx.row;
    i0.ɵɵproperty("ngModel", row_r57.checked);
    i0.ɵɵadvance(1);
    i0.ɵɵtextInterpolate1(" ", i0.ɵɵpipeBind2(2, 2, row_r57.TimeIn, "MMM dd yyyy, HH:mm"), " ");
} }
function ImportRecordComponent_ng_template_93_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
} if (rf & 2) {
    var row_r59 = ctx.row;
    i0.ɵɵtextInterpolate1(" ", row_r59.ClinicId, " ");
} }
function ImportRecordComponent_ng_template_95_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
} if (rf & 2) {
    var row_r60 = ctx.row;
    i0.ɵɵtextInterpolate1(" ", row_r60.FinClass, " ");
} }
function ImportRecordComponent_ng_template_97_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
} if (rf & 2) {
    var row_r61 = ctx.row;
    i0.ɵɵtextInterpolate1(" ", row_r61.OfficeKey, " ");
} }
function ImportRecordComponent_ng_template_99_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
} if (rf & 2) {
    var row_r62 = ctx.row;
    i0.ɵɵtextInterpolate2(" ", row_r62.PatientInformation.FirstName, " ", row_r62.PatientInformation.LastName, " ");
} }
function ImportRecordComponent_ng_template_101_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
} if (rf & 2) {
    var row_r63 = ctx.row;
    i0.ɵɵtextInterpolate1(" ", row_r63.ImportStatusDate, " ");
} }
function ImportRecordComponent_ng_template_103_mat_icon_0_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-icon", 37);
    i0.ɵɵtext(1, "warning");
    i0.ɵɵelementEnd();
} if (rf & 2) {
    var row_r64 = i0.ɵɵnextContext().row;
    i0.ɵɵpropertyInterpolate("matTooltip", row_r64.VisitImportLogs[0] == null ? null : row_r64.VisitImportLogs[0].ResponseRaw);
} }
function ImportRecordComponent_ng_template_103_mat_icon_1_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-icon");
    i0.ɵɵtext(1, "check");
    i0.ɵɵelementEnd();
} }
function ImportRecordComponent_ng_template_103_mat_icon_6_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-icon", 37);
    i0.ɵɵtext(1, "warning");
    i0.ɵɵelementEnd();
} if (rf & 2) {
    var row_r64 = i0.ɵɵnextContext().row;
    i0.ɵɵpropertyInterpolate("matTooltip", row_r64.PatientImportLogs[0] == null ? null : row_r64.PatientImportLogs[0].ResponseRaw);
} }
function ImportRecordComponent_ng_template_103_mat_icon_7_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-icon");
    i0.ɵɵtext(1, "check");
    i0.ɵɵelementEnd();
} }
function ImportRecordComponent_ng_template_103_Template(rf, ctx) { if (rf & 1) {
    var _r72 = i0.ɵɵgetCurrentView();
    i0.ɵɵtemplate(0, ImportRecordComponent_ng_template_103_mat_icon_0_Template, 2, 1, "mat-icon", 34);
    i0.ɵɵtemplate(1, ImportRecordComponent_ng_template_103_mat_icon_1_Template, 2, 0, "mat-icon", 35);
    i0.ɵɵelementStart(2, "a", 36);
    i0.ɵɵlistener("click", function ImportRecordComponent_ng_template_103_Template_a_click_2_listener() { i0.ɵɵrestoreView(_r72); var row_r64 = ctx.row; var ctx_r71 = i0.ɵɵnextContext(); return ctx_r71.viewLogs(row_r64.VisitImportLogs); });
    i0.ɵɵtext(3, "View detail");
    i0.ɵɵelementEnd();
    i0.ɵɵtext(4, " Visit-Import ");
    i0.ɵɵelement(5, "br");
    i0.ɵɵtemplate(6, ImportRecordComponent_ng_template_103_mat_icon_6_Template, 2, 1, "mat-icon", 34);
    i0.ɵɵtemplate(7, ImportRecordComponent_ng_template_103_mat_icon_7_Template, 2, 0, "mat-icon", 35);
    i0.ɵɵelementStart(8, "a", 36);
    i0.ɵɵlistener("click", function ImportRecordComponent_ng_template_103_Template_a_click_8_listener() { i0.ɵɵrestoreView(_r72); var row_r64 = ctx.row; var ctx_r73 = i0.ɵɵnextContext(); return ctx_r73.viewLogs(row_r64.PatientImportLogs); });
    i0.ɵɵtext(9, "View detail");
    i0.ɵɵelementEnd();
    i0.ɵɵtext(10, " Patient-Import ");
    i0.ɵɵelement(11, "br");
} if (rf & 2) {
    var row_r64 = ctx.row;
    i0.ɵɵproperty("ngIf", !(row_r64.VisitImportLogs[0] == null ? null : row_r64.VisitImportLogs[0].IsSuccess));
    i0.ɵɵadvance(1);
    i0.ɵɵproperty("ngIf", row_r64.VisitImportLogs[0] == null ? null : row_r64.VisitImportLogs[0].IsSuccess);
    i0.ɵɵadvance(5);
    i0.ɵɵproperty("ngIf", !(row_r64.PatientImportLogs[0] == null ? null : row_r64.PatientImportLogs[0].IsSuccess));
    i0.ɵɵadvance(1);
    i0.ɵɵproperty("ngIf", row_r64.PatientImportLogs[0] == null ? null : row_r64.PatientImportLogs[0].IsSuccess);
} }
function ImportRecordComponent_mat_option_124_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-option", 31);
    i0.ɵɵtext(1);
    i0.ɵɵelementEnd();
} if (rf & 2) {
    var c_r74 = ctx.$implicit;
    i0.ɵɵproperty("value", c_r74.id);
    i0.ɵɵadvance(1);
    i0.ɵɵtextInterpolate(c_r74.text);
} }
function ImportRecordComponent_mat_option_129_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-option", 31);
    i0.ɵɵtext(1);
    i0.ɵɵelementEnd();
} if (rf & 2) {
    var c_r75 = ctx.$implicit;
    i0.ɵɵproperty("value", c_r75.text);
    i0.ɵɵadvance(1);
    i0.ɵɵtextInterpolate(c_r75.text);
} }
function ImportRecordComponent_button_137_Template(rf, ctx) { if (rf & 1) {
    var _r77 = i0.ɵɵgetCurrentView();
    i0.ɵɵelementStart(0, "button", 10);
    i0.ɵɵlistener("click", function ImportRecordComponent_button_137_Template_button_click_0_listener() { i0.ɵɵrestoreView(_r77); var ctx_r76 = i0.ɵɵnextContext(); return ctx_r76.downloadCSV(2); });
    i0.ɵɵelementStart(1, "mat-icon");
    i0.ɵɵtext(2, "arrow_circle_down");
    i0.ɵɵelementEnd();
    i0.ɵɵtext(3, "Download csv");
    i0.ɵɵelementEnd();
} }
function ImportRecordComponent_span_138_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "span", 32);
    i0.ɵɵtext(1, "generating and downloading your csv..");
    i0.ɵɵelementEnd();
} }
function ImportRecordComponent_ng_template_141_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
    i0.ɵɵpipe(1, "date");
} if (rf & 2) {
    var row_r78 = ctx.row;
    i0.ɵɵtextInterpolate1(" ", i0.ɵɵpipeBind2(1, 1, row_r78.TimeIn, "MMM dd yyyy, HH:mm"), " ");
} }
function ImportRecordComponent_ng_template_143_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
} if (rf & 2) {
    var row_r79 = ctx.row;
    i0.ɵɵtextInterpolate1(" ", row_r79.ClinicId, " ");
} }
function ImportRecordComponent_ng_template_145_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
} if (rf & 2) {
    var row_r80 = ctx.row;
    i0.ɵɵtextInterpolate1(" ", row_r80.FinClass, " ");
} }
function ImportRecordComponent_ng_template_147_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
} if (rf & 2) {
    var row_r81 = ctx.row;
    i0.ɵɵtextInterpolate1(" ", row_r81.OfficeKey, " ");
} }
function ImportRecordComponent_ng_template_149_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵtext(0);
} if (rf & 2) {
    var row_r82 = ctx.row;
    i0.ɵɵtextInterpolate2(" ", row_r82.PatientInformation.FirstName, " ", row_r82.PatientInformation.LastName, " ");
} }
function ImportRecordComponent_ng_template_151_mat_icon_2_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-icon", 37);
    i0.ɵɵtext(1, "warning");
    i0.ɵɵelementEnd();
} if (rf & 2) {
    var row_r83 = i0.ɵɵnextContext().row;
    i0.ɵɵpropertyInterpolate("matTooltip", row_r83.VisitImportLogs[0] == null ? null : row_r83.VisitImportLogs[0].ResponseRaw);
} }
function ImportRecordComponent_ng_template_151_mat_icon_3_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-icon");
    i0.ɵɵtext(1, "check");
    i0.ɵɵelementEnd();
} }
function ImportRecordComponent_ng_template_151_mat_icon_8_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-icon", 37);
    i0.ɵɵtext(1, "warning");
    i0.ɵɵelementEnd();
} if (rf & 2) {
    var row_r83 = i0.ɵɵnextContext().row;
    i0.ɵɵpropertyInterpolate("matTooltip", row_r83.PatientImportLogs[0] == null ? null : row_r83.PatientImportLogs[0].ResponseRaw);
} }
function ImportRecordComponent_ng_template_151_mat_icon_9_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "mat-icon");
    i0.ɵɵtext(1, "check");
    i0.ɵɵelementEnd();
} }
function ImportRecordComponent_ng_template_151_Template(rf, ctx) { if (rf & 1) {
    var _r91 = i0.ɵɵgetCurrentView();
    i0.ɵɵtext(0);
    i0.ɵɵelement(1, "br");
    i0.ɵɵtemplate(2, ImportRecordComponent_ng_template_151_mat_icon_2_Template, 2, 1, "mat-icon", 34);
    i0.ɵɵtemplate(3, ImportRecordComponent_ng_template_151_mat_icon_3_Template, 2, 0, "mat-icon", 35);
    i0.ɵɵelementStart(4, "a", 36);
    i0.ɵɵlistener("click", function ImportRecordComponent_ng_template_151_Template_a_click_4_listener() { i0.ɵɵrestoreView(_r91); var row_r83 = ctx.row; var ctx_r90 = i0.ɵɵnextContext(); return ctx_r90.viewLogs(row_r83.VisitImportLogs); });
    i0.ɵɵtext(5, "View detail");
    i0.ɵɵelementEnd();
    i0.ɵɵtext(6, " Visit-Import ");
    i0.ɵɵelement(7, "br");
    i0.ɵɵtemplate(8, ImportRecordComponent_ng_template_151_mat_icon_8_Template, 2, 1, "mat-icon", 34);
    i0.ɵɵtemplate(9, ImportRecordComponent_ng_template_151_mat_icon_9_Template, 2, 0, "mat-icon", 35);
    i0.ɵɵelementStart(10, "a", 36);
    i0.ɵɵlistener("click", function ImportRecordComponent_ng_template_151_Template_a_click_10_listener() { i0.ɵɵrestoreView(_r91); var row_r83 = ctx.row; var ctx_r92 = i0.ɵɵnextContext(); return ctx_r92.viewLogs(row_r83.PatientImportLogs); });
    i0.ɵɵtext(11, "View detail");
    i0.ɵɵelementEnd();
    i0.ɵɵtext(12, " Patient-Import ");
    i0.ɵɵelement(13, "br");
} if (rf & 2) {
    var row_r83 = ctx.row;
    i0.ɵɵtextInterpolate1(" ", row_r83.ImportStatusDate, " ");
    i0.ɵɵadvance(2);
    i0.ɵɵproperty("ngIf", !(row_r83.VisitImportLogs[0] == null ? null : row_r83.VisitImportLogs[0].IsSuccess));
    i0.ɵɵadvance(1);
    i0.ɵɵproperty("ngIf", row_r83.VisitImportLogs[0] == null ? null : row_r83.VisitImportLogs[0].IsSuccess);
    i0.ɵɵadvance(5);
    i0.ɵɵproperty("ngIf", !(row_r83.PatientImportLogs[0] == null ? null : row_r83.PatientImportLogs[0].IsSuccess));
    i0.ɵɵadvance(1);
    i0.ɵɵproperty("ngIf", row_r83.PatientImportLogs[0] == null ? null : row_r83.PatientImportLogs[0].IsSuccess);
} }
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
    ImportRecordComponent.ɵfac = function ImportRecordComponent_Factory(t) { return new (t || ImportRecordComponent)(i0.ɵɵdirectiveInject(i1.ApiService), i0.ɵɵdirectiveInject(i2.MatDialog)); };
    ImportRecordComponent.ɵcmp = i0.ɵɵdefineComponent({ type: ImportRecordComponent, selectors: [["app-import-record"]], decls: 152, vars: 85, consts: [["animationDuration", "0ms", 3, "selectedTabChange"], ["label", "Processed In"], [2, "background", "white", "margin", "10px"], ["matInput", "", 3, "matDatepicker", "ngModel", "ngModelChange", "dateChange"], ["matSuffix", "", 3, "for"], ["picker", ""], ["pickerEnd", ""], ["multiple", "", 3, "ngModel", "ngModelChange", "selectionChange"], [3, "value", 4, "ngFor", "ngForOf"], ["matInput", "", 3, "ngModel", "ngModelChange"], ["mat-raised-button", "", 3, "click"], [2, "padding", "5px"], ["mat-raised-button", "", 3, "click", 4, "ngIf"], ["class", "warning text-warning", 4, "ngIf"], ["rowHeight", "auto", 1, "material", 3, "rows", "headerHeight", "footerHeight", "externalPaging", "count", "offset", "limit", "loadingIndicator", "page"], ["name", "TimeIn", 3, "width"], ["ngx-datatable-cell-template", ""], ["name", "ClinicId", 3, "width"], ["name", "FinClass", 3, "width"], ["name", "OfficeKey", 3, "width"], ["name", "Pat-Name", 3, "width"], ["label", "Queued"], ["picker1", ""], ["pickerEnd1", ""], ["mat-button", "", 3, "click"], ["name", "Queued Date", 3, "width"], ["name", "Import Status", 3, "width"], ["label", "Processed Out"], ["picker2", ""], ["pickerEnd2", ""], ["name", "Imported Date", 3, "width"], [3, "value"], [1, "warning", "text-warning"], [3, "ngModel", "ngModelChange"], [3, "matTooltip", 4, "ngIf"], [4, "ngIf"], ["href", "javascript:void()", 3, "click"], [3, "matTooltip"]], template: function ImportRecordComponent_Template(rf, ctx) { if (rf & 1) {
            i0.ɵɵelementStart(0, "mat-tab-group", 0);
            i0.ɵɵlistener("selectedTabChange", function ImportRecordComponent_Template_mat_tab_group_selectedTabChange_0_listener($event) { return ctx.tabChanged($event); });
            i0.ɵɵelementStart(1, "mat-tab", 1);
            i0.ɵɵelementStart(2, "div", 2);
            i0.ɵɵelementStart(3, "mat-form-field");
            i0.ɵɵelementStart(4, "mat-label");
            i0.ɵɵtext(5, "Visit Start Date");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(6, "input", 3);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_input_ngModelChange_6_listener($event) { return ctx.filter.visitStartDate = $event; })("dateChange", function ImportRecordComponent_Template_input_dateChange_6_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵelementEnd();
            i0.ɵɵelement(7, "mat-datepicker-toggle", 4);
            i0.ɵɵelement(8, "mat-datepicker", null, 5);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(10, "mat-form-field");
            i0.ɵɵelementStart(11, "mat-label");
            i0.ɵɵtext(12, "Visit End Date");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(13, "input", 3);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_input_ngModelChange_13_listener($event) { return ctx.filter.visitEndDate = $event; })("dateChange", function ImportRecordComponent_Template_input_dateChange_13_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵelementEnd();
            i0.ɵɵelement(14, "mat-datepicker-toggle", 4);
            i0.ɵɵelement(15, "mat-datepicker", null, 6);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(17, "mat-form-field");
            i0.ɵɵelementStart(18, "mat-label");
            i0.ɵɵtext(19, "Clinics");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(20, "mat-select", 7);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_mat_select_ngModelChange_20_listener($event) { return ctx.filter.clinicIds = $event; })("selectionChange", function ImportRecordComponent_Template_mat_select_selectionChange_20_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵtemplate(21, ImportRecordComponent_mat_option_21_Template, 2, 2, "mat-option", 8);
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(22, "mat-form-field");
            i0.ɵɵelementStart(23, "mat-label");
            i0.ɵɵtext(24, "FinClasses");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(25, "mat-select", 7);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_mat_select_ngModelChange_25_listener($event) { return ctx.filter.finClassIds = $event; })("selectionChange", function ImportRecordComponent_Template_mat_select_selectionChange_25_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵtemplate(26, ImportRecordComponent_mat_option_26_Template, 2, 2, "mat-option", 8);
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(27, "mat-form-field");
            i0.ɵɵelementStart(28, "mat-label");
            i0.ɵɵtext(29, "First,Last Name, Address1 or City");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(30, "input", 9);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_input_ngModelChange_30_listener($event) { return ctx.filter.patSearch = $event; });
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(31, "button", 10);
            i0.ɵɵlistener("click", function ImportRecordComponent_Template_button_click_31_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵtext(32, "Search");
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(33, "div", 11);
            i0.ɵɵtemplate(34, ImportRecordComponent_button_34_Template, 4, 0, "button", 12);
            i0.ɵɵtemplate(35, ImportRecordComponent_span_35_Template, 2, 0, "span", 13);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(36, "div");
            i0.ɵɵelementStart(37, "ngx-datatable", 14);
            i0.ɵɵlistener("page", function ImportRecordComponent_Template_ngx_datatable_page_37_listener($event) { return ctx.setPage($event); });
            i0.ɵɵelementStart(38, "ngx-datatable-column", 15);
            i0.ɵɵtemplate(39, ImportRecordComponent_ng_template_39_Template, 2, 4, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(40, "ngx-datatable-column", 17);
            i0.ɵɵtemplate(41, ImportRecordComponent_ng_template_41_Template, 1, 1, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(42, "ngx-datatable-column", 18);
            i0.ɵɵtemplate(43, ImportRecordComponent_ng_template_43_Template, 1, 1, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(44, "ngx-datatable-column", 19);
            i0.ɵɵtemplate(45, ImportRecordComponent_ng_template_45_Template, 1, 1, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(46, "ngx-datatable-column", 20);
            i0.ɵɵtemplate(47, ImportRecordComponent_ng_template_47_Template, 4, 4, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(48, "mat-tab", 21);
            i0.ɵɵelementStart(49, "div", 2);
            i0.ɵɵelementStart(50, "mat-form-field");
            i0.ɵɵelementStart(51, "mat-label");
            i0.ɵɵtext(52, "Visit Start Date");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(53, "input", 3);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_input_ngModelChange_53_listener($event) { return ctx.filter.visitStartDate = $event; })("dateChange", function ImportRecordComponent_Template_input_dateChange_53_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵelementEnd();
            i0.ɵɵelement(54, "mat-datepicker-toggle", 4);
            i0.ɵɵelement(55, "mat-datepicker", null, 22);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(57, "mat-form-field");
            i0.ɵɵelementStart(58, "mat-label");
            i0.ɵɵtext(59, "Visit End Date");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(60, "input", 3);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_input_ngModelChange_60_listener($event) { return ctx.filter.visitEndDate = $event; })("dateChange", function ImportRecordComponent_Template_input_dateChange_60_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵelementEnd();
            i0.ɵɵelement(61, "mat-datepicker-toggle", 4);
            i0.ɵɵelement(62, "mat-datepicker", null, 23);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(64, "mat-form-field");
            i0.ɵɵelementStart(65, "mat-label");
            i0.ɵɵtext(66, "Clinics");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(67, "mat-select", 7);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_mat_select_ngModelChange_67_listener($event) { return ctx.filter.clinicIds = $event; })("selectionChange", function ImportRecordComponent_Template_mat_select_selectionChange_67_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵtemplate(68, ImportRecordComponent_mat_option_68_Template, 2, 2, "mat-option", 8);
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(69, "mat-form-field");
            i0.ɵɵelementStart(70, "mat-label");
            i0.ɵɵtext(71, "FinClasses");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(72, "mat-select", 7);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_mat_select_ngModelChange_72_listener($event) { return ctx.filter.finClassIds = $event; })("selectionChange", function ImportRecordComponent_Template_mat_select_selectionChange_72_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵtemplate(73, ImportRecordComponent_mat_option_73_Template, 2, 2, "mat-option", 8);
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(74, "mat-form-field");
            i0.ɵɵelementStart(75, "mat-label");
            i0.ɵɵtext(76, "First,Last Name, Address1 or City");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(77, "input", 9);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_input_ngModelChange_77_listener($event) { return ctx.filter.patSearch = $event; });
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(78, "button", 24);
            i0.ɵɵlistener("click", function ImportRecordComponent_Template_button_click_78_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵtext(79, "Search");
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(80, "div", 11);
            i0.ɵɵtemplate(81, ImportRecordComponent_button_81_Template, 4, 0, "button", 12);
            i0.ɵɵtemplate(82, ImportRecordComponent_span_82_Template, 2, 0, "span", 13);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(83, "div");
            i0.ɵɵtemplate(84, ImportRecordComponent_button_84_Template, 4, 0, "button", 12);
            i0.ɵɵtemplate(85, ImportRecordComponent_span_85_Template, 2, 0, "span", 13);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(86, "div");
            i0.ɵɵtemplate(87, ImportRecordComponent_button_87_Template, 4, 0, "button", 12);
            i0.ɵɵtemplate(88, ImportRecordComponent_span_88_Template, 2, 0, "span", 13);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(89, "ngx-datatable", 14);
            i0.ɵɵlistener("page", function ImportRecordComponent_Template_ngx_datatable_page_89_listener($event) { return ctx.setPage($event); });
            i0.ɵɵelementStart(90, "ngx-datatable-column", 15);
            i0.ɵɵtemplate(91, ImportRecordComponent_ng_template_91_Template, 3, 5, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(92, "ngx-datatable-column", 17);
            i0.ɵɵtemplate(93, ImportRecordComponent_ng_template_93_Template, 1, 1, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(94, "ngx-datatable-column", 18);
            i0.ɵɵtemplate(95, ImportRecordComponent_ng_template_95_Template, 1, 1, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(96, "ngx-datatable-column", 19);
            i0.ɵɵtemplate(97, ImportRecordComponent_ng_template_97_Template, 1, 1, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(98, "ngx-datatable-column", 20);
            i0.ɵɵtemplate(99, ImportRecordComponent_ng_template_99_Template, 1, 2, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(100, "ngx-datatable-column", 25);
            i0.ɵɵtemplate(101, ImportRecordComponent_ng_template_101_Template, 1, 1, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(102, "ngx-datatable-column", 26);
            i0.ɵɵtemplate(103, ImportRecordComponent_ng_template_103_Template, 12, 4, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(104, "mat-tab", 27);
            i0.ɵɵelementStart(105, "div", 2);
            i0.ɵɵelementStart(106, "mat-form-field");
            i0.ɵɵelementStart(107, "mat-label");
            i0.ɵɵtext(108, "Visit Start Date");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(109, "input", 3);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_input_ngModelChange_109_listener($event) { return ctx.filter.visitStartDate = $event; })("dateChange", function ImportRecordComponent_Template_input_dateChange_109_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵelementEnd();
            i0.ɵɵelement(110, "mat-datepicker-toggle", 4);
            i0.ɵɵelement(111, "mat-datepicker", null, 28);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(113, "mat-form-field");
            i0.ɵɵelementStart(114, "mat-label");
            i0.ɵɵtext(115, "Visit End Date");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(116, "input", 3);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_input_ngModelChange_116_listener($event) { return ctx.filter.visitEndDate = $event; })("dateChange", function ImportRecordComponent_Template_input_dateChange_116_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵelementEnd();
            i0.ɵɵelement(117, "mat-datepicker-toggle", 4);
            i0.ɵɵelement(118, "mat-datepicker", null, 29);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(120, "mat-form-field");
            i0.ɵɵelementStart(121, "mat-label");
            i0.ɵɵtext(122, "Clinics");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(123, "mat-select", 7);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_mat_select_ngModelChange_123_listener($event) { return ctx.filter.clinicIds = $event; })("selectionChange", function ImportRecordComponent_Template_mat_select_selectionChange_123_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵtemplate(124, ImportRecordComponent_mat_option_124_Template, 2, 2, "mat-option", 8);
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(125, "mat-form-field");
            i0.ɵɵelementStart(126, "mat-label");
            i0.ɵɵtext(127, "FinClasses");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(128, "mat-select", 7);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_mat_select_ngModelChange_128_listener($event) { return ctx.filter.finClassIds = $event; })("selectionChange", function ImportRecordComponent_Template_mat_select_selectionChange_128_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵtemplate(129, ImportRecordComponent_mat_option_129_Template, 2, 2, "mat-option", 8);
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(130, "mat-form-field");
            i0.ɵɵelementStart(131, "mat-label");
            i0.ɵɵtext(132, "First,Last Name, Address1 or City");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(133, "input", 9);
            i0.ɵɵlistener("ngModelChange", function ImportRecordComponent_Template_input_ngModelChange_133_listener($event) { return ctx.filter.patSearch = $event; });
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(134, "button", 24);
            i0.ɵɵlistener("click", function ImportRecordComponent_Template_button_click_134_listener() { return ctx.setPage({ offset: 0 }); });
            i0.ɵɵtext(135, "Search");
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(136, "div", 11);
            i0.ɵɵtemplate(137, ImportRecordComponent_button_137_Template, 4, 0, "button", 12);
            i0.ɵɵtemplate(138, ImportRecordComponent_span_138_Template, 2, 0, "span", 13);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(139, "ngx-datatable", 14);
            i0.ɵɵlistener("page", function ImportRecordComponent_Template_ngx_datatable_page_139_listener($event) { return ctx.setPage($event); });
            i0.ɵɵelementStart(140, "ngx-datatable-column", 15);
            i0.ɵɵtemplate(141, ImportRecordComponent_ng_template_141_Template, 2, 4, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(142, "ngx-datatable-column", 17);
            i0.ɵɵtemplate(143, ImportRecordComponent_ng_template_143_Template, 1, 1, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(144, "ngx-datatable-column", 18);
            i0.ɵɵtemplate(145, ImportRecordComponent_ng_template_145_Template, 1, 1, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(146, "ngx-datatable-column", 19);
            i0.ɵɵtemplate(147, ImportRecordComponent_ng_template_147_Template, 1, 1, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(148, "ngx-datatable-column", 20);
            i0.ɵɵtemplate(149, ImportRecordComponent_ng_template_149_Template, 1, 2, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(150, "ngx-datatable-column", 30);
            i0.ɵɵtemplate(151, ImportRecordComponent_ng_template_151_Template, 14, 5, "ng-template", 16);
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
        } if (rf & 2) {
            var _r0 = i0.ɵɵreference(9);
            var _r1 = i0.ɵɵreference(16);
            var _r11 = i0.ɵɵreference(56);
            var _r12 = i0.ɵɵreference(63);
            var _r28 = i0.ɵɵreference(112);
            var _r29 = i0.ɵɵreference(119);
            i0.ɵɵadvance(6);
            i0.ɵɵproperty("matDatepicker", _r0)("ngModel", ctx.filter.visitStartDate);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("for", _r0);
            i0.ɵɵadvance(6);
            i0.ɵɵproperty("matDatepicker", _r1)("ngModel", ctx.filter.visitEndDate);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("for", _r1);
            i0.ɵɵadvance(6);
            i0.ɵɵproperty("ngModel", ctx.filter.clinicIds);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("ngForOf", ctx.clinics);
            i0.ɵɵadvance(4);
            i0.ɵɵproperty("ngModel", ctx.filter.finClassIds);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("ngForOf", ctx.finClasses);
            i0.ɵɵadvance(4);
            i0.ɵɵproperty("ngModel", ctx.filter.patSearch);
            i0.ɵɵadvance(4);
            i0.ɵɵproperty("ngIf", !ctx.generatingAndDownLoadCSV);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("ngIf", ctx.generatingAndDownLoadCSV);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("rows", ctx.rows)("headerHeight", 50)("footerHeight", 50)("externalPaging", true)("count", ctx.page.RowCount)("offset", ctx.page.CurrentPage)("limit", ctx.page.PageSize)("loadingIndicator", ctx.loading);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("width", 300);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 300);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 300);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 300);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 300);
            i0.ɵɵadvance(7);
            i0.ɵɵproperty("matDatepicker", _r11)("ngModel", ctx.filter.visitStartDate);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("for", _r11);
            i0.ɵɵadvance(6);
            i0.ɵɵproperty("matDatepicker", _r12)("ngModel", ctx.filter.visitEndDate);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("for", _r12);
            i0.ɵɵadvance(6);
            i0.ɵɵproperty("ngModel", ctx.filter.clinicIds);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("ngForOf", ctx.clinics);
            i0.ɵɵadvance(4);
            i0.ɵɵproperty("ngModel", ctx.filter.finClassIds);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("ngForOf", ctx.finClasses);
            i0.ɵɵadvance(4);
            i0.ɵɵproperty("ngModel", ctx.filter.patSearch);
            i0.ɵɵadvance(4);
            i0.ɵɵproperty("ngIf", !ctx.generatingAndDownLoadCSV);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("ngIf", ctx.generatingAndDownLoadCSV);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("ngIf", !ctx.unqueuing && ctx.getSelected().length > 0);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("ngIf", ctx.unqueuing);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("ngIf", !ctx.reruning && ctx.getSelected().length > 0);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("ngIf", ctx.reruning);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("rows", ctx.rowsQueueOrDone)("headerHeight", 50)("footerHeight", 50)("externalPaging", true)("count", ctx.page.RowCount)("offset", ctx.page.CurrentPage)("limit", ctx.page.PageSize)("loadingIndicator", ctx.loading);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("width", 300);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 150);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 50);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 150);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 200);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 200);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 300);
            i0.ɵɵadvance(7);
            i0.ɵɵproperty("matDatepicker", _r28)("ngModel", ctx.filter.visitStartDate);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("for", _r28);
            i0.ɵɵadvance(6);
            i0.ɵɵproperty("matDatepicker", _r29)("ngModel", ctx.filter.visitEndDate);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("for", _r29);
            i0.ɵɵadvance(6);
            i0.ɵɵproperty("ngModel", ctx.filter.clinicIds);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("ngForOf", ctx.clinics);
            i0.ɵɵadvance(4);
            i0.ɵɵproperty("ngModel", ctx.filter.finClassIds);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("ngForOf", ctx.finClasses);
            i0.ɵɵadvance(4);
            i0.ɵɵproperty("ngModel", ctx.filter.patSearch);
            i0.ɵɵadvance(4);
            i0.ɵɵproperty("ngIf", !ctx.generatingAndDownLoadCSV);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("ngIf", ctx.generatingAndDownLoadCSV);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("rows", ctx.rowsQueueOrDone)("headerHeight", 50)("footerHeight", 50)("externalPaging", true)("count", ctx.page.RowCount)("offset", ctx.page.CurrentPage)("limit", ctx.page.PageSize)("loadingIndicator", ctx.loading);
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("width", 300);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 300);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 300);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 300);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 300);
            i0.ɵɵadvance(2);
            i0.ɵɵproperty("width", 300);
        } }, directives: [i3.MatTabGroup, i3.MatTab, i4.MatFormField, i4.MatLabel, i5.MatInput, i6.MatDatepickerInput, i7.DefaultValueAccessor, i7.NgControlStatus, i7.NgModel, i6.MatDatepickerToggle, i4.MatSuffix, i6.MatDatepicker, i8.MatSelect, i9.NgForOf, i10.MatButton, i9.NgIf, i11.DatatableComponent, i11.DataTableColumnDirective, i11.DataTableColumnCellDirective, i12.MatOption, i13.MatIcon, i14.MatCheckbox, i15.MatTooltip], pipes: [i9.DatePipe], styles: ["mat-form-field[_ngcontent-%COMP%] {\n  padding: 10px;\n}\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIi4uXFwuLlxcLi5cXGltcG9ydC1yZWNvcmQuY29tcG9uZW50LnNjc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7RUFFSSxhQUFBO0FBQUoiLCJmaWxlIjoiaW1wb3J0LXJlY29yZC5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIm1hdC1mb3JtLWZpZWxke1xyXG5cclxuICAgIHBhZGRpbmc6MTBweFxyXG59Il19 */"] });
    return ImportRecordComponent;
}());
exports.ImportRecordComponent = ImportRecordComponent;


/***/ }),

/***/ "RnhZ":
/*!**************************************************!*\
  !*** ./node_modules/moment/locale sync ^\.\/.*$ ***!
  \**************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var map = {
	"./af": "K/tc",
	"./af.js": "K/tc",
	"./ar": "jnO4",
	"./ar-dz": "o1bE",
	"./ar-dz.js": "o1bE",
	"./ar-kw": "Qj4J",
	"./ar-kw.js": "Qj4J",
	"./ar-ly": "HP3h",
	"./ar-ly.js": "HP3h",
	"./ar-ma": "CoRJ",
	"./ar-ma.js": "CoRJ",
	"./ar-sa": "gjCT",
	"./ar-sa.js": "gjCT",
	"./ar-tn": "bYM6",
	"./ar-tn.js": "bYM6",
	"./ar.js": "jnO4",
	"./az": "SFxW",
	"./az.js": "SFxW",
	"./be": "H8ED",
	"./be.js": "H8ED",
	"./bg": "hKrs",
	"./bg.js": "hKrs",
	"./bm": "p/rL",
	"./bm.js": "p/rL",
	"./bn": "kEOa",
	"./bn-bd": "loYQ",
	"./bn-bd.js": "loYQ",
	"./bn.js": "kEOa",
	"./bo": "0mo+",
	"./bo.js": "0mo+",
	"./br": "aIdf",
	"./br.js": "aIdf",
	"./bs": "JVSJ",
	"./bs.js": "JVSJ",
	"./ca": "1xZ4",
	"./ca.js": "1xZ4",
	"./cs": "PA2r",
	"./cs.js": "PA2r",
	"./cv": "A+xa",
	"./cv.js": "A+xa",
	"./cy": "l5ep",
	"./cy.js": "l5ep",
	"./da": "DxQv",
	"./da.js": "DxQv",
	"./de": "tGlX",
	"./de-at": "s+uk",
	"./de-at.js": "s+uk",
	"./de-ch": "u3GI",
	"./de-ch.js": "u3GI",
	"./de.js": "tGlX",
	"./dv": "WYrj",
	"./dv.js": "WYrj",
	"./el": "jUeY",
	"./el.js": "jUeY",
	"./en-au": "Dmvi",
	"./en-au.js": "Dmvi",
	"./en-ca": "OIYi",
	"./en-ca.js": "OIYi",
	"./en-gb": "Oaa7",
	"./en-gb.js": "Oaa7",
	"./en-ie": "4dOw",
	"./en-ie.js": "4dOw",
	"./en-il": "czMo",
	"./en-il.js": "czMo",
	"./en-in": "7C5Q",
	"./en-in.js": "7C5Q",
	"./en-nz": "b1Dy",
	"./en-nz.js": "b1Dy",
	"./en-sg": "t+mt",
	"./en-sg.js": "t+mt",
	"./eo": "Zduo",
	"./eo.js": "Zduo",
	"./es": "iYuL",
	"./es-do": "CjzT",
	"./es-do.js": "CjzT",
	"./es-mx": "tbfe",
	"./es-mx.js": "tbfe",
	"./es-us": "Vclq",
	"./es-us.js": "Vclq",
	"./es.js": "iYuL",
	"./et": "7BjC",
	"./et.js": "7BjC",
	"./eu": "D/JM",
	"./eu.js": "D/JM",
	"./fa": "jfSC",
	"./fa.js": "jfSC",
	"./fi": "gekB",
	"./fi.js": "gekB",
	"./fil": "1ppg",
	"./fil.js": "1ppg",
	"./fo": "ByF4",
	"./fo.js": "ByF4",
	"./fr": "nyYc",
	"./fr-ca": "2fjn",
	"./fr-ca.js": "2fjn",
	"./fr-ch": "Dkky",
	"./fr-ch.js": "Dkky",
	"./fr.js": "nyYc",
	"./fy": "cRix",
	"./fy.js": "cRix",
	"./ga": "USCx",
	"./ga.js": "USCx",
	"./gd": "9rRi",
	"./gd.js": "9rRi",
	"./gl": "iEDd",
	"./gl.js": "iEDd",
	"./gom-deva": "qvJo",
	"./gom-deva.js": "qvJo",
	"./gom-latn": "DKr+",
	"./gom-latn.js": "DKr+",
	"./gu": "4MV3",
	"./gu.js": "4MV3",
	"./he": "x6pH",
	"./he.js": "x6pH",
	"./hi": "3E1r",
	"./hi.js": "3E1r",
	"./hr": "S6ln",
	"./hr.js": "S6ln",
	"./hu": "WxRl",
	"./hu.js": "WxRl",
	"./hy-am": "1rYy",
	"./hy-am.js": "1rYy",
	"./id": "UDhR",
	"./id.js": "UDhR",
	"./is": "BVg3",
	"./is.js": "BVg3",
	"./it": "bpih",
	"./it-ch": "bxKX",
	"./it-ch.js": "bxKX",
	"./it.js": "bpih",
	"./ja": "B55N",
	"./ja.js": "B55N",
	"./jv": "tUCv",
	"./jv.js": "tUCv",
	"./ka": "IBtZ",
	"./ka.js": "IBtZ",
	"./kk": "bXm7",
	"./kk.js": "bXm7",
	"./km": "6B0Y",
	"./km.js": "6B0Y",
	"./kn": "PpIw",
	"./kn.js": "PpIw",
	"./ko": "Ivi+",
	"./ko.js": "Ivi+",
	"./ku": "JCF/",
	"./ku.js": "JCF/",
	"./ky": "lgnt",
	"./ky.js": "lgnt",
	"./lb": "RAwQ",
	"./lb.js": "RAwQ",
	"./lo": "sp3z",
	"./lo.js": "sp3z",
	"./lt": "JvlW",
	"./lt.js": "JvlW",
	"./lv": "uXwI",
	"./lv.js": "uXwI",
	"./me": "KTz0",
	"./me.js": "KTz0",
	"./mi": "aIsn",
	"./mi.js": "aIsn",
	"./mk": "aQkU",
	"./mk.js": "aQkU",
	"./ml": "AvvY",
	"./ml.js": "AvvY",
	"./mn": "lYtQ",
	"./mn.js": "lYtQ",
	"./mr": "Ob0Z",
	"./mr.js": "Ob0Z",
	"./ms": "6+QB",
	"./ms-my": "ZAMP",
	"./ms-my.js": "ZAMP",
	"./ms.js": "6+QB",
	"./mt": "G0Uy",
	"./mt.js": "G0Uy",
	"./my": "honF",
	"./my.js": "honF",
	"./nb": "bOMt",
	"./nb.js": "bOMt",
	"./ne": "OjkT",
	"./ne.js": "OjkT",
	"./nl": "+s0g",
	"./nl-be": "2ykv",
	"./nl-be.js": "2ykv",
	"./nl.js": "+s0g",
	"./nn": "uEye",
	"./nn.js": "uEye",
	"./oc-lnc": "Fnuy",
	"./oc-lnc.js": "Fnuy",
	"./pa-in": "8/+R",
	"./pa-in.js": "8/+R",
	"./pl": "jVdC",
	"./pl.js": "jVdC",
	"./pt": "8mBD",
	"./pt-br": "0tRk",
	"./pt-br.js": "0tRk",
	"./pt.js": "8mBD",
	"./ro": "lyxo",
	"./ro.js": "lyxo",
	"./ru": "lXzo",
	"./ru.js": "lXzo",
	"./sd": "Z4QM",
	"./sd.js": "Z4QM",
	"./se": "//9w",
	"./se.js": "//9w",
	"./si": "7aV9",
	"./si.js": "7aV9",
	"./sk": "e+ae",
	"./sk.js": "e+ae",
	"./sl": "gVVK",
	"./sl.js": "gVVK",
	"./sq": "yPMs",
	"./sq.js": "yPMs",
	"./sr": "zx6S",
	"./sr-cyrl": "E+lV",
	"./sr-cyrl.js": "E+lV",
	"./sr.js": "zx6S",
	"./ss": "Ur1D",
	"./ss.js": "Ur1D",
	"./sv": "X709",
	"./sv.js": "X709",
	"./sw": "dNwA",
	"./sw.js": "dNwA",
	"./ta": "PeUW",
	"./ta.js": "PeUW",
	"./te": "XLvN",
	"./te.js": "XLvN",
	"./tet": "V2x9",
	"./tet.js": "V2x9",
	"./tg": "Oxv6",
	"./tg.js": "Oxv6",
	"./th": "EOgW",
	"./th.js": "EOgW",
	"./tk": "Wv91",
	"./tk.js": "Wv91",
	"./tl-ph": "Dzi0",
	"./tl-ph.js": "Dzi0",
	"./tlh": "z3Vd",
	"./tlh.js": "z3Vd",
	"./tr": "DoHr",
	"./tr.js": "DoHr",
	"./tzl": "z1FC",
	"./tzl.js": "z1FC",
	"./tzm": "wQk9",
	"./tzm-latn": "tT3J",
	"./tzm-latn.js": "tT3J",
	"./tzm.js": "wQk9",
	"./ug-cn": "YRex",
	"./ug-cn.js": "YRex",
	"./uk": "raLr",
	"./uk.js": "raLr",
	"./ur": "UpQW",
	"./ur.js": "UpQW",
	"./uz": "Loxo",
	"./uz-latn": "AQ68",
	"./uz-latn.js": "AQ68",
	"./uz.js": "Loxo",
	"./vi": "KSF8",
	"./vi.js": "KSF8",
	"./x-pseudo": "/X5v",
	"./x-pseudo.js": "/X5v",
	"./yo": "fzPg",
	"./yo.js": "fzPg",
	"./zh-cn": "XDpg",
	"./zh-cn.js": "XDpg",
	"./zh-hk": "SatO",
	"./zh-hk.js": "SatO",
	"./zh-mo": "OmwH",
	"./zh-mo.js": "OmwH",
	"./zh-tw": "kOpN",
	"./zh-tw.js": "kOpN"
};


function webpackContext(req) {
	var id = webpackContextResolve(req);
	return __webpack_require__(id);
}
function webpackContextResolve(req) {
	if(!__webpack_require__.o(map, req)) {
		var e = new Error("Cannot find module '" + req + "'");
		e.code = 'MODULE_NOT_FOUND';
		throw e;
	}
	return map[req];
}
webpackContext.keys = function webpackContextKeys() {
	return Object.keys(map);
};
webpackContext.resolve = webpackContextResolve;
module.exports = webpackContext;
webpackContext.id = "RnhZ";

/***/ }),

/***/ "Sy1n":
/*!**********************************!*\
  !*** ./src/app/app.component.ts ***!
  \**********************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
exports.AppComponent = void 0;
var i0 = __webpack_require__(/*! @angular/core */ "fXoL");
var i1 = __webpack_require__(/*! @angular/router */ "tyNb");
var AppComponent = /** @class */ (function () {
    function AppComponent() {
        this.title = 'MRM-ang';
    }
    AppComponent.ɵfac = function AppComponent_Factory(t) { return new (t || AppComponent)(); };
    AppComponent.ɵcmp = i0.ɵɵdefineComponent({ type: AppComponent, selectors: [["app-root"]], decls: 1, vars: 0, template: function AppComponent_Template(rf, ctx) { if (rf & 1) {
            i0.ɵɵelement(0, "router-outlet");
        } }, directives: [i1.RouterOutlet], encapsulation: 2 });
    return AppComponent;
}());
exports.AppComponent = AppComponent;


/***/ }),

/***/ "ZAI4":
/*!*******************************!*\
  !*** ./src/app/app.module.ts ***!
  \*******************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
exports.AppModule = void 0;
var forms_1 = __webpack_require__(/*! @angular/forms */ "3Pt+");
var platform_browser_1 = __webpack_require__(/*! @angular/platform-browser */ "jhN1");
var import_record_component_1 = __webpack_require__(/*! ../components/import-record/import-record.component */ "LpmO");
var ngx_datatable_1 = __webpack_require__(/*! @swimlane/ngx-datatable */ "lDzL");
var app_routing_module_1 = __webpack_require__(/*! ./app-routing.module */ "vY5A");
var app_component_1 = __webpack_require__(/*! ./app.component */ "Sy1n");
var http_1 = __webpack_require__(/*! @angular/common/http */ "tk/3");
var animations_1 = __webpack_require__(/*! @angular/platform-browser/animations */ "R1ws");
var select_1 = __webpack_require__(/*! @angular/material/select */ "d3UM");
var form_field_1 = __webpack_require__(/*! @angular/material/form-field */ "kmnG");
var input_1 = __webpack_require__(/*! @angular/material/input */ "qFsG");
var datepicker_1 = __webpack_require__(/*! @angular/material/datepicker */ "iadO");
var core_1 = __webpack_require__(/*! @angular/material/core */ "FKr1");
var tabs_1 = __webpack_require__(/*! @angular/material/tabs */ "wZkO");
var button_1 = __webpack_require__(/*! @angular/material/button */ "bTqV");
var icon_1 = __webpack_require__(/*! @angular/material/icon */ "NFeN");
var checkbox_1 = __webpack_require__(/*! @angular/material/checkbox */ "bSwM");
var tooltip_1 = __webpack_require__(/*! @angular/material/tooltip */ "Qu3c");
var dialog_1 = __webpack_require__(/*! @angular/material/dialog */ "0IaG");
var import_logs_component_1 = __webpack_require__(/*! ../components/import-logs/import-logs.component */ "b5bQ");
var i0 = __webpack_require__(/*! @angular/core */ "fXoL");
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule.ɵfac = function AppModule_Factory(t) { return new (t || AppModule)(); };
    AppModule.ɵmod = i0.ɵɵdefineNgModule({ type: AppModule, bootstrap: [app_component_1.AppComponent] });
    AppModule.ɵinj = i0.ɵɵdefineInjector({ providers: [datepicker_1.MatDatepickerModule], imports: [[
                platform_browser_1.BrowserModule,
                http_1.HttpClientModule,
                forms_1.FormsModule,
                app_routing_module_1.AppRoutingModule,
                ngx_datatable_1.NgxDatatableModule,
                animations_1.NoopAnimationsModule,
                select_1.MatSelectModule, form_field_1.MatFormFieldModule, input_1.MatInputModule, datepicker_1.MatDatepickerModule, core_1.MatNativeDateModule, tabs_1.MatTabsModule, button_1.MatButtonModule, icon_1.MatIconModule,
                checkbox_1.MatCheckboxModule, tooltip_1.MatTooltipModule, dialog_1.MatDialogModule
            ]] });
    return AppModule;
}());
exports.AppModule = AppModule;
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && i0.ɵɵsetNgModuleScope(AppModule, { declarations: [app_component_1.AppComponent,
        import_record_component_1.ImportRecordComponent,
        import_logs_component_1.ImportLogsComponent], imports: [platform_browser_1.BrowserModule,
        http_1.HttpClientModule,
        forms_1.FormsModule,
        app_routing_module_1.AppRoutingModule,
        ngx_datatable_1.NgxDatatableModule,
        animations_1.NoopAnimationsModule,
        select_1.MatSelectModule, form_field_1.MatFormFieldModule, input_1.MatInputModule, datepicker_1.MatDatepickerModule, core_1.MatNativeDateModule, tabs_1.MatTabsModule, button_1.MatButtonModule, icon_1.MatIconModule,
        checkbox_1.MatCheckboxModule, tooltip_1.MatTooltipModule, dialog_1.MatDialogModule] }); })();


/***/ }),

/***/ "b5bQ":
/*!*************************************************************!*\
  !*** ./src/components/import-logs/import-logs.component.ts ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
exports.ImportLogsComponent = void 0;
var dialog_1 = __webpack_require__(/*! @angular/material/dialog */ "0IaG");
var i0 = __webpack_require__(/*! @angular/core */ "fXoL");
var i1 = __webpack_require__(/*! @angular/common */ "ofXK");
function ImportLogsComponent_li_1_Template(rf, ctx) { if (rf & 1) {
    i0.ɵɵelementStart(0, "li");
    i0.ɵɵtext(1);
    i0.ɵɵelementEnd();
} if (rf & 2) {
    var log_r1 = ctx.$implicit;
    i0.ɵɵadvance(1);
    i0.ɵɵtextInterpolate2(" ", log_r1.IsSuccess, " ", log_r1.ResponseRaw, " ");
} }
var ImportLogsComponent = /** @class */ (function () {
    function ImportLogsComponent(data) {
        this.data = data;
        this.logs = [];
        console.log(data);
        this.logs = data.logs;
    }
    ImportLogsComponent.prototype.ngOnInit = function () {
    };
    ImportLogsComponent.ɵfac = function ImportLogsComponent_Factory(t) { return new (t || ImportLogsComponent)(i0.ɵɵdirectiveInject(dialog_1.MAT_DIALOG_DATA)); };
    ImportLogsComponent.ɵcmp = i0.ɵɵdefineComponent({ type: ImportLogsComponent, selectors: [["app-import-logs"]], decls: 2, vars: 1, consts: [[4, "ngFor", "ngForOf"]], template: function ImportLogsComponent_Template(rf, ctx) { if (rf & 1) {
            i0.ɵɵelementStart(0, "ul");
            i0.ɵɵtemplate(1, ImportLogsComponent_li_1_Template, 2, 2, "li", 0);
            i0.ɵɵelementEnd();
        } if (rf & 2) {
            i0.ɵɵadvance(1);
            i0.ɵɵproperty("ngForOf", ctx.logs);
        } }, directives: [i1.NgForOf], encapsulation: 2 });
    return ImportLogsComponent;
}());
exports.ImportLogsComponent = ImportLogsComponent;


/***/ }),

/***/ "hD8V":
/*!*************************************!*\
  !*** ./src/services/api.service.ts ***!
  \*************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
exports.ApiService = void 0;
var http_1 = __webpack_require__(/*! @angular/common/http */ "tk/3");
var i0 = __webpack_require__(/*! @angular/core */ "fXoL");
var i1 = __webpack_require__(/*! @angular/common/http */ "tk/3");
var ApiService = /** @class */ (function () {
    function ApiService(http) {
        this.http = http;
    }
    ApiService.prototype.getProcessedIn = function (page, filter) {
        return this.http.post("/Record/GetVisitsProcessedIn?page=" + page + "&pageSize=20", filter);
    };
    ApiService.prototype.getProcessedIn4Report = function (filter) {
        return this.http.post("/Record/GetVisitsProcessedIn4Report", filter, {
            responseType: "blob",
            headers: new http_1.HttpHeaders().append("Content-Type", "application/json")
        });
    };
    ApiService.prototype.getVisitsByStatus = function (page, status, filter) {
        return this.http.post("/Record/GetVisitsByStatus?status=" + status + "&page=" + page + "&pageSize=20", filter);
    };
    ApiService.prototype.unqueue = function (visitIds) {
        return this.http.post("/Record/Unqueue", visitIds);
    };
    ApiService.prototype.rerunImport = function (visitIds) {
        return this.http.post("/Record/RerunImport", visitIds);
    };
    ApiService.prototype.getVisitsByStatus4Report = function (status, filter) {
        return this.http.post("/Record/GetVisitsByStatus4Report?status=" + status, filter, {
            responseType: "blob",
            headers: new http_1.HttpHeaders().append("Content-Type", "application/json")
        });
    };
    ApiService.prototype.getClinics = function () {
        return this.http.get("/Record/GetClinics");
    };
    ApiService.prototype.getFinClasses = function () {
        return this.http.get("/Record/GetFinClasses");
    };
    ApiService.ɵfac = function ApiService_Factory(t) { return new (t || ApiService)(i0.ɵɵinject(i1.HttpClient)); };
    ApiService.ɵprov = i0.ɵɵdefineInjectable({ token: ApiService, factory: ApiService.ɵfac, providedIn: 'root' });
    return ApiService;
}());
exports.ApiService = ApiService;


/***/ }),

/***/ "vY5A":
/*!***************************************!*\
  !*** ./src/app/app-routing.module.ts ***!
  \***************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
exports.AppRoutingModule = void 0;
var router_1 = __webpack_require__(/*! @angular/router */ "tyNb");
var import_record_component_1 = __webpack_require__(/*! ../components/import-record/import-record.component */ "LpmO");
var i0 = __webpack_require__(/*! @angular/core */ "fXoL");
var i1 = __webpack_require__(/*! @angular/router */ "tyNb");
var routes = [
    {
        path: "record/ImportReport",
        component: import_record_component_1.ImportRecordComponent
    }
];
var AppRoutingModule = /** @class */ (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule.ɵfac = function AppRoutingModule_Factory(t) { return new (t || AppRoutingModule)(); };
    AppRoutingModule.ɵmod = i0.ɵɵdefineNgModule({ type: AppRoutingModule });
    AppRoutingModule.ɵinj = i0.ɵɵdefineInjector({ imports: [[router_1.RouterModule.forRoot(routes)], router_1.RouterModule] });
    return AppRoutingModule;
}());
exports.AppRoutingModule = AppRoutingModule;
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && i0.ɵɵsetNgModuleScope(AppRoutingModule, { imports: [i1.RouterModule], exports: [router_1.RouterModule] }); })();


/***/ }),

/***/ "zUnb":
/*!*********************!*\
  !*** ./src/main.ts ***!
  \*********************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
var __NgCli_bootstrap_1 = __webpack_require__(/*! @angular/platform-browser */ "jhN1");
var core_1 = __webpack_require__(/*! @angular/core */ "fXoL");
var app_module_1 = __webpack_require__(/*! ./app/app.module */ "ZAI4");
var environment_1 = __webpack_require__(/*! ./environments/environment */ "AytR");
if (environment_1.environment.production) {
    core_1.enableProdMode();
}
__NgCli_bootstrap_1.platformBrowser().bootstrapModule(app_module_1.AppModule)
    .catch(function (err) { return console.error(err); });


/***/ }),

/***/ "zn8P":
/*!******************************************************!*\
  !*** ./$$_lazy_route_resource lazy namespace object ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncaught exception popping up in devtools
	return Promise.resolve().then(function() {
		var e = new Error("Cannot find module '" + req + "'");
		e.code = 'MODULE_NOT_FOUND';
		throw e;
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = "zn8P";

/***/ })

},[[0,"runtime","vendor"]]]);
//# sourceMappingURL=main.js.map