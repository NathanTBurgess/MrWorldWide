import {IdentityChangedEvent, TokenExpiringEvent, UserLoadedEvent, UserSignedOutEvent} from "../events";
import {TokenContextProps} from "./TokenProvider";
import {AuthorizationsApi} from "../../api/AuthorizationsApi";
import {ClaimsIdentity} from "./claims/ClaimsIdentity";
import {UserManagerEvents} from "./UserManagerEvents";
import {ProblemDetails} from "../../domain/models";
import {ILogger} from "../logging";
import {User} from "./User";


export type SignInResult = { succeeded: true, user: User } | { succeeded: false, details: ProblemDetails };

export class UserManager {
    user: User | null = null;
    private userLoadedEventMethods = UserLoadedEvent;
    private userSignedOutEventMethods = UserSignedOutEvent;
    private accessTokenExpiringEventMethods = TokenExpiringEvent;

    public get events(): UserManagerEvents {
        return {
            addUserLoaded: this.userLoadedEventMethods.addListener.bind(this.userLoadedEventMethods),
            removeUserLoaded: this.userLoadedEventMethods.removeListener.bind(this.userLoadedEventMethods),
            addUserSignedOut: this.userSignedOutEventMethods.addListener.bind(this.userSignedOutEventMethods),
            removeUserSignedOut: this.userSignedOutEventMethods.removeListener.bind(this.userSignedOutEventMethods),
            addAccessTokenExpiring: this.accessTokenExpiringEventMethods.addListener.bind(this.accessTokenExpiringEventMethods),
            removeAccessTokenExpiring: this.accessTokenExpiringEventMethods.removeListener.bind(this.accessTokenExpiringEventMethods)
        }
    }


    constructor(private readonly tokenContext: TokenContextProps, private readonly logger: ILogger) {
        IdentityChangedEvent.addListener.bind(this.handleIdentityChanged);
        this.handleIdentityChanged({detail: tokenContext.identity});
    }

    async signout(): Promise<void> {
        try {
            const response = await AuthorizationsApi.signout();
            if (!response.isSuccessStatusCode) {
                this.logger.error(response.data, "Server error while signing out. See error for details.");
                return;
            }
        } catch (e) {
            this.logger.error(e as Error, 'An unexpected error occurred during the signout sequence');
            throw e;
        } finally {
            this.tokenContext.clear();
        }
    }

    async handleGoogleSso(idToken: string): Promise<SignInResult> {
        const response = await AuthorizationsApi.authorizeGoogleSignin(idToken);
        if (!response.isSuccessStatusCode) {
            this.tokenContext.clear();
            this.logger.error(response.data, 'Signin through Google SSO failed');
            return {succeeded: false, details: response.data};
        }
        const token = response.data.accessToken;
        this.tokenContext.setToken(token);
        const user = this.buildUserFromClaimsIdentity(ClaimsIdentity.fromJwt(token));
        return {
            succeeded: true,
            user
        };
    }

    async silentRefresh(): Promise<SignInResult> {
        const response = await AuthorizationsApi.refreshToken();
        if (!response.isSuccessStatusCode) {
            this.tokenContext.clear();
            this.logger.error(response.data, 'Attempt to sign in user silently has failed');
            return {succeeded: false, details: response.data};
        }
        const token = response.data.accessToken;
        this.tokenContext.setToken(token);
        const user = this.buildUserFromClaimsIdentity(ClaimsIdentity.fromJwt(token));
        return {
            succeeded: true,
            user
        };
    }

    private handleIdentityChanged({detail}: { detail: ClaimsIdentity | null }) {
        const existing = this.user;
        if (detail === null) {
            this.user = null;
            if (existing !== null) {
                UserSignedOutEvent.dispatch({detail: existing});
            }
            return;
        }
        this.user = this.buildUserFromClaimsIdentity(detail);
        if (existing === null || (this.user.id !== existing.id)) {
            UserLoadedEvent.dispatch({detail: this.user});
        }
    }

    private buildUserFromClaimsIdentity(claims: ClaimsIdentity): User {
        const {id, name, email} = claims;
        if (!id) {
            throw new Error("Unable to handle provided Claims Identity as a user ID could not be determined");

        }
        return {
            id,
            name: name ?? "Unknown",
            email: email ?? "Unknown"
        }
    }
}