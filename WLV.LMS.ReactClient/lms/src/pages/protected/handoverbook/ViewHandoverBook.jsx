import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';

export default class ViewHandoverBook extends Component {

    constructor() {
        super();

        this.state = {
            ReturnBookId: '',
            redirectToReferrer: false,
            imageLinkUrl:'',
            ReturnBook:{},
            IsLate:false,
            LateDays:0
        };

        this.Back = this.Back.bind(this);
        this.CRUDService = new CRUDService();
        this.viewbook = this.viewbook.bind(this);
        this.viewmember = this.viewmember.bind(this);

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
   
    viewbook(id){
        this.props.history.push('/book/view/' + id);
    }
    viewmember(id){
        this.props.history.push('/member/view/' + id);
    }

    Back() {
        this.props.history.push('/handoverbook');
    }

    componentDidMount() {
        if (!sessionStorage.getItem('userData')) {
            this.props.history.push('/');
            return;
        }
        const { match: { params } } = this.props;
        this.setState({borrowbookId: params.id});
        let findRoute = 'api/ReturnBook/' + params.id;
        this.CRUDService.find(findRoute).then((result) => {
            if(result.Message!=undefined){
                window.ShowPopup(result.Message);
                return;
            }

            if(!result.Status.IsError){
                this.setState({ReturnBookId: result.Result.Id});
                this.setState({ReturnBook: result.Result});
            }else{
                window.ShowPopup(result.Status.ErrorMessage);
                return;
            }
            let responseJson = result.Result;
            this.setState({imageLinkUrl: responseJson.BorrowBook.Book.ImageLink});
            this.refs.title.value=responseJson.BorrowBook.Book.Title;
            this.refs.membername.value=responseJson.BorrowBook.Member.FirstName+ " "+responseJson.BorrowBook.Member.LastName;
            this.refs.Created.value=responseJson.CreatedAt;
            this.refs.BorrowedDate.value=responseJson.BorrowBook.CreatedAt;


            if(responseJson.LatePayment != null && responseJson.LatePayment.length>0){
                this.setState({IsLate:true});
                this.refs.NoOfDateDelays.value=responseJson.LatePayment[0].NoOfDateDelays;
                this.refs.PaymentAmount.value=responseJson.LatePayment[0].PaymentAmount;
                
            }           
        });
    }

    render() {
        let {IsLate} = this.state;
        
        return (
            <div>
                <Navigation/>
                <div className="container">

                    <div className="row">

                             <div className="col-md-8">

                            <h2 className="my-4">View Retrun Book</h2>
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
                                    <label htmlFor="dname"> Borrowed Date</label>
                                    <input type="text" className="form-control" ref="BorrowedDate" id="title"
                                        placeholder="Title" readOnly />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="dname"> Returned Date</label>
                                    <input type="text" className="form-control" ref="Created" id="title"
                                        placeholder="Title" readOnly />
                                </div>

                                {IsLate?
                                    <div className="form-group">
                                        <label htmlFor="dname"> No Of Date Delays</label>
                                        <input type="text" className="form-control" ref="NoOfDateDelays" id="title"
                                            placeholder="No Of Date Delays" readOnly />
                                    </div>
                                :""
                                }
                                {IsLate?
                                    <div className="form-group">
                                        <label htmlFor="dname"> Payment Amount</label>
                                        <input type="text" className="form-control" ref="PaymentAmount" id="title"
                                            placeholder="Payment Amount"  readOnly/>
                                    </div>
                                :""
                                }
                                 <button type="button" className="btn btn-primary btn-sm btn-space"
                                                            onClick={() => this.Back()}>
                                                        Back</button>

                                 {this.HasManagePower()?
                                    <button type="button" className="btn btn-primary btn-sm btn-space"
                                                            onClick={() => this.viewmember(this.state.ReturnBook.BorrowBook.Member.Id)}>
                                                        View Member</button>
                                :""}
                                <button type="button" className="btn btn-primary btn-sm btn-space"
                                                            onClick={() => this.viewbook(this.state.ReturnBook.BorrowBook.Book.Id)}>
                                                        View Book</button>
                            </div>
                    </div>
                
                </div>
                <Footer/>
            </div>
                
        );

    }

}
