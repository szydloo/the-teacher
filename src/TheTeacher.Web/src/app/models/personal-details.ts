import { Address } from "./address";

export class PersonalDetails {
    firstName: string = "";
    lastName: string = "";
    dateOfBirth: Date = new Date(1999,0,1);
    address: Address;
    university: string;
    title: string;
    fieldOfStudy: string;
    

    constructor() {
        
        this.address = { street: "", zipcode: "", city: "", country: ""};
    }
}