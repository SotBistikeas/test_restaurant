import axios from 'axios';

export default axios.create({
    baseURL: `http://localhost:21021/api/services/app/`,
    withCredentials: false, // This is the default
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
    },
    timeout: 10000
});