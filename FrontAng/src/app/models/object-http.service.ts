import { Injectable } from "@angular/core";
import { Guid } from "guid-typescript";
import { Object } from "./object";
import { objectService } from "./object.service";
import { SearchResult } from "./search-result";
import { HttpClient } from '@angular/common/http';
import { SearchResultDTO } from "../dtos/SearchResultDTO";
import { interval, lastValueFrom, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class ObjectHttpService implements objectService {

    constructor(private httpClient: HttpClient) {

    }

    async saveItemAsync(item: Object) {
        var guid = Guid.create();
        var dto = {
            "id": guid.toString(),
            "l": item.label,
            "d": item.description,
            "ep": item.estimatedPrice

        };
        var requete = this.httpClient.post(`http://localhost:5088/api/Object`, dto);
        var promesse = lastValueFrom(requete);
        var resultatPromesse = await promesse as boolean;
        return guid;
    }

    async deleteItemAsync(id: Guid): Promise<void> {
        var requete = this.httpClient.delete(`http://localhost:5088/api/Object/${id.toString()}`);
        var promesse = lastValueFrom(requete);

        var resultatPromesse = await promesse as boolean;

        // await this.httpClient.delete(`http://localhost:5088/api/Object/${id.toString()}`).toPromise();
    }

    async updateItemAsync(id: Guid, item: Object) {
        var dto = {
            "id": id.toString(),
            "l": item.label,
            "d": item.description,
            "ep": item.estimatedPrice
        };
        var requete = this.httpClient.put(`http://localhost:5088/api/Object/${id.toString()}`, dto);
        var promesse=lastValueFrom(requete);
        var resultat=await promesse;

    }


    async getItemAsync(id: Guid): Promise<Object> {
        var requete = this.httpClient.get(`http://localhost:5088/api/Object/${id.toString()}`);
        var promesse = lastValueFrom(requete);

        var dto = await promesse as { id: string, l: string, d: string, idp: string[] };

        var resultat = new Object(dto.l);
        resultat.description = dto.d;
        resultat.photos=dto.idp;
        // resultat.photos = dto.p.map(photo => photo.path);
        return resultat;

    }


    async searchItemAsync(searchText: string = "") {
        var requete = this.httpClient.get<SearchResultDTO[]>("http://localhost:5088/api/Object?searchText=" + searchText);
        var promesse = lastValueFrom(requete);
        var dtos = await promesse;
        var results = dtos.map(dto => ({
            id: Guid.parse(dto.id),
            label: dto.l,
            description: dto.d,

        } as SearchResult));
        return results;
    }
}
