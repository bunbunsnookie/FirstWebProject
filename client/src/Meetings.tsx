import './Meetings.css';
import { Link, useNavigate } from "react-router-dom";
import AuthContext from './contexts/AuthContext';
import React, { useContext } from 'react';


function Meetings() {
    const {isAuthenticated, username } = useContext(AuthContext);

    return (
        <div className="background_m">
            {isAuthenticated && <div className="object_m">
                <label>Дата создания пары</label>
                <label>Формат общения</label>
                <label>Ссылка на анкету собеседника</label>
                <label>Почта собеседника</label>
                <textarea placeholder="Отзыв"/>
                <div className="rating-mini">

                    <span className="active"></span>

                    <span className="active"></span>

                    <span className="active"></span>

                    <span></span>

                    <span></span>

                </div>
            </div>}
        </div>
    );
}

export default Meetings;
