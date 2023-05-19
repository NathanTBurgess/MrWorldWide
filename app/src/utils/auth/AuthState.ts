import {UserManager} from "./UserManager";
import {User} from "./User";

export type AuthState = { userManager: UserManager }  & ({isAuthenticated: false} | {isAuthenticated: true, user: User});