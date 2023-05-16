import React from 'react';
import {Box, Card, CardContent, Container, Typography} from '@mui/material';
import {CredentialResponse, GoogleLogin} from '@react-oauth/google';
import {useApi} from "../../utils/hooks";
import {AuthorizationsApi} from "../../api/AuthorizationsApi";
import {useLogger} from "../../utils/logging";


function Login() {
    const api = useApi(AuthorizationsApi);
    const logger = useLogger();

    async function handleSuccess(credentialResponse: CredentialResponse) {
        const {accessToken} = await api.invoke(auth => auth.authorizeGoogleSignin({
            idToken: credentialResponse.credential ?? ""
        }));
        logger.debug('Token: {token}', {accessToken});
    }

    function handleError() {
        console.log('Login Failed');
    }

    return (
        <>
            <Container maxWidth="sm">
                <Box sx={{textAlign: 'center', paddingTop: 4}}>
                    <Card sx={{boxShadow: 1}}>
                        <CardContent>
                            <Typography variant="h4" gutterBottom>
                                Welcome to Mr Worldwide
                            </Typography>
                            <Typography variant="body1" gutterBottom>
                                As the exclusive admin user, Jackson, you can access advanced features by logging in
                                with your Google account. Enjoy full control over content management.
                            </Typography>
                            <Box sx={{marginTop: 2}}>
                                <GoogleLogin
                                    onSuccess={handleSuccess}
                                    onError={handleError}
                                    useOneTap
                                />
                            </Box>
                            <Typography variant="body2" sx={{marginTop: 3}}>
                                Psst, Jackson! This secret login page is just for you. If you are not Jackson,
                                you must have taken a wrong turn somewhere. No worries, just head back to the home page!
                            </Typography>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </>
    );
}

export default React.memo(Login);
