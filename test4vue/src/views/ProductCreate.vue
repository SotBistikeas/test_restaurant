<template>
  <div>
    <b-card title="Create a new product" sub-title="">
      <form @submit.prevent="createProduct">
        <BaseInput
          label="Name"
          v-model="product.name"
          @blur="$v.product.name.$touch()"
          :state="!$v.product.name.$error"
          errorMessage="Name is required"
        />

        <BaseInput
          label="Timi ana kilo:"
          v-model="product.price"
          class="field"
          :class="{ error: $v.product.price.$error }"
          @blur="$v.product.price.$touch()"
          type="number"
          :state="!$v.product.price.$error"
          errorMessage="Price is required"
        />

        <b-form-group label="Unit of measure">
          <b-form-select v-model="product.unitOfMeasureId">
            <option disabled hidden :value="null">Please select one</option>
            <option v-for="option in unitOfMeasureOptions" v-bind:value="option.id" v-bind:key="option.id">{{ option.name }}</option>
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
import NProgress from 'nprogress';
import { required } from 'vuelidate/lib/validators';
import UnitOfMesureService from '@/services/UnitOfMeasureService.js';

export default {
  beforeCreate: function() {
    //get Unit categories
    UnitOfMesureService.getUnit()
      .then(responce => {
        this.unitOfMeasureOptions = responce.data.result.items;
      })
      .catch(error => {
        console.log('There war an error ' + error.responce);
      });
  },
  data() {
    return {
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
      unitOfMeasureId: { required }
    }
  },

  methods: {
    createProduct() {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        NProgress.start();
        this.$store
          .dispatch('product/createProduct', this.product)
          .then(() => {
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
        name: '',
        user: user,
        price: '',
        unitOfMeasureId: 402, // kgs
        quantity: 1
      };
    }
  }
};
</script>

<style scoped></style>
