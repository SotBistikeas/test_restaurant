import axios from 'axios';
import apiClient from '@/services/ApiClient';

export default {
  getVat() {
    return apiClient.get('/VatCategory/GetAll').catch(error => {
      console.log('There was an error from vat services:', error.message);
    });
  },
  getVatById(id) {
    return apiClient.get('/VatCategory/Get?Id=' + id).catch(error => {
      console.log('There was an error from vat services:', error.message);
    });
  }
};
