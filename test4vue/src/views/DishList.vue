<template>
  <div>
    <DishCard class="box" v-for="dish in dishes" :key="dish.id" :dish="dish" />
  </div>
</template>
<script>
import DishCard from '@/components/DishCard.vue';
import axios from 'axios';



export default {
  data(){
    return{
      dish:{},
      dishes:[],
    }
  },
  components: {
    DishCard
  },
  beforeCreate() {
    axios
      .get('http://localhost:21021/api/services/app/Dish/GetAll?MaxResultCount=100000', { headers: { Accept: 'application/json' } })
      .then(response => {
        this.dishes = response.data.result.items;
      });
  },
};
</script>

<style scoped>
.box {
  border: 1px, solid, black;
}
</style>
