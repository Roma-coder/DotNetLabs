import { React } from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogTitle from '@material-ui/core/DialogTitle';

export default function AlertDialog({ handleSubmit, handleClose, text, open }) {
    return (
            <Dialog
                open={open}
                onClose={handleClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">{text}</DialogTitle>
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Cкасувати
                    </Button>
                    <Button onClick={handleSubmit} color="primary" autoFocus>
                        Підтвердити
                    </Button>
                </DialogActions>
            </Dialog>
    );
}