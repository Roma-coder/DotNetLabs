import { React, useEffect, useState } from 'react';
import StyledTable from '../../components/table';
import { axiosInstance as axios } from '../../config/axios';
import { useHistory } from 'react-router-dom';
import Dialog from '../../components/dialog';
import Backdrop from '../../components/backdrop';
import { Typography, Button } from '@material-ui/core';

const titles = ['Клієнт', 'Послуга', 'Дата початку', 'Дата завершення', 'Кількість']

export default function OptionsPage() {
    const history = useHistory();

    const [data, setData] = useState([]);
    const [isModelOpen, setIsModelOpen] = useState(false);
    const [isBackdropOpen, setIsBackdopOpen] = useState(false)
    const [currentId, setCurrentId] = useState('');

    useEffect(() => {
        async function fetchData() {
            const result = await axios.get('/orders');
            setData(result.data.map(value => {
                return {
                    id: value.id,
                    client: value.client.fullName,
                    option: value.option.title,
                    dateStart: value.dateStart.slice(0, 10),
                    dateFinish: value.dateFinish.slice(0, 10),
                    quantity: value.quantity
                };
            }));
        };
        fetchData();
    }, []);

    const handleDelete = id => {
        setIsModelOpen(true);
        setCurrentId(id);
    }

    const handleSubmittedDelete = async () =>  {
        setIsModelOpen(false);
        await axios.delete(`orders/${currentId}`);
        setIsBackdopOpen(true);
        setData(data.filter(item => item.id !== currentId));
    }

    const handleEdit = id => history.push(`/orders/${id}`)

    const handleCreate = () => {
        history.push('/orders/create');
    }
    const renderTable = () => {
        if (data.length !== 0) return <StyledTable titles={titles} rows={data}
            handleDelete={handleDelete} handleEdit={handleEdit} shouldGenerateButtons={true}/>
    };

    return (
        <div>
            <Typography variant='h4' align='center' style={{margin: '10px'}}>Замовлення</Typography>
            <Button color='primary' variant='contained' style={{display: 'block', margin: '20px auto'}} onClick={handleCreate}>
                Створити замовлення
            </Button>
            {renderTable()}
            <Dialog open={isModelOpen} text='Ви впенені що хочете видалити це замовлення' 
                handleClose={() => setIsModelOpen(false)} handleSubmit={handleSubmittedDelete}></Dialog>
            <Backdrop open={isBackdropOpen} text='Замовлення успішно видалене' close={() => setIsBackdopOpen(false)} />
        </div>
    )
}