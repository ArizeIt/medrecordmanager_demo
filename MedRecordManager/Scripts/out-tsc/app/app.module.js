"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppModule = void 0;
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var platform_browser_1 = require("@angular/platform-browser");
var import_record_component_1 = require("../components/import-record/import-record.component");
var ngx_datatable_1 = require("@swimlane/ngx-datatable");
var app_routing_module_1 = require("./app-routing.module");
var app_component_1 = require("./app.component");
var http_1 = require("@angular/common/http");
var animations_1 = require("@angular/platform-browser/animations");
var select_1 = require("@angular/material/select");
var form_field_1 = require("@angular/material/form-field");
var input_1 = require("@angular/material/input");
var datepicker_1 = require("@angular/material/datepicker");
var core_2 = require("@angular/material/core");
var tabs_1 = require("@angular/material/tabs");
var button_1 = require("@angular/material/button");
var icon_1 = require("@angular/material/icon");
var checkbox_1 = require("@angular/material/checkbox");
var tooltip_1 = require("@angular/material/tooltip");
var dialog_1 = require("@angular/material/dialog");
var import_logs_component_1 = require("../components/import-logs/import-logs.component");
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            declarations: [
                app_component_1.AppComponent,
                import_record_component_1.ImportRecordComponent,
                import_logs_component_1.ImportLogsComponent
            ],
            imports: [
                platform_browser_1.BrowserModule,
                http_1.HttpClientModule,
                forms_1.FormsModule,
                app_routing_module_1.AppRoutingModule,
                ngx_datatable_1.NgxDatatableModule,
                animations_1.NoopAnimationsModule,
                select_1.MatSelectModule, form_field_1.MatFormFieldModule, input_1.MatInputModule, datepicker_1.MatDatepickerModule, core_2.MatNativeDateModule, tabs_1.MatTabsModule, button_1.MatButtonModule, icon_1.MatIconModule,
                checkbox_1.MatCheckboxModule, tooltip_1.MatTooltipModule, dialog_1.MatDialogModule
            ],
            providers: [datepicker_1.MatDatepickerModule],
            bootstrap: [app_component_1.AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map