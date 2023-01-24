import './Home.css';
import { Link, useNavigate } from "react-router-dom";
import AuthContext from './contexts/AuthContext';
import React, { useContext } from 'react';
import Questionnaire from './Questionnaire';
import Meetings from './Meetings'
import roleType from "./enums/RoleType";

function Home() {
    const { login, role, isAuthenticated, logout, username, check } = useContext(AuthContext);
    const navigate = useNavigate()

    if(roleType.Undefined === role){
        check();
    }

    return (
        <div className="body_h">
            <div className="header">
                <div>
                    {isAuthenticated && <span className="login">Логин: {username}</span>}
                </div>
                {isAuthenticated ? <button className="auth" onClick={logout}>Выйти</button> : <Link className="auth" to="/auth/login">Авторизация</Link>}
            </div>
            <div className="home">
                {isAuthenticated && <Questionnaire/>}
                {isAuthenticated && <Meetings/>}
            </div>
        </div>
    );
}


export default Home;
