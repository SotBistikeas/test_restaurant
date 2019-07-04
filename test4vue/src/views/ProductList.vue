<template>
    <div >
        
    <ProductCard  v-for="product in products" :key="product.id" :product="product"/>


    </div>
</template>
<script>
import ProductCard from '@/components/ProductCard.vue';
import ProductService from '@/services/ProductService.js';

export default {
        components:{
            ProductCard
        },
        data(){
            return {
                products:[]
            }
        },
        created(){
            ProductService.getAllProducts()
            .then(response => {
                this.products = response.data.result.items
            })
            .catch(error =>{
                console.log('There was an error getting products from db '+ error.response)
            })
        }

    }
</script>


<style scoped>
.box{
    border: 1px, solid ,black;
}
</style>


























