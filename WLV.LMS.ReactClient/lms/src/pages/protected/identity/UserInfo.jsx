import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';

export default class UserInfo extends Component {

    constructor() {
        super();

        this.state = {
            memeberId: '',
            redirectToReferrer: false
        };

        this.back = this.back.bind(this);       
        this.CRUDService = new CRUDService();

    };

    back() {       
        this.props.history.push('/');
    }

    
    componentDidMount() {
        if (!sessionStorage.getItem('userData')) {
            this.props.history.push('/');
            return;
        }      
        
        let findRoute = 'api/Account/UserInfo';
        this.CRUDService.find(findRoute).then((result) => {
            if(result.Message!=undefined){
                window.ShowPopup(result.Message);
                return;
            }
            let responseJson = result.Result.Member;
            this.refs.RefNumber.value=responseJson.RefNumber;
            this.refs.FirstName.value=responseJson.FirstName;
            this.refs.LastName.value=responseJson.LastName;
            this.refs.SSID.value=responseJson.SSID;
            this.refs.Email.value=responseJson.Email;
            this.refs.MobileNumber.value=responseJson.MobileNumber;
            this.refs.StreetAddressFirst.value=responseJson.StreetAddressFirst;
            this.refs.StreetAddressSecond.value=responseJson.StreetAddressSecond;
            this.refs.City.value=responseJson.City;
            this.refs.State.value=responseJson.State;
            this.refs.ZipCode.value=responseJson.ZipCode;
            this.refs.Country.value=responseJson.Country;
        });

        

    }

    render() {

        return (
            <div>
                <Navigation/>
                <div className="container">

                    <div className="row">

                         <div className="col-md-12">

                            <h2 className="my-8">Member Information</h2>
                            <hr />

                            <div>
                                <div className="form-group">
                                    <label htmlFor="dname">Refrence Number</label>
                                    <input type="text" className="form-control" ref="RefNumber"
                                        placeholder="Enter Refrence Number" maxLength="10" readOnly />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">First Name</label>
                                    <input type="text" className="form-control" ref="FirstName" id="FirstName"
                                        placeholder="First Name" readOnly/>
                                        
                                </div>

                                <div className="form-group">
                                    <label htmlFor="dname">Last Name</label>
                                    <input type="text" className="form-control" ref="LastName" id="LastName"
                                        placeholder="Last Name" readOnly/>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="dname">SSID</label>
                                    <input type="text" className="form-control" ref="SSID" id="SSID"
                                        placeholder="SSID"  readOnly/>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="loc">Email-UserName</label>
                                    <input type="email" className="form-control" ref="Email" id ='Email'
                                        placeholder="Email" readOnly/>
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">MobileNumber</label>
                                    <input type="text" className="form-control" ref="MobileNumber" id ='MobileNumber'
                                        placeholder="Mobile Number" maxLength="20" readOnly/>
                                </div>
                                
                                <div className="form-group">
                                    <label htmlFor="loc">Street Address First</label>
                                    <input type="text" className="form-control" ref="StreetAddressFirst" id ='StreetAddressFirst'
                                        placeholder="Street Address First" maxLength="150" readOnly/>
                                        
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Street Address Second</label>
                                    <input type="text" className="form-control" ref="StreetAddressSecond" id ='StreetAddressSecond'
                                        placeholder="Street Address Second" maxLength="150" readOnly/>
                                        
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">City</label>
                                    <input type="text" className="form-control" ref="City" id ='City'
                                        placeholder="City"  maxLength="150" readOnly/>
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">State</label>
                                    <input type="text" className="form-control" ref="State" id ='State'
                                        placeholder="State" maxLength="150" readOnly/>
                                </div>

                                 <div className="form-group">
                                    <label htmlFor="loc">ZipCode</label>
                                    <input type="text" className="form-control" ref="ZipCode" id ='ZipCode'
                                        placeholder="ZipCode" maxLength="20" readOnly/>
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Country</label>
                                    <input type="text" className="form-control" ref="Country" id="Country"
                                        placeholder="Country"   maxLength="150" readOnly/>
                                </div>     
                                <button type="submit" className="btn btn-primary" onClick={this.back}>Back</button>

                            </div>
                        </div>
                    </div>
                    </div>
                    
                <Footer/>
                </div>
                
        );

    }

}