import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';

export default class AddMember extends Component {

    constructor() {
        super();

        this.state = {
            redirectToReferrer: false,
            errorRefNumber:'',
            errorFirstName:'',
            errorLastName:'',
            errorSSID:'',
            errorEmail:'',
            errorPassword:'',
            errorCity:'',            
        };

        this.save = this.save.bind(this);
        this.resetRef = this.resetRef.bind(this);        
        this.validatePassword = this.validatePassword.bind(this);        
        this.validateEmail = this.validateEmail.bind(this);        
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

    componentDidMount() {
        if (!sessionStorage.getItem('userData') || ! this.HasManagePower()) {
            this.props.history.push('/');
            return;
        }
    }


    save() {

        let requestData = {
            'RefNumber': this.refs.RefNumber.value,           
            'FirstName': this.refs.FirstName.value,           
            'LastName': this.refs.LastName.value,           
            'SSID': this.refs.SSID.value,           
            'Email': this.refs.Email.value,                 
            'MobileNumber': this.refs.MobileNumber.value,           
            'StreetAddressFirst': this.refs.StreetAddressFirst.value,           
            'StreetAddressSecond': this.refs.StreetAddressSecond.value,           
            'City': this.refs.City.value,           
            'State': this.refs.State.value,           
            'ZipCode': this.refs.ZipCode.value,           
            'Country': this.refs.Country.value,
            'IdentityUser':{},   
            'UserId':"",      
        };
        this.setState({errorRefNumber: '' });
        this.setState({errorFirstName: ''});
        this.setState({errorLastName: '' });
        this.setState({errorSSID:'' });
        this.setState({errorEmail:'' });
        this.setState({errorPassword:'' });
        this.setState({errorCity:'' });
        let isValid=true;

        isValid=this.validatePassword(this.refs.Password.value);
       
        if(requestData.RefNumber==''){
            this.setState({errorRefNumber: <span className="label label-danger">Refrence Number required</span>  });
            isValid=false;
        }

        if(requestData.FirstName==''){
            this.setState({errorFirstName: <span className="label label-danger">First Name required</span>  });
            isValid=false;
        }

        if(requestData.LastName==''){
            this.setState({errorLastName: <span className="label label-danger">Last Name required</span>  });
            isValid=false;
        }

        if(requestData.SSID==''){
            this.setState({errorSSID: <span className="label label-danger">SSID required</span>  });
            isValid=false;
        }

        if(!this.validateEmail(requestData.Email)){
            this.setState({errorEmail: <span className="label label-danger">Email required</span>  });
            isValid=false;
        }

        if(this.refs.Password.value==''){
            this.setState({errorPassword: <span className="label label-danger">Password required</span>  });
            isValid=false;
        }

        if(requestData.City==''){
            this.setState({errorCity: <span className="label label-danger">City required</span>  });
            isValid=false;
        }

        if(isValid){
            let RegistrationModel = {
                "Email":requestData.Email,
                "Password":this.refs.Password.value,
                "ConfirmPassword":this.refs.Password.value,
                "Roles":"Member"
            }
            
            this.CRUDService.add('api/Account/Register', RegistrationModel).then((result) => {
                let responseJson = result;
                if(responseJson.Message!=undefined){
                    window.ShowPopup(responseJson.Message);
                    return;
                }
                if (!responseJson.Status.IsError) {
                    requestData.UserId = responseJson.Result;
                    this.CRUDService.add('api/member', requestData).then((result) => {
                        let responseJson = result;
                        if(responseJson.Message!=undefined){
                            window.ShowPopup(responseJson.Message);
                            return;
                        }
                        if (!responseJson.Status.IsError) {
                            window.ShowPopup("Member Created");
                            this.props.history.push('/member');
                        }
                    });
                }               

            });

            
        }
           
        
    }
     validatePassword(password) {
        let isvalid=false;        
        
        var matchedCase = new Array();
        matchedCase.push("[$@$!%*#?&]"); // Special Charector
        matchedCase.push("[A-Z]");      // Uppercase Alpabates
        matchedCase.push("[0-9]");      // Numbers
        matchedCase.push("[a-z]");     // Lowercase Alphabates

        // Check the conditions
        var ctr = 0;
        for (var i = 0; i < matchedCase.length; i++) {
            if (new RegExp(matchedCase[i]).test(password)) {
                ctr++;
            }
        }     
        switch (ctr) {
            case 0:
            case 1:
            case 2:
            this.setState({errorPassword: <span className="label label-danger">Very Weak</span>  });
                break;
            case 3:
            this.setState({errorPassword: <span className="label label-primary">Medium</span>  });               
                break;
            case 4:
            this.setState({errorPassword: <span className="label label-success">Strong</span>  });      
            isvalid=true;       
            break;
        }
        return isvalid;
    }

    validateEmail(email) {
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }

    resetRef(){
        this.refs.RefNumber.value = "";
        this.refs.FirstName.value = "";
        this.refs.LastName.value = "";
        this.refs.SSID.value = "";
        this.refs.Email.value = "";
        this.refs.Password.value = "";
        this.refs.MobileNumber.value = "";
        this.refs.StreetAddressFirst.value = "";
        this.refs.StreetAddressSecond.value = "";
        this.refs.City.value = "";
        this.refs.State.value = "";
        this.refs.ZipCode.value = "";
        this.refs.Country.value = "";
    }

    render() {
        return (
            <div>
                <Navigation />
                <div className="container">

                    <div className="row">

                        <div className="col-md-12">

                            <h2 className="my-8">Add Member</h2>
                            <hr />

                            <div>
                                <div className="form-group">
                                    <label htmlFor="dname">Refrence Number</label>
                                    <input type="text" className="form-control" ref="RefNumber"
                                        placeholder="Enter Refrence Number" maxLength="10" />
                                    {this.state.errorRefNumber}
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">First Name</label>
                                    <input type="text" className="form-control" ref="FirstName" id="FirstName"
                                        placeholder="First Name" />
                                        {this.state.errorFirstName}
                                        
                                </div>

                                <div className="form-group">
                                    <label htmlFor="dname">Last Name</label>
                                    <input type="text" className="form-control" ref="LastName" id="LastName"
                                        placeholder="Last Name" />
                                        {this.state.errorLastName}
                                </div>
                                <div className="form-group">
                                    <label htmlFor="dname">SSID-NIC</label>
                                    <input type="text" className="form-control" ref="SSID" id="SSID"
                                        placeholder="SSID" />
                                        {this.state.errorSSID}
                                </div>
                                <div className="form-group">
                                    <label htmlFor="loc">Email-UserName</label>
                                    <input type="email" className="form-control" ref="Email" id ='Email'
                                        placeholder="Email" />
                                          {this.state.errorEmail}
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Password</label>
                                    <input type="password" className="form-control" ref="Password" id ='Password'
                                        placeholder="Password" />
                                          {this.state.errorPassword}
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Mobile Number</label>
                                    <input type="text" className="form-control" ref="MobileNumber" id ='MobileNumber'
                                        placeholder="Mobile Number" maxLength="20" />
                                         {this.state.errorMobileNumber}
                                </div>
                                
                                <div className="form-group">
                                    <label htmlFor="loc">Street Address First</label>
                                    <input type="text" className="form-control" ref="StreetAddressFirst" id ='StreetAddressFirst'
                                        placeholder="Street Address First" maxLength="150" />
                                        
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Street Address Second</label>
                                    <input type="text" className="form-control" ref="StreetAddressSecond" id ='StreetAddressSecond'
                                        placeholder="Street Address Second" maxLength="150" />
                                        
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">City</label>
                                    <input type="text" className="form-control" ref="City" id ='City'
                                        placeholder="City"  maxLength="150"/>
                                         {this.state.errorCity}
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">State</label>
                                    <input type="text" className="form-control" ref="State" id ='State'
                                        placeholder="State" maxLength="150" />
                                </div>

                                 <div className="form-group">
                                    <label htmlFor="loc">ZipCode</label>
                                    <input type="text" className="form-control" ref="ZipCode" id ='ZipCode'
                                        placeholder="ZipCode" maxLength="20" />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">Country</label>
                                    <input type="text" className="form-control" ref="Country" id="Country"
                                        placeholder="Country"   maxLength="150"/>
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

