import axios from 'axios';
import NProgress from 'nprogress';

const apiClient = axios.create({
  baseURL: `http://localhost:21021/api/services/app/FoodIngredient`,
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
  getFoodIngredient() {
    return apiClient.get('/GetAll?MaxResultCount=1000');
  },
  getFoodIngredientById(id) {
    return apiClient.get('Get?Id=' + id);
  },
  postFoodIngredient(FoodIngredient) {
    return apiClient.post('/Create', FoodIngredient).catch(error => {
      console.log(error.message);
    });
  }
};
