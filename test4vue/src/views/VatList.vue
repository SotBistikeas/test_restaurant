<template>
    <div >
        
    <VatCard  v-for="vat in vats" :key="vat.id" :vat="vat"/>

    </div>
</template>
<script>
import VatCard from '@/components/VatCard.vue';
import VatService from '@/services/VatService.js';

export default {
        components:{
            VatCard
        },
        data(){
            return {
                vats:[]
            }
        },
        created(){
            VatService.getVat().then(response => {
                this.vats = response.data.result.items;
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


























