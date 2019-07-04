import axios from 'axios';
import NProgress from 'nprogress';

const apiClient = axios.create({
  baseURL: `http://localhost:21021/api/services/app`,
  withCredentials: false, // This is the default
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json'
  },
  timeout: 10000
});

apiClient.interceptors.request.use(config => {
  NProgress.start();
  return config;
});

apiClient.interceptors.response.use(response => {
  NProgress.done();
  return response;
});
export default {
  getProducts(perPage, page) {
    return apiClient.get('/Product/GetAll?_limit=' + perPage + '&_page=' + page).catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  getAllProducts() {
    return apiClient.get('/Product/GetAll').catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  getProduct(id) {
    return apiClient.get('/Product/Get?Id=' + id).catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  postProduct(product) {
    return apiClient.post('/Product/Create', product).catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  deleteProduct(id) {
    return apiClient.delete('/Product/Delete?Id=' + id).catch(error => {
      console.log('There was an error:', error.message);
    });
  }
};
// http://localhost:21021/api/services/app/Product/Get?Id=
// http://localhost:21021/api/services/app/Product/GetAll
// http://localhost:21021/api/services/app/Product/Create
// http://localhost:21021/api/services/app/Product/Update
// http://localhost:21021/api/services/app/Product/Delete?Id=2