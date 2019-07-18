import React, { Component } from 'react';
export default class Footer extends Component {

    
    render() {

        return (
            <div>
                <nav className="navbar navbar-inverse">
                    <div className="navbar-header">
                        <button type="button" data-target="#navbarCollapse" data-toggle="collapse" className="navbar-toggle">
                            <span className="sr-only">Toggle navigation</span>
                            <span className="icon-bar"></span>
                            <span className="icon-bar"></span>
                            <span className="icon-bar"></span>
                        </button>
                        <a href="/home" className="navbar-brand">Home</a>
                    </div>
                    <div id="navbarCollapse" className="collapse navbar-collapse">
                        <ul className="nav navbar-nav">                           
                            <li><a href="/member">Member</a></li>
                            <li><a href="/book">Book</a></li>
                            <li><a href="/reservebook">Reserve Book</a></li>
                            <li><a href="/borrowbook">Borrow Book</a></li>                            
                            <li><a href="/handoverbook">Return Book</a></li>
                        </ul>

                        <ul className="nav navbar-nav navbar-right">

                            <li><a href="/changepassword">ChangePassword</a></li>
                            <li><a href="/logout">Logout</a></li>
                        </ul>
                    </div>
                </nav>
            </div>
        );

    }
} 