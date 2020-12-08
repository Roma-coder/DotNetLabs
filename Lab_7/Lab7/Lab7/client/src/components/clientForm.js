import { React } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Button, FormControl, InputLabel, Input, Typography } from '@material-ui/core';
import { Formik } from 'formik';


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
        }
    },
    title: {
        margin: '20px 0'
    }
});

export default function ClientForm({ handleSubmit, client, title }) {
    const classes = useStyles();
    return (
        <div>
            <Typography variant="h4" align='center' className={classes.title}>{title}</Typography>
            <Formik
                enableReinitialize
                initialValues={client}
                onSubmit={(values) => handleSubmit(values)}
            >
                {({ values, handleSubmit, handleChange }) => (
                    <form onSubmit={handleSubmit} className={classes.form}>
                        <FormControl>
                            <InputLabel htmlFor='name' >Прізвище</InputLabel>
                            <Input id='name' name='name' required value={values.name} onChange={handleChange} />
                        </FormControl>
                        <FormControl>
                            <InputLabel htmlFor='surname' >Ім'я</InputLabel>
                            <Input name='surname' value={values.surname} required onChange={handleChange} />
                        </FormControl>
                        <FormControl>
                            <InputLabel htmlFor='middleName' >По-батькові</InputLabel>
                            <Input name='middleName' value={values.middleName} required onChange={handleChange} />
                        </FormControl>
                        <FormControl>
                            <InputLabel htmlFor='address' >Адреса</InputLabel>
                            <Input name='address' value={values.address} required onChange={handleChange} />
                        </FormControl>
                        <FormControl>
                            <InputLabel htmlFor='phoneNumber' >Номер телефону</InputLabel>
                            <Input name="phoneNumber" value={values.phoneNumber} required onChange={handleChange} />
                        </FormControl>
                        <Button type="submit" color='primary' variant='contained'>Підтвердити</Button>
                    </form>
                )}
            </Formik>
        </div>
    )
}