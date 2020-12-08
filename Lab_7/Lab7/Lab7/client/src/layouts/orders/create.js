import { React } from 'react';
import { axiosInstance as axios } from '../../config/axios';
import { useHistory } from 'react-router-dom';
import OrderForm from '../../components/orderForm';

export default function CreateOrderPage() {
    const history = useHistory();

    const handleCreate = async (data) => {
        await axios.post('/orders', data);
        history.push('/orders');
    }
    return <OrderForm handleSubmit={handleCreate} title='Створити замовлення' order={{
        clientId: '', optionId: '', quantity: 0, dateStart: new Date().toISOString().slice(0, 10), 
        dateFinish: new Date().toISOString().slice(0, 10)}} />
}