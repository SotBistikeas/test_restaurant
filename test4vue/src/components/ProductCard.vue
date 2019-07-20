<template>
  <router-link :to="{ name: 'ProductShow', params: { id: product.id } }">
    <span
      >name: {{ product.name }} | price: {{ product.price }} | unit: {{ product.unitOfMeasureId }} | quantity: {{ product.quantity }} | id:
      {{ product.id }}</span
    >
  </router-link>
</template>

<script>
export default {
  props: {
    product: Object
  }
};
</script>
