<template>
  <div>
    <b-card
      title="Details"
      header-tag="header"
      footer-tag="footer"
      style="max-width: 20rem;"
      class="mb-2"
      header-class="yo"
      body-bg-variant="info"
      footer-class="bake"
    >
      <h5 slot="header" class="mb-0">{{ product.result.name }}</h5>
      <p>name: {{ product.result.name }}</p>
      <p>price: {{ product.result.price }}</p>
      <p>
        unit id: {{ product.result.unitOfMeasureId }} -
        <UnitName :unitId="product.result.unitOfMeasureId" />
      </p>
      <b-form-checkbox v-model="del" switch>Delete</b-form-checkbox>
      <b-form-checkbox v-model="edit" switch>Edit name</b-form-checkbox>

      <div v-if="del == true">
        <BaseButton @click="deleteIt()" variant="danger">Delete</BaseButton>
      </div>

      <h5 slot="footer" class="mb-0" v-if="edit == true">
        <BaseInput v-model="editedname" label="enter new name" />
        <BaseInput v-model="editedPrice" label="enter new price" />
        <BaseButton pill size="sm" @click="updateIt()" variant="dark">Update</BaseButton>
      </h5>

      <span slot="footer" class="mb-0">
        <router-link :to="{ name: 'ProductList' }">Back to Product List</router-link>
      </span>
    </b-card>
  </div>
</template>

<script>
import ProductService from '@/services/ProductService.js';
import UnitName from '@/components/UnitName.vue';
import axios from 'axios';
import apiClient from '@/services/ApiClient';

export default {
  props: ['id'],
  components: {
    UnitName
  },
  data() {
    return {
      product: {
        result: {}
      },
      edit: false,
      editedname: '',
      editedPrice: '',
      del: false
    };
  },
  created() {
    this.getIt();
  },
  methods: {
    deleteIt() {
      console.log('this id = ' + this.id);
      apiClient
        .delete('/Product/Delete?Id=' + this.id)
        .then(response => {
          this.$router.push({
            name: 'ProductList'
          });
          console.log(response.data);
          console.log('record with id:' + this.id + ' has been deleted');
        })
        .catch(error => {
          console.log(error.message);
        });
    },
    updateIt() {
      apiClient
        .put('/Product/Update', {
          name: this.editedname,
          price: this.editedPrice,
          unitOfMeasureId: this.product.result.unitOfMeasureId,
          quantity: 1,
          id: this.id
        })
        .then(response => {
          if (response.data.error == null) {
            this.getIt();
            this.edit = false;
            this.editedname = '';
            this.editedPrice = '';
            this.editedUnit = '';
          }
        })
        .catch(error => {
          console.log(error.message);
        });
    },
    getIt() {
      ProductService.getProduct(this.id)
        .then(response => {
          this.product = response.data;
        })
        .catch(error => {
          console.log(error.response);
        });
    }
  }
};
</script>

<style scoped>
.yo {
  background-color: rgb(208, 219, 49);
}
.bake {
  background-color: rgb(208, 125, 233);
}
</style>
