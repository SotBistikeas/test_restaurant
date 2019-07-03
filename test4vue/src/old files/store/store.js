import Vue from 'vue';
import Vuex from 'vuex';
import * as user from '@/store/modules/user.js';
import * as product from '@/store/modules/product.js';
import * as dish from '@/store/modules/dish.js';
import * as notification from '@/store/modules/notification.js';
import * as vat from '@/store/modules/vat.js';

Vue.use(Vuex);
export default new Vuex.Store({
  modules: {
    user,
    product,
    notification,
    dish,
    vat
  },
  state: {
    vat: [],
    products: [],
    prodsOfDish: [],
    quantity: 0,
    price: 0,
    tempPrice: 0,
    pds: []
  }
});
