import Repository from "./Repository";

class UsersRepository extends Repository<any, any> {
    constructor() {
        super("api");
    }

    async check(user: object) {
        let result = await super.postWithData("users", user);
        return result;
    }

}

const usersRepository = new UsersRepository();
export default usersRepository;
