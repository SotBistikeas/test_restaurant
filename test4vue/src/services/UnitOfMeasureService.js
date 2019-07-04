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
  getUnit() {
    return apiClient.get('/UnitOfMeasure/GetAll?MaxResultCount=56');
  },
  getUnitById(id) {
    return apiClient.get('/UnitOfMeasure/Get?Id=' + id);
  }
};
