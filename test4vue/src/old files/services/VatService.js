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
  data: {
    vat: [],
    x: {}
  },
  getVat() {
    apiClient
      .get('/VatCategory/GetAll')
      .then(responce => {
        this.x = responce.data;
      })
      .then(() => {
        this.vat = this.x.result;
      })
      .catch(error => console.log('There was an error:' + error.responce));
  }
};
