<template>
  <div v-if="FoodIngredient != null">
    <h2>Showing FoodIngredient #: {{FoodIngredient.id}}</h2>
    <p>name: {{ FoodIngredient.name }}</p>
    <p>cost: {{ FoodIngredient.cost }} </p>
    <p>unit id: {{ FoodIngredient.unitOfMeasureId }} </p>
    <p>Quantity: {{ FoodIngredient.quantity }} </p>
    <!-- <BaseButton>Delete</BaseButton> -->
    <br><br>
    <select v-model="FoodIngredient.products">
        <option disabled hidden :value="null">Please select one</option>
        <option
          v-for="option in ProductOptions"
          v-bind:value="option.id"
          v-bind:key="option.id"
        >{{ option.name }}</option>
      </select>
  </div>
</template>

<script>
import FoodIngredientService from '@/services/FoodIngredientService.js';
// import ProductService from '@/services/ProductService.js';
import axios from 'axios';

  export default {
    props:['id'],
    data(){
      return{
        ProductOptions:[],
        FoodIngredient:{
          result:{}
        },
      }
    },
    beforeCreate(){
      axios
      .get('http://localhost:21021/api/services/app/Product/GetAll?MaxResultCount=10000', {headers: {'Accept': 'application/json'}})
      .then(response =>{
        this.ProductOptions = response.data.result.items;
        console.log(this.ProductOptions);
      })
    },
    created(){
      
      // ProductService.getAllProducts()
      // .then(responce => {
      //   this.ProductOptions = responce.data;
      //   console.log('responce = '+ responce.data + "    lol");
      // })
      // .catch(error => {
      //   console.log("There war an error " + error.responce);
      // });
      
      FoodIngredientService.getFoodIngredientById(this.id)
      .then(response => {
        this.FoodIngredient = response.data.result;
        console.log(response.data.result);
      })
      .catch(error =>{
        console.log(error.response);
      })
    }
  }
</script>

<style scoped>

</style>