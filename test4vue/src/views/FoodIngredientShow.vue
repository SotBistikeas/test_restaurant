<template>
  <div v-if="FoodIngredient != null">
    <h2>Showing FoodIngredient #: {{FoodIngredient.id}}</h2>
    <p>name: {{ FoodIngredient.name }}</p>
    <p>cost: {{ FoodIngredient.cost }} </p>
    <p>unit id: {{ FoodIngredient.unitOfMeasureId }} </p>
    <p>Quantity: {{ FoodIngredient.quantity }} </p>
    <Button @click="deleteIt()">Delete</Button> 
    <br><br>
    <select v-model="product">
        <option
          v-for="option in ProductOptions"
          v-bind:value="option"
          v-bind:key="option.id"
        >{{ option.name }}</option>
      </select>
      <BaseInput
        label="quantity:"
        v-model="quantity"
        class="field"
      />
      <span v-if="!(isNaN(product.price * quantity))">price per FoodIngredient: {{product.price * quantity}}</span>
      <BaseButton @click="addToList()">Add to list</BaseButton>
      <!-- <div v-if="list.length > 0">
        <div v-for="l in list" :key="l.name">
          name:{{ l.name }} <br> id:{{ l.id }} <br> unit:{{ l.unitOfMeasureId}} <br> vat:{{ l.vatCategoryId }}
        </div>
      </div> -->
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
        list:[],
        product:{
          productId:'',
          quantity: '',
          unitOfMeasureId: '',
          id: ''
        },
        quantity:'',
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
          productId: this.product.productId,
          quantity: this.quantity,
          unitOfMeasureId: this.product.unitOfMeasureId,
          id: 0,
      })
      .then(response =>{
        console.log(response.data);
        console.log(this.product.id+' product id');
        console.log(this.quantity+' quantity');
        console.log(this.product.unitOfMeasureId+' unit id');
      })
      this.list.push(this.product);
      console.log(this.list);
    }
    },
  }
</script>

<style scoped>

</style>