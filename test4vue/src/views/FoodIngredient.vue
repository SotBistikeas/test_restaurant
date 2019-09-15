<template>
  <div>
    <b-card
      header-tag="header"
      footer-tag="footer"
      style="max-width: 20rem;"
      class="mb-2"
      header-class="yo"
      body-bg-variant="info"
      footer-bg-variant="danger"
    >
      <h5 slot="header" class="mb-0">Create new Food igredient</h5>
      <form @submit.prevent="createFoodIngredient">
        <BaseInput label="Name:" v-model="FoodIngredient.name" class="field" />
        <div>
          <span>Unit:</span>
          <select v-model="FoodIngredient.unitOfMeasureId">
            <option disabled hidden :value="null">Please select one</option>
            <option v-for="option in unitOfMeasureOptions" v-bind:value="option.id" v-bind:key="option.id">{{ option.name }}</option>
          </select>
        </div>
      </form>

      <span slot="footer" class="mb-0">
        <BaseButton type="submit" variant="success" size="lg">Create Food Ingredient</BaseButton>
      </span>
    </b-card>
  </div>
</template>

<script>
import UnitOfMeasureService from '@/services/UnitOfMeasureService.js';
import FoodIngredientService from '@/services/FoodIngredientService.js';

export default {
  beforeCreate: function() {
    UnitOfMeasureService.getUnit()
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
      FoodIngredientService.postFoodIngredient(this.FoodIngredient)
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
