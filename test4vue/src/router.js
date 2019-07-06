import Vue from 'vue';
import Router from 'vue-router';
import ProductList from './views/ProductList.vue';
import DishList from './views/DishList.vue';
import ProductCreate from './views/ProductCreate.vue';
import DishCreate from './views/DishCreate.vue';
import ProductShow from './views/ProductShow.vue';
import User from './views/User.vue';
import NProgress from 'nprogress';
import store from '@/store/store';
import NotFound from './views/NotFound.vue';
import NetworkIssue from './views/NetworkIssue.vue';
import VatList from './views/VatList.vue';
import UnitList from './views/UnitList.vue';
import UnitShow from './views/UnitShow.vue';
import VatShow from './views/VatShow.vue';
import FoodIngredient from './views/FoodIngredient.vue';
Vue.use(Router);

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'HeloWorld',
      component: User
    },
    {
      path: '/plist',
      name: 'ProductList',
      component: ProductList,
      props: true
      // alias:"/faeskata"
    },
    {
      path: '/create',
      name: 'ProductCreate',
      component: ProductCreate
    },
    {
      path: '/foodingredient',
      name: 'FoodIngredient',
      component: FoodIngredient
    },
    {
      path: '/createDish',
      name: 'DishCreate',
      component: DishCreate
    },
    {
      path: '/dishlist',
      name: 'DishList',
      component: DishList,
    },
    {
      path: '/user/:username',
      name: 'user',
      component: User,
      props: true
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
      component: NotFound,
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
  next();
});

router.afterEach(() => {
  NProgress.done();
});

export default router;
