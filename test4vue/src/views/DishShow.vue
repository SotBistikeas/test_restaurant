<template>
  <div>
    <!-- <template v-if="!edit">
      <h1>Dish Show</h1>
      <h3>id: {{dish.id}}</h3>
      <h3>name: {{ dish.name }}</h3>
      <h3 v-if="dish.price != null">price: {{ dish.price }}</h3>
      <BaseButton v-on:click="deleteDish(dish.id)">Delet dish</BaseButton>
      <BaseButton v-on:click="edit = true" v-if="!edit">Edit</BaseButton>
    </template>

    <template v-if="edit">
      <p>edit name for the dish {{dish.name}}</p>
      <BaseInput label="Onoma piatou:" v-model="editedname" />
      <BaseButton v-on:click="savedish(dish.id)">Save new name</BaseButton>
      <BaseButton v-on:click="edit = false">Cancel</BaseButton>
    </template> 
    <br>
    <span>Dish Object{{dish}}</span> -->
    <b-card
        title="Details"
        header-tag="header"
        footer-tag="footer"
        style="max-width: 20rem;"
        class="mb-2"
        header-bg-variant="success"
        body-bg-variant="info"
        footer-bg-variant="warning"
      >
        <h5 slot="header" class="mb-0">{{ dish.name }}</h5>
        <div v-for="ingredient in ingredients" :key="ingredient.id">
          Id: {{ ingredient.id }} <br>
          Food-Id: {{ ingredient.foodIngredientId }} <br>
          Quantity: {{ ingredient.quantity }} <br>
          Unit-Id: {{ ingredient.unitOfMeasureId }} 
        </div>
        <!-- <span>{{ingredients}}</span>
        <span>{{ingredients.id}}</span> *** <span>{{ingredients.quantity}}</span> *** <span>{{ingredients.foodIngredientid}}</span> -->
        <br>
        <h5 slot="footer" class="mb-0" v-if="edit == true">
          <BaseInput v-model="editedname" label="update name" />
          <BaseButton pill size="sm" @click="updateIt()" variant="dark">Update</BaseButton>
        </h5>

        <BaseButton @click="deleteIt()" variant="danger">Delete</BaseButton>

        <b-form-checkbox v-model="edit" switch>Edit name</b-form-checkbox>
        <span slot="footer" class="mb-0">
          <router-link :to="{ name: 'DishList' }">Back to Dish List</router-link>
        </span>
      </b-card>
      
  </div>
</template>
<script>
import NProgress from 'nprogress';
import axios from 'axios';
import FoodIngredientService from '@/services/FoodIngredientService.js';
import DishService from '@/services/DishService';
export default {
  props: ['id'],
  data() {
    return {
      dish:{
        result:{}
      },
      ProductOptions:[],
      // products:[],
      FoodIngredient:{},
      edit: false,
      editedname: '',
      ingredients:[],
      ingredient:{
        id:'',
        foodIngredientid:'',
        quantity:'',
        unitOfMeasureId:'',
      }
    };
  },
  // props: {
  //   dish: Object,
  //   required: true
  // },
  beforeCreate() {
    axios
      .get('http://localhost:21021/api/services/app/Product/GetAll?MaxResultCount=10000', { headers: { Accept: 'application/json' } })
      .then(response => {
        this.ProductOptions = response.data.result.items;
      });
  },
  created() {
    this.getIt();
    this.loadFoodIngredient();
    // this.loadProducts();
  },
  methods: {
    getIt(){
      DishService.getDishById(this.id)
      .then(response => {
        this.dish = response.data.result;
      })
      .catch(error => {
        console.log(error.response);
      });
      DishService.getFoodIngredientsByDish(this.id)
      .then(response =>{
        this.ingredients = response.data.result;
      })
      .catch(error =>{
        console.log(error.message);
      })
    },
    deleteIt() {
      NProgress.start();
      axios
        .delete('http://localhost:21021/api/services/app/Dish/Delete?Id=' + this.id)
        .then(() => {
          this.$router.push({
            name: 'DishList'
          })
        })
        .catch(error => {
          console.log(error.message);
          NProgress.done();
        });
    },
    updateIt(){
      NProgress.start();
      axios
        .put('http://localhost:21021/api/services/app/Dish/Update',{
          name: this.editedname,
          id: this.id
        })
        .then(response =>{
          if (response.data.error == null ){
            this.getIt();
            this.edit = false;
          }
        })
        .catch(error =>{
          console.log(error.message);
        })
      NProgress.done();
    },
    loadFoodIngredient() {
      FoodIngredientService.getFoodIngredient()
        .then(response => {
          this.FoodIngredient = response.data.result;
        })
        .catch(error => {
          console.log(error.response);
        });
    },
    // loadProducts() {
    //   axios.get('http://localhost:21021/api/services/app/Product/GetAll?MaxResultCount=100000').then(response => {
    //     this.products = response.data.result;
    //   });
    // },
  }
};
</script>

<style scoped>

</style>
