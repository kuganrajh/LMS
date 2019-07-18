import React, {Component} from 'react';
import {Redirect} from "react-router-dom";
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';


export default class ListBorrowBook extends Component {

    constructor(props) {
        super(props);
        this.state = {
            items: [],
            isLoaded: false,
        }

        this.doAction = this.doAction.bind(this);
        this.retrunbook =this.retrunbook.bind(this);
        this.CRUDService = new CRUDService();
        this.viewretrunbook = this.viewretrunbook.bind(this);
        this.viewReservation = this.viewReservation.bind(this);
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
        this.CRUDService.getAll('api/BorrowBook').then((result) => {
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
                window.ShowPopup("No Borrow activities found");
            }
            
        });
    }

    doAction(actionName,recordId) {
        if(actionName === 'delete'){
            this.CRUDService.delete('api/BorrowBook',recordId).then((result) => {
                if(result.Message!=undefined){
                    window.ShowPopup(result.Message);
                    return;
                }
                let responseJson = result;
                if(!responseJson.Status.IsError){
                    this.loadDataTable();
                    window.ShowPopup("Borrow activitiy deleted");
                } else{
                    window.ShowPopup(responseJson.Status.ErrorMessage);
                }                
            });
        } else {
            this.props.history.push('/BorrowBook/' + actionName + '/' + recordId);
        }
    }

    viewReservation(id){
        this.props.history.push('/reservebook/view/' +id);
    }
    retrunbook(id){        
        this.props.history.push('/handoverbook/add/' + id);
    }
    viewretrunbook(retrunbook){
        this.props.history.push('/handoverbook/view/' + retrunbook[0].Id);
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
      //  if(this.HasManagePower()){
            this.loadDataTable();
       // }else{
        //    this.props.history.push('/');
        //}
    }

    render() {
        let {isLoaded, items} = this.state;        
        return (
            <div>
            <Navigation />
                <div className="container">

                    <div className="row">

                        <div className="col-md-12">

                            <h1 className="my-4">Borrow Books</h1>
                            <hr/>

                            <article>
                              
                                {!isLoaded ?
                                    <div>Loading...</div>
                                    :
                                    <table className="table table-striped table-bordered" id="Tabledetails" >
                                        <thead>
                                        <tr>
                                            <th>RSV No</th>
                                            <th>Borrow Id</th>
                                            <th>ISBN No</th>
                                            <th>Title</th>
                                            <th>Borrower</th>
                                            <th>Borrowed Time</th>
                                            <th></th>
                                        </tr>
                                        </thead>

                                        <tbody>
                                        {items.map((item, i) =>
                                            <tr key={i}>
                                                <td>{item.ReserveBook!=null ?
                                                    <div onClick={() => this.viewReservation(item.ReserveBook.Id)}><p className="text-primary"> RSV{item.ReserveBook.Id}</p></div>
                                                    :""}
                                                </td>
                                                <td>B{item.Id}</td>
                                                <td><div onClick={() => this.viewbook(item.Book.Id)}><p className="text-primary">{item.Book.ISBN}</p></div></td>
                                                <td>{item.Book.Title}</td>
                                                <td><div onClick={() => this.viewmember(item.Member.Id)}><p className="text-primary">{item.Member.RefNumber} - {item.Member.FirstName} {item.Member.LastName}</p></div></td>
                                                <td>{item.CreatedAt}</td>                                            
                                                
                                                <td>                                                
                                                    <button type="button" className="btn btn-info btn-sm btn-space"
                                                            onClick={() => this.doAction('view',item.Id)}>
                                                        View</button>
                                                    {this.HasManagePower() && (item.ReturnBook ==null || item.ReturnBook.length<1 ) && item.ReserveBook==null?
                                                    
                                                            <button type="button" className="btn btn-primary btn-sm btn-space"
                                                                onClick={() => this.doAction('edit',item.Id)}>
                                                            Edit</button> 
                                                    :""}

                                                   
                                                     {this.HasManagePower() && (item.ReturnBook ==null || item.ReturnBook.length<1 ) ?
                                                    <button type="button" className="btn btn-danger btn-sm btn-space"
                                                                onClick={() => this.doAction('delete',item.Id)}>
                                                            Delete</button>
                                                     :""}

                                                        {this.HasManagePower() &&(item.ReturnBook ==null || item.ReturnBook.length<1)?
                                                            <button type="button" className="btn btn-primary btn-sm btn-space"
                                                            onClick={() => this.retrunbook(item.Id)}>
                                                            Retrun   </button>
                                                        :
                                                        ""}
                                                         {!(item.ReturnBook ==null || item.ReturnBook.length<1)?
                                                        <button type="button" className="btn btn-info btn-sm btn-space"
                                                        onClick={() => this.viewretrunbook(item.ReturnBook)}>
                                                        View Retrun   </button>
                                                        :""
                                                        }            
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
