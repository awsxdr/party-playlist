import React, { useState } from 'react';
import { useAsync } from 'react-async';
import { makeStyles } from '@material-ui/core/styles';
import { TextField, InputAdornment, IconButton, FormControl, Table, TableContainer, Paper, TableHead, TableBody, TableRow, TableCell } from '@material-ui/core';
import { Search, Send } from '@material-ui/icons';
import axios from 'axios';

const search = (query) =>
    axios.get('api/search', {
        params: {
            query: query,
        },
    });

const useStyles = makeStyles((theme) => ({
    albumImage: {
        maxWidth: '64px',
        maxHeight: '64px',
        border: '1px solid black',
    },
}));

const SearchPage = () => {
    const classes = useStyles();

    const [query, setQuery] = useState("");

    const { data: searchResults, run: runSearch } = useAsync({ deferFn: search });

    return (
        <>
            <FormControl fullWidth>
                <TextField
                    label="Search"
                    InputProps={{
                        startAdornment: (
                            <InputAdornment position="start">
                                <Search />
                            </InputAdornment>
                        ),
                        endAdornment: (
                            <InputAdornment position="end">
                                <IconButton color="primary" onClick={runSearch(query)}>
                                    <Send />
                                </IconButton>
                            </InputAdornment>
                        ),
                    }}
                    value={query}
                    onChange={setQuery}
                />
            </FormControl>
            {searchResults && (
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell />
                                <TableCell>Name</TableCell>
                                <TableCell>Artist</TableCell>
                                <TableCell />
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {searchResults.tracks.items.map((track) => (
                                <TableRow key={track.name}>
                                    <TableCell>
                                        <img src={track.album.images[0].url} className={classes.albumImage} />
                                    </TableCell>
                                    <TableCell>{track.name}</TableCell>
                                    <TableCell>{track.artists.map(a => a.name).join(', ')}</TableCell>
                                    <TableCell />
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
        </>
    );
};

export default SearchPage;