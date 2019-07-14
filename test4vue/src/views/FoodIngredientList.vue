<template>
    <div>
    <FoodIngredientCard  class="gap" v-for="FoodIngredient in FoodIngredients" :key="FoodIngredient.id" :FoodIngredient="FoodIngredient"/>


    </div>
</template>
<script>
import FoodIngredientCard from '@/components/FoodIngredientCard.vue';
import {mapState} from 'vuex';
export default {
        components:{
            FoodIngredientCard
        },
        beforeCreate(){
            this.$store.dispatch('fetchFoodIngredient')
        },
        computed: mapState(['FoodIngredients'])

    }
</script>


<style scoped>
.gap{
    margin: 5px;
}
</style>


























