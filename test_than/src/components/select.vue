<template>
  <v-select label="name" :filterable="false" :options="options" @search="onSearch">
    <template slot="no-options">
      type to search GitHub repositories..
    </template>
    <template slot="option" slot-scope="option">
      <div class="d-center">
        <img :src='option.owner.avatar_url'/> 
        {{ option.full_name }}
        </div>
    </template>
    <template slot="selected-option" slot-scope="option">
      <div class="selected d-center">
        <img :src='option.owner.avatar_url'/> 
        {{ option.full_name }}
      </div>
    </template>
  </v-select>
 

</template>

<script>
import axios from 'axios';


export default {

    name: 'selected',
    props:{options:[]},
    
    created(){
    axios.get('http://localhost:9000/products').then (response => {
        this.options = response.data;
    }); },
    methods:{
        
       
            
    },
    computed:{
         onSearch(search) {
           
            this.search( search, this);
    },
    search(){ 
      fetch(`http://localhost:9000/products=${escape(search)}` )
            .then(res => {
                res.json().then(json => (vm.options = json.items));
                
            })
            }
    
    }
  
}
</script>
<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
