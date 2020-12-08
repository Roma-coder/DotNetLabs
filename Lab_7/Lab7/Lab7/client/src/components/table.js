import { React } from 'react'
import { withStyles, makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';

const StyledTableCell = withStyles((theme) => ({
  head: {
    backgroundColor: theme.palette.common.black,
    color: theme.palette.common.white,
  },
  body: {
    fontSize: 14,
  },
}))(TableCell);

const StyledTableRow = withStyles((theme) => ({
  root: {
    '&:nth-of-type(odd)': {
      backgroundColor: theme.palette.action.hover,
    },
  },
}))(TableRow);


const useStyles = makeStyles({
  table: {
    minWidth: 700,
  },
  container: {
    width: '70%',
    margin: '5vh auto'
  }
});

export default function StyledTable({ titles, rows, handleDelete, handleEdit, shouldGenerateButtons }) {
  const classes = useStyles();

  const generateButtons = (row) => {
    if(shouldGenerateButtons) {
      return (
        <div>
              <StyledTableCell component="th" scope="row" align='center'>
                <Button color='primary' variant='outlined' onClick={() => handleEdit(row.id)}>Редагувати</Button>
              </StyledTableCell>
              <StyledTableCell component="th" scope="row" align='center'>
                <Button color='secondary' variant='outlined' onClick={() => handleDelete(row.id)}>Видалити</Button>
              </StyledTableCell>
        </div>
      )
    }
  }

  return (
    <TableContainer component={Paper} className={classes.container}>
      <Table className={classes.table} aria-label="customized table">
        <TableHead>
          <TableRow>
            {titles.map((title, index) =>
              <StyledTableCell align='center' key={index}>
                {title}
              </StyledTableCell>
            )}
            <StyledTableCell align='center' key={Math.random()} />
            <StyledTableCell align='center' key={Math.random()} />
          </TableRow>
        </TableHead>
        <TableBody>
          {rows.map((row) => (
            <StyledTableRow key={row.id}>
              {Object.values(row).slice(shouldGenerateButtons ? 1 : 0).map((value, index) => (
                <StyledTableCell component="th" scope="row" key={index} align='center'>
                  {value}
                </StyledTableCell>
              ))}
              {generateButtons(row)}

              
            </StyledTableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}