import axios from 'axios';

var _apiClient;
if (localStorage.getItem('token')) {
    _apiClient = axios.create({
        baseURL: `http://localhost:21021/api/services/app/`,
        withCredentials: false, // This is the default
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            Authorization: "Bearer " + localStorage.getItem('token')
        },
        timeout: 10000
    });
}
else {
    _apiClient = axios.create({
        baseURL: `http://localhost:21021/api/services/app/`,
        withCredentials: false, // This is the default
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        timeout: 10000
    });
}
export default _apiClient;