import Snackbar from '@material-ui/core/Snackbar';
import MuiAlert from '@material-ui/lab/Alert';

function Alert(props) {
    return <MuiAlert elevation={6} variant="filled" {...props} />;
}


export default function Backdrop({ text, open, close }) {
    const handleClose = (event, reason) => {
        if (reason === 'clickaway') {
            return;
        }

        close();
    };

    return (
        <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
            <Alert onClose={handleClose} severity="success">
                {text}
            </Alert>
        </Snackbar>
    )
}