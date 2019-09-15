import ProductService from '@/services/ProductService.js';

export const namespaced = true;

export const state = {
  products: [],
  productsTotal: 0,
  product: {},
  perPage: 4
};
export const mutations = {
  ADD_PRODUCT(state, product) {
    state.products.push(product);
  },
  SET_PRODUCTS(state, products) {
    state.products = products;
  },
  SET_PRODUCTS_TOTAL(state, productsTotal) {
    state.productsTotal = productsTotal;
  },
  SET_PRODUCT(state, product) {
    state.product = product;
  },
  REMOVE_NEW_PRODUCTS(state) {
    state.products = state.products.filter(p => p.id != 0);
  },
  UPDATE_PRODUCT(state, { oldProduct, newProduct }) {
    state.products = state.products.map(el => (el === oldProduct ? newProduct : el));
  }
};
export const actions = {
  createProduct({ commit, dispatch }, product) {
    commit('ADD_PRODUCT', product);
    return ProductService.postProduct(product)
      .then(resp => {
        commit('UPDATE_PRODUCT', {
          oldProduct: product,
          newProduct: resp.data.result
        });
        const notification = {
          type: 'success',
          message: 'Your product has been created!'
        };
        dispatch('notification/add', notification, {
          root: true
        });
      })
      .catch(error => {
        commit('UPDATE_PRODUCT', {
          oldProduct: product,
          newProduct: {
            id: -1,
            name: product.name,
            price: product.price,
            unitOfMeasureId: product.unitOfMeasureId
          }
        });
        const notification = {
          type: 'error',
          message: 'There was a problem creating your product: ' + error.message
        };
        dispatch('notification/add', notification, {
          root: true
        });
        throw error;
      });
  },
  fetchProducts({ commit, dispatch, state }) {
    return ProductService.getProducts(state)
      .then(response => {
        commit('SET_PRODUCTS', response.data.result.items);
      })
      .catch(error => {
        const notification = {
          type: 'error',
          message: 'there was a problem fetching products: ' + error.message
        };
        dispatch('notification/add', notification, {
          root: true
        });
      });
  },
  fetchProduct({ commit, getters, state }, id) {
    if (id == state.product.id) {
      return state.product;
    }
    var product = getters.getProductById(id);
    if (product) {
      commit('SET_PRODUCT', product);
      return product;
    } else {
      return ProductService.getProduct(id).then(response => {
        commit('SET_PRODUCT', response.data);
        return response.data;
      });
    }
  }
};
export const getters = {
  // activeDishes: state =>{
  //   return state.dishes.filter(dishes => dishes.active)
  // },
  getProductById: state => id => {
    return state.products.find(product => product.id === id);
  }
  // getDishById: state => id => {
  //   return state.dishes.find(dish => dish.id === id)
  // },
};
