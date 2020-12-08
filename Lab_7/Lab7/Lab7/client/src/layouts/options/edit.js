import { React, useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { Box } from '@material-ui/core';
import { axiosInstance as axios } from '../../config/axios';
import OptionForm from '../../components/optionForm';
import Backdrop from '../../components/backdrop';

export default function EditOptionPage() {
    const [option, setOption] = useState(null);
    const [isMessageShowed, setIsMessageShowed] = useState(false);
    const { id } = useParams();


    useEffect(() => {
        async function fetchOption(optionId) {
            const result = await axios.get(`/options/${optionId}`);
            setOption(result.data);
        }

        fetchOption(id);
    }, [id]);

    const handleSubmit = value => {
        async function updateOption(optionId) {
            await axios.put(`/options/${optionId}`, value);
            setIsMessageShowed(true);
        }
        updateOption(id);
    }

    return (
        <Box>
            {option !== null && <OptionForm handleSubmit={handleSubmit} option={option} title='Редагувати послугу' />}
            <Backdrop open={isMessageShowed} text='Послуга успішно змінена' close={() => setIsMessageShowed(false)}/>
        </Box>
    )
}