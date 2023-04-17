import React from 'react';
import { Box, Button, Card, CardContent, Container, Typography } from '@mui/material';
import { GoogleLogin } from '@react-oauth/google';

function handleSuccess(credentialResponse: any) {
    const profile = credentialResponse.getBasicProfile();

    if (profile.getName() === 'Jackson') {
        // Grant admin and content management permissions
        console.log('Logged in successfully');
    } else {
        // Handle unsuccessful login
        console.log('Login failed: unauthorized user');
    }
}

function handleError() {
    console.log('Login Failed');
}

function Login() {
    return (
        <>
            <Container maxWidth="sm">
                <Box sx={{ textAlign: 'center', paddingTop: 4 }}>
                    <Card sx={{ boxShadow: 1 }}>
                        <CardContent>
                            <Typography variant="h4" gutterBottom>
                                Welcome to Mr Worldwide
                            </Typography>
                            <Typography variant="body1" gutterBottom>
                                As the exclusive admin user, Jackson, you can access advanced features by logging in
                                with your Google account. Enjoy full control over content management.
                            </Typography>
                            <Box sx={{ marginTop: 2 }}>
                                <GoogleLogin
                                    onSuccess={handleSuccess}
                                    onError={handleError}
                                    useOneTap
                                />
                            </Box>
                            <Typography variant="body2" sx={{ marginTop: 3 }}>
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
