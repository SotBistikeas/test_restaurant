<template>
  <div class="box">
    <router-link
      class="box"
      :to="{ name: 'FoodIngredientShow', params: { id: foodIngredient.id } }"
    >{{ foodIngredient.name }} - {{ foodIngredient.id }} - {{foodIngredient.isProduct}}</router-link>
  </div>
</template>

<script>
export default {
  props: {
    foodIngredient: Object
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
