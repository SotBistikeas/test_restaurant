<template>
    <div >
     
    <VatCard class="gap" v-for="vat in vats" :key="vat.id" :vat="vat"/>

    </div>
</template>
<script>
import VatCard from '@/components/VatCard.vue';
import {mapState} from 'vuex';

export default {
        components:{
            VatCard
        },
        created(){
            this.$store.dispatch('fetchVats')
            console.log('store.dispatch-fetchvats')
        },
        computed: mapState(['vats'])

    }
</script>


<style scoped>
.gap{
    margin: 5px;
}
</style>


























