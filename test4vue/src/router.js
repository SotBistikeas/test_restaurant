import Vue from 'vue';
import Router from 'vue-router';
import ProductList from './views/ProductList.vue';
import DishList from './views/DishList.vue';
import ProductCreate from './views/ProductCreate.vue';
import DishCreate from './views/DishCreate.vue';
import ProductShow from './views/ProductShow.vue';
import DishShow from './views/DishShow.vue';
import User from './views/User.vue';
import NProgress from 'nprogress';
import store from '@/store/store';
import NotFound from './views/NotFound.vue';
import NetworkIssue from './views/NetworkIssue.vue';
import Vat from './views/Vat.vue';

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
      path: '/createDish',
      name: 'DishCreate',
      component: DishCreate
    },
    {
      path: '/dishlist',
      name: 'DishList',
      component: DishList,
      props: true
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
      beforeEnter(routeTo, routeFrom, next) {
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
      path: '/showDish/:id',
      name: 'DishShow',
      component: DishShow,
      props: true,
      beforeEnter(routeTo, routeFrom, next) {
        store
          .dispatch('dish/fetchDish', routeTo.params.id)
          .then(dish => {
            routeTo.params.dish = dish;
            next();
          })
          .catch(error => {
            if (error.response && error.response.status == 404) {
              next({ name: '404', params: { resource: 'dish' } });
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
      props: true
    },
    {
      path: '/vat',
      name: 'vat',
      component: Vat,
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
    // {
    //   path: '/about',
    //   name: 'about',
    //   // route level code-splitting
    //   // this generates a separate chunk (about.[hash].js) for this route
    //   // which is lazy-loaded when the route is visited.
    //   component: () => import(/* webpackChunkName: "about" */ './views/About.vue')
    // },
    // {
    //   path:"/tipota",
    //   redirect: {name: "about"}
    // }
  ]
});

router.beforeEach((routeTo, routeFrom, next) => {
  NProgress.start();
  next();
});

router.afterEach(() => {
  NProgress.done();
});

export default router;
