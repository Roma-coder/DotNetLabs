import { React } from 'react';
import { axiosInstance as axios } from '../../config/axios';
import { useHistory } from 'react-router-dom';
import OptionForm from '../../components/optionForm';

export default function CreateOptionPage() {
    const history = useHistory();

    const handleCreate = async (data) => {
        await axios.post('/options', data);
        history.push('/options');
    }
    return <OptionForm handleSubmit={handleCreate} title='Створити послугу' option={{
        title: '', description: '', price: 0
    }}/>
}