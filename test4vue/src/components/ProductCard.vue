<template>
  <div class="box">
    <router-link :to="{name:'ProductShow', params:{ id : product.id}}">
    <span>name: {{ product.name }} |
    price: {{ product.price }} |
    vat: {{ product.vatCategoryId }} |
    unit: {{ product.unitOfMeasureId }} |
    quantity: {{ product.quantity }} |
    id: {{ product.id }}</span>
    </router-link>
  </div>
</template>

<script>
  export default {
    props:{
      product:Object
    }
  }
</script>

<style scoped>
h3 {
  color: red;
}
.box {
  border: 1px dotted green;
}
</style>