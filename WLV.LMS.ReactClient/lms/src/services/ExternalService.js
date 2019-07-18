import {GetGoogleBookServiceUrl} from './GetGoogleBookServiceUrl';

const GoogleBookService_URL = GetGoogleBookServiceUrl();

export default  class CRUDService {


    constructor() {
        this.googleBookServiceURL = GoogleBookService_URL;
    }

    find(route) {
        return new Promise((resolve, reject) => {
            fetch(this.googleBookServiceURL + route, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
            })
                .then((response) => response.json())
                .then((res) => {
                    if(res.Message=="Unauthorized Access"){                      
                        sessionStorage.clear();
                    }
                    resolve(res);
                })
                .catch((error) => {
                    if(error.Message=="Unauthorized Access"){
                        debugger;
                      sessionStorage.clear();
                    }
                    reject(error);
                });
        });
    }
}
