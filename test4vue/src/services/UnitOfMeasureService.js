import axios from 'axios';
import apiClient from '@/services/ApiClient';

export default {
  getUnit() {
    return apiClient.get('/UnitOfMeasure/GetAll?MaxResultCount=56');
  },
  getUnitById(id) {
    return apiClient.get('/UnitOfMeasure/Get?Id=' + id);
  }
};
