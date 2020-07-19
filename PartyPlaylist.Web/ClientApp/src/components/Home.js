import React from 'react';
import { Paper } from '@material-ui/core';
import { useAsync } from 'react-async';

const getSpotifyLoginStatus = async () => {
    const result = await fetch('/api/login');
    return result.ok;
}

const Home = () => {

    const { data: hasSpotifyLogin, isPending: hasSpotifyLoginPending } = useAsync({ promiseFn: getSpotifyLoginStatus });

    return (
        <Paper>
            {
                hasSpotifyLoginPending ? "Pending" 
                    : !hasSpotifyLogin ? <span>Not logged in. Please visit <a href="https://accounts.spotify.com/authorize?client_id=a633eb94dbb84deb8e8641826bc37a39&response_type=code&redirect_uri=http%3A%2F%2Flocalhost%3A62974%2Fapi%2fauthcallback&scopes=user-read-playback-state%20user-modify-playback-state%20user-read-currently-playing">this link</a> to connect a Spotify account</span>
                : "Logged in and ready to go!"
            }
        </Paper>
    );
};

export default Home;