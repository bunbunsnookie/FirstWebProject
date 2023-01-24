import Repository from "./Repository";

class QuestionnaireRepository extends Repository<any, any> {
    constructor() {
        super("api");
    }

    async save(data: object) {
        let result = await super.postWithData("save_questionnaire", data);
        return result;
    }

    async get(){
        let result = await super.get("questionnaire");
        return result;
    }

}

const questionnaireRepository = new QuestionnaireRepository();
export default questionnaireRepository;
