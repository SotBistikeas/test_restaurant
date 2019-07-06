
<template>
  <div>
    <!-- <h1>Product Create by user: "{{user.name}}"</h1>
    <h1>With id: {{user.id}}</h1>-->
    <h2>Create a new product</h2>

    <form @submit.prevent="createFoodIngredient">
      <BaseInput
        label="Name:"
        v-model="FoodIngredient.name"
        class="field"
        :class="{error: $v.FoodIngredient.name.$error}"
        @blur="$v.FoodIngredient.name.$touch()"
      />
      <template v-if="$v.FoodIngredient.name.$error">
        <p v-if="!$v.FoodIngredient.name.required" class="errorMessage">Name is required</p>
      </template>
      <select v-model="FoodIngredient.unitOfMeasureId">
        <option disabled hidden :value="null">Please select one</option>
        <option
          v-for="option in unitOfMeasureOptions"
          v-bind:value="option.id"
          v-bind:key="option.id"
        >{{ option.name }}</option>
      </select>
      <BaseButton type="submit" :disabled="$v.$anyError">Create Food Ingredient</BaseButton>
    </form>
      <!-- <BaseInput
        label="Timi ana kilo:"
        v-model="product.price"
        class="field"
        :class="{error: $v.product.price.$error}"
        @blur="$v.product.price.$touch()"
      />
      <template v-if="$v.product.price.$error">
        <p v-if="!$v.product.price.required" class="errorMessage">Price is required</p>
      </template>
      <select v-model="product.vatCategoryId">
        <option disabled hidden :value="null">Please select one</option>
        <option
          v-for="option in vatCategoryOptions"
          v-bind:value="option.id"
          v-bind:key="option.id"
        >{{ option.name }}</option>
      </select>
      <template v-if="$v.product.vatCategoryId.$error">
        <p v-if="!$v.product.vatCategoryId.required" class="errorMessage">Vat is required</p>
      </template>
      <select v-model="product.unitOfMeasureId">
        <option disabled hidden :value="null">Please select one</option>
        <option
          v-for="option in unitOfMeasureOptions"
          v-bind:value="option.id"
          v-bind:key="option.id"
        >{{ option.name }}</option>
      </select>
      <template v-if="$v.product.unitOfMeasureId.$error">
        <p v-if="!$v.product.unitOfMeasureId.required" class="errorMessage">Vat is required</p>
      </template>

      <BaseButton type="submit" :disabled="$v.$anyError">Save</BaseButton>
      <p v-if="$v.$anyError">Please fill out all the fields</p>
    </form> -->
  </div>
</template>


<script>
import NProgress from "nprogress";
import { required } from "vuelidate/lib/validators";
import UnitOfMesureService from "@/services/UnitOfMeasureService.js";
import axios from "axios";

export default {
  beforeCreate: function() {
    //get vat categories
    axios
      .get("http://localhost:21021/api/services/app/Product/GetAll")
      .then(responce => {
        this.Products = responce.data.result.items;
      })
      .catch(error => console.log("There was an error:" + error.responce));
    UnitOfMesureService.getUnit()
      .then(responce => {
        this.unitOfMeasureOptions = responce.data.result.items;
      })
      .catch(error => {
        console.log("There war an error " + error.responce);
      });
  },
  data() {
    return {
      unitOfMeasureOptions: [],
      Products:[],
      FoodIngredient: {}
    };
  },

  validations: {
    FoodIngredient: {
      name: { required },
    }
  },

  methods: {
    createFoodIngredient() {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        NProgress.start();
        // this.$store
        //   .dispatch("product/createProduct", this.product)
        //   .then(() => {
        //     //this.$router.push({
        //     //  name:'ProductShow',
        //     // params:{ id : this.product.id}
        //     // })
        //     this.product = this.createFreshProductObject();
        //   })
        //   .catch(() => {
             NProgress.done();
        //   });
      }
    },
    createFreshFoodIngredient() {
      const user = this.$store.state.user.user;
      return {
        // id: 0,
        // user: user,
        // name:'',
        // totalCount: '',
        // items:[],
        };
    }
  }
};
</script>



<style scoped>
</style>
