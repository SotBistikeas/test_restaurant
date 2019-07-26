<template>
  <div>
    <h2>Create a new FoodIngredient</h2>

    <form @submit.prevent="createFoodIngredient">
      <BaseInput label="Name:" v-model="FoodIngredient.name" class="field" />
      <select v-model="FoodIngredient.unitOfMeasureId">
        <option disabled hidden :value="null">Please select one</option>
        <option v-for="option in unitOfMeasureOptions" v-bind:value="option.id" v-bind:key="option.id">{{ option.name }}</option>
      </select>
      <BaseButton type="submit" variant="success">Create Food Ingredient</BaseButton>
    </form>
  </div>
</template>

<script>
import ApiService from '@/services/ApiService.js';

export default {
  beforeCreate: function() {
    ApiService.getUnit()
      .then(responce => {
        this.unitOfMeasureOptions = responce.data.result.items;
      })
      .catch(error => {
        console.log('There war an error ' + error.responce);
      });
  },
  data() {
    return {
      unitOfMeasureOptions: [],
      FoodIngredient: {
        name: '',
        unitOfMeasureId: ''
      },
      x: ''
    };
  },

  methods: {
    createFoodIngredient() {
      ApiService.postFoodIngredient(this.FoodIngredient)
        .then(response => {
          this.$router.push({
            name: 'FoodIngredientShow',
            params: { id: response.data.result.id }
          });
        })
        .catch(error => {
          console.log(error.message);
        });
    }
  }
};
</script>

<style scoped></style>
