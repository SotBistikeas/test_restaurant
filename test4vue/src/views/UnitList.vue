<template>
    <div>
        <span v-if="units.length != 0">Total unit options: {{ units.length }}</span>
    
        <UnitCard class="gap" v-for="unit in units " :key="unit.id" :unit="unit"/> 
   
    </div>
</template>

<script>
import UnitCard from '@/components/UnitCard.vue';
import {mapState} from 'vuex';
export default {
    components:{
        UnitCard,
    },
    created(){
            this.$store.dispatch('fetchUnits')
            console.log('store.dispatch-fetchunits')
        },
        computed: mapState(['units'])
}
   
</script>

<style scoped>
.gap{
    margin: 5px;
}
</style>