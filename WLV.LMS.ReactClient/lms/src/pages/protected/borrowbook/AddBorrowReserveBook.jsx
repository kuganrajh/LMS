import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';

export default class AddBorrowReserveBook extends Component {

    constructor() {
        super();

        this.state = {
            redirectToReferrer: false,
            ReservedBookDetails:'',
            isReservedBookLoaded: false,    
            BookImageLink:''             
        };

        this.save = this.save.bind(this);
        this.loadReservedBook = this.loadReservedBook.bind(this);
        this.handleMemberChange = this.handleMemberChange.bind(this);

        this.resetRef = this.resetRef.bind(this);        
        this.CRUDService = new CRUDService();

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

    loadReservedBook(id){
        let findRoute = 'api/ReserveBook/' +id;
        this.CRUDService.find(findRoute).then((result) => {
            if(result.Message!=undefined){
                window.ShowPopup(result.Message);
                return;
            }

            let responseJson = result;
           
            if(!responseJson.Status.IsError){
                if(!responseJson.Result.IsActive){
                    window.ShowPopup("Rreservation Already Expired");
                    this.props.history.push('/reservebook/view/'+responseJson.Result.Id);
                    return
                }
                if(responseJson.Result.BorrowBook !=null && responseJson.Result.BorrowBook.length>0){
                    window.ShowPopup("Rreservation Already Borrowed");
                    this.props.history.push('/BorrowBook/view/'+responseJson.Result.BorrowBook.Id);
                    return
                }
                this.setState({
                    isReservedBookLoaded: true,
                    ReservedBookDetails: responseJson.Result,
                    BookImageLink:responseJson.Result.Book.ImageLink
                });


                this.refs.title.value=responseJson.Result.Book.Title;
                this.refs.member.value=responseJson.Result.Member.FirstName +" "+responseJson.Result.Member.LastName;
                this.refs.ReserveId.value="RSV"+responseJson.Result.Id;
                this.refs.ReserveDate.value=responseJson.Result.CreatedAt;
                this.refs.BorrowDate.value=responseJson.Result.BarrowDate;
            } else{
                window.ShowPopup("No Reservation found to Borrow");
            }            
        });
    }
    componentDidMount() {
        if (!sessionStorage.getItem('userData') ||! this.HasManagePower()) {
            this.props.history.push('/');
            return;
        }
        const { match: { params } } = this.props;
        this.loadReservedBook(params.id)
    }

    
    save() {
       
        let requestData = {
            'BookId': this.state.ReservedBookDetails.BookId,
            'MemberId':this.state.ReservedBookDetails.MemberId,
           'ReserveBookId':this.state.ReservedBookDetails.Id,
        };
           
        this.CRUDService.add('api/BorrowBook', requestData).then((result) => {
            let responseJson = result;
            if(result.Message!=undefined){
                window.ShowPopup(responseJson.Message);
                return;
            }
            if (!responseJson.Status.IsError) {
                window.ShowPopup("Reserved Book Borrowed");
                this.props.history.push('/borrowbook');
            }else{
                window.ShowPopup("Books Borrow Failed");
            } 
        });
        
    }

    resetRef(){
      
    }

    handleMemberChange(event) {
        this.setState({SelectedMemer: event.target.value});
    }
    render() {
        let {isMembersLoaded, Members} = this.state;
        return (
            <div>
                <Navigation />
                <div className="container">

                    <div className="row">

                        <div className="col-md-12">

                            <h2 className="my-8">Add Borrow Book</h2>
                            <hr />

                            <div>                                
                                
                                <div className="form-group">
                                    <label htmlFor="loc"></label>                                    
                                         <img src={this.state.BookImageLink} />
                                </div>

                                 <div className="form-group">
                                    <label htmlFor="dname"> Rreserve Ref Number</label>
                                    <input type="text" className="form-control" ref="ReserveId" id="title"
                                        placeholder="Rreserve Ref Number" readOnly />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="dname"> Book Title</label>
                                    <input type="text" className="form-control" ref="title" id="title"
                                        placeholder=" Book Title" readOnly />
                                </div>

                               
                                
                                <div className="form-group">
                                    <label htmlFor="dname"> Member Name</label>
                                    <input type="text" className="form-control" ref="member" id="title"
                                        placeholder="Member Name" readOnly />
                                </div>  

                                <div className="form-group">
                                    <label htmlFor="dname"> Reserved Date</label>
                                    <input type="text" className="form-control" ref="ReserveDate" id="title"
                                        placeholder="Member Name" readOnly />
                                </div>  

                                <div className="form-group">
                                    <label htmlFor="dname"> Borrow Date</label>
                                    <input type="text" className="form-control" ref="BorrowDate" id="title"
                                        placeholder="Member Name" readOnly />
                                </div>                                

                                <button type="submit" className="btn btn-primary" onClick={this.save}>Borrow</button>

                            </div>
                        </div>
                    </div>
                </div>
                <Footer />
            </div>
        );

    }
}

