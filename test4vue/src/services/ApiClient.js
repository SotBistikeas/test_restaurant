import axios from 'axios';

var _apiClient = axios.create({
  baseURL: `http://localhost:21021/api/services/app/`,
  withCredentials: false, // This is the default
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json'
  },
  timeout: 10000
});
if (localStorage.getItem('token')) {
  _apiClient.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
}
export default _apiClient;
