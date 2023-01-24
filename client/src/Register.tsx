import './Auth.css';
import {Link, useNavigate} from "react-router-dom";

function Register(){
    const navigate = useNavigate()

    return (
        <div className="body">
            <div className="block">
                <div className="login">
                    <h1 className="h1_log">Регистрация</h1>
                    <input className="inp" type="text" id="login" required placeholder="Логин"></input>
                    <input className="inp" type="password" id="password" required placeholder="Пароль"></input>
                    <input className="inp" type="password" required placeholder="Повторите Пароль"></input>
                    <button className="btn_auth" onClick={() => {navigate("/")}} >Зарегистрироваться</button>
                    <Link className="reg" to="/auth/login">Логин</Link>
                </div>
            </div>
        </div>

    );
}

export default Register;
