import { Guid } from "guid-typescript";
import { SearchResult } from "./search-result";

// Desdcription des opérations SCRUD pour un type T
// T peut être n'importe quelle class
export abstract class SCRUDService<T> {
    // Sauvegarde un film => opération asynchrone
    // car elle comprend probablement un accès à un serveur distant
    // donc on ne peut garantir la rapidité
     abstract saveItemAsync(item:T):Promise<Guid>;  // Create
     abstract deleteItemAsync(id:Guid):Promise<void>;  // Delete
     abstract updateItemAsync(id:Guid,item:T):Promise<void>; // Update
     abstract getItemAsync(id:Guid):Promise<T>; // Read
     abstract searchItemAsync(searchText:string):Promise<SearchResult[]>; // Search
  }