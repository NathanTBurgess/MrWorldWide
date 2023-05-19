import React, {useEffect} from "react";
import axios from "axios";
import {useLogger} from "../logging";
import {useToken} from "./TokenProvider";

function AxiosConfig() {
    const {token} = useToken();
    const logger = useLogger(AxiosConfig);
    useEffect(() => {
        if (token) {
            axios.defaults.headers.common = {Authorization: `Bearer ${token}`};
        } else {
            axios.defaults.headers.common = {};
        }
    }, [token]);


    return null;
}

export default React.memo(AxiosConfig);
