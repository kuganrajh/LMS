import React, {Component} from 'react';
import {Redirect} from "react-router-dom";

export default class Logout extends Component {

    constructor(props) {
        super(props);
        this.logout = this.logout.bind(this);
        this.noLogout = this.noLogout.bind(this);
    }

    logout() {
        sessionStorage.clear();
        this.props.history.push('/');
    }

    noLogout(){
        this.props.history.push('/');
    }

   render() {

       if (!sessionStorage.getItem('userData')){
           return (<Redirect to={'/login'}/>)
       }


       return (
           <div>
               <div className="container">

                   <div className="row">

                       <div className="col-md-8">

                           <h1 className="my-4">Are you sure you want to logout?</h1>
                           <hr/>

                           <article>
                               <p>Click 'Yes' button to continue.</p>


                               <button type="submit" className="btn btn-primary" onClick={this.logout}>Yes</button>
                               <button type="submit" className="btn btn-secondary m-lg-2" onClick={this.noLogout}>No</button>

                           </article>
                       </div>
                   </div>
               </div>
           </div>
       );

   }
}