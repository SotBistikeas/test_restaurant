<template>
  <b-list-group>
    <b-list-group-item v-for="product in products" :key="product.id" :to="{ name: 'ProductShow', params: { id: product.id } }">
      <span>
        name: {{ product.name }} | price: {{ product.price }} | unit: {{ product.unitOfMeasureId }} | quantity: {{ product.quantity }} | id:
        {{ product.id }}
      </span>
    </b-list-group-item>
  </b-list-group>
</template>
<script>
import { mapState } from 'vuex';
export default {
  beforeCreate() {
    this.$store.dispatch('fetchProducts');
  },
  computed: mapState(['products'])
};
</script>
