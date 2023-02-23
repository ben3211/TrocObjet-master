export class Object {
    constructor(label?: string, description?: string, estimatedPrice?: number) {
        this.label = label || '';
        this.description = description || '';
        this.estimatedPrice = estimatedPrice || 0;
    }

    //#region id
    private _id! : string;
    public get id(){
    return this._id;
    }
    public set id(v:string){
    // TODO : Vérifier la valeur de v
    // if(condition){
    // throw new Error('message');
    // }
     this._id=v;
    }
    //#endregion

    //#region label
    private _label!: string;
    public get label() {
        return this._label;
    }
    public set label(v: string) {

        this._label = v;
    }
    //#endregion

    //#region description
    private _description!: string;
    public get description() {
        return this._description;
    }
    public set description(v: string) {
        // TODO : Vérifier la valeur de v
        // if(condition){
        // throw new Error('message');
        // }
        this._description = v;
    }
    //#endregion

    //#region estimatedPrice
    private _estimatedPrice!: number;
    public get estimatedPrice() {
        return this._estimatedPrice;
    }
    public set estimatedPrice(v: number) {
        // TODO : Vérifier la valeur de v
        // if(condition){
        // throw new Error('message');
        // }
        this._estimatedPrice = v;
    }
    //#endregion




    //#region Photo
    private _photos: string[] = [];
    public get photos() {
        return this._photos;
    }
    public set photos(v: string[]) {
        this._photos = v;
    }
    //#endregion
}
