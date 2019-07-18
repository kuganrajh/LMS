import {GetApiUrl} from './GetApiUrl';
import { debug } from 'util';

const API_URL = GetApiUrl();

export default  class CRUDService {


    constructor() {
        this.baseURL = API_URL;
        this.userData = JSON.parse(sessionStorage.getItem('userData'));
    }

    find(route) {
        debugger;
        return new Promise((resolve, reject) => {
            fetch(this.baseURL + route, {
                method: 'GET',
                
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer '+this.userData.access_token,
                   
                },
            })
                .then((response) => response.json())
                .then((res) => {
                    if(res.Message=="Authorization has been denied for this request."){                      
                        sessionStorage.clear();
                    }
                    resolve(res);
                })
                .catch((error) => {
                    if(error.Message=="Authorization has been denied for this request."){
                        debugger;
                      sessionStorage.clear();
                    }
                    reject(error);
                });
        });
    }

    add(route,data) {
        return new Promise((resolve, reject) => {
            fetch(this.baseURL + route, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer '+this.userData.access_token
                },
                body: JSON.stringify(data)
            })
                .then((response) => response.json())
                .then((res) => {
                    debugger;
                    if(res.Message=="Authorization has been denied for this request."){
                          debugger;
                        sessionStorage.clear();
                    }
                    resolve(res);
                })
                .catch((error) => {
                    if(error.Message=="Authorization has been denied for this request."){
                        debugger;
                      sessionStorage.clear();
                    }
                    reject(error);
                });
        });
    }

    update(route,data) {
        console.log(route,data);
        return new Promise((resolve, reject) => {
            fetch(this.baseURL + route, {
                method: 'PUT',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer '+this.userData.access_token
                },
                body: JSON.stringify(data)
            })
                .then((response) => response.json())
                .then((res) => {
                    debugger;
                    if(res.Message=="Authorization has been denied for this request."){
                        sessionStorage.clear();
                    }
                    resolve(res);
                })
                .catch((error) => {
                    if(error.Message=="Authorization has been denied for this request."){
                        debugger;
                      sessionStorage.clear();
                    }
                    reject(error);
                });
        });
    }

    delete(route,recordId) {
        return new Promise((resolve, reject) => {
            fetch(this.baseURL + route + '/' + recordId, {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer '+this.userData.access_token
                }
            })
                .then((response) => response.json())
                .then((res) => {
                    if(res.Message=="Authorization has been denied for this request."){
                        sessionStorage.clear();
                    }
                    resolve(res);
                })
                .catch((error) => {
                    if(error.Message=="Authorization has been denied for this request."){
                        debugger;
                      sessionStorage.clear();
                    }
                    reject(error);
                });
        });
    }

    getAll(route) {
        return new Promise((resolve, reject) => {
                fetch(this.baseURL + route, {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer '+this.userData.access_token
                    }
                })
                .then((response) => response.json())
                .then((res) => {
                    if(res.Message=="Authorization has been denied for this request."){
                        debugger;
                        sessionStorage.clear();
                    }                   
                    resolve(res);
                })
                .catch((error) => {
                    if(error.Message=="Authorization has been denied for this request."){
                        debugger;
                      sessionStorage.clear();
                    }
                    reject(error);
                });
        });
    }


}
