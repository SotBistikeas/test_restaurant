import axios from 'axios';
import NProgress from 'nprogress';
import apiClient from "@/services/ApiClient"

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
