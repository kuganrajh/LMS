import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';

export default class ViewBorrowBook extends Component {

    constructor() {
        super();

        this.state = {
            borrowbookId: '',
            redirectToReferrer: false,
            imageLinkUrl:'',
            IsRetruned:false,
            BorrowBook:{},
            IsReserved:false
        };

        this.Back = this.Back.bind(this);
        this.CRUDService = new CRUDService();
        this.viewbook = this.viewbook.bind(this);
        this.viewmember = this.viewmember.bind(this);
        this.viewbook = this.viewbook.bind(this);
        this.retrunbook =this.retrunbook.bind(this);
        this.viewretrunbook = this.viewretrunbook.bind(this);

        this.HasManagePower = this.HasManagePower.bind(this);   
        if(sessionStorage.getItem('userData'))      
        this.UserRoles = JSON.parse(sessionStorage.getItem('userData')).Roles.split(',');
        
    };

    HasManagePower(){
        if (!sessionStorage.getItem('userData')) {
            this.props.history.push('/');
            return;
        }
        return ((this.UserRoles.indexOf("Librarian") > -1) || (this.UserRoles.indexOf("Admin") > -1) )
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
        this.props.history.push('/member/view/' + id);
    }

    Back() {
        this.props.history.push('/borrowbook');
    }

    componentDidMount() {
        if (!sessionStorage.getItem('userData') ) {
            this.props.history.push('/');
            return;
        }
        const { match: { params } } = this.props;
        this.setState({borrowbookId: params.id});
        let findRoute = 'api/BorrowBook/' + params.id;
        this.CRUDService.find(findRoute).then((result) => {
            if(result.Message!=undefined){
                window.ShowPopup(result.Message);
                return;
            }

            if(!result.Status.IsError){
                this.setState({borrowbookId: result.Result.Id});
                this.setState({BorrowBook: result.Result});
            }else{
                window.ShowPopup(result.Status.ErrorMessage);
                return;
            }
            let responseJson = result.Result;
            this.setState({imageLinkUrl: responseJson.Book.ImageLink});
            this.refs.title.value=responseJson.Book.Title;
            this.refs.membername.value=responseJson.Member.FirstName+ " "+responseJson.Member.LastName;
            this.refs.Created.value= responseJson.CreatedAt;
            if(responseJson.ReturnBook!=null && responseJson.ReturnBook.length>0){
                this.setState({IsRetruned:true});
            }
            if(responseJson.ReserveBook!=null ){
                this.setState({IsReserved:true});
                this.refs.ReservedAt.value=responseJson.ReserveBook.CreatedAt;
                this.refs.ReservedRef.value="RSV"+responseJson.ReserveBook.Id;
            }

        });
    }

    render() {

        
        return (
            <div>
                <Navigation/>
                <div className="container">

                    <div className="row">

                             <div className="col-md-8">

                            <h2 className="my-4">View Borrow Book</h2>
                            <hr/>

                                <div className="form-group">
                                    <label htmlFor="loc"></label>                                    
                                         <img src={this.state.imageLinkUrl} />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="dname"> Book Title</label>
                                    <input type="text" className="form-control" ref="title" id="title"
                                        placeholder="Title" readOnly />
                                </div>
                                
                                <div className="form-group">
                                    <label htmlFor="dname"> Memeber Name</label>
                                    <input type="text" className="form-control" ref="membername" id="title"
                                        placeholder="Title" readOnly />
                                </div>
                                {this.state.IsReserved?
                                    <div className="form-group">
                                        <label htmlFor="dname"> Reserved Ref Number</label>
                                        <input type="text" className="form-control" ref="ReservedRef" id="title"
                                            placeholder="Title" readOnly />
                                    </div>
                                :""}

                                {this.state.IsReserved?
                                    <div className="form-group">
                                        <label htmlFor="dname"> Reserved Date</label>
                                        <input type="text" className="form-control" ref="ReservedAt" id="title"
                                            placeholder="Title" readOnly />
                                    </div>
                                :""}
                                 <div className="form-group">
                                    <label htmlFor="dname"> Borrowed Date</label>
                                    <input type="text" className="form-control" ref="Created" id="title"
                                        placeholder="Title" readOnly />
                                </div>

                                 <button type="button" className="btn btn-primary btn-sm btn-space"
                                                            onClick={() => this.Back()}>
                                                        Back</button>

                                 {this.HasManagePower()?
                                    <button type="button" className="btn btn-primary btn-sm btn-space"
                                                            onClick={() => this.viewmember(this.state.BorrowBook.Member.Id)}>
                                                        View Member</button>
                                :""}
                                <button type="button" className="btn btn-primary btn-sm btn-space"
                                                            onClick={() => this.viewbook(this.state.BorrowBook.Book.Id)}>
                                                        View Book</button>
                                
                               

                                {this.state.IsRetruned?
                                        <button type="button" className="btn btn-secondary btn-sm btn-space"
                                            onClick={() => this.viewretrunbook(this.state.BorrowBook.ReturnBook)}>
                                            View Retrun Details   </button>
                                :""}
                                  {!this.state.IsRetruned && this.HasManagePower() ?
                                        <button type="button" className="btn btn-success btn-sm btn-space"
                                        onClick={() => this.retrunbook(this.state.BorrowBook.Id)}>
                                        Retrun Book   </button>
                                :""}

                            </div>
                    </div>
                
                </div>
                <Footer/>
            </div>
                
        );

    }

}