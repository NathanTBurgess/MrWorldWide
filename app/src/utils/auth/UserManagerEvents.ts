import {CustomEventListener} from "../events";
import {User} from "./User";

export interface UserManagerEvents {
    addUserLoaded(callbackFn: CustomEventListener<User>): void;

    removeUserLoaded(callbackFn: CustomEventListener<User>): void;

    addUserSignedOut(callbackFn: CustomEventListener<User>): void;

    removeUserSignedOut(callbackFn: CustomEventListener<User>): void;

    addAccessTokenExpiring(callbackFn: CustomEventListener<string>): void;

    removeAccessTokenExpiring(callbackFn: CustomEventListener<string>): void;
}