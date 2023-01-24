import RoleType from "../enums/RoleType"

type AuthInfo = {
    checkAutorization: boolean;
    role: RoleType;
    username: string;
    id: string;
}

export default AuthInfo;
