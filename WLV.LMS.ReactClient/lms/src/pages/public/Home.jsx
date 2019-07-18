import React, {Component} from 'react';
import {Redirect} from 'react-router-dom';
import Navigation from './components/Navigation'
import poems from './components/images/poems.jpg'
import hunter from './components/images/hunter.jpg'
import harvey from './components/images/harvey.jpg'
import tumblr from './components/images/tumblr.jpg'

class Home extends Component {

    constructor(){
        super();


    };

   



   render() {
      
       return (
           <div>  
                <Navigation />
                <div className="container">
                
                    <div className="row">

                        <div className="col-md-12">
                            <div className="w3-container w3-padding-32" id="projects">
                                <h3 className="w3-border-bottom w3-border-light-grey w3-padding-16">Books</h3>
                            </div>

                            <div className="w3-row-padding">
                                <div className="w3-col l3 m6 w3-margin-bottom">
                                    <div className="w3-display-container">
                                        
                                        <img src={poems} alt="House"  className="imgclass" />
                                    </div>
                                </div>
                                <div className="w3-col l3 m6 w3-margin-bottom">
                                    <div className="w3-display-container">
                                    
                                        <img src={hunter} alt="House" className="imgclass"/>
                                    </div>
                                </div>
                                <div className="w3-col l3 m6 w3-margin-bottom">
                                    <div className="w3-display-container">
                                    
                                        <img src={harvey} alt="House" className="imgclass"/>
                                    </div>
                                </div>
                                <div className="w3-col l3 m6 w3-margin-bottom">
                                    <div className="w3-display-container">
                                        
                                        <img src={tumblr} alt="House" className="imgclass"/>
                                    </div>
                                </div>
                            </div>

 
                        <div className="w3-container w3-padding-32" id="about">
                            <h3 className="w3-border-bottom w3-border-light-grey w3-padding-16">About</h3>
                            
                            <article>
                            Library Management System is a software used to manages the catalog of a library.  This helps to keep the records of whole transactions of the books available in the library.

                                AmpleTrails provides Library Management System which is very easy to use and fulfills all the requirement of a librarian. There are many features which helps librarian to keep records of available books as well as issued books. This software is available in both mode i.e. web-based or local host based.  We provide best Library Management System of this planet.
                            </article>
                            <p className="ph"><a href="http://libraryreads.org/" className="hling" target="_blank">GO to Book Sites</a></p>
                        </div>


        </div>
        </div>
        </div>
        </div>
       );

   }
}

export default Home;