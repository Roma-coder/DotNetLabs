import { React, useEffect, useState } from 'react';
import StyledTable from '../../components/table';
import { axiosInstance as axios } from '../../config/axios';
import { useHistory } from 'react-router-dom';
import Dialog from '../../components/dialog';
import Backdrop from '../../components/backdrop';
import { Typography, Button } from '@material-ui/core';

const titles = ['Ім\'я', 'Адреса', 'Номер телефону']

export default function ClientsPage() {
    const history = useHistory();

    const [data, setData] = useState([]);
    const [isModelOpen, setIsModelOpen] = useState(false);
    const [isBackdropOpen, setIsBackdopOpen] = useState(false)
    const [currentId, setCurrentId] = useState('');

    useEffect(() => {
        async function fetchData() {
            const result = await axios.get('/clients');
            setData(result.data.map(value => {
                return {
                    id: value.id,
                    name: value.fullName,
                    address: value.address,
                    phoneNumber: value.phoneNumber
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
        await axios.delete(`clients/${currentId}`);
        setIsBackdopOpen(true);
        setData(data.filter(item => item.id !== currentId));
    }

    const handleEdit = id => history.push(`/clients/${id}`)

    const handleCreate = () => {
        history.push('/clients/create');
    }
    const renderTable = () => {
        if (data.length !== 0) return <StyledTable titles={titles} rows={data}
            handleDelete={handleDelete} handleEdit={handleEdit} shouldGenerateButtons={true}/>
    };

    return (
        <div>
            <Typography variant='h4' align='center' style={{margin: '10px'}}>Клієнти</Typography>
            <Button color='primary' variant='contained' style={{display: 'block', margin: '20px auto'}} onClick={handleCreate}>
                Створити клієнта
            </Button>
            {renderTable()}
            <Dialog open={isModelOpen} text='Ви впенені що хочете видалити цього клієнта?' 
                handleClose={() => setIsModelOpen(false)} handleSubmit={handleSubmittedDelete}></Dialog>
            <Backdrop open={isBackdropOpen} text='Клієнт успішно видалений' close={() => setIsBackdopOpen(false)} />
        </div>
    )
}