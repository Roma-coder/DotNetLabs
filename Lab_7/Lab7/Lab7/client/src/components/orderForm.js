import { React, useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Button, FormControl, InputLabel, Input, Typography, Select, MenuItem } from '@material-ui/core';
import { Formik } from 'formik';
import { axiosInstance as axios } from '../config/axios';



const useStyles = makeStyles({
    form: {
        '& > *': {
            display: 'block',
            margin: '20px auto',
            width: 'fit-content',
            '& input': {
                width: '300px'
            },
            '& > *': {
                textAlign: 'center'
            }
        },
    },
    title: {
        margin: '20px 0'
    },
    select: {
        width: '300px'
    }
});

export default function OrderForm({ handleSubmit, order, title }) {
    const classes = useStyles();
    const [clients, setClients] = useState([]);
    const [options, setOptions] = useState([]);

    useEffect(() => {
        async function fetchClients() {
            const result = await axios.get('/clients');
            setClients(result.data);
        }
        async function fetchOptions() {
            const result = await axios.get('/options');
            setOptions(result.data);
        }

        fetchClients();
        fetchOptions();
    }, [])

    
    return (
        <div>
            <Typography variant="h4" align='center' className={classes.title}>{title}</Typography>
            <Formik
                enableReinitialize
                initialValues={order}
                onSubmit={(values) => handleSubmit(values)}
            >
                {({ values, handleSubmit, handleChange }) => (
                    <form onSubmit={handleSubmit} className={classes.form}>
                        <FormControl>
                            <InputLabel id='clientId-label' >Клієнт</InputLabel>
                            <Select labelId='clientId-label'
                                name='clientId'
                                required
                                value={values.clientId}
                                onChange={handleChange}
                                className={classes.select}
                            >
                                {clients.map(client => <MenuItem key={client.id} value={client.id}>{client.fullName}</MenuItem>)}
                            </Select>
                        </FormControl>
                        <FormControl>
                            <InputLabel id='optionId-label' >Послуга</InputLabel>
                            <Select labelId='optionId-label'
                                name='optionId'
                                required
                                value={values.optionId}
                                onChange={handleChange}
                                className={classes.select}
                            >
                                {options.map(option => <MenuItem key={option.id} value={option.id}>{option.title}</MenuItem>)}
                            </Select>
                        </FormControl>
                        <FormControl>
                            <InputLabel htmlFor='dateStart' >Дата створення замовлення</InputLabel>
                            <Input id='dateStart' name='dateStart' type='date' 
                            value={values.dateStart} required onChange={handleChange} />
                        </FormControl>
                        <FormControl>
                            <InputLabel htmlFor='dateFinish' >Дата виконання замовлення</InputLabel>
                            <Input id='dateFinish' name='dateFinish' type='date'
                                value={values.dateFinish} required onChange={handleChange} />
                        </FormControl>
                        <FormControl>
                            <InputLabel htmlFor='quantity' >Кількість</InputLabel>
                            <Input id='quantity' name='quantity' min='0' type='number' value={values.quantity} required onChange={handleChange} />
                        </FormControl>
                        <Button type="submit" color='primary' variant='contained'>Підтвердити</Button>
                    </form>
                )}
            </Formik>
        </div>
    )
}