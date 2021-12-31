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
exports.ApiService = void 0;
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
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
    ApiService = __decorate([
        core_1.Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], ApiService);
    return ApiService;
}());
exports.ApiService = ApiService;
//# sourceMappingURL=api.service.js.map