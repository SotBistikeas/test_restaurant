<template>
  <div>
    <h1>Dish List for user: {{ user.user.name }}</h1>
    <DishCard class="box" v-for="dish in dish.dishes" :key="dish.id" :dish="dish" />
    <template v-if="page != 1">
      <router-link :to="{ name: 'DishList', query: { page: page - 1 } }" rel="prev">Prev page</router-link>
      <template v-if="hasNextPage">
        |
      </template>
    </template>

    <router-link v-if="hasNextPage" :to="{ name: 'DishList', query: { page: page + 1 } }" rel="next">Next page</router-link>
  </div>
</template>
<script>
import DishCard from '@/components/DishCard.vue';
import { mapState } from 'vuex';
import store from '@/store/store';

function getPageDishes(routeTo, next) {
  const currentPage = parseInt(routeTo.query.page) || 1;
  store
    .dispatch('dish/fetchDishes', {
      page: currentPage
    })
    .then(() => {
      routeTo.params.page = currentPage;
      next();
    });
}

export default {
  props: {
    page: {
      type: Number,
      required: true
    }
  },

  components: {
    DishCard
  },
  beforeRouteEnter(routeTo, routeFrom, next) {
    getPageDishes(routeTo, next);
  },

  beforeRouteUpdate(routeTo, routeFrom, next) {
    getPageDishes(routeTo, next);
  },
  computed: {
    hasNextPage() {
      return this.dish.dishesTotal > this.page * this.dish.perPage;
    },
    ...mapState(['dish', 'user'])
  }
};
</script>

<style scoped>
.box {
  border: 1px, solid, black;
}
</style>
