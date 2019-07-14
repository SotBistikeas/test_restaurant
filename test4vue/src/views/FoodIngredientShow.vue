<template>
  <div v-if="FoodIngredient != null">
    <h2>Showing FoodIngredient #: {{FoodIngredient.id}}</h2>
    <p>name: {{ FoodIngredient.name }}</p>
    <p>cost: {{ FoodIngredient.cost }} </p>
    <p>unit id: {{ FoodIngredient.unitOfMeasureId }} </p>
    <p>Quantity: {{ FoodIngredient.quantity }} </p>
    <Button @click="deleteIt()">Delete</Button> 
    <br>
    <br>
    {{x}} -- lol
    <br><br>
    <!-- <select v-model="product">
        <option
          v-for="option in ProductOptions"
          v-bind:value="option"
          v-bind:key="option.id"
        >{{ option.name }}</option>
      </select> -->
      <b-form-group label="Products">
        <b-form-select v-model="product">
          <option disabled hidden :value="null">Please select one</option>
            <option
              v-for="option in ProductOptions"
              v-bind:value="option"
              v-bind:key="option.id"
            >{{ option.name }}</option>
        </b-form-select>
      </b-form-group>
      <form v-on:submit="addToList()"> 
      <BaseInput
        label="quantity:"
        v-model="quantity"
        class="field"
      />
      <span v-if="!(isNaN(product.price * quantity))">price per FoodIngredient: {{product.price * quantity}}</span>
      <BaseButton type="submit">Add to list</BaseButton>
      </form>
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
        product:{
          productId:'',
          quantity: '',
          unitOfMeasureId: '',
          id: ''
        },
        quantity:'',
        x:''
      }
    },
    beforeCreate(){
      axios
      .get('http://localhost:21021/api/services/app/Product/GetAll?MaxResultCount=10000', {headers: {'Accept': 'application/json'}})
      .then(response =>{
        this.ProductOptions = response.data.result.items;
      })
      
    },
    created(){
      FoodIngredientService.getFoodIngredientById(this.id)
      .then(response => {
        this.FoodIngredient = response.data.result;
      })
      .catch(error =>{
        console.log(error.response);
      })
      axios
      .get('http://localhost:21021/api/services/app/FoodIngredient/GetProducts?foodIngredientId='+this.id)
      .then(response =>{
        this.x = response.data.result
        console.log(this.x);
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
      
      axios
      .post('http://localhost:21021/api/services/app/FoodIngredient/AddProduct?foodIngredientId=' + this.id,{
          productId: this.product.id,
          quantity: this.quantity,
          unitOfMeasureId: this.product.unitOfMeasureId,
          id: 0,
      })
            
      axios
      .get('http://localhost:21021/api/services/app/FoodIngredient/GetProducts?foodIngredientId='+this.id)
      .then(response =>{
        this.x = response.data.result
      })
    }
    },
  }
</script>

<style scoped>

</style>