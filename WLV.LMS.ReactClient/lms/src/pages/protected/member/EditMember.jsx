import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';

export default class EditMember extends Component {

    constructor() {
        super();

        this.state = {
            memeberId: '',        
            EmailId:'',
            UserId:'',   
            redirectToReferrer: false,
            errorRefNumber:'',
            errorFirstName:'',
            errorLastName:'',
            errorSSID:'',
            errorEmail:'',
            errorPassword:'',
            errorCity:'',
            IsActive :true   

        };

        this.save = this.save.bind(this);
        this.CRUDService = new CRUDService();  
        this.resetRef = this.resetRef.bind(this);
        this.back = this.back.bind(this); 
        this.ChangeStatus = this.ChangeStatus.bind(this);
        
        this.HasManagePower = this.HasManagePower.bind(this);   
        if(sessionStorage.getItem('userData'))      
        this.UserRoles = JSON.parse(sessionStorage.getItem('userData')).Roles.split(',');


    };

    ChangeStatus(){
        this.setState({IsActive:!this.state.IsActive  });    
    }
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
        const { match: { params } } = this.props;
        this.setState({memeberId: params.id});
        let findRoute = 'api/member/' + params.id;
        this.CRUDService.find(findRoute).then((result) => {
            if(result.Message!=undefined){
                window.ShowPopup(result.Message);
                return;
            }            
            let responseJson = result.Result;
            this.refs.RefNumber.value=responseJson.RefNumber;
            this.refs.FirstName.value=responseJson.FirstName;
            this.refs.LastName.value=responseJson.LastName;
            this.refs.SSID.value=responseJson.SSID;
            this.refs.MobileNumber.value=responseJson.MobileNumber;
            this.refs.StreetAddressFirst.value=responseJson.StreetAddressFirst;
            this.refs.StreetAddressSecond.value=responseJson.StreetAddressSecond;
            this.refs.City.value=responseJson.City;
            this.refs.State.value=responseJson.State;
            this.refs.ZipCode.value=responseJson.ZipCode;
            this.refs.Country.value=responseJson.Country;
            this.setState({EmailId:responseJson.Email });
            this.setState({UserId:responseJson.UserId  });
            this.setState({IsActive:responseJson.IsActive  });
        });

    }

    save() {

        let requestData = {
            'Id':this.state.memeberId,
            'RefNumber': this.refs.RefNumber.value,           
            'FirstName': this.refs.FirstName.value,           
            'LastName': this.refs.LastName.value,           
            'SSID': this.refs.SSID.value,           
            'Email': this.state.EmailId,                    
            'MobileNumber': this.refs.MobileNumber.value,           
            'StreetAddressFirst': this.refs.StreetAddressFirst.value,           
            'StreetAddressSecond': this.refs.StreetAddressSecond.value,           
            'City': this.refs.City.value,           
            'State': this.refs.State.value,           
            'ZipCode': this.refs.ZipCode.value,           
            'Country': this.refs.Country.value,
            'UserId':this.state.UserId, 
            'IsActive':this.state.IsActive,    
        };
        this.setState({errorRefNumber: '' });
        this.setState({errorFirstName: ''});
        this.setState({errorLastName: '' });
        this.setState({errorSSID:'' });
        this.setState({errorEmail:'' });
        this.setState({errorPassword:'' });
        this.setState({errorCity:'' });
        let isValid=true;

       
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

        if(requestData.Password==''){
            this.setState({errorPassword: <span className="label label-danger">Password required</span>  });
            isValid=false;
        }

        if(requestData.City==''){
            this.setState({errorCity: <span className="label label-danger">City required</span>  });
            isValid=false;
        }

        if(isValid){
            let updateRoute = 'api/member/' + this.state.memeberId;
            this.CRUDService.update(updateRoute, requestData).then((result) => {
                if(result.Message!=undefined){
                    window.ShowPopup(result.Message);
                    return;
                }
                let responseJson = result;
                if (!responseJson.Status.IsError) {
                    window.ShowPopup("Member Updated");
                    setTimeout(this.back(), 3000);
                    
                }
            });
        }
           
        
    }   
    back() {       
        this.props.history.push('/member');
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

                            <h2 className="my-8">Edit Member</h2>
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
                                    <label htmlFor="dname">SSID</label>
                                    <input type="text" className="form-control" ref="SSID" id="SSID"
                                        placeholder="SSID" />
                                        {this.state.errorSSID}
                                </div>                              

                                <div className="form-group">
                                    <label htmlFor="loc">MobileNumber</label>
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

                                <div className="form-group">
                                    <label htmlFor="loc">Member Status</label><br></br>
                                    <button type="submit" className={this.state.IsActive?"btn btn-success":"btn btn-danger"} onClick={this.ChangeStatus}>{this.state.IsActive?"Active":"Blocked"}</button>
                                </div>    
                                <button type="submit" className="btn btn-success" onClick={this.save}>Save</button>

                            </div>
                        </div>
                    </div>
                </div>
                <Footer />
            </div>
        );

    }
}

