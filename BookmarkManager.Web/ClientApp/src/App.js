import React from 'react';
import { Route } from 'react-router-dom';
import { AuthContextComponent } from './AuthContext';
import Home from './pages/Home';
import Login from './pages/Login';
import Signup from './pages/Signup';
import MyBookmarks from './pages/MyBookmarks';
import AddBookmark from './pages/AddBookmark';
import Logout from './pages/Logout';
import PrivateRoute from './components/PrivateRoute';
import Layout from './components/Layout';

const App = () => {    
    return (
        <AuthContextComponent>
            <Layout>
                <Route exact path='/' component={Home} />
                <Route exact path='/signup' component={Signup} />
                <Route exact path='/login' component={Login} />
                <PrivateRoute exact path='/logout' component={Logout} />
                <PrivateRoute exact path={`/mybookmarks`} component={MyBookmarks} />
                <PrivateRoute exact path='/addbookmark' component={AddBookmark} />
            </Layout>
        </AuthContextComponent>
    );
}

export default App;