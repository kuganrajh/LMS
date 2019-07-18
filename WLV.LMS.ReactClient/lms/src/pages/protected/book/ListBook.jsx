import React, {Component} from 'react';
import {Redirect} from "react-router-dom";
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';


export default class ListBook extends Component {

    constructor(props) {
        super(props);
        this.state = {
            items: [],
            isLoaded: false,
        }

        this.doAction = this.doAction.bind(this);
        this.barrow =this.barrow.bind(this);
        this.CRUDService = new CRUDService();
        this.IsBorrowButtonAvailable = this.IsBorrowButtonAvailable.bind(this);       
        if(sessionStorage.getItem('userData')) 
        this.UserRoles = JSON.parse(sessionStorage.getItem('userData')).Roles.split(',');
    }

    IsBorrowButtonAvailable(){
        if (!sessionStorage.getItem('userData')) {
            this.props.history.push('/');
            return;
        }
        return ((this.UserRoles.indexOf("Librarian") > -1) || (this.UserRoles.indexOf("Admin") > -1) )

    }

    loadDataTable(){
        this.CRUDService.getAll('api/book').then((result) => {
            if(result.Message!=undefined){
                window.ShowPopup(result.Message);
                return;
            }
            let responseJson = result;
            if(!responseJson.Status.IsError){
                this.setState({
                    isLoaded: true,
                    items: responseJson.Result,
                });
                window.MakeJTable();
            } else{
                window.ShowPopup("No Books found");
            }
            
        });
    }

    doAction(actionName,recordId) {
        if(actionName === 'delete'){
            this.CRUDService.delete('api/book',recordId).then((result) => {
                if(result.Message!=undefined){
                    window.ShowPopup(result.Message);
                    return;
                }
                let responseJson = result;
                if(!responseJson.Status.IsError){                   
                    this.loadDataTable();
                    window.ShowPopup("Book Deleted");
                } else{
                    window.ShowPopup(responseJson.Status.ErrorMessage);
                }                
            });
        } else {
            this.props.history.push('/book/' + actionName + '/' + recordId);
        }
    }
    
    barrow(id,currcount){
        if(currcount<1){
            window.ShowPopup('Book is not available');
        }else{
            this.props.history.push('/borrowbook/add/' + id);
        }
    }

    componentDidMount() {
        if (!sessionStorage.getItem('userData')) {
            this.props.history.push('/');
            return;
        }
        this.loadDataTable();
    }

    render() {
        let {isLoaded, items} = this.state;

        return (
            <div>
            <Navigation />
                <div className="container">

                    <div className="row">

                        <div className="col-md-12">

                            <h1 className="my-4">Books</h1>
                            <hr/>
                            {this.IsBorrowButtonAvailable()?
                                <p>
                                    <a className="btn btn-success" href="/book/add">Add New Book</a>
                                </p>
                                :""
                            }

                                {!isLoaded ?
                                    <div>Loading...</div>
                                    :
                                    <table className="table table-striped table-bordered" id="Tabledetails" >
                                        <thead>
                                        <tr>
                                            <th>ISBN NO</th>
                                            <th>Title</th>
                                            <th>Current Count</th>
                                            <th>Total Count</th>
                                            <th>Created At</th>
                                            <th></th>
                                        </tr>
                                        </thead>

                                        <tbody>
                                        {items.map((item, i) =>
                                            <tr key={i}>
                                                <td>{item.ISBN}</td>
                                                <td>{item.Title}</td>
                                                <td>{item.BookCurrentCount}</td>
                                                <td>{item.BookTotalCount}</td>
                                                <td>{item.CreatedAt}</td>                                                
                                                <td>
                                               
                                                    <button type="button" className="btn btn-info btn-sm btn-space"
                                                            onClick={() => this.doAction('view',item.Id)}>
                                                        View</button>
                                                    {this.IsBorrowButtonAvailable()?
                                                        <button type="button" className="btn btn-primary btn-sm btn-space"
                                                            onClick={() => this.doAction('edit',item.Id)}>
                                                        Edit</button>
                                                    :""}
                                                    {this.IsBorrowButtonAvailable()?

                                                        <button type="button" className="btn btn-danger btn-sm btn-space"
                                                            onClick={() => this.doAction('delete',item.Id)}>
                                                        Delete</button>
                                                    :""}

                                                    {this.IsBorrowButtonAvailable()?
                                                        <button type="button" className="btn btn-success btn-sm btn-space"
                                                            onClick={() => this.barrow(item.Id,item.BookCurrentCount)}>
                                                          Barrow   </button>
                                                   
                                                    :""}
                                                </td>
                                            </tr>
                                        )}
                                        </tbody>

                                    </table>
                                }


                            
                        </div>


                    </div>
                </div>
               
                <Footer/>
            </div>
        );

    }
}
