import Vue from 'vue';
import Vuex from 'vuex';
import createPersistedState from 'vuex-persistedstate'

import user from '@/store/modules/user.js';
import * as baharika from '@/store/modules/baharika.js';
import * as product from '@/store/modules/product.js';
import * as dish from '@/store/modules/dish.js';
import * as notification from '@/store/modules/notification.js';
import * as unit from '@/store/modules/unit.js';
import * as FoodIngredient from '@/store/modules/FoodIngredient.js';
import FoodIngredientService from '@/services/FoodIngredientService.js';
import ApiService from '@/services/ApiService.js';
import AuthService from "@/services/AuthService";
import api from "@/services/ApiClient"

Vue.use(Vuex);
export default new Vuex.Store({
  modules: {
    baharika,
    user,
    product,
    notification,
    dish,
    unit,
    FoodIngredient
  },
  state: {
    //user is logged or logout
    status: '',
    token: localStorage.getItem('token') || '',
    userId: null,
    userFullName: null,

    //other
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
    SET_VATS(state, vats) {
      state.vats = vats;
    },
    SET_UNITS(state, units) {
      state.units = units;
    },
    ADD_FOODINGREDIENT(state, FoodIngredient) {
      state.FoodIngredient = FoodIngredient;
    },
    SET_FOODINGREDIENTS(state, FoodIngredients) {
      state.FoodIngredients = FoodIngredients;
    },
    auth_request(state) {
      state.status = 'loading'
    },
    auth_success(state, payload) {
      state.status = 'success';
      state.token = payload.token;
      state.userId = payload.userId;
      state.userFullName = payload.userFullName;
    },
    auth_error(state) {
      state.status = 'error'
    },
    logout(state) {
      state.status = ''
      state.token = ''
    },
  },
  actions: {
    createFoodIngredient({ commit }, FoodIngredient) {
      FoodIngredientService.postFoodIngredient(FoodIngredient);
      commit('ADD_FOODINGREDIENT', FoodIngredient);
    },
    fetchVats({ commit }) {
      ApiService.getVat().then(response => {
        commit('SET_VATS', response.data.result.items);
      });
    },
    fetchUnits({ commit }) {
      if (this.state.units.length == 0) {
        ApiService.getUnit().then(responce => {
          commit('SET_UNITS', responce.data.result.items);
        });
      }
    },
    fetchFoodIngredient({ commit }) {
      ApiService.getAllFoodIngredients().then(responce => {
        commit('SET_FOODINGREDIENTS', responce.data.result.items);
      });
    },
    login({ commit, dispatch }, user) {
      return new Promise((resolve, reject) => {
        commit('auth_request')
        AuthService.login(user.name, user.password)
          .then(resp => {
            const token = resp.accessToken;
            const userId = resp.userId;
            const userFullName = resp.userFullName;
            localStorage.setItem('token', token);
            api.defaults.headers.common['Authorization'] = "Bearer " + token
            commit('auth_success', { token, userId, userFullName })
            dispatch('user/updateCurrentUser');
            resolve(resp)
          })
          .catch(err => {
            commit('auth_error')
            localStorage.removeItem('token')
            reject(err)
          })
      })
    },
    register({ commit }, user) {
      return new Promise((resolve, reject) => {
        commit('auth_request')
        AuthService.register(user)
          .then(resp => {
            if (resp.canLogin) {
              resolve()
            }
            else {
              debugger;
              commit('auth_error', err)
              reject('Cannot create account')
            }

          })
          .catch(err => {
            commit('auth_error', err)
            localStorage.removeItem('token')
            reject(err)
          })
      })
    },
    logout({ commit }) {
      return new Promise((resolve, reject) => {
        commit('logout')
        localStorage.removeItem('token')
        delete api.defaults.headers.common['Authorization']
        resolve()
      })
    }
  },
  getters: {
    isLoggedIn: state => !!state.token,
    authStatus: state => state.status,
  },
  plugins: [createPersistedState()]
});
