import { HttpClient, HttpParams } from "@angular/common/http";
import { map } from "rxjs";
import { PaginatedResult } from "../_models/pagination";

export function getPaginatedResult<T>(url,params, http: HttpClient) {
    const  paginatedResult: PaginatedResult<T>= new PaginatedResult<T>();
      
      return http.get<T>(url, { observe: 'response', params }).pipe(
        map(response => {
          paginatedResult.result = response.body;
          const paginatedheader = response.headers.get('Pagination');
          paginatedResult.pagination = JSON.parse(paginatedheader);
          var pagination = response.headers.get('Pagination');
          if (pagination == null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('headers'));
  
          }
          return paginatedResult;
        })
      );
    }
  
    export function getPaginationHeaders(pageNumber: number, pageSize:number){
      let params = new HttpParams();
  
        params= params.append('pageNumber', pageNumber.toString());
        params= params.append('pageSize', pageSize.toString());
        
        return params;
  
    }