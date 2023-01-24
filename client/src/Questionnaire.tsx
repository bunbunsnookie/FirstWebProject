import './Questionnaire.css';
import AuthContext from './contexts/AuthContext';
import React, {useContext, useEffect, useState} from 'react';
import questionnaireRepository from "./repositories/QuestionnaireRepository";
import Select, {MultiValue} from 'react-select';

interface InputModel{
    email: string,
    know: string[],
    wantKnow: string[],
    searchCompanion: boolean,
    wantBeMentor: boolean,
    searchMentor: boolean,
    aboutMe: string,
}

function Questionnaire() {
    const {isAuthenticated, username } = useContext(AuthContext);
    const repository = questionnaireRepository;
    const [inputField , setInputField] = useState <InputModel>({
        email: '',
        know: [],
        wantKnow: [],
        searchCompanion: false,
        wantBeMentor: false,
        searchMentor: false,
        aboutMe: '',
    })
    const [initKnow, setFieldKnow] = useState<any>([]);
    const [initWantKnow, setFieldWantKnow] = useState<any>([]);


    const inputsHandler = (e: React.ChangeEvent<HTMLInputElement>) =>{
        // @ts-ignore
        setInputField( prev => ({...prev,[e.target.name]: e.target.value}))
    }

    const options = [
        { value: '0', label: 'Как программировать на C#' },
        { value: '1', label: 'Как программировать на Vue' },
        { value: '2', label: 'Как кататься на велосипеде' },
        { value: '3', label: 'Как правильно бегать' },
        { value: '4', label: 'Как сочинить лучшую шутку' },
        { value: '5', label: 'Как испечь капкейки' },
        { value: '6', label: 'Как извлечь из книги больше информации' },
        { value: '7', label: 'Как играть на гитаре' },
        { value: '8', label: 'Как дрессировать собаку' }

    ]

    useEffect(()=>{
        repository.get().then((data: any) => {
            if(!data) return;
            function getOptionsIndex(x: string){
                return options.findIndex((value) => x == value.label)
            }
            setFieldKnow(data.know.map((item: any) => ({value: getOptionsIndex(item), label: item})));
            setFieldWantKnow(data.wantKnow.map((item: any) => ({value: getOptionsIndex(item), label: item})))
            setInputField(data);
        })
    }, []);

    const textHandler = (e: React.ChangeEvent<HTMLTextAreaElement>) =>{
        // @ts-ignore
        setInputField( prev => ({...prev,aboutMe: e.target.value}))
    }

    const checkBoxHandler = (e: React.ChangeEvent<HTMLInputElement>) =>{
        // @ts-ignore
        setInputField( prev => ({...prev,[e.target.name]: e.target.checked}))
    }


    function save(){
        repository.save(inputField)
    }

    function selectKnowChange(value: MultiValue<{label: string}>){
        setInputField( prev => ({...prev,know: value.map(item => (item.label)) }))
        setFieldKnow(value)
    }

    function selectWantKnowChange(value: MultiValue<{label: string}>){
        setInputField( prev => ({...prev,wantKnow: value.map(item => (item.label)) }))
        setFieldWantKnow(value)
    }


    return (
        <div className="background_q">
            <div>
                 <div className="object_q">
                    <label className="label_q">Анкета</label>
                    <input type="email" placeholder="Почта" onChange={inputsHandler}  name="email" value={inputField.email} className="email"/>
                    <Select
                        isMulti
                        name="know"
                        options = {options}
                        className="basic-multi-select"
                        classNamePrefix="select"
                        onChange={selectKnowChange}
                        placeholder="Знаю"
                        value = {initKnow}
                    />
                    <Select
                        isMulti
                        name="wantKnow"
                        options = {options}
                        className="basic-multi-select"
                        classNamePrefix="select"
                        onChange={selectWantKnowChange}
                        placeholder="Хочу узнать"
                        value = {initWantKnow}
                    />
                    <div className="checkbox">
                        <input onChange={checkBoxHandler} name="searchCompanion"  checked={inputField.searchCompanion} type="checkbox"/>
                        <label htmlFor="companion">Ищу собеседника</label>
                    </div>
                    <div className="checkbox">
                        <input type="checkbox" onChange={checkBoxHandler} name="wantBeMentor" checked={inputField.wantBeMentor}/>
                        <label htmlFor="want_be_mentor">Хочу быть ментором</label>
                    </div>
                    <div className="checkbox">
                        <input type="checkbox" onChange={checkBoxHandler} name="searchMentor" checked={inputField.searchMentor}/>
                        <label htmlFor="search_mentor">Ищу ментора</label>
                    </div>
                    <textarea className="about_me" placeholder="О себе" name="aboutMe" onChange={textHandler}  value={inputField.aboutMe}/>
                    <button className="save" onClick={save}>Сохранить</button>

                </div>
            </div>
            {isAuthenticated && <div>
                <button className="comp">Поиск собеседника</button>
                <button className="ment">Поиск ментора</button>
            </div>}
        </div>
    );
}

export default Questionnaire;
