import React, {Component} from 'react';
import {Redirect} from "react-router-dom";
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';


export default class ListReserveBook extends Component {

    constructor(props) {
        super(props);
        this.state = {
            items: [],
            isLoaded: false,
        }

        this.doAction = this.doAction.bind(this);
        this.borrowbook =this.borrowbook.bind(this);
        this.CRUDService = new CRUDService();
        this.viewborrowbook = this.viewborrowbook.bind(this);
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
        this.CRUDService.getAll('api/ReserveBook').then((result) => {
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
            this.CRUDService.delete('api/ReserveBook',recordId).then((result) => {
                if(result.Message!=undefined){
                    window.ShowPopup(result.Message);
                    return;
                }
                let responseJson = result;
                if(!responseJson.Status.IsError){
                    this.loadDataTable();
                    window.ShowPopup("Reserve activity Deletd");

                } else{
                    window.ShowPopup(responseJson.Status.ErrorMessage);
                }                
            });
        } else {
            this.props.history.push('/reservebook/' + actionName + '/' + recordId);
        }
    }

    borrowbook(id){        
        this.props.history.push('/borrowreservebook/add/' + id);
    }
    viewborrowbook(BorrowBook){
        this.props.history.push('/BorrowBook/view/' + BorrowBook[0].Id);
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

                            <h1 className="my-4">Reserve Books</h1>
                            <hr/>

                            <article>
                              
                                {!isLoaded ?
                                    <div>Loading...</div>
                                    :
                                    <table className="table table-striped table-bordered" id="Tabledetails" >
                                        <thead>
                                        <tr>
                                            <th>Ref Number</th>
                                            <th>ISBN NO</th>
                                            <th>Title</th>
                                            <th>Borrower</th>
                                            <th>Reserved Time</th>
                                            <th>Borrow Time</th>
                                            <th></th>
                                        </tr>
                                        </thead>

                                        <tbody>
                                        {items.map((item, i) =>
                                            <tr key={i}>
                                                 <td>RSV{item.Id}</td>
                                                <td><div onClick={() => this.viewbook(item.Book.Id)}><p className="text-primary">{item.Book.ISBN}</p></div></td>
                                                <td>{item.Book.Title}</td>
                                                <td><div onClick={() => this.viewmember(item.Member.Id)}><p className="text-primary">{item.Member.RefNumber} - {item.Member.FirstName} {item.Member.LastName}</p></div></td>
                                                <td>{item.CreatedAt}</td>  
                                                <td>{item.BarrowDate}</td>                                                                                 
                                                
                                                <td>                                                
                                                    <button type="button" className="btn btn-info btn-sm btn-space"
                                                            onClick={() => this.doAction('view',item.Id)}>
                                                        View</button>
                                                    
                                                     {this.HasManagePower() && (item.BorrowBook ==null || item.BorrowBook.length<1 ) && item.IsActive ?
                                                        <button type="button" className="btn btn-danger btn-sm btn-space"
                                                                onClick={() => this.doAction('delete',item.Id)}>
                                                            Delete</button>
                                                     :""}

                                                    { item.IsActive?
                                                        (this.HasManagePower() &&(item.BorrowBook ==null || item.BorrowBook.length<1)) ?
                                                            <button type="button" className="btn btn-success btn-sm btn-space"
                                                            onClick={() => this.borrowbook(item.Id)}>
                                                            Borrow Book   </button>
                                                        :
                                                        <button type="button" className="btn btn-primary btn-sm btn-space"
                                                        onClick={() => this.viewborrowbook(item.BorrowBook)}>
                                                        View Borrowed Book   </button>
                                                    :
                                                    <button type="button" className="btn btn-danger btn-sm btn-space">Expired</button>
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
