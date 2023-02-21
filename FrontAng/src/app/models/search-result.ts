import { Guid } from "guid-typescript";

// Cette interface est un POCO
export interface SearchResult{
    label:string;
    description : string;
    id:Guid;
}