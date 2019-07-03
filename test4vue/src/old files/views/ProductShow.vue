<template>
    <div>
        <h1>Product Show</h1>
        <h3>id: {{product.result.id}}</h3>
        <!-- <template v-if="!edit"> -->
          <h3>name: {{product.result.name}}</h3>
          <h3>price per kilo: {{product.result.price}} €</h3>
          <h3>vat: {{product.result.vatCategoryId}} %</h3>
          <h3>Unit: {{product.result.unitOfMeasureId}}</h3>
          <h3>Quantity: {{product.result.quantity}}</h3>
          <!-- <BaseButton v-on:click="deleteproduct(product.id)">Delet product</BaseButton> -->
        <!-- </template> -->

<BaseButton v-on:click="edit = true" v-if="!edit">Edit</BaseButton>
<template v-if="edit" @submit.prevent="createProduct">
<BaseInput v-model="product.onoma"></BaseInput>
<template v-if="$v.product.onoma.$error">
            <p v-if="!$v.product.onoma.required" class="errorMessage">Name is required</p>
        </template>
<BaseInput v-model="product.timi"></BaseInput>
<template v-if="$v.product.timi.$error">
            <p v-if="!$v.product.timi.required" class="errorMessage">Price is required</p>
        </template> 
<BaseInput v-model="product.vat"></BaseInput>
<BaseButton type="submit" :disabled="$v.$anyError">Save Edited product (pote)</BaseButton>
<p v-if="$v.$anyError">Please fill out all the fields</p>
<BaseButton v-on:click="edit = false">Cancel</BaseButton>
</template>



    </div>
</template>
<script>
import NProgress from 'nprogress';
import axios from 'axios';
import { required } from 'vuelidate/lib/validators';

    export default {
        data(){
            return {
              edit: false
            }
        
        },
        validations:{
            product:{
                onoma: {required},
                timi: {required},
                vat: {required},
            }
        },
        props:{
          product: Object,
          // required: true,
        },
        methods:{
          deleteproduct(id){
          NProgress.start()
          axios.delete('http://localhost:21021/api/services/app/Product/Get?Id='+id)
          .then(()=>{
          this.$router.push({
          name:'ProductList',
          })
          })
                .catch(()=>{
                    NProgress.done()
                })          } 
        },
        createProduct(){
                this.$v.$touch()
                if (!this.$v.$invalid){
                    NProgress.start()
                    this.$store
                    .dispatch('product/createProduct', this.product)
                    .then(()=>{
                     //this.$router.push({
                       //  name:'ProductShow',
                        // params:{ id : this.product.id}
                    // })
                    this.product = this.createFreshProductObject()
                })
                .catch(()=>{
                    NProgress.done()
                })
                }

                
                
            },
        
      }
</script>
<style scoped>
.location {
  margin-bottom: 0;
}
.location > .icon {
  margin-left: 10px;
}
.event-header > .title {
  margin: 0;
}
.list-group {
  margin: 0;
  padding: 0;
  list-style: none;
}
.list-group > .list-item {
  padding: 1em 0;
  border-bottom: solid 1px #e5e5e5;
}
</style>
