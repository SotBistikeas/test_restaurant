import DishService from '@/services/DishService.js';

export const namespaced = true;

export const state = {
  dishes: [],
  dishesTotal: 0,
  dish: {},
  perPage: 2
};
export const mutations = {
  ADD_DISH(state, dish) {
    state.dishes.push(dish);
  },
  SET_DISHES(state, dishes) {
    state.dishes = dishes;
  },
  SET_DISHES_TOTAL(state, dishesTotal) {
    state.dishesTotal = dishesTotal;
  },
  SET_DISH(state, dish) {
    state.dish = dish;
  }
};
export const actions = {
  createDish({ commit, dispatch }, dish) {
    return DishService.postDish(dish)
      .then(() => {
        commit('ADD_DISH', dish);
        const notification = {
          type: 'success',
          message: 'Your dish has been created!'
        };
        dispatch('notification/add', notification, { root: true });
      })
      .catch(error => {
        const notification = {
          type: 'error',
          message: 'There was a problem creating your dish: ' + error.message
        };
        dispatch('notification/add', notification, { root: true });
        throw error;
      });
  },
  fetchDishes({ commit, dispatch, state }, { page }) {
    return DishService.getDishes(state.perPage, page)
      .then(response => {
        commit('SET_DISHES_TOTAL', parseInt(response.headers['x-total-count']));
        commit('SET_DISHES', response.data);
      })
      .catch(error => {
        const notification = {
          type: 'error',
          message: 'there was a problem fetching the dishes: ' + error.message
        };
        dispatch('notification/add', notification, { root: true });
      });
  },
  fetchDish({ commit, getters, state }, id) {
    if (id == state.dish.id) {
      return state.dish;
    }
    var dish = getters.getDishById(id);
    if (dish) {
      commit('SET_DISH', dish);
      return dish;
    } else {
      return DishService.getDish(id).then(response => {
        commit('SET_DISH', response.data);
        return response.data;
      });
    }
  }
};
export const getters = {
  getDishById: state => id => {
    return state.dishes.find(dish => dish.id === id);
  }
};
