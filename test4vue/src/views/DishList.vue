<template>
  <div>
    <DishCard class="box" v-for="dish in dishes" :key="dish.id" :dish="dish" />
  </div>
</template>
<script>
import DishCard from '@/components/DishCard.vue';
import ApiService from '@/services/ApiService.js';

export default {
  data() {
    return {
      dish: {},
      dishes: []
    };
  },
  components: {
    DishCard
  },
  beforeCreate() {
    ApiService.getDish().then(response => {
      this.dishes = response.data.result.items;
    });
  }
};
</script>

<style scoped>
.box {
  border: 1px, solid, black;
}
</style>
