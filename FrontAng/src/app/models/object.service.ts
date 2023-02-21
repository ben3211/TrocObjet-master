
import { Object } from './object';
import { SCRUDService } from './scrud.service';

// FilmService est une class qui dérive de la class SCRUDService<T>
// Avec T = Film
// 
export abstract class objectService extends SCRUDService<Object>{
}