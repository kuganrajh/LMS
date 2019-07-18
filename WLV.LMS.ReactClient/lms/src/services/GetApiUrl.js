export function GetApiUrl() {
    let apiUrl = null;
    let env = 'production';
    if(env === 'local'){
        apiUrl = 'http://localhost/WLV.LMS.WEBAPI/';
       
    } else if(env === 'production') {
        apiUrl = 'http://wlvlms.azurewebsites.net/';
    }
    sessionStorage.setItem('BaseURL',apiUrl)
    return apiUrl;
}


