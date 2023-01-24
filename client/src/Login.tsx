import './Auth.css';
import { Link, useNavigate } from "react-router-dom";
import AuthContext from './contexts/AuthContext';
import AuthInput from './objects/AuthInput';
import React, { useContext } from 'react';
import usersRepository from "./repositories/UsersRepository";


function Login() {
    const { login, role, isAuthenticated, logout, username } = useContext(AuthContext);
    const navigate = useNavigate()
    const repository = usersRepository;
    const loginRef: React.RefObject<HTMLInputElement> = React.createRef();
    const passwordRef: React.RefObject<HTMLInputElement> = React.createRef();

    async function get_login() {
        const authInput: AuthInput = {
            login: loginRef.current?.value ?? "",
            password: passwordRef.current?.value ?? ""
        }
        const result = await login(authInput);
        if (result) navigate('/');
    }


    return (
        <div className="body">
            <div className="block">
                <div className="login">
                    <h1 className="h1_log">Вход</h1>
                    <input className="inp" type="text" ref={loginRef} required placeholder="Логин"></input>
                    <input className="inp" type="password" ref={passwordRef} required placeholder="Пароль"></input>
                    <button className="btn_auth" onClick={get_login} >Войти</button>
                    <Link className="reg" to="/auth/register">Регистрация</Link>
                </div>
            </div>
        </div>
    );
}


export default Login;
