import { Guid } from "guid-typescript";
import { AppUser } from "./appUser";
import { SCRUDService } from "./scrud.service";

export abstract class userService extends SCRUDService<AppUser>{
    abstract   getObjectsAsync(id: Guid): Promise<Object[]>;
}