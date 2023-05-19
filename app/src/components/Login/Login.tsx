import React from 'react';
import {Box, Button, Card, CardContent, Container, Typography} from '@mui/material';
import {CredentialResponse, GoogleLogin} from '@react-oauth/google';
import {useLogger} from "../../utils/logging";
import {useAuth} from "../../utils/auth";
import {isDevelopment} from "../../utils/env";


function Login() {
    const {userManager, ...authState} = useAuth();
    const logger = useLogger(Login);
    const devMode = isDevelopment();

    async function handleSuccess(credentialResponse: CredentialResponse) {
        if (credentialResponse.credential) {
            const result = await userManager.handleGoogleSso(credentialResponse.credential);
            if (result.succeeded) {
                logger.debug('User Login Successful! Hello {name} ({email})!', {
                    name: result.user.name,
                    email: result.user.email
                });
            } else {
                logger.debug('Login failed');
            }
        }
    }

    async function attemptRefresh(){
        const result = await userManager.silentRefresh();
        if(result.succeeded){
            logger.debug("Refresh Success!")
        }
        else{
            logger.debug("Refresh failure!")
        }
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
                            {authState.isAuthenticated ?
                                <>
                                    <Typography variant="body1" gutterBottom>
                                        {`It looks like you're already signed in as ${authState.user.name}. Sign out?`}
                                </Typography>
                                    <Box sx={{marginTop: 2, justifyContent: 'center', display: 'flex'}}>
                                        <Button variant={"contained"} onClick={async ()=>await userManager.signout()}>
                                            Yea, sign me out
                                        </Button>
                                        {devMode &&
                                            <Button variant={"contained"} onClick={attemptRefresh}>
                                                Lets attempt a refresh
                                            </Button>
                                        }
                                    </Box>
                                </> :
                                <>
                                    <Typography variant="body1" gutterBottom>
                                        As the exclusive admin user, Jackson, you can access advanced features by
                                        logging in
                                        with your Google account. Enjoy full control over content management.
                                    </Typography>
                                    <Box sx={{marginTop: 2, justifyContent: 'center', display: 'flex'}}>
                                        <GoogleLogin
                                            onSuccess={handleSuccess}
                                        />
                                    </Box>
                                </>}
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
