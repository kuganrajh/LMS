import React, {Component} from 'react';
import {Redirect} from "react-router-dom";
import Navigation from "../../public/components/Navigation";
import Footer from "../../public/components/Footer";
import CRUDService from '../../../services/CRUDService';


export default class ListBook extends Component {

    constructor(props) {
        super(props);
        this.state = {
            items: [],
            isLoaded: false,
        }

        this.doAction = this.doAction.bind(this);
        this.CRUDService = new CRUDService();
        this.HasManagePower = this.HasManagePower.bind(this);       
        if(sessionStorage.getItem('userData')) 
        this.UserRoles = JSON.parse(sessionStorage.getItem('userData')).Roles.split(',');

    }

    HasManagePower(){
        if (!sessionStorage.getItem('userData')) {
            this.props.history.push('/');
            return;
        }
        return ((this.UserRoles.indexOf("Librarian") > -1) || (this.UserRoles.indexOf("Admin") > -1) )
    }

    loadDataTable(){
        this.CRUDService.getAll('api/member').then((result) => {
            if(result.Message!=undefined){
                window.ShowPopup(result.Message);
                return;
            }
            let responseJson = result;
            if(!responseJson.Status.IsError){
                this.setState({
                    isLoaded: true,
                    items: responseJson.Result,
                });
                window.MakeJTable();
            } else{
                window.ShowPopup("No Members found");
            }            
        });
    }

    doAction(actionName,recordId) {
        if(actionName === 'delete'){
            this.CRUDService.delete('api/member',recordId).then((result) => {
                if(result.Message!=undefined){
                    window.ShowPopup("Unable to Delete, This Member Has Activities with our System ");
                    return;
                }
                let responseJson = result;
                if(!responseJson.Status.IsError){
                    this.loadDataTable();
                } else{
                    window.ShowPopup(responseJson.Status.ErrorMessage);
                } 
            });
        } else {
            this.props.history.push('/member/' + actionName + '/' + recordId);
        }
    }

    componentDidMount() {
        if (!sessionStorage.getItem('userData')) {
            this.props.history.push('/');
            return;
        }
        if(this.HasManagePower()){
            this.loadDataTable();
        }else{
            this.props.history.push('/userinfo');
        }       
    }

    render() {
        let {isLoaded, items} = this.state;

        return (
            <div>
            <Navigation />
                <div className="container">

                    <div className="row">

                        <div className="col-md-12">

                            <h1 className="my-4">Members</h1>
                            <hr/>

                            <article>
                                <p>
                                    <a className="btn btn-success" href="/member/add">Add New Member</a>
                                </p>

                                {!isLoaded ?
                                    <div>Loading...</div>
                                    :
                                    <table className="table table-striped table-bordered" id="Tabledetails">
                                        <thead>
                                        <tr>
                                            <th>Refrence Number</th>
                                            <th>Full Name</th>
                                            <th>Mobile Number</th>
                                            <th>Email</th>
                                            <th>Created At</th>
                                            <th>Status</th>
                                            <th></th>
                                        </tr>
                                        </thead>

                                        <tbody>
                                        {items.map((item, i) =>
                                            <tr key={i}>
                                                <td>{item.RefNumber}</td>
                                                <td>{item.FirstName} {item.LastName}</td>
                                                <td>{item.MobileNumber}</td>
                                                <td>{item.Email}</td>
                                                <td>{item.CreatedAt}</td>   
                                                <td>{ <button className={item.IsActive?"btn btn-success":"btn btn-danger"} >{item.IsActive?"Active":"Blocked"}</button>}</td>   
                                               
                                                <td>
                                                <button type="button" className="btn btn-info btn-sm btn-space"
                                                            onClick={() => this.doAction('view',item.Id)}>
                                                        View</button>
                                                
                                                    <button type="button" className="btn btn-primary btn-sm btn-space"
                                                            onClick={() => this.doAction('edit',item.Id)}>
                                                        Edit</button>
                                                    <button type="button" className="btn btn-danger btn-sm btn-space"
                                                            onClick={() => this.doAction('delete',item.Id)}>
                                                        Delete</button>
                                                        
                                                </td>
                                            </tr>
                                        )}
                                        </tbody>
                                    </table>
                                }
                            </article>
                        </div>
                    </div>
                </div>
                <Footer/>
            </div>
        );

    }
}
