<template>
    <div>
                
        <UnitCard v-for="unit in units" :key="unit.id" :unit="unit"/> 
   
    </div>
</template>

<script>
import UnitCard from '@/components/UnitCard.vue';
import UnitOfMesureService from '@/services/UnitOfMeasureService.js';
export default {
    components:{
        UnitCard,
    },
    data(){
            return {
                units:[]
            }
        },
    created(){
        UnitOfMesureService.getUnit()
        .then(responce=>{
            this.units = responce.data.result.items
        })
        .catch(error =>{
            console.log('There war an error ' + error.responce);
        })
        
    }
}
   
</script>

<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
