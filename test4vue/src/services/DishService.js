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
    return apiClient.get('/Dish/GetFull?Id=' + id);
  },
  postDish(dish) {
    return apiClient.post('/Dish/Create', dish).catch(error => {
      console.log(error.message);
    });
  },
  postFoodInDish(dishId, foodIngredientId, quantity, unitOfMeasureId) {
    return apiClient.post('Dish/AddFoodIngredient?dishId=' + dishId, foodIngredientId, quantity, unitOfMeasureId);
  },  
  deleteDishById(id) {
    return apiClient.delete('Dish/Delete?Id=' + id).catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  editDish(name, id) {
    return apiClient.put('Dish/Update', name, id);
  },
  deleteFoodFromDish(dishId, foodId) {
    return apiClient.delete('Dish/RemoveFoodIngredient?dishId=' + dishId + '&foodIngredientId=' + foodId)
  },
};
