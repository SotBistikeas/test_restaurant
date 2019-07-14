import FoodIngredientService from '@/services/FoodIngredientService.js';

export const namespaced = true;

export const state = {
  FoodIngredient: Object
};
export const mutations = {
  ADD_FOODINGREDIENT(state, FoodIngredient) {
    state.FoodIngredient.push(FoodIngredient);
  },
  SET_FOODINGREDIENT(state, FoodIngredient) {
    state.FoodIngredient = FoodIngredient;
  },
  SET_FOODINGREDIENT_TOTAL(state, FoodIngredientTotal) {
    state.FoodIngredientTotal = FoodIngredientTotal;
  }
};
export const actions = {
  createFoodIngredient({ dispatch }, FoodIngredient) {
    return FoodIngredientService.postFoodIngredient(FoodIngredient)
      .then(() => {
        const notification = {
          type: 'success',
          message: 'Your FoodIngredient has been created!'
        };
        dispatch('notification/add', notification, { root: true });
      })
      .catch(error => {
        const notification = {
          type: 'error',
          message: 'There was a problem creating your FoodIngredient: ' + error.message
        };
        dispatch('notification/add', notification, { root: true });
        console.log('FUCK');
      });
  },
  fetchFoodIngredient({ commit, dispatch, state }) {
    return FoodIngredientService.getFoodIngredient(state)
      .then(response => {
        commit('SET_FOODINGREDIENT', response.data);
      })
      .catch(error => {
        const notification = {
          type: 'error',
          message: 'there was a problem fetching FoodIngredient: ' + error.message
        };
        dispatch('notification/add', notification, { root: true });
      });
  }
};
export const getters = {
  // activeDishes: state =>{
  //   return state.dishes.filter(dishes => dishes.active)
  // },
  getFoodIngredientById: state => id => {
    return state.FoodIngredient.find(FoodIngredient => FoodIngredient.id === id);
  }
  // getDishById: state => id => {
  //   return state.dishes.find(dish => dish.id === id)
  // },
};
