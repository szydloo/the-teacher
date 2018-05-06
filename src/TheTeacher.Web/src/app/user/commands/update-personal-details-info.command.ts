import { Address } from "../../models/address";


export class UpdatePersonalDetailsInfo {
    firstName: string;
    lastName: string;
    dateOfBirth: Date;
    address: Address;
    university: string;
    fieldOfStudy: string;
    title: string;

    constructor() {
        this.address = new Address();
    }
}