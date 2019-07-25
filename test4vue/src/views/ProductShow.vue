<template>
  <div>
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
        <h5 slot="header" class="mb-0">{{ product.result.name }}</h5>
        <p>name: {{ product.result.name }}</p>
        <p>price: {{ product.result.price }}</p>
        <p>unit id: {{ product.result.unitOfMeasureId }}</p>
        <p>Quantity: {{ product.result.quantity }}</p>

        <BaseButton @click="deleteIt()" variant="danger">Delete</BaseButton>

        <h5 slot="footer" class="mb-0" v-if="edit == true">
          <BaseInput v-model="editedname" label="enter new name" />
          <BaseInput v-model="editedPrice" label="enter new price" />
          <BaseInput v-model="editedUnit" label="enter new unit" />
          <BaseButton pill size="sm" @click="updateIt()" variant="dark">Update</BaseButton>
        </h5>

        

        <b-form-checkbox v-model="edit" switch>Edit name</b-form-checkbox>
        <span slot="footer" class="mb-0">
          <router-link :to="{ name: 'ProductList' }">Back to Product List</router-link>
        </span>
      </b-card>
  </div>
</template>

<script>
import NProgress from 'nprogress';
import ProductService from '@/services/ProductService.js';
import axios from 'axios';

export default {
  props: ['id'],
  data() {
    return {
      product: {
        result: {}
      },
      edit: false,
      editedname:'',
      editedPrice:'',
      editedUnit:'',

    };
  },
  created() {
    this.getIt();
  },
  methods: {
    deleteIt() {
      console.log('this id = ' +this.id);
      axios
        .delete('http://localhost:21021/api/services/app/Product/Delete?Id=' + this.id)
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
    updateIt(){
      NProgress.start();
      axios
        .put('http://localhost:21021/api/services/app/Product/Update',{
          name: this.editedname,
          price: this.editedPrice,
          unitOfMeasureId: this.editedUnit,
          quantity: this.product.quantity,
          id: this.id
        })
        .then(response =>{
          if (response.data.error == null ){
            this.getIt();
            this.edit = false;
            this.editedname = '';
            this.editedPrice = '';
            this.editedUnit = '';
          }
        })
        .catch(error =>{
          console.log(error.message);
        })
      NProgress.done();
    },
    getIt(){
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

<style scoped></style>
