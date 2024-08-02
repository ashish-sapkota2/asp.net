import { User } from "./user";

export class UserParams{
    username:string;
    gender: string;
    minAge=18;
    maxAge=99;
    pageNumber=1;
    pageSize=5;
    orderBy ='lastActive';

    constructor(user:User){
        this.gender= user.gender==='female'? 'male': 'female';
        this.username= user.username;
    }
}