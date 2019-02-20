import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import Product from '@/components/Product'
import Test from '@/components/Test'
import Dishes from '@/components/Dishes'
import selected from '@/components/select'
import ProductList from '@/components/ProductList'
Vue.use(Router)
export default new Router({
  routes: [
    {
      path: '/',
      name: 'HelloWorld',
      component: HelloWorld
    },
    {
      path: '/product',
      name: 'Product',
      component: Product
    },
    {
      path:'/test',
      name:'Test',
      component: Test
    },
    {
      path:'/dishes',
      name:'Dishes',
      component: Dishes
    },
    {
      path:'/select',
      name:'selected',
      component: selected
    },
    {
      path: '/productlist',
      name: 'ProductList',
      component: ProductList
    },
  ]
})
