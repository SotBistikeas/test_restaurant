<template>
  <div>
    <b-table :items="products" :fields="productFields" striped responsive small>
      <template slot="actions" slot-scope="row">
        <b-button
          :to="{ name: 'ProductShow', params: { id: row.item.id } }"
          variant="outline-primary"
          size="sm"
        >Edit</b-button>
      </template>
      <template slot="unitOfMeasureId" slot-scope="data">
        <UnitName :unitId="data.value" />
      </template>
    </b-table>
    <b-list-group>
      <b-list-group-item
        v-for="product in products"
        :key="product.id"
        :to="{ name: 'ProductShow', params: { id: product.id } }"
      >
        <span>
          name: {{ product.name }} | price: {{ product.price }} | unit: {{ product.unitOfMeasureId }} | quantity: {{ product.quantity }} | id:
          {{ product.id }}
        </span>
      </b-list-group-item>
    </b-list-group>
  </div>
</template>
<script>
import { mapState } from "vuex";
import UnitName from "@/components/UnitName.vue";
export default {
  components: {
    UnitName
  },
  beforeCreate() {
    this.$store.dispatch("fetchProducts");
    this.$store.dispatch("fetchUnits");
  },
  data() {
    return {
      productFields: {
        id: {},
        name: {},
        price: {},
        unitOfMeasureId: {},
        actions: {}
      }
    };
  },
  computed: mapState(["products"])
};
</script>
