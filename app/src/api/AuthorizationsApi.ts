import {useApiArea} from "../utils/env";
import {TokenModel} from "../domain/models/TokenModel";
import axios from "axios";

export class AuthorizationsApi {
    static apiArea = useApiArea('authorizations');

    static async authorizeGoogleSignin(idToken: string): Promise<TokenModel> {
        const url = this.apiArea.urlForEndpoint('google');
        const {data} = await axios.post<TokenModel>(url, {idToken});
        return data;
    }

    static async refreshToken(): Promise<TokenModel> {
        const url = this.apiArea.urlForEndpoint('refresh');
        const {data} = await axios.get<TokenModel>(url, {withCredentials: true});
        return data;
    }
}