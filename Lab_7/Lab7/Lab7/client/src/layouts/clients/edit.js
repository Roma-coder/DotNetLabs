import { React, useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { Box } from '@material-ui/core';
import { axiosInstance as axios } from '../../config/axios';
import ClientForm from '../../components/clientForm';
import Backdrop from '../../components/backdrop';

export default function Edit() {
    const [client, setClient] = useState(null);
    const [isMessageShowed, setIsMessageShowed] = useState(false);
    const { id } = useParams();


    useEffect(() => {
        async function fetchClient(clientId) {
            const result = await axios.get(`/clients/${clientId}`);
            setClient(result.data);
        }

        fetchClient(id);
    }, [id]);

    const handleSubmit = value => {
        async function updateClient(clientId) {
            await axios.put(`/clients/${clientId}`, value);
            setIsMessageShowed(true);
        }
        updateClient(id);
    }

    return (
        <Box>
            {client !== null && <ClientForm handleSubmit={handleSubmit} client={client} title='Редагувати клієнта' />}
            <Backdrop open={isMessageShowed} text='Клієнт успішно змінений' close={() => setIsMessageShowed(false)}/>
        </Box>
    )
}