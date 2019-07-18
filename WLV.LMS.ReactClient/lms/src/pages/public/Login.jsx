import React, {Component} from 'react';
import UserService from '../../services/UserService';
import {Redirect} from 'react-router-dom';

class Login extends Component {

    constructor(){
        super();

        this.state = {
          
            // username: 'devkuganrajh@gmail.com',
            // password: 'kuganRajh12#$',
            username: '',
            password: '',
            errorUsername:'',
            errorPassword:'',
            grant_type:'password',
            redirectToReferrer: false,
            errorMsg:''

        };
        this.login = this.login.bind(this);
        this.onChange = this.onChange.bind(this);

    };

    
    login() {
        debugger;
        this.setState({errorUsername: '' }); 
        this.setState({errorPassword: '' });  
        this.setState({errorMsg: '' });          

        if(this.state.username && this.state.password){
            let userService = new UserService();
            userService.userLogin('token?',this.state).then((result) => {
                let responseJson = result;
                debugger;
                if(responseJson.error == undefined){
                    sessionStorage.setItem('userData',JSON.stringify(responseJson));
                    this.setState({redirectToReferrer: true});
                }
                else{
                    this.setState({errorMsg: <span className="label label-danger">{responseJson.error_description}</span>  });
                }
            });
            console.log(this.state.errorMsg);
        }
        else{
            if(this.state.username==''){
                this.setState({errorUsername: <span className="label label-danger">UserName is required</span>  });            
            }else{
                this.setState({errorPassword: <span className="label label-danger">Password is required</span> });            
            }
        }
    }

    onChange(e){
        this.setState({[e.target.name]:e.target.value});
    }



   render() {
       if (this.state.redirectToReferrer || sessionStorage.getItem('userData')){
           return (<Redirect to={'/Home'}/>);
       }


       return (
           <div>
               <div className="container">

                   <div className="row">

                       <div className="col-md-8">

                           <h1 className="my-4">User Login</h1>
                           <hr/>

                           <article>
                                    <div className="form-group">
                                       <label htmlFor="exampleInputEmail1">Email address</label>
                                       <input type="email" className="form-control" id="username" name="username" placeholder="Enter email" onChange={this.onChange}/>
                                       {this.state.errorUsername}
                                   </div>
                                   <div className="form-group">
                                       <label htmlFor="exampleInputPassword1">Password</label>
                                       <input type="password" className="form-control" id="password" name="password"
                                              placeholder="password" onChange={this.onChange} />
                                        {this.state.errorPassword}
                                        {this.state.errorMsg}
                                   </div>
                                  

                                   <br/>
                                   <button type="submit" className="btn btn-primary" onClick={this.login}>Login</button>
                                   <br/>
                                   
                           </article>
                       </div>
                   </div>
               </div>
           </div>
       );

   }
}

export default Login;