import React from 'react';
import { Container } from '@material-ui/core';
import NavMenu from './NavMenu';

const Layout = ({ children }) => {
    return (
        <div>
            <NavMenu />
            <Container>
                {children}
            </Container>
        </div>
    );
}

export default Layout;
