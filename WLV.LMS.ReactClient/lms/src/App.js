import React, {Component} from 'react';
import {BrowserRouter as Router,Route} from 'react-router-dom';
import './App.css';
import Login from './pages/public/Login';
import Logout from './pages/public/Logout';
import Home from './pages/public/Home';



// memeber
import AddMember from './pages/protected/member/AddMember';
import EditMember from './pages/protected/member/EditMember';
import ViewMember from './pages/protected/member/ViewMember';
import ListMember from './pages/protected/member/ListMember';


// book
import AddBook from './pages/protected/book/AddBook';
import EditBook from './pages/protected/book/EditBook';
import ViewBook from './pages/protected/book/ViewBook';
import ListBook from './pages/protected/book/ListBook';

// borrowbook
import AddBorrowBook from './pages/protected/borrowbook/AddBorrowBook';
import AddBorrowReserveBook from './pages/protected/borrowbook/AddBorrowReserveBook';
import EditBorrowBook from './pages/protected/borrowbook/EditBorrowBook';
import ViewBorrowBook from './pages/protected/borrowbook/ViewBorrowBook';
import ListBorrowBook from './pages/protected/borrowbook/ListBorrowBook';

// Retrun book
import AddHandoverBook from './pages/protected/handoverbook/AddHandoverBook';
import ViewHandoverBook from './pages/protected/handoverbook/ViewHandoverBook';
import ListHandoverBook from './pages/protected/handoverbook/ListHandoverBook';

// Reserve book
import ViewReserveBook from './pages/protected/reservebook/ViewReserveBook';
import ListReserveBook from './pages/protected/reservebook/ListReserveBook';

// Identity
import ChangePassword from './pages/protected/identity/ChangePassword';
import UserInfo from './pages/protected/identity/UserInfo';




class App extends Component {
  render() {
    return (
      <Router>
        <div>
                    <Route exact path="/" component={Login}/>                    
                    <Route exact path="/home" component={Home}/>
                    <Route exact path="/logout" component={Logout}/>
                    <Route exact path="/changepassword" component={ChangePassword}/>
                    <Route exact path="/userinfo" component={UserInfo}/>                   

                    <Route exact path="/member/add" component={AddMember}/>
                    <Route exact path="/member/edit/:id" component={EditMember}/>
                    <Route exact path="/member/view/:id" component={ViewMember}/>
                    <Route exact={true} path="/member" strict={false} component={ListMember}/>     
                              
                    <Route exact path="/book/add" component={AddBook}/>
                    <Route exact path="/book/edit/:id" component={EditBook}/>
                    <Route exact path="/book/view/:id" component={ViewBook}/>
                    <Route exact={true} path="/book" strict={false} component={ListBook}/>

                    <Route exact path="/borrowbook/add/:id" component={AddBorrowBook}/>
                    <Route exact path="/borrowreservebook/add/:id" component={AddBorrowReserveBook}/>
                    <Route exact path="/borrowbook/edit/:id" component={EditBorrowBook}/>
                    <Route exact path="/borrowbook/view/:id" component={ViewBorrowBook}/>
                    <Route exact={true} path="/borrowbook" strict={false} component={ListBorrowBook}/>

                    <Route exact path="/handoverbook/add/:id" component={AddHandoverBook}/>
                    <Route exact path="/handoverbook/view/:id" component={ViewHandoverBook}/>
                    <Route exact={true} path="/handoverbook" strict={false} component={ListHandoverBook}/>

                    <Route exact path="/reservebook/view/:id" component={ViewReserveBook}/>
                    <Route exact={true} path="/reservebook" strict={false} component={ListReserveBook}/>


                    
        </div>
      </Router>
    );
  }
}

export default App;
