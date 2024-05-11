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
    AppComponent.ɵcmp = i0.ɵɵdefineComponent({ type: AppComponent, selectors: [["app-root"]], decls: 22, vars: 2, consts: [[1, "content", 2, "text-align", "center"], [2, "display", "block"], ["width", "300", "alt", "Angular Logo", "src", "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZpZXdCb3g9IjAgMCAyNTAgMjUwIj4KICAgIDxwYXRoIGZpbGw9IiNERDAwMzEiIGQ9Ik0xMjUgMzBMMzEuOSA2My4ybDE0LjIgMTIzLjFMMTI1IDIzMGw3OC45LTQzLjcgMTQuMi0xMjMuMXoiIC8+CiAgICA8cGF0aCBmaWxsPSIjQzMwMDJGIiBkPSJNMTI1IDMwdjIyLjItLjFWMjMwbDc4LjktNDMuNyAxNC4yLTEyMy4xTDEyNSAzMHoiIC8+CiAgICA8cGF0aCAgZmlsbD0iI0ZGRkZGRiIgZD0iTTEyNSA1Mi4xTDY2LjggMTgyLjZoMjEuN2wxMS43LTI5LjJoNDkuNGwxMS43IDI5LjJIMTgzTDEyNSA1Mi4xem0xNyA4My4zaC0zNGwxNy00MC45IDE3IDQwLjl6IiAvPgogIDwvc3ZnPg=="], ["target", "_blank", "rel", "noopener", "href", "https://angular.io/tutorial"], ["target", "_blank", "rel", "noopener", "href", "https://angular.io/cli"], ["target", "_blank", "rel", "noopener", "href", "https://blog.angular.io/"]], template: function AppComponent_Template(rf, ctx) { if (rf & 1) {
            i0.ɵɵelementStart(0, "div", 0);
            i0.ɵɵelementStart(1, "h1");
            i0.ɵɵtext(2);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(3, "span", 1);
            i0.ɵɵtext(4);
            i0.ɵɵelementEnd();
            i0.ɵɵelement(5, "img", 2);
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(6, "h2");
            i0.ɵɵtext(7, "Here are some links to help you start: ");
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(8, "ul");
            i0.ɵɵelementStart(9, "li");
            i0.ɵɵelementStart(10, "h2");
            i0.ɵɵelementStart(11, "a", 3);
            i0.ɵɵtext(12, "Tour of Heroes");
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(13, "li");
            i0.ɵɵelementStart(14, "h2");
            i0.ɵɵelementStart(15, "a", 4);
            i0.ɵɵtext(16, "CLI Documentation");
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementStart(17, "li");
            i0.ɵɵelementStart(18, "h2");
            i0.ɵɵelementStart(19, "a", 5);
            i0.ɵɵtext(20, "Angular blog");
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelementEnd();
            i0.ɵɵelement(21, "router-outlet");
        } if (rf & 2) {
            i0.ɵɵadvance(2);
            i0.ɵɵtextInterpolate1(" Welcome to ", ctx.title, "! ");
            i0.ɵɵadvance(2);
            i0.ɵɵtextInterpolate1("", ctx.title, " app is running!");
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
var platform_browser_1 = __webpack_require__(/*! @angular/platform-browser */ "jhN1");
var app_routing_module_1 = __webpack_require__(/*! ./app-routing.module */ "vY5A");
var app_component_1 = __webpack_require__(/*! ./app.component */ "Sy1n");
var i0 = __webpack_require__(/*! @angular/core */ "fXoL");
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule.ɵfac = function AppModule_Factory(t) { return new (t || AppModule)(); };
    AppModule.ɵmod = i0.ɵɵdefineNgModule({ type: AppModule, bootstrap: [app_component_1.AppComponent] });
    AppModule.ɵinj = i0.ɵɵdefineInjector({ providers: [], imports: [[
                platform_browser_1.BrowserModule,
                app_routing_module_1.AppRoutingModule
            ]] });
    return AppModule;
}());
exports.AppModule = AppModule;
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && i0.ɵɵsetNgModuleScope(AppModule, { declarations: [app_component_1.AppComponent], imports: [platform_browser_1.BrowserModule,
        app_routing_module_1.AppRoutingModule] }); })();


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
var i0 = __webpack_require__(/*! @angular/core */ "fXoL");
var i1 = __webpack_require__(/*! @angular/router */ "tyNb");
var routes = [];
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