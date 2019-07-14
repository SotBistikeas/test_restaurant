<template>
  <div>
    <h2>Showing product #: {{product.result.id}}</h2>
    <p>name: {{ product.result.name }}</p>
    <p>price: {{ product.result.price }} </p>
    <p>unit id: {{ product.result.unitOfMeasureId }} </p>
    <p>Quantity: {{ product.result.quantity }} </p>
    <Button @click="deleteIt()">Delete</Button> 
    <br>
    <router-link :to="{name: 'ProductList'}">Back to Product List</router-link>
  </div>
</template>

<script>
import ProductService from '@/services/ProductService.js';
import axios from 'axios';

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
    },
    methods:{
      deleteIt(id){
      axios
      .delete('http://localhost:21021/api/services/app/Product/Delete?Id='+ this.id)
      .then(response => {
        this.$router.push({
                name:'ProductList'
              })
              console.log(response.data)
              console.log('record with id:' + this.id + ' has been deleted');
        }).catch(error =>{
          console.log(error.message);
        })
    },
    }
  }
</script>

<style scoped>

</style>