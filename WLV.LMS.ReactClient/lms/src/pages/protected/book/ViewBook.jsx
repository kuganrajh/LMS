import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';

export default class ViewBook extends Component {

    constructor() {
        super();

        this.state = {
            bookId: '',
            redirectToReferrer: false,
            imageLinkUrl:'',
            book_current_count:'',
            book_total_count:'',
        };

        this.Back = this.Back.bind(this);
        this.CRUDService = new CRUDService();
        this.IsBorrowButtonAvailable = this.IsBorrowButtonAvailable.bind(this);        
        this.UserRoles = JSON.parse(sessionStorage.getItem('userData')).Roles.split(',');
        this.barrow =this.barrow.bind(this);

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

    barrow(id,currcount,totalcount){
        if(currcount<1){
            window.ShowPopup('Book is not available');
        }else{
            this.props.history.push('/borrowbook/add/' + id);
        }
    }
    
    Back() {
        this.props.history.push('/book');
    }

    IsBorrowButtonAvailable(){
        return ((this.UserRoles.indexOf("Librarian") > -1) || (this.UserRoles.indexOf("Admin") > -1) )

    }
    componentDidMount() {
        if (!sessionStorage.getItem('userData') ) {
            this.props.history.push('/');
            return;
        }
        const { match: { params } } = this.props;
        this.setState({bookId: params.id});
        let findRoute = 'api/book/' + params.id;
        this.CRUDService.find(findRoute).then((result) => {
            if(result.Message!=undefined){
                window.ShowPopup(result.Message);
                return;
            }
            let responseJson = result.Result;
            console.log(responseJson)
            this.refs.isbn.value=responseJson.ISBN;
            this.refs.title.value=responseJson.Title;
            this.refs.category.value=responseJson.Category;
            this.refs.publisher.value=responseJson.Publisher;
            this.refs.book_current_count.value= responseJson.BookCurrentCount;
            this.refs.book_total_count.value=responseJson.BookTotalCount;
            this.refs.publishedDate.value=responseJson.PublishedDate;
            this.refs.description.value=responseJson.Description;
            this.refs.infolink.value=responseJson.InfoLink;
            this.setState({imageLinkUrl: responseJson.ImageLink});
            this.setState({book_current_count: responseJson.BookCurrentCount});
            this.setState({book_total_count: responseJson.BookTotalCount});
            let authors= '';
            debugger;
            if(responseJson.Authors != null && responseJson.Authors.length>0)
            {
                responseJson.Authors.forEach(element => {
                    authors= authors+element.Name+',';
                   
                });
                authors = authors.slice(0, -1);                
                this.refs.authors.value = authors;
            }

            let Category= '';
            debugger;
            if(responseJson.Categories != null && responseJson.Categories.length>0)
            {
                responseJson.Categories.forEach(element => {
                    Category= Category+element.Name+',';                   
                });
                Category = Category.slice(0, -1);                
                this.refs.category.value = Category;
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

                            <div>
                                <div className="form-group">
                                            <label htmlFor="dname">Enter Isbn number</label>
                                            <input type="text" className="form-control" ref="isbn"
                                                placeholder="Enter Isbn" readOnly />
                                            
                                       
                                    </div>
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc"></label>                                    
                                         <img src={this.state.imageLinkUrl} />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="dname">Title</label>
                                    <input type="text" className="form-control" ref="title" id="title"
                                        placeholder="Title" readOnly/>
                                       
                                </div>
                                <div className="form-group">
                                    <label htmlFor="dname">Category</label>
                                    <input type="text" className="form-control" ref="category" id="category"
                                        placeholder="Category" readOnly/>
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Publisher</label>
                                    <input type="text" className="form-control" ref="publisher" id ='publisher'
                                        placeholder="Publisher"  readOnly/>
                                </div>
                               
                                <div className="form-group">
                                    <label htmlFor="loc">Published Date</label>
                                    <input type="text" className="form-control" ref="publishedDate" id ='PublishedDate'
                                        placeholder="Published Date" readOnly />
                                </div>

                                 <div className="form-group">
                                    <label htmlFor="loc">Description</label>
                                    <textarea className="form-control" ref="description" id ='Description' rows="4" readOnly> 
                                       
                                    </textarea>
                                    
                                </div>                                

                                  <div className="form-group">
                                    <label htmlFor="loc">Authors</label>
                                    <input type="text" className="form-control" ref="authors" id ='authors'
                                        placeholder="Authors" readOnly/>
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Info Link</label>
                                    <input type="text" className="form-control" ref="infolink" id="info_link"
                                        placeholder="Info Link" readOnly />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Book Current Count</label>
                                    <input type="number" className="form-control" ref="book_current_count" id="book_current_count"
                                        placeholder="Book Current Count" readOnly/>
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Book Total Count</label>
                                    <input type="number" className="form-control" ref="book_total_count"
                                        placeholder="Book Total Count" id="book_total_count"  readOnly/>
                                </div>

                               <button type="button" className="btn btn-primary btn-sm btn-space"
                                                            onClick={() => this.Back()}>
                                                        Back</button>

                                {this.IsBorrowButtonAvailable()?
                                                        <button type="button" className="btn btn-success btn-sm btn-space"
                                                            onClick={() => this.barrow(this.state.bookId,this.state.book_current_count,this.state.book_total_count)}>
                                                          Barrow   </button>
                                                        :""}

                            </div>
                    </div>
                
                </div>
                <Footer/>
            </div>
                
        );

    }

}