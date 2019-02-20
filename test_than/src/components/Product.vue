<template>
<div id="databinding" >
    <h1>{{msg}}</h1>
    <router-link to="/">Go to home page</router-link>
    <router-link to="/Dishes">Go to dish page</router-link>

    <form >
   
    <h1>Details</h1>
    <span>Product Name</span>
    <input type="text" placeholder="enter name" v-model = "name" v-validate="'min:5'" name="name">
    <p class="alert" v-if="errors.has('name')">{{ errors.first('name')}}</p>
    

    <span>Price per kilo</span>
    <input type="number" placeholder="enter price per kilo" v-model = "price" >
       
    <!-- <span>VAT</span>
    
        <select v-model="vat">
            <option disabled value="">Please select Vat</option>
            <option value="3">3%</option>
            <option value="13">13%</option>
            <option value="23">23%</option>
          </select> -->
         
    <button @click="showdata" v-bind:style="styleobj">Add</button>
  
    </form>
        
      
     
    <br/>
    <div>
      <table class="Table">
        <tr class="Row">
          <th>Name</th>
          <th>Price per kilo/litre</th>
        </tr>
        <tr class="Row" v-for="(product,index) in products" :key="index" v-bind:style="styleobj">
          <td>{{product.name}}</td>
          <td>{{product.price}}</td>
          <td><button v-on:click="products.splice(index, 1)">remove item</button></td>
        </tr>
      </table>
    <!--<productcomponent v-for="(product, index) in products" :key="index"-->
                       <!--v-bind:product="product"-->
                       <!--v-bind:index="index"-->
                       <!---->
                       <!--v-on:removeelement="products.splice(index, 1)"> -->
                       <!---->
    <!--</productcomponent>-->
   
    
    </div>
</div>
</template>


<script>
import axios from 'axios';
import { Component, Vue } from 'vue-property-decorator';

export default {

    name:'product',    
    
    data () {
    return {
        name:'',
        price:'',
        // vat:'',
        product:'',
        
        products:[],
        styleobj:{
            backgroundColor:'#2196F3!important',
            cursor:'pointer',
            padding:'8px 16px',
            verticalAling: 'left',
            },
        msg:'Product page'   
        }
    },
    created(){
        axios.get('http://localhost:9000/products').then (response => {
                this.products = response.data;
            });
            
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
            
            
            
            // this.products.push({
            //     name:this.name,
            //     price:this.price,
            //     vat:this.vat
            //});
            this.name='';
            this.price='';
            // this.vat='';
        },

        getcolor: function () {
          if (this.index % 2){
            return "#00FFFF"
          } else{
            return "#008080"
          }

      },
    },
    components:{
        productcomponent: {
            template: '<div class="Table"><div class="Row" v-bind:style="styleobj"><div class="Cell"><p>{{product.name}}</p></div><div class="Cell"><p>{{product.price}}</p></div></div></div>',
            props: ['product','index'],
            data: function(){
             return {
                styleobj:{
                    backgroundColor:this.getcolor(),
                    fontsize:20
                }
             }
            },
            methods: {
                getcolor: function () {
                    if (this.index % 2){
                        return "#00FFFF"
                    } else{
                        return "#008080"
                    }            
                }
            },

        }
    },
    computed: {
     productsFilter: function() {
       var textSearch = this.textSearch;
       return this.products.filter(function(el) {
         return el.name.indexOf(textSearch) !== -1;
       });
     }
  },
}

</script>




<style >
#databinding{
    padding: 20px 15px 15px 15px;
    margin: 0 0 25px 0;
    
}
span, option, input {
    font-size: 20px;
}
.Table{
    display: table;
    width: 80%;
}
.Title{
    display: table-caption;
    text-align: center;
    font-weight: bold;
    font-size: larger;
}
.Heading{
    display: table-row;
    font-weight: bolder;
    text-align: center;
}
.Row{
    display: table-row;
}
.Cell{
    display: table-cell;
    border: solid;
    border-width: thin;
    padding-left: 5px;
    padding-right: 5px;
    width: 30%;
}
.alert{
    border: solid;
    width: 30%;
    display: table-cell;
}
</style>
