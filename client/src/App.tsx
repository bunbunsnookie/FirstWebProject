import React from 'react';
import Login from "./Login";
import Register from "./Register";
import Home from "./Home";
import './App.css';
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import {AuthContextProvider} from './contexts/AuthContext';

function App() {
    return (
        <AuthContextProvider>
            <div className="App">
                <Router>
                    <Routes>
                        <Route path='/' element={<Home />} />
                        <Route path='auth/register' element={<Register />} />
                        <Route path='auth/login' element={<Login />} />
                    </Routes>
                </Router>
            </div>
        </AuthContextProvider>
    );
}

export default App;
