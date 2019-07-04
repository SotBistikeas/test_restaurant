<template>
    <div >
    <ProductCard  class="gap" v-for="product in products" :key="product.id" :product="product"/>


    </div>
</template>
<script>
import ProductCard from '@/components/ProductCard.vue';
import {mapState} from 'vuex';
export default {
        components:{
            ProductCard
        },
        created(){
            this.$store.dispatch('fetchProducts')
        },
        computed: mapState(['products'])

    }
</script>


<style scoped>
.gap{
    margin: 5px;
}
</style>


























