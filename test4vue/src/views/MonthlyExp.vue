<template>
  <div>
    <h2>Monthly expenses</h2>
    <span> Rent<baseInput v-model.number="rent" /> </span>
    <span> Utility bills<baseInput v-model.number="bills" /> </span>
    <span> Staff wages<baseInput v-model.number="wages" /> </span>
    <span>Total cost: {{ total }} euros</span>
  </div>
</template>
<script>
export default {
  data() {
    return {
      rent: 0,
      bills: 0,
      wages: 0,
      total: 0
    };
  },
  computed: {
    totalPrice: function() {
      return (this.total = this.rent + this.bills + this.wages);
    }
  }
};
</script>
<style scoped></style>
