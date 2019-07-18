import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';

export default class ViewReserveBook extends Component {

    constructor() {
        super();

        this.state = {
           reservebookId: '',
            redirectToReferrer: false,
            imageLinkUrl:'',
            IsBorrowed:false,
            ReserveBook:{}
        };

        this.Back = this.Back.bind(this);
        this.CRUDService = new CRUDService();
        this.viewbook = this.viewbook.bind(this);
        this.viewmember = this.viewmember.bind(this);
        this.viewbook = this.viewbook.bind(this);
        this.borrowbook =this.borrowbook.bind(this);
        this.viewborrowbook = this.viewborrowbook.bind(this);

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
        this.props.history.push('/member/view/' + id);
    }

    Back() {
        this.props.history.push('/reservebook');
    }

    componentDidMount() {
        if (!sessionStorage.getItem('userData') ) {
            this.props.history.push('/');
            return;
        }
        const { match: { params } } = this.props;
        this.setState({borrowbookId: params.id});
        let findRoute = 'api/ReserveBook/' + params.id;
        this.CRUDService.find(findRoute).then((result) => {
            if(result.Message!=undefined){
                window.ShowPopup(result.Message);
                return;
            }

            if(!result.Status.IsError){
                this.setState({reservebookId: result.Result.Id});
                this.setState({ReserveBook: result.Result});
            }else{
                window.ShowPopup(result.Status.ErrorMessage);
                return;
            }
            let responseJson = result.Result;
            this.setState({imageLinkUrl: responseJson.Book.ImageLink});
            this.refs.title.value=responseJson.Book.Title;
            this.refs.membername.value=responseJson.Member.FirstName+ " "+responseJson.Member.LastName;
            this.refs.Created.value=responseJson.CreatedAt;
            this.refs.BorrowDate.value=responseJson.BarrowDate;
            if(responseJson.BorrowBook!=null && responseJson.BorrowBook.length>0){
                this.setState({IsBorrowed:true});
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

                            <h2 className="my-4">Book Information</h2>
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

                                 <div className="form-group">
                                    <label htmlFor="dname"> Reserved Date</label>
                                    <input type="text" className="form-control" ref="Created" id="title"
                                        placeholder="Title" readOnly />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="dname"> Borrow Date</label>
                                    <input type="text" className="form-control" ref="BorrowDate" id="title"
                                        placeholder="Title" readOnly />
                                </div>

                                 <button type="button" className="btn btn-primary btn-sm btn-space"
                                                            onClick={() => this.Back()}>
                                                        Back</button>

                                 {this.HasManagePower()?
                                    <button type="button" className="btn btn-primary btn-sm btn-space"
                                                            onClick={() => this.viewmember(this.state.ReserveBook.Member.Id)}>
                                                        View Member</button>
                                :""}
                                <button type="button" className="btn btn-primary btn-sm btn-space"
                                                            onClick={() => this.viewbook(this.state.ReserveBook.Book.Id)}>
                                                        View Book</button>
                                
                               
                                { this.state.ReserveBook.IsActive && this.state.IsBorrowed?
                                            <button type="button" className="btn btn-secondary btn-sm btn-space"
                                                onClick={() => this.viewborrowbook(this.state.ReserveBook.BorrowBook)}>
                                                View Borrow Details   </button>
                                    :""
                                }
                                {!this.state.IsBorrowed && this.HasManagePower() &&  this.state.ReserveBook.IsActive?
                                        <button type="button" className="btn btn-success btn-sm btn-space"
                                        onClick={() => this.borrowbook(this.state.ReserveBook.Id)}>
                                        Borrow Book   </button>
                                    :""
                                }
                                {this.state.ReserveBook.IsActive?
                                    "": <button type="button" className="btn btn-danger btn-sm btn-space">Expired</button>
                                }

                            </div>
                    </div>
                
                </div>
                <Footer/>
            </div>
                
        );

    }

}