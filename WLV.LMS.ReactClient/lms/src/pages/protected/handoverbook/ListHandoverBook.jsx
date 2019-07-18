import React, {Component} from 'react';
import {Redirect} from "react-router-dom";
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';


export default class ListHandoverBook extends Component {

    constructor(props) {
        super(props);
        this.state = {
            items: [],
            isLoaded: false,
        }

        this.doAction = this.doAction.bind(this);
        this.CRUDService = new CRUDService();
        this.viewbook = this.viewbook.bind(this);
        this.viewmember = this.viewmember.bind(this);
        this.HasManagePower = this.HasManagePower.bind(this);   
        if(sessionStorage.getItem('userData'))      
        this.UserRoles = JSON.parse(sessionStorage.getItem('userData')).Roles.split(',');
    }

    HasManagePower(){
        if (!sessionStorage.getItem('userData')) {
            this.props.history.push('/');
            return;
        }
        return ((this.UserRoles.indexOf("Librarian") > -1) || (this.UserRoles.indexOf("Admin") > -1) )
    }

    loadDataTable(){
        this.CRUDService.getAll('api/ReturnBook').then((result) => {
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
                window.ShowPopup("No Return activities found");
            }
            
        });
    }

    doAction(actionName,recordId) {
        if(actionName === 'delete'){
            this.CRUDService.delete('api/ReturnBook',recordId).then((result) => {
                if(result.Message!=undefined){
                    window.ShowPopup(result.Message);
                    return;
                }
                let responseJson = result;
                if(!responseJson.Status.IsError){
                    this.loadDataTable();
                    window.ShowPopup("Return activity deleted");
                } else{
                    window.ShowPopup(responseJson.Status.ErrorMessage);
                }                
            });
        } else {
            this.props.history.push('/HandoverBook/' + actionName + '/' + recordId);
        }
    }

    viewbook(id){
        this.props.history.push('/book/view/' + id);
    }
    viewmember(id){
        if(this.HasManagePower()){
            this.props.history.push('/member/view/' + id);
        }
        else{
            this.props.history.push('/userinfo');
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

                            <h1 className="my-4">Return Books</h1>
                            <hr/>

                            <article>
                              
                                {!isLoaded ?
                                    <div>Loading...</div>
                                    :
                                    <table className="table table-striped table-bordered" id="Tabledetails" >
                                        <thead>
                                        <tr>
                                            <th>Borrow Id</th>
                                            <th>ISBN NO</th>
                                            <th>Title</th>
                                            <th>Borrower</th>
                                            <th>Borrowed Time</th>
                                            <th>Retuned Time</th>
                                            <th></th>
                                        </tr>
                                        </thead>

                                        <tbody>
                                        {items.map((item, i) =>
                                            <tr key={i}>
                                                <td>B{item.BorrowBook.Id}</td>
                                                <td><div onClick={() => this.viewbook(item.BorrowBook.Book.Id)}><p className="text-primary">{item.BorrowBook.Book.ISBN}</p></div></td>
                                                <td>{item.BorrowBook.Book.Title}</td>
                                                <td><div onClick={() => this.viewmember(item.BorrowBook.Member.Id)}><p className="text-primary">{item.BorrowBook.Member.RefNumber} - {item.BorrowBook.Member.FirstName} {item.BorrowBook.Member.LastName}</p></div></td>
                                                <td>{item.BorrowBook.CreatedAt}</td>   
                                                <td>{item.CreatedAt}</td>                                            
                                                
                                                <td>                                                
                                                    <button type="button" className="btn btn-info btn-sm btn-space"
                                                            onClick={() => this.doAction('view',item.Id)}>
                                                        View</button>                                                  
                                                     {this.HasManagePower() && (item.RetrunBook == null || item.RetrunBook.Length<1 ) ?
                                                    <button type="button" className="btn btn-danger btn-sm btn-space"
                                                                onClick={() => this.doAction('delete',item.Id)}>
                                                            Delete</button>
                                                     :""}
                                                                
                                                </td>
                                            </tr>
                                        )}
                                        </tbody>

                                    </table>
                                }


                            </article>
                        </div>


                    </div>
                </div>
                <Footer/>
            </div>
        );

    }
}
