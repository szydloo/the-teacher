// TODO: Add custom claims?
export class UserAuth {
    userId: string;
    token: string = "";
    isAuthenticated: boolean = false;
    role: string = "";
    username: string = "";
}