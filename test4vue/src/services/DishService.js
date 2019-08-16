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
  getDish() {
    return apiClient.get('/Dish/GetAll?MaxResultCount=1000');
  },
  getFoodIngredientsByDish(id) {
    return apiClient.get('/Dish/GetFoodIngredients?dishId=' + id);
  },
  getDishById(id) {
    return apiClient.get('/Dish/Get?Id=' + id);
  },
  postDish(dish) {
    return apiClient.post('/Dish/Create', dish).catch(error => {
      console.log(error.message);
    });
  }
};
