import {GetApiUrl} from './GetApiUrl';
import {qsd} from 'qs-stringify';


const API_URL = GetApiUrl();
var qs = require('qs');
export default class UserService {

    userLogin(route, userData) {
        let BaseURL = API_URL;

        var params = {
            grant_type: 'password',
            username: userData.username,
            password: userData.password,
           
        };      
        
      

        // return new Promise((resolve, reject) => {
        //     fetch(BaseURL + route, {
        //         method: 'POST',
        //         headers: {
        //             'Accept': 'application/json',
        //            'Content-Type': 'application/x-www-form-urlencoded',
                   
        //         },
        //         body: params
        //     })
        //         .then((response) => response.json())
        //         .then((res) => {
        //             resolve(res);
        //         })
        //         .catch((error) => {
        //             reject(error);
        //         });

        // });  

       
        return new Promise((resolve, reject) => {

            fetch(BaseURL + route, {
                method: 'POST',
                headers: {                   
                    
                    'Access-Control-Allow-Origin': BaseURL,
                   'Access-Control-Allow-Credentials': 'true',
                   'Content-Type': 'application/x-www-form-urlencoded',
                },
                body: qs.stringify({
                    grant_type: 'password',
                    username: userData.username,
                    password: userData.password
                  })
            })
                .then((response) => response.json())
                .then((res) => {
                    resolve(res);
                })
                .catch((error) => {
                    reject(error);
                });

        });
    }
}
  