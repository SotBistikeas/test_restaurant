<template>
  <div class="product-list">
    <h1>Test page</h1>
    <product/>
    <dish/>
  </div>
</template>

<script >
import { Component, Vue } from 'vue-property-decorator';
import axios from 'axios';
import product from '@/components/Product';
import dish from '@/components/Dishes';

export default {
    name: 'productList',
    components:{
        product,
        dish
    }

}






</script>

<style scoped>
.grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  grid-gap: 10px;
  grid-auto-rows: minmax(100px, auto);
}
</style>
