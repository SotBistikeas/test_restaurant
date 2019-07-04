<template>
  <div>
    <span>Name: {{unit.result.name}}</span> <br>
    <span>Id: {{unit.result.id}}</span>
    <br><br><br>
    <router-link :to="{name: 'UnitList'}">Back to Units List</router-link>
  </div>
</template>

<script>
import UnitOfMeasureService from '@/services/UnitOfMeasureService.js';

  export default {
    props:['id'],
    data(){
      return{
        unit:{
          result:{}
        }
      }
    },
    created(){
      UnitOfMeasureService.getUnitById(this.id)
      .then(response => {
        this.unit = response.data;
      })
      .catch(error =>{
        console.log(error.response);
      })
    }
  }
</script>

<style scoped>

</style>