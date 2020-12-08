import { React, useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { Box } from '@material-ui/core';
import { axiosInstance as axios } from '../../config/axios';
import OrderForm from '../../components/orderForm';
import Backdrop from '../../components/backdrop';

export default function EditOrderPage() {
    const [order, setOrder] = useState(null);
    const [isMessageShowed, setIsMessageShowed] = useState(false);
    const { id } = useParams();


    useEffect(() => {
        async function fetchOrder(orderId) {
            const result = await axios.get(`/orders/${orderId}`);
            result.data.dateStart = result.data.dateStart.slice(0,10);
            result.data.dateFinish = result.data.dateFinish.slice(0,10);
            setOrder(result.data);
        }

        fetchOrder(id);
    }, [id]);

    const handleSubmit = value => {
        async function updateOrder(orderId) {
            await axios.put(`/orders/${orderId}`, value);
            setIsMessageShowed(true);
        }
        updateOrder(id);
    }

    return (
        <Box>
            {order !== null && <OrderForm handleSubmit={handleSubmit} order={order} title='Редагувати замовлення' />}
            <Backdrop open={isMessageShowed} text='Замовлення успішно змінене' close={() => setIsMessageShowed(false)}/>
        </Box>
    )
}