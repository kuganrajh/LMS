import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';

export default class EditBorrowBook extends Component {

    constructor() {
        super();

        this.state = {
            redirectToReferrer: false,
            BookDetails:'',
            BorrowBookDetails:'',
            isBookLoaded: false,   
            Members: [],
            isMembersLoaded: false,   
            SelectedMemer:'',    
            errorBookId: '',  
            errorMemberId: ''   
        };

        this.save = this.save.bind(this);
        this.loadMembers = this.loadMembers.bind(this);
        this.loadBook = this.loadBook.bind(this);
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

    loadMembers(){
        this.CRUDService.getAll('api/member').then((result) => {
            if(result.Message!=undefined){
                window.ShowPopup(result.Message);
                return;
            }
            let responseJson = result;
            if(!responseJson.Status.IsError){
                this.setState({
                    isMembersLoaded: true,
                    Members: responseJson.Result,
                });
            } else{
                window.ShowPopup("No Members found to Borrow");
            }            
        });
    }

    loadBook(id){
        let findRoute = 'api/BorrowBook/' +id;
        this.CRUDService.find(findRoute).then((result) => {
            if(result.Message!=undefined){
                window.ShowPopup(result.Message);
                return;
            }

            let responseJson = result;
           
            if(!responseJson.Status.IsError){
                this.setState({
                    isBookLoaded: true,
                    BookDetails: responseJson.Result.Book,
                    BorrowBookDetails:responseJson.Result,
                    SelectedMemer:responseJson.Result.MemberId
                });

                this.refs.title.value=responseJson.Result.Book.Title;
            } else{
                window.ShowPopup("No Borrow Records found");
            }            
        });
    }
    componentDidMount() {
        if (!sessionStorage.getItem('userData') || ! this.HasManagePower()) {
            this.props.history.push('/');
            return;
        }

        const { match: { params } } = this.props;
        this.loadMembers();
        this.loadBook(params.id)
       
    }

    
    save() {
       
        let requestData = {
            'Id':this.state.BorrowBookDetails.Id,
            'BookId': this.state.BookDetails.Id,
            'MemberId':this.state.SelectedMemer,
            'ReserveBookId':null
        };

        this.setState({errorBookId: '' });
        this.setState({errorMemberId: ''});

        let isValid=true;
        if(requestData.BookId==''){
            this.setState({errorBookId: <span className="label label-danger">BookId Number required</span>  });
            isValid=false;
        }
        if(requestData.MemberId==''){
            this.setState({errorMemberId: <span className="label label-danger">Member required</span>  });
            isValid=false;
        }
       

        if(isValid){
           
            let updateRoute = 'api/BorrowBook/' + this.state.BorrowBookDetails.Id;
            this.CRUDService.update(updateRoute, requestData).then((result) => {
                let responseJson = result;
                if(responseJson.Message!=undefined){
                    window.ShowPopup(responseJson.Message);
                    return;
                }
                if (!responseJson.Status.IsError) {
                    window.ShowPopup("Book Borrow Updated");
                    this.props.history.push('/borrowbook');
                }else{
                    window.ShowPopup("Books Borrow Failed");
                } 
            });

        }
    }

    resetRef(){
        this.refs.title.value = "";
        this.refs.category.value = "";
        this.refs.publisher.value = "";
        this.refs.authors.value = "";
        this.refs.infolink.value = "";
        this.refs.imageLink.value = "";
        this.refs.publishedDate.value = "";
        this.refs.description.value = "";
        this.setState({imageLinkUrl: ""});   
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

                            <h2 className="my-8">Edit Borrow Book</h2>
                            <hr />

                            <div>                                
                                
                                <div className="form-group">
                                    <label htmlFor="loc"></label>                                    
                                         <img src={this.state.BookDetails.ImageLink} />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="dname"> Book Title</label>
                                    <input type="text" className="form-control" ref="title" id="title"
                                        placeholder="Title" readOnly />
                                </div>
                                
                                <div className="form-group">
                                    <label htmlFor="dname"> Member </label>
                                    <select value={this.state.SelectedMemer} onChange={this.handleMemberChange} className="form-control" >
                                    <option value="">Please Select Member</option>
                                    {!isMembersLoaded ?
                                            <option value="None">No Members To select</option>
                                        :                                  
                                            Members.map((item, i) =>
                                            item.IsActive?
                                                <option  key={i} value={item.Id}>{item.RefNumber} - {item.FirstName} {item.LastName}</option>
                                            :""
                                            )}                                       
                                    }                                      
                                        
                                    </select>
                                    {this.state.errorMemberId}
                                </div>

                                <button type="submit" className="btn btn-primary" onClick={this.save}>Update</button>

                            </div>
                        </div>
                    </div>
                </div>
                <Footer />
            </div>
        );

    }
}