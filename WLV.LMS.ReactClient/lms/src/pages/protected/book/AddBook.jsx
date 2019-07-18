import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';
import ExternalService from '../../../services/ExternalService';

export default class AddBook extends Component {

    constructor() {
        super();

        this.state = {
            redirectToReferrer: false,
            errorCurrentCount:'',
            errorIsbn:'',
            errorTitle:'',
            errorCategory:'',
            imageLinkUrl:'',
        };

        this.save = this.save.bind(this);
        this.search = this.search.bind(this);
        this.resetRef = this.resetRef.bind(this);        
        this.CRUDService = new CRUDService();
        this.ExternalService = new ExternalService();

        this.HasManagePower = this.HasManagePower.bind(this);   
        if(sessionStorage.getItem('userData'))      
        this.UserRoles = JSON.parse(sessionStorage.getItem('userData')).Roles.split(',');

    };

    HasManagePower(){
        return ((this.UserRoles.indexOf("Librarian") > -1) || (this.UserRoles.indexOf("Admin") > -1) )
    }

    componentDidMount() {
        if (!sessionStorage.getItem('userData') || ! this.HasManagePower()) {
            this.props.history.push('/');
            return;
        }
    }

    
    save() {
        let isbn = this.refs.isbn.value;
        let title = this.refs.title.value;

        let CategoriesArray = [];
        if(this.refs.category.value!='')
         CategoriesArray =  this.refs.category.value.split(", ")
        let publisher = this.refs.publisher.value;
        let publishedDate = this.refs.publishedDate.value;
        
        let book_current_count = this.refs.book_current_count.value;
        let book_total_count = this.refs.book_total_count.value;

        let authorsArray = [];
        if(this.refs.authors.value!='')
         authorsArray = this.refs.authors.value.split(", ");
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
        this.setState({errorCategory:'' });

        if(requestData.BookTotalCount<requestData.BookCurrentCount){
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
            debugger;
            this.CRUDService.add('api/book', requestData).then((result) => {
                if(result.Message!=undefined){
                    window.ShowPopup(result.Message);
                    return;
                }
                let responseJson = result;
                if(result.Message!=undefined){
                    window.ShowPopup(result.Message);
                    return;
                }
                if (!responseJson.Status.IsError) {
                    window.ShowPopup("Book Created");
                    this.props.history.push('/book');
                }
            });
        }
           
        
    }

    search() {        
        let findRoute = "q=isbn:"+ this.refs.isbn.value
        this.ExternalService.find(findRoute).then((result) => {
            let responseJson = result;
            if(responseJson.totalItems==undefined &&  responseJson.error!=undefined ){
                this.resetRef();
                window.ShowPopup(responseJson.error.message);
                return;
            }else if(responseJson.totalItems<1 ){
                this.resetRef();
                window.ShowPopup("No Books found with this ISBN number Please check your ISBN Or Please fill the Form manualy");
                return;
            }
            // console.log(responseJson)
            this.refs.title.value = responseJson.items[0].volumeInfo.title;
            if(responseJson.items[0].volumeInfo.categories!=undefined)
            this.refs.category.value = responseJson.items[0].volumeInfo.categories.join(", ");
            if(responseJson.items[0].volumeInfo.publisher!=undefined)
            this.refs.publisher.value = responseJson.items[0].volumeInfo.publisher;
            if(responseJson.items[0].volumeInfo.authors!=undefined)
            this.refs.authors.value = responseJson.items[0].volumeInfo.authors.join(", ");
            this.refs.infolink.value = responseJson.items[0].volumeInfo.infoLink;
            this.refs.imageLink.value = responseJson.items[0].volumeInfo.imageLinks.thumbnail;
            this.refs.publishedDate.value = responseJson.items[0].volumeInfo.publishedDate;
            this.refs.description.value = responseJson.items[0].volumeInfo.description;
            this.setState({imageLinkUrl: responseJson.items[0].volumeInfo.imageLinks.thumbnail });            
            
        });     
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

    render() {
        return (
            <div>
                <Navigation />
                <div className="container">

                    <div className="row">

                        <div className="col-md-12">

                            <h2 className="my-8">Add Book</h2>
                            <hr />

                            <div>
                                <div className="form-group">
                                    <label htmlFor="dname">Enter Isbn number</label>
                                    <input type="text" className="form-control" ref="isbn"
                                        placeholder="Enter Isbn" />
                                    {this.state.errorIsbn}
                                    <button className="btn btn-primary pull-right" onClick={this.search}>Search</button>
                                 
                                    
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc"></label>
                                    <input type="hidden" className="form-control" ref="imageLink" id="image_link"
                                        placeholder="Image Link"   />
                                         <img src={this.state.imageLinkUrl} />
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
                                    <textarea className="form-control" ref="description" id ='Description' rows="4" placeholder="Description"> 
                                       
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
                                        placeholder="Enter location" />
                                        {this.state.errorCurrentCount}
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Book Total Count</label>
                                    <input type="number" className="form-control" ref="book_total_count"
                                        placeholder="Book Total Count" id="book_total_count" />
                                </div>
                                <button type="submit" className="btn btn-primary" onClick={this.save}>Save</button>

                            </div>
                        </div>
                    </div>
                </div>
                <Footer />
            </div>
        );

    }
}

