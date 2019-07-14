<template>
  <b-list-group>
    <b-list-group-item
      v-for="product in products"
      :key="product.id"
      :to="{name:'ProductShow', params:{ id : product.id}}"
    >
      <span>
        name: {{ product.name }} |
        price: {{ product.price }} |
        vat: {{ product.vatCategoryId }} |
        unit: {{ product.unitOfMeasureId }} |
        quantity: {{ product.quantity }} |
        id: {{ product.id }}
      </span>
    </b-list-group-item>
  </b-list-group>
</template>
<script>
import ProductCard from "@/components/ProductCard.vue";
import { mapState } from "vuex";
export default {
  components: {
    ProductCard
  },
        beforeCreate(){
    this.$store.dispatch("fetchProducts");
  },
  computed: mapState(["products"])
};
</script>


























