export class AppUser {
    constructor(firstName?: string, lastName?: string, phoneNumber?: string, city?: string) {
        this.firstName = firstName || '';
        this.lastName = lastName|| '';
        this.phoneNumber = phoneNumber|| '';
        this.city = city|| '';
    }

    //#region firstName
    private _firstName!: string;
    public get firstName() {
        return this._firstName
    }
    public set firstName(v: string) {
        this._firstName = v;
    }
    //#endregion

    //#region lastName
    private _lastName!: string;
    public get lastName() {
        return this._lastName
    }
    public set lastName(v: string) {
        this._lastName = v;
    }
    //#endregion

    //#region phoneNumber
    private _phoneNumber!: string;
    public get phoneNumber() {
        return this._phoneNumber
    }
    public set phoneNumber(v: string) {
        this._phoneNumber = v;
    }
    //#endregion

    //#region city
    private _city!: string;
    public get city() {
        return this._city
    }
    public set city(v: string) {
        this._city = v;
    }
    //#endregion  
}

