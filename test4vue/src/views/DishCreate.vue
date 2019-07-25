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
import NProgress from 'nprogress';
import ProductService from '@/services/ProductService.js';
import axios from 'axios';

export default {
  
  data() {
    return {
      dish:{}
    };
  },
  
  mounted() {
    ProductService.getAllProducts().then(response => (this.products = response.data));
  },
  methods: {
    // add() {
    //   this.pds.push(
    //     (this.skato = {
    //       Id: this.prodsOfDish.id,
    //       name: this.prodsOfDish.onoma,
    //       Quan: this.quantity,
    //       Pri: (this.price = this.quantity * Number(this.prodsOfDish.timi))
    //     }),
    //     (this.TP = this.tempPrice += Number(this.price)),
    //     (this.dish.price = this.TP)
    //   );
    // },
    createDish() {
      NProgress.start();
      axios
        .post('http://localhost:21021/api/services/app/Dish/Create', this.dish, {
          headers: { Accept: 'application/json' }
        })
        .then(response => {
          this.$router.push({
            name: 'DishShow',
            params: { id: response.data.result.id }
          });
        })
        .catch(error => {
          console.log(error.message);
        });
      NProgress.done();
    
    },
    
  }

};
</script>

<style scoped>

</style>
