import React, { useEffect } from 'react';
import { useAuthContext } from '../AuthContext';
import { useHistory } from 'react-router-dom';
import axios from 'axios';

const Logout = () => {
    const history = useHistory();
    const { setUser } = useAuthContext();
    useEffect(() => {
        const doLogout = async () => {
            setUser(null);
            await axios.post('/api/account/logout');
        }
        doLogout();
        history.push('/');
    }, [])
    return (<></>);
}

export default Logout;