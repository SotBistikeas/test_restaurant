<template>
  <div>
    <FoodIngredientCard class="gap" v-for="foodIngredient in FoodIngredients" :key="foodIngredient.id" :foodIngredient="foodIngredient" />
  </div>
</template>
<script>
import FoodIngredientCard from '@/components/FoodIngredientCard.vue';
import FoodIngredientServices from '@/services/FoodIngredientService.js';
export default {
  data() {
    return {
      FoodIngredients: []
    };
  },
  components: {
    FoodIngredientCard
  },
  beforeCreate() {
    FoodIngredientServices.getAllFoodIngredients().then(Response => {
      this.FoodIngredients = Response.data.result.items;
    });
  }
};
</script>

<style scoped>
.gap {
  margin: 5px;
}
</style>
