import Vue from 'vue';
import Vuex from 'vuex';
import * as user from '@/store/modules/user.js';
import * as product from '@/store/modules/product.js';
import * as dish from '@/store/modules/dish.js';
import * as notification from '@/store/modules/notification.js';
import * as unit from '@/store/modules/unit.js';
import * as FoodIngredient from '@/store/modules/FoodIngredient.js';
import ProductService from '@/services/ProductService.js';
import VatService from '@/services/VatService.js';
import UnitOfMeasureService from '@/services/UnitOfMeasureService.js';
import FoodIngredientService from '@/services/FoodIngredientService.js';

Vue.use(Vuex);
export default new Vuex.Store({
  modules: {
    user,
    product,
    notification,
    dish,
    unit,
    FoodIngredient
  },
  state: {
    products: [],
    vats: [],

    units: [],
    FoodIngredient: {},
    FoodIngredients: [],
    prodsOfDish: [],
    quantity: 0,
    price: 0,
    tempPrice: 0,
    pds: []
  },
  mutations: {
    ADD_PRODUCT(state, product) {
      state.products.push(product);
    },
    SET_PRODUCTS(state, products) {
      state.products = products;
    },
    SET_VATS(state, vats) {
      state.vats = vats;
    },
    SET_UNITS(state, units) {
      state.units = units;
    },
    ADD_FOODINGREDIENT(state, FoodIngredient) {
      state.FoodIngredient = FoodIngredient;
    },
    SET_FOODINGREDIENT(state, FoodIngredient) {
      state.FoodIngredients = FoodIngredient;
    }
  },
  actions: {
    createProduct({ commit }, product) {
      // kanei post sto db
      ProductService.postProduct(product);
      // kanei update to state
      commit('ADD_PRODUCT', product);
    },
    fetchProducts({ commit }) {
      ProductService.getAllProducts().then(response => {
        commit('SET_PRODUCTS', response.data.result.items);
        // this.totalCount = response.data.result.totalCount;
      });
    },
    createFoodIngredient({ commit }, FoodIngredient) {
      FoodIngredientService.postFoodIngredient(FoodIngredient);
      commit('ADD_FOODINGREDIENT', FoodIngredient);
    },
    fetchVats({ commit }) {
      VatService.getVat().then(response => {
        commit('SET_VATS', response.data.result.items);
      });
    },
    fetchUnits({ commit }) {
      UnitOfMeasureService.getUnit().then(responce => {
        commit('SET_UNITS', responce.data.result.items);
      });
    },
    fetchFoodIngredient({ commit }) {
      FoodIngredientService.getFoodIngredient().then(responce => {
        commit('SET_FOODINGREDIENT', responce.data.result.items);
      });
    }
  }
});
