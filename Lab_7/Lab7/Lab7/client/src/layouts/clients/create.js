import { React } from 'react';
import { axiosInstance as axios } from '../../config/axios';
import { useHistory } from 'react-router-dom';
import ClientForm from '../../components/clientForm';

export default function CreatePage() {
    const history = useHistory();

    const handleCreate = async (data) => {
        await axios.post('/clients', data);
        history.push('/clients');
    }
    return <ClientForm handleSubmit={handleCreate} title='Створити клієнта' client={{
        name: '', surname: '', lastname: '',
        middleName: '', address: '', phoneNumber: ''
    }}/>
}