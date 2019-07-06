
<template>
  <div>
    <b-card title="Create a new product" sub-title="Card subtitle">
      <form @submit.prevent="createProduct">
        <BaseInput
          label="Name:"
          v-model="product.name"
          class="field"
          :class="{error: $v.product.name.$error}"
          @blur="$v.product.name.$touch()"
        />

        <template v-if="$v.product.name.$error">
          <p v-if="!$v.product.name.required" class="errorMessage">Name is required</p>
        </template>
        <BaseInput
          label="Timi ana kilo:"
          v-model="product.price"
          class="field"
          :class="{error: $v.product.price.$error}"
          @blur="$v.product.price.$touch()"
        />
        <template v-if="$v.product.price.$error">
          <p v-if="!$v.product.price.required" class="errorMessage">Price is required</p>
        </template>
        <b-form-group label="Vat category">
          <b-form-select v-model="product.vatCategoryId">
            <option disabled hidden :value="null">Please select one</option>
            <option
              v-for="option in vatCategoryOptions"
              v-bind:value="option.id"
              v-bind:key="option.id"
            >{{ option.name }}</option>
          </b-form-select>
        </b-form-group>
        <template v-if="$v.product.vatCategoryId.$error">
          <p v-if="!$v.product.vatCategoryId.required" class="errorMessage">Vat is required</p>
        </template>
        <b-form-group label="Unit of measure">
          <b-form-select v-model="product.unitOfMeasureId">
            <option disabled hidden :value="null">Please select one</option>
            <option
              v-for="option in unitOfMeasureOptions"
              v-bind:value="option.id"
              v-bind:key="option.id"
            >{{ option.name }}</option>
          </b-form-select>
        </b-form-group>
        <template v-if="$v.product.unitOfMeasureId.$error">
          <p v-if="!$v.product.unitOfMeasureId.required" class="errorMessage">Vat is required</p>
        </template>

        <BaseButton type="submit" :disabled="$v.$anyError" variant="primary">Save</BaseButton>
       
        <p v-if="$v.$anyError">Please fill out all the fields</p>
      </form>
    </b-card>
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
      .get("http://localhost:21021/api/services/app/VatCategory/GetAll")
      .then(responce => {
        this.vatCategoryOptions = responce.data.result.items;
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
      vatCategoryOptions: [],
      unitOfMeasureOptions: [],
      result: {},
      items: [],
      product: this.createFreshProductObject()
    };
  },

  validations: {
    product: {
      name: { required },
      price: { required },
      vatCategoryId: { required },
      unitOfMeasureId: { required }
    }
  },

  methods: {
    createProduct() {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        NProgress.start();
        this.$store
          .dispatch("product/createProduct", this.product)
          .then(() => {
            //this.$router.push({
            //  name:'ProductShow',
            // params:{ id : this.product.id}
            // })
            this.product = this.createFreshProductObject();
          })
          .catch(() => {
            NProgress.done();
          });
      }
    },
    createFreshProductObject() {
      const user = this.$store.state.user.user;
      return {
        id: 0,
        user: user,
        price: null,
        vatCategoryId: null,
        unitOfMeasureId: 402, // kgs
        quantity: 1
      };
    }
  }
};
</script>



<style scoped>
</style>
