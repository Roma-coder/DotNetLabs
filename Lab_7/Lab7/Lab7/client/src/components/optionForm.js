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

export default function OptionForm({ handleSubmit, option, title }) {
    const classes = useStyles();
    return (
        <div>
            <Typography variant="h4" align='center' className={classes.title}>{title}</Typography>
            <Formik
                enableReinitialize
                initialValues={option}
                onSubmit={(values) => handleSubmit(values)}
            >
                {({ values, handleSubmit, handleChange }) => (
                    <form onSubmit={handleSubmit} className={classes.form}>
                        <FormControl>
                            <InputLabel htmlFor='title' >Назва</InputLabel>
                            <Input id='title' name='title' required value={values.title} onChange={handleChange} />
                        </FormControl>
                        <FormControl>
                            <InputLabel htmlFor='description' >Опис</InputLabel>
                            <Input id='desription' name='description' value={values.description} required onChange={handleChange} />
                        </FormControl>
                        <FormControl>
                            <InputLabel htmlFor='price' >Ціна</InputLabel>
                            <Input id='price' name='price' min='0' type='number' value={values.price} required onChange={handleChange} />
                        </FormControl>
                        <Button type="submit" color='primary' variant='contained'>Підтвердити</Button>
                    </form>
                )}
            </Formik>
        </div>
    )
}