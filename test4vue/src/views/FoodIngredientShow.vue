<template>
  <div v-if="FoodIngredient != null">
    <h2>Showing FoodIngredient #: {{FoodIngredient.id}}</h2>
    <p>name: {{ FoodIngredient.name }}</p>
    <p>cost: {{ FoodIngredient.cost }} </p>
    <p>unit id: {{ FoodIngredient.unitOfMeasureId }} </p>
    <p>Quantity: {{ FoodIngredient.quantity }} </p>
    <Button @click="deleteIt()">Delete</Button> 
    <br><br>
    <select v-model="FoodIngredient.products">
        <option disabled hidden :value="null">Please select one</option>
        <option
          v-for="option in ProductOptions"
          v-bind:value="option.id"
          v-bind:key="option.id"
        >{{ option.name }}</option>
      </select>
      <BaseButton @click="addToList()">Add to list</BaseButton>
      <div v-if="list.length > 0">
        <div v-for="l in list" :key="l.id">
          {{ l }}
        </div>
      </div>
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
        list:[]
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
      FoodIngredientService.getFoodIngredientById(this.id)
      .then(response => {
        this.FoodIngredient = response.data.result;
        console.log(response.data.result);
      })
      .catch(error =>{
        console.log(error.response);
      })
    },
    methods:{
    deleteIt(id){
      axios
      .delete('http://localhost:21021/api/services/app/FoodIngredient/Delete?Id='+ this.id)
      .then(response => {
        this.$router.push({
                name:'FoodIngredientList'
              })
              console.log('record with id:' + this.id + ' has been deleted');
        }).catch(error =>{
          console.log(error.message);
        })
    },
    addToList(){
      this.list.push(this.FoodIngredient.products);
      console.log(this.list);
    }
    },
  }
</script>

<style scoped>

</style>