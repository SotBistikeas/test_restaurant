<template>
  <div class="box">
    <router-link class="box" :to="{ name: 'FoodIngredientShow', params: { id: FoodIngredient.id } }">
      {{ FoodIngredient.name }} - {{ FoodIngredient.id }} - HI!
    </router-link>
  </div>
</template>

<script>
export default {
  props: {
    FoodIngredient: Object
  }
};
</script>

<style scoped>
h3 {
  color: red;
}
.box {
  border: 1px dotted green;
}
</style>
