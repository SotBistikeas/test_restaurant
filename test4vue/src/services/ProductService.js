import axios from 'axios';
import NProgress from 'nprogress';
import apiClient from '@/services/ApiClient';

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
    return apiClient.get('/Product/GetAll?MaxResultCount=10000').catch(error => {
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
