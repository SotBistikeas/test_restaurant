import axios from 'axios';

const apiClient = axios.create({
  baseURL: `http://localhost:21021/api/services/app`,
  withCredentials: false, // This is the default
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json'
  },
  timeout: 10000
});

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
