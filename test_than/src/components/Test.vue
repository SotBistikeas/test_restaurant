<template>
    <div id="test">
       <form >
   
    <h1>Details</h1>
    <span>Product Name</span>
    <input type="text" placeholder="enter name" v-model = "name" v-validate="'min:5'" name="name">
    <p class="alert" v-if="errors.has('name')">{{ errors.first('name')}}</p>
    

    <span>Price per kilo</span>
    <input type="number" placeholder="enter price per kilo" v-model = "price" >
         
    <button @click="showdata">Add</button>
  
    </form>
    </div>
</template>


<script>
export default {
    name:'test',

    data(){
        return {
            product:{
                name:'',
                price:''
            }
        }
    },
    methods:{
       showdata: function(){
            
            axios.post('http://localhost:9000/product',{
                name : this.name,
                price : this.price,
                // vat : this.vat
            }).then( () => {
                axios.get('http://localhost:9000/products').then (response => {
                this.products = response.data;
                });
            } );
            
            this.name='';
            this.price='';
        }
    }
}
</script>
