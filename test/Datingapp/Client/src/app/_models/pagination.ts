export interface Pagination{
    currentPage: number;
    itemsPerPage: number;
    totalItems:number;
    totalPages: number;
}
export class PaginatedResult<T>{
    //here t will be arrary of members but T represent anything
    //list of member will be stored in result and pagination informaiton to pagination

    result: T;
    pagination: Pagination;
    
}