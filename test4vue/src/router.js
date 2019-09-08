import Vue from 'vue';
import Router from 'vue-router';
import Welcome from './views/Welcome.vue';
import ProductList from './views/ProductList.vue';
import DishList from './views/DishList.vue';
import ProductCreate from './views/ProductCreate.vue';
import DishCreate from './views/DishCreate.vue';
import DishShow from './views/DishShow.vue';
import ProductShow from './views/ProductShow.vue';
import User from './views/User.vue';
import Login from './views/Login.vue';
import Register from './views/Register.vue';
import NProgress from 'nprogress';
import store from '@/store/store';
import NotFound from './views/NotFound.vue';
import NetworkIssue from './views/NetworkIssue.vue';
import VatList from './views/VatList.vue';
import UnitList from './views/UnitList.vue';
import UnitShow from './views/UnitShow.vue';
import VatShow from './views/VatShow.vue';
import FoodIngredient from './views/FoodIngredient.vue';
import FoodIngredientShow from './views/FoodIngredientShow.vue';
import FoodIngredientList from './views/FoodIngredientList.vue';
import baharika from './views/baharika.vue';
import MonthlyExp from './views/MonthlyExp.vue';
Vue.use(Router);

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Welcome
      //meta: { requiresAuth: true }
    },
    {
      path: '/expenses',
      name: 'MonthlyExp',
      component: MonthlyExp,
      //meta: { requiresAuth: true }
    },
    {
      path: '/login',
      name: 'Login',
      component: Login
    },
    {
      path: '/Register',
      name: 'Register',
      component: Register
    },
    {
      path: '/profile',
      name: 'profile',
      component: User,
      meta: { requiresAuth: true }
    },
    {
      path: '/plist',
      name: 'ProductList',
      component: ProductList,
      props: true,
      meta: { requiresAuth: true }
      // alias:"/faeskata"
    },
    {
      path: '/create',
      name: 'ProductCreate',
      component: ProductCreate,
      meta: { requiresAuth: true }
    },
    {
      path: '/foodingredient',
      name: 'FoodIngredient',
      component: FoodIngredient,
      meta: { requiresAuth: true }
    },
    {
      path: '/foodlist',
      name: 'FoodIngredientList',
      component: FoodIngredientList,
      meta: { requiresAuth: true }
    },
    {
      path: '/FoodIngredient/:id',
      name: 'FoodIngredientShow',
      component: FoodIngredientShow,
      props: true,
      meta: { requiresAuth: true }
    },
    {
      path: '/createDish',
      name: 'DishCreate',
      component: DishCreate,
      meta: { requiresAuth: true }
    },
    {
      path: '/DishShow/:id',
      name: 'DishShow',
      component: DishShow,
      props: true,
      meta: { requiresAuth: true }
    },
    {
      path: '/baharika',
      name: 'baharika',
      component: baharika,
      meta: { requiresAuth: true }
    },
    {
      path: '/dishlist',
      name: 'DishList',
      component: DishList,
      meta: { requiresAuth: true }
    },
    {
      path: '/user/:username',
      name: 'user',
      component: User,
      props: true,
      meta: { requiresAuth: true }
    },
    {
      path: '/show/:id',
      name: 'ProductShow',
      component: ProductShow,
      props: true,
      beforeEnter(routeTo, _routeFrom, next) {
        store
          .dispatch('product/fetchProduct', routeTo.params.id)
          .then(product => {
            routeTo.params.product = product;
            next();
          })
          .catch(error => {
            if (error.response && error.response.status == 404) {
              next({ name: '404', params: { resource: 'product' } });
            } else {
              next({ name: 'network-issue' });
            }
          });
      }
    },
    {
      path: '/404',
      name: '404',
      component: NotFound
    },
    {
      path: '/vat',
      name: 'VatList',
      component: VatList
    },
    {
      path: '/vat/:id',
      name: 'VatShow',
      component: VatShow,
      props: true
    },
    {
      path: '/unit',
      name: 'UnitList',
      component: UnitList
    },
    {
      path: '/unit/:id',
      name: 'UnitShow',
      component: UnitShow,
      props: true
    },
    {
      path: '*',
      redirect: { name: '404', params: { resource: 'page' } }
    },
    {
      path: '/network-issue',
      name: 'network-issue',
      component: NetworkIssue
    }
  ]
});

router.beforeEach((_routeTo, _routeFrom, next) => {
  NProgress.start();
  if (_routeTo.matched.some(record => record.meta.requiresAuth)) {
    if (store.getters.isLoggedIn) {
      next()
      return
    }
    next('/login')
    NProgress.done();
  } else {
    next()
  }

});

router.afterEach(() => {
  NProgress.done();
});

export default router;
