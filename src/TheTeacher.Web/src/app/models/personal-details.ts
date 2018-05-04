export class PersonalDetails {
    firstName: string = "";
    lastName: string = "";
    dateOfBirth: Date = new Date(1999,0,1);
    address: {
        street: string;
        zipcode: string;
        city: string;
        country: string;
    } 

    constructor() {
        
        this.address = { street: "", zipcode: "", city: "", country: ""};
    }
}