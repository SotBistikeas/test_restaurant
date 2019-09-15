import apiClient from './ApiClient';
import NProgress from 'nprogress';

apiClient.interceptors.request.use(config => {
  NProgress.start();
  return config;
});

apiClient.interceptors.response.use(response => {
  NProgress.done();
  return response;
});

export default {
  // Product Services

  getAllProducts() {
    return apiClient.get('Product/GetAll?MaxResultCount=10000').catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  getProduct(id) {
    return apiClient.get('Product/Get?Id=' + id).catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  postProduct(product) {
    return apiClient.post('Product/Create', product).catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  deleteProduct(id) {
    return apiClient.delete('Product/Delete?Id=' + id).catch(error => {
      console.log('There was an error:', error.message);
    });
  },

  // Unit Services

  getUnit() {
    return apiClient.get('UnitOfMeasure/GetAll?MaxResultCount=56').catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  getUnitById(id) {
    return apiClient.get('UnitOfMeasure/Get?Id=' + id).catch(error => {
      console.log('There was an error:', error.message);
    });
  },

  // Vat Services

  getVat() {
    return apiClient.get('VatCategory/GetAll').catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  getVatById(id) {
    return apiClient.get('VatCategory/Get?Id=' + id).catch(error => {
      console.log('There was an error:', error.message);
    });
  }
};
