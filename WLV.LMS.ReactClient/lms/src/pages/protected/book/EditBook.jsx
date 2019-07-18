import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';

export default class EditBook extends Component {

    constructor() {
        super();

        this.state = {
            bookId: '',
            redirectToReferrer: false,
            errorCurrentCount:'',
            errorIsbn:'',
            errorTitle:'',
            errorCategory:'',
            imageLinkUrl:''
        };

        this.save = this.save.bind(this);
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

    save() {
        let isbn = this.refs.isbn.value;
        let title = this.refs.title.value;

        let CategoriesArray = [];
        if(this.refs.category.value!='')
         CategoriesArray =  this.refs.category.value.split(",")
        let publisher = this.refs.publisher.value;
        let publishedDate = this.refs.publishedDate.value;
        
        let book_current_count = this.refs.book_current_count.value;
        let book_total_count = this.refs.book_total_count.value;

        let authorsArray = [];
        if(this.refs.authors.value!='')
         authorsArray = this.refs.authors.value.split(",");
        let Description = this.refs.description.value;
        let InfoLink = this.refs.infolink.value;
        let ImageLink = this.state.imageLinkUrl;
        
        let Categories = Object.keys(CategoriesArray).map(function(value){
            return {Name: CategoriesArray[value]}
        });

       
        let Authors = Object.keys(authorsArray).map(function(value){
            return {Name: authorsArray[value]}
        });
        let requestData = {
            'Id':this.state.bookId,
            'ISBN': isbn,
            'Title': title.toUpperCase(),
            'Categories': Categories,
            'Publisher': publisher.toUpperCase(),
            'PublishedDate': publishedDate,
            'Authors': Authors,
            'BookCurrentCount': book_current_count,
            'BookTotalCount': book_total_count,
            'Description':Description,
            'InfoLink':InfoLink,
            'ImageLink':ImageLink,
        };

        this.setState({errorCurrentCount: '' });
        this.setState({errorIsbn: ''});
        this.setState({errorTitle: '' });
        this.setState({errorCategory:'' });

        if(requestData.BookTotalCount=='' || requestData.BookCurrentCount==''|| parseInt(requestData.BookTotalCount)<parseInt(requestData.BookCurrentCount)){
            this.setState({errorCurrentCount: <span className="label label-danger">Book current count should be less than Book Total count</span>  });
        }
        else if(requestData.ISBN==''){
            this.setState({errorIsbn: <span className="label label-danger">Isbn Number required</span>  });
        }
        else if(requestData.title==''){
            this.setState({errorTitle: <span className="label label-danger">Title required</span>  });
        }
        else if(requestData.category==''){
            this.setState({errorCategory: <span className="label label-danger">Category required</span>  });
        }
        else if(requestData.BookCurrentCount==''){
            this.setState({errorCurrentCount: <span className="label label-danger">Book Current required</span>  });
        }
        else{
            let updateRoute = 'api/book/' + this.state.bookId;
            this.CRUDService.update(updateRoute, requestData).then((result) => {
                if(result.Message!=undefined){
                    window.ShowPopup(responseJson.Message);
                    return;
                }
                let responseJson = result;
                if (!responseJson.Status.IsError) {
                    window.ShowPopup("Book Updated");
                    this.props.history.push('/book');
                }
            });
        }
    }

    componentDidMount() {
        if (!sessionStorage.getItem('userData') || ! this.HasManagePower()) {
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
                                                placeholder="Enter Isbn" />
                                            <span className="label label-danger" id="errorMsg"></span>
                                            {this.state.errorIsbn}
                                       
                                    </div>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="dname">Title</label>
                                    <input type="text" className="form-control" ref="title" id="title"
                                        placeholder="Title" />
                                        {this.state.errorTitle}
                                </div>
                                <div className="form-group">
                                    <label htmlFor="dname">Category</label>
                                    <input type="text" className="form-control" ref="category" id="category"
                                        placeholder="Category" />
                                        {this.state.errorCategory}
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Publisher</label>
                                    <input type="text" className="form-control" ref="publisher" id ='publisher'
                                        placeholder="Publisher" />
                                </div>
                               
                                <div className="form-group">
                                    <label htmlFor="loc">Published Date</label>
                                    <input type="text" className="form-control" ref="publishedDate" id ='PublishedDate'
                                        placeholder="Published Date" />
                                </div>

                                 <div className="form-group">
                                    <label htmlFor="loc">Description</label>
                                    <textarea className="form-control" ref="description" id ='Description' rows="4"> 
                                       
                                    </textarea>
                                    
                                </div>                                

                                  <div className="form-group">
                                    <label htmlFor="loc">Authors</label>
                                    <input type="text" className="form-control" ref="authors" id ='authors'
                                        placeholder="Authors" />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Info Link</label>
                                    <input type="text" className="form-control" ref="infolink" id="info_link"
                                        placeholder="Info Link"  />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Book Current Count</label>
                                    <input type="number" className="form-control" ref="book_current_count" id="book_current_count"
                                        placeholder="Book Current Count" />
                                        {this.state.errorCurrentCount}
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Book Total Count</label>
                                    <input type="number" className="form-control" ref="book_total_count"
                                        placeholder="Book Total Count" id="book_total_count" />
                                </div>

                                <button type="submit" className="btn btn-primary" onClick={this.save}>Update</button>

                            </div>
                    </div>
                </div>
                    
                <Footer/>
            </div>
                
        );

    }

}