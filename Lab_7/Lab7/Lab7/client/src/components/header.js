import {React} from 'react';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Avatar from '@material-ui/core/Avatar';
import {NavLink} from 'react-router-dom';
import logo from '../img/logo.png';


const useStyles = makeStyles((theme) => ({
    root: {
      flexGrow: 1,
    },
    menuButton: {
      marginRight: theme.spacing(2),
    },
    title: {
      marginRight: '50px',
      '& a': {
        color: 'white'
      }
    },
    logo: {
        marginRight: '20px'
    },
    nav: {
        display: 'flex'
    },
    container: {
        justifyContent: 'space-between'
    }
}));

export default function Header() {
    const classes = useStyles();

    return(
        <div className={classes.root}>
        <AppBar position="static">
            <Toolbar className={classes.container}>
                <NavLink to='/'>
                    <Avatar alt='Commpany logo' src={logo} className={classes.logo}/>
                </NavLink>
                <nav className={classes.nav}>
                    <Typography variant="h6" className={classes.title}>
                        <NavLink to='/clients'>Клієнти</NavLink>
                    </Typography>
                    <Typography variant="h6" className={classes.title}>
                        <NavLink to='/options'>Послуги</NavLink>
                    </Typography>
                    <Typography variant="h6" className={classes.title}>
                        <NavLink to='/orders'>Замовлення</NavLink>
                    </Typography>
                </nav>
            </Toolbar>
        </AppBar>
      </div>
    )
}