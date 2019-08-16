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
  getAllFoodIngredients() {
    return apiClient.get('FoodIngredient/GetAll?MaxResultCount=1000').catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  getFoodIngredientById(id) {
    return apiClient.get('FoodIngredient/Get?Id=' + id).catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  postFoodIngredient(FoodIngredient) {
    return apiClient.post('FoodIngredient/Create', FoodIngredient).catch(error => {
      console.log('There was an error:', error.message);
    });
  },
};
