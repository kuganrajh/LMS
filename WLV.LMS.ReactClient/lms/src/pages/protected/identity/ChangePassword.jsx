import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';

export default class ChangePassword extends Component {

    constructor() {
        super();

        this.state = {
            redirectToReferrer: false,
            errorCurrentPassword:'',
            errorNewPassword:'',
            errorConfirmPassword:'',                   
        };

        this.save = this.save.bind(this);
        this.resetRef = this.resetRef.bind(this);        
        this.validatePassword = this.validatePassword.bind(this);        
        this.CRUDService = new CRUDService();
    };


    componentDidMount() {
        if (!sessionStorage.getItem('userData')) {
            this.props.history.push('/');
            return;
        }
    }


    save() {

        let ChangePasswordModel = {
            'OldPassword': this.refs.OldPassword.value,           
            'NewPassword': this.refs.NewPassword.value,           
            'ConfirmPassword': this.refs.ConfirmPassword.value,   
        };

        this.setState({errorCurrentPassword: '' });
        this.setState({errorNewPassword: ''});
        this.setState({errorConfirmPassword: '' });
        let isValid=true;

        isValid=this.validatePassword(this.refs.NewPassword.value);
               
        if(ChangePasswordModel.NewPassword!=ChangePasswordModel.ConfirmPassword){
            this.setState({errorConfirmPassword: <span className="label label-danger">Confirm Password Does Not Match with New Password</span>  });
            isValid=false;
        }       

        if(isValid){          
            
            this.CRUDService.add('api/Account/ChangePassword', ChangePasswordModel).then((result) => {
                debugger;
                let responseJson = result;
                if(responseJson.Message!=undefined){
                    window.ShowPopup(responseJson.Message);
                    this.setState({errorCurrentPassword: <span className="label label-danger">Invalid Current Password</span>  });
                    return;
                }                
                if (!responseJson.Status.IsError) {
                    sessionStorage.clear();
                    this.props.history.push('/');
                }
                else{
                    this.setState({errorCurrentPassword: <span className="label label-danger">Invalid Current Password</span>  });                  
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
            this.setState({errorNewPassword: <span className="label label-danger">Very Weak</span>  });
                break;
            case 3:
            this.setState({errorNewPassword: <span className="label label-primary">Medium</span>  });               
                break;
            case 4:
            this.setState({errorNewPassword: <span className="label label-success">Strong</span>  });      
            isvalid=true;       
            break;
        }
        return isvalid;
    }

   

    resetRef(){
        this.refs.OldPassword.value = "";
        this.refs.NewPassword.value = "";
        this.refs.LastName.value = "";        
    }

    render() {
        return (
            <div>
                <Navigation />
                <div className="container">

                    <div className="row">

                        <div className="col-md-12">

                            <h2 className="my-8">Change Password</h2>
                            <hr />

                            <div>
                                <div className="form-group">
                                    <label htmlFor="dname">Current Password</label>
                                    <input type="text" className="form-control" ref="OldPassword"
                                        placeholder="Enter Current Password" />
                                    {this.state.errorCurrentPassword}
                                </div>

                                <div className="form-group">
                                    <label htmlFor="loc">New Password</label>
                                    <input type="password" className="form-control" ref="NewPassword" id="FirstName"
                                        placeholder="New Password" />
                                        {this.state.errorNewPassword}
                                        
                                </div>

                                <div className="form-group">
                                    <label htmlFor="dname">Confirm Password</label>
                                    <input type="password" className="form-control" ref="ConfirmPassword" id="LastName"
                                        placeholder="Enter Confirm Password" />
                                        {this.state.errorConfirmPassword}
                                </div>
                                 
                                <button type="submit" className="btn btn-primary" onClick={this.save}>Change</button>

                            </div>
                        </div>
                    </div>
                </div>
                <Footer />
            </div>
        );

    }
}

