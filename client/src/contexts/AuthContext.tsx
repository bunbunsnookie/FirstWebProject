import React, {useState, useMemo, useEffect} from "react"
import RoleType from "../enums/RoleType"
import AuthInfo from "../objects/AuthInfo"
import AuthInput from "../objects/AuthInput"
import authRepository from "../repositories/AuthRepository"


interface IAuthContext {
    isAuthenticated: boolean,
    role: RoleType,
    login: (authInput: AuthInput) => Promise<boolean>,
    logout: () => void,
    username: string,
    check: () => void,
}

export const AuthContext = React.createContext<IAuthContext>({
    isAuthenticated: false,
    role: RoleType.Undefined,
    check(){

    },
    login(authInput: AuthInput) {
        throw Error("Отсутствует реализация метода")
    },
    logout() {
        throw Error("Отсутствует реализация метода")
    },
    username: "",
})



export const AuthContextProvider = ({ children }: any) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [username, setLogin] = useState("");
    const [role, setRole] = useState(RoleType.Undefined);

   // const updateInfo = useEffect(() => {
   //     authRepository.getInfo()
    //        .then((data) => setState(data));
    //    console.log("update");
    //})

    const setState = (authInfo: AuthInfo) => {
        setIsAuthenticated(authInfo.checkAutorization);
        setRole(authInfo.role);
        setLogin(authInfo.username);
    }

    const check = async (): Promise<void> => {
        let data = await authRepository.check();
        setState(data);
    }

    const login = async (authInput: AuthInput): Promise<boolean> => {
        let data = await authRepository.login(authInput);
        const state: AuthInfo = {
            checkAutorization: data.checkAutorization,
            role: data.role,
            username: authInput.login,
            id: data.id,
        }
        setState(state);
        return data.checkAutorization;
    }

    const logout = async (): Promise<void> => {
        let data = await authRepository.logout();
        setState(data);
    }

    return (
        <AuthContext.Provider value={{ isAuthenticated, role, login, logout, username, check }}>
            {children}
        </AuthContext.Provider>
    )
}

export default AuthContext;
