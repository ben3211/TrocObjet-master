import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Guid } from "guid-typescript";
import { lastValueFrom } from "rxjs";
import { SearchResultDTO } from "../dtos/SearchResultDTO";
import { AppUser } from "./appUser";
import { userService } from "./appUser.service";
import { SearchResult } from "./search-result";
import {Object} from '../models/object';

@Injectable({
    providedIn: 'root'
})

export class userHttpService implements userService {
    constructor(private httpClient: HttpClient) {

    }

    async saveItemAsync(item: AppUser) {
        var guid = Guid.create();
        var dto = {
            "id": guid.toString(),
            "fNa": item.firstName,
            "lNa": item.lastName,
            "pn": item.phoneNumber,
            "c": item.city
        };
        var requete = this.httpClient.post(`http://localhost:5088/api/User`, dto);
        var promesse = lastValueFrom(requete);
        var resultatPromesse = await promesse as boolean;
        return guid;
    }
    async deleteItemAsync(id: Guid): Promise<void> {
        var requete = this.httpClient.delete(`http://localhost:5088/api/User/${id.toString()}`);
        var promesse = lastValueFrom(requete);

        var resultatPromesse = await promesse as boolean;
    }
    async updateItemAsync(id: Guid, item: AppUser) {
        var dto = {
            "id": id.toString(),
            "fNa": item.firstName,
            "lNa": item.lastName,
            "pn": item.phoneNumber,
            "c": item.city
        };
        var requete = this.httpClient.put(`http://localhost:5088/api/User/${id.toString()}`, dto);
        var promesse = lastValueFrom(requete);
        var resultat = await promesse;
    }
    async getItemAsync(id: Guid): Promise<AppUser> {
        var requete = this.httpClient.get(`http://localhost:5088/api/User/${id.toString()}`);
        var promesse = lastValueFrom(requete);

        var dto = await promesse as { id: string, fNa: string, lNa: string, pn: string, c: string };

        var resultat = new AppUser(dto.fNa);
        resultat.lastName = dto.lNa;
        resultat.phoneNumber = dto.pn;
        resultat.city = dto.c;



        return resultat;
    }

    async getObjectsAsync(id: Guid): Promise<Object[]> {
        var requete = this.httpClient.get(`http://localhost:5088/api/User/${id.toString()}/objects`);
        var promesse = lastValueFrom(requete);

        var dtos = await promesse as { id: string, l: string, d: string, idp: string[] }[];

        var resultats = dtos.map(dto => {
            var poco = new Object();
            poco.id = dto.id;
            poco.label = dto.l;
            poco.description=dto.d;
            poco.photos=dto.idp
            return poco;

        })
        return resultats;
    }


    async searchItemAsync(searchText: string = "") {
        var requete = this.httpClient.get<SearchResultDTO[]>("http://localhost:5088/api/User?searchText=" + searchText);
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