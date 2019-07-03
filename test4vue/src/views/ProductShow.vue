<template>
  <div>
    <h2>Showing product #: {{product.result.id}}</h2>
    <p>name: {{ product.result.name }}</p>
    <p>price: {{ product.result.price }} </p>
    <p>vat id: {{ product.result.vatCategoryId }} </p>
    <p>unit id: {{ product.result.unitOfMeasureId }} </p>
    <p>Quantity: {{ product.result.quantity }} </p>
    <br>
    <router-link :to="{name: 'ProductList'}">Back to Product List</router-link>
  </div>
</template>

<script>
import ProductService from '@/services/ProductService.js';

  export default {
    props:['id'],
    data(){
      return{
        product:{
          result:{}
        },
      }
    },
    created(){
      ProductService.getProduct(this.id)
      .then(response => {
        this.product = response.data;
      })
      .catch(error =>{
        console.log(error.response);
      })
    }
  }
</script>

<style scoped>

</style>