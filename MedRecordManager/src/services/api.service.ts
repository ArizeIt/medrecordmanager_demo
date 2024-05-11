
import { Injectable } from "@angular/core";

import { HttpClient, HttpHandler, HttpHeaders, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";


@Injectable({
    providedIn: 'root'
})
export class ApiService {

    constructor(private http: HttpClient) {

    }

    getProcessedIn(page, filter): Observable<any>{
       return  this.http.post(`/Record/GetVisitsProcessedIn?page=${page}&pageSize=20`, filter);
    }
    getProcessedIn4Report(filter): Observable<any> {
        return this.http.post(`/Record/GetVisitsProcessedIn4Report`, filter, {
            responseType: "blob",
            headers: new HttpHeaders().append("Content-Type", "application/json")
        });
    }

    getVisitsByStatus(page,status, filter): Observable<any> {
        return this.http.post(`/Record/GetVisitsByStatus?status=${status}&page=${page}&pageSize=20`, filter);
    }

    unqueue(visitIds) {
        return this.http.post(`/Record/Unqueue`, visitIds);
    }

    rerunImport(visitIds) {
        return this.http.post(`/Record/RerunImport`, visitIds);
    }

    getVisitsByStatus4Report( status, filter): Observable<any> {
        return this.http.post(`/Record/GetVisitsByStatus4Report?status=${status}`, filter, {
            responseType: "blob",
            headers: new HttpHeaders().append("Content-Type", "application/json")
        });
    }

    getClinics(): Observable<any> {
        return this.http.get(`/Record/GetClinics`);
    }
    getFinClasses(): Observable<any> {
        return this.http.get(`/Record/GetFinClasses`);
    }
}
