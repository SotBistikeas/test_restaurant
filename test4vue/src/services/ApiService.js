import axios from 'axios';
import NProgress from 'nprogress';

const apiClient = axios.create({
  baseURL: `http://localhost:21021/api/services/app/`,
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
  // Dish Services

  getDish() {
    return apiClient.get('Dish/GetAll?MaxResultCount=1000');
  },
  getFoodIngredientsByDish(id) {
    return apiClient.get('Dish/GetFoodIngredients?dishId=' + id);
  },
  getDishById(id) {
    return apiClient.get('Dish/GetFull?Id=' + id);
  },
  postDish(dish) {
    return apiClient.post('Dish/Create', dish).catch(error => {
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

  // Product Services

  getAllProducts() {
    return apiClient.get('Product/GetAll?MaxResultCount=10000').catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  getProduct(id) {
    return apiClient.get('Product/Get?Id=' + id).catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  postProduct(product) {
    return apiClient.post('Product/Create', product).catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  deleteProduct(id) {
    return apiClient.delete('Product/Delete?Id=' + id).catch(error => {
      console.log('There was an error:', error.message);
    });
  },

  // FoodIngredient Services

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

  // Unit Services

  getUnit() {
    return apiClient.get('UnitOfMeasure/GetAll?MaxResultCount=56').catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  getUnitById(id) {
    return apiClient.get('UnitOfMeasure/Get?Id=' + id).catch(error => {
      console.log('There was an error:', error.message);
    });
  },

  // Vat Services

  getVat() {
    return apiClient.get('VatCategory/GetAll').catch(error => {
      console.log('There was an error:', error.message);
    });
  },
  getVatById(id) {
    return apiClient.get('VatCategory/Get?Id=' + id).catch(error => {
      console.log('There was an error:', error.message);
    });
  }
};
