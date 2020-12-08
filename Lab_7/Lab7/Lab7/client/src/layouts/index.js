import { React, useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles';
import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import Typography from '@material-ui/core/Typography';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { axiosInstance as axios } from '../config/axios';
import Table from '../components/table';

const useStyles = makeStyles((theme) => ({
    root: {
        width: '70%',
        margin: '20px auto !important'
    },
    heading: {
        fontSize: theme.typography.pxToRem(15),
        flexBasis: '33.33%',
        flexShrink: 0,
    },
    secondaryHeading: {
        fontSize: theme.typography.pxToRem(15),
        color: theme.palette.text.secondary,
    },
}));

export default function IndexPage() {
    const [data, setData] = useState([]);
    const classes = useStyles();

    useEffect(() => {
        async function fetchData() {
            const result = await axios.get('/clients/orders');
            result.data.forEach(item => item.orders.forEach(order => {
                order.dateStart = order.dateStart.slice(0, 10);
                order.dateFinish = order.dateFinish.slice(0, 10);
            }));
            setData(result.data);
        }

        fetchData();
    }, []);

    const renderDataItems = () => {
        return data.map(item => (
            <Accordion key={item.id} className={classes.root}>
                <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}
                    
                >
                    <Typography className={classes.heading}>{item.name} {item.surname}</Typography>
                    <Typography className={classes.secondaryHeading}>Кількість замовлень: {item.count}</Typography>
                </AccordionSummary>
                <AccordionDetails>
        
                        <Table titles={['Послуга', 'Дата замовлення', 'Дата заврешення замолвення', 'Сумарна ціна', 'Кількість']}
                            rows={item.orders} shouldGenerateButtons={false} />
                </AccordionDetails>
            </Accordion>
        ))
    }

    return (
        <div>
            <Typography variant='h4' align='center' style={{ margin: '10px' }}>Фото-студія</Typography>
            {renderDataItems()}
        </div>
    )
}