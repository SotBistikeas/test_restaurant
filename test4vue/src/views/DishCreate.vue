<template>
  <div>
    <h2>Create a new Dish</h2>

    <form @submit.prevent="createDish">
      <BaseInput label="Name:" v-model="dish.name" class="field" />
      <BaseButton type="submit" variant="success">Create Dish</BaseButton><span>Upon creation you will be redirected to add products</span>
    </form>
  </div>
</template>

<script>
import ApiService from '@/services/ApiService.js';

export default {
  data() {
    return {
      dish: {}
    };
  },

  methods: {
    createDish() {
      ApiService.postDish(this.dish)
        .then(response => {
          this.$router.push({
            name: 'DishShow',
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
