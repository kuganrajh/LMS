export function GetGoogleBookServiceUrl() {
    let googleBookServiceUrl = "https://www.googleapis.com/books/v1/volumes?";   
    sessionStorage.setItem('BaseGoogleBookServiceUrl',googleBookServiceUrl)
    return googleBookServiceUrl;
}