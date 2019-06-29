<template>
    <div >
        
        <h1>Product List for user: {{ user.user.name }}</h1>
    <ProductCard  class="box" v-for="product in product.products" />
    <template v-if="page != 1">
        <router-link :to="{name: 'ProductList', query:{page: page - 1}}" rel="prev">Prev page</router-link>
        <template v-if="hasNextPage"> | </template>
    </template>
    <router-link v-if="hasNextPage" :to="{name: 'ProductList', query:{page: page + 1}}" rel="next">Next page</router-link>
    </div>
</template>
<script>
import ProductCard from '@/components/ProductCard.vue';
import {mapState} from 'vuex';
import store from '@/store/store';

function getPageProducts(routeTo,next){
    const currentPage = parseInt(routeTo.query.page) || 1
            store.dispatch('product/fetchProducts', {
                page: currentPage  
            })
            .then(()=>{
                routeTo.params.page = currentPage
                next()
            })
}

export default {
        props: {
            page: {
                type: Number,
                required: true 
            },
        },
        
        components:{
            ProductCard
        },
        beforeRouteEnter (routeTo, routeFrom, next) {
           getPageProducts(routeTo,next)
        },
        
        beforeRouteUpdate( routeTo, routeFrom,next){
            getPageProducts(routeTo,next)
        },
        computed:{ 
       
        hasNextPage() {
            return this.product.productsTotal > this.page * this.product.perPage
        },
        ...mapState(['product','user'])
        }
    }
</script>


<style scoped>
.box{
    border: 1px, solid ,black;
}
</style>


























