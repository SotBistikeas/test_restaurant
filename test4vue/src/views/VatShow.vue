<template>
  <div>
    <span>{{ vat.name }}</span> | 
    <span>{{ vat.id }}</span> |
    <span>{{ vat.vat }}</span>
    <br><br><br><br>
    <router-link :to="{name: 'VatList'}">Back to List</router-link>
  </div>
</template>

<script>
import VatService from '@/services/VatService.js';

  export default {
    props:['id'],
    data(){
      return{
        vat:Object
      }
    },
    created(){
      VatService.getVatById(this.id)
      .then(response => {
        this.vat = response.data.result;
        console.log(this.vat)
      })
      .catch(error =>{
        console.log(error.response);
      })
    }
  }
</script>

<style scoped>

</style>