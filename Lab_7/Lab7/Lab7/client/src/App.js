import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import ClientsPage from './layouts/clients/index';
import OptionsPage from './layouts/options/index';
import OrdersPage from './layouts/orders/index';
import EditOrderPage from './layouts/orders/edit';
import CreateOrderPage from './layouts/orders/create';
import Header from './components/header';
import Edit from './layouts/clients/edit';
import CreatePage from './layouts/clients/create';
import CreateOptionPage from './layouts/options/create';
import EditOptionPage from './layouts/options/edit';
import IndexPage from './layouts/index';


function App() {
  return (
    <Router>
      <Header />
      <main>
        <Switch>
          <Route exact path="/clients">
            <ClientsPage />
          </Route>
          <Route exact path="/clients/create">
            <CreatePage />
          </Route>
          <Route exact path="/clients/:id">
            <Edit />
          </Route>
          <Route exact path="/options">
            <OptionsPage />
          </Route>
          <Route exact path="/options/create">
            <CreateOptionPage />
          </Route>
          <Route exact path="/options/:id">
            <EditOptionPage />
          </Route>
          <Route exact path="/orders">
            <OrdersPage />
          </Route>
          <Route exact path="/orders/create">
            <CreateOrderPage />
          </Route>
          <Route exact path="/orders/:id">
            <EditOrderPage />
          </Route>
          <Route exact path="/">
            <IndexPage />
          </Route>

        </Switch>
      </main>
    </Router>
  );
}

export default App;
