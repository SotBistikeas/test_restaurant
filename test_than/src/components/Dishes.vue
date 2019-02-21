<template>
    <div id="dish">
        <h1>{{ msg }}</h1>
        <h3>
            <router-link to="/">Go to home page</router-link>
        </h3>
        <h3>
            <router-link to="/product">Go to product page</router-link>
        </h3>
        <div class="Table">
            <div class="Row">
                <span>Dish Name</span>
                <input type="text" placeholder="enter name" v-model="nameinput">
              <span>{{cost}}</span>
                <button v-on:click="addit">add dish</button>
            </div>


    

            <div class="Row" v-if="products.length !=0 ">
    
    
                <div class="Cell">
                    <span>onoma ylikou: </span>
                    <br/> {{product.name}}
    

                    <select v-model="proion">
                            <option v-for="product of products" v-text="product.name +' '+ product.price + '€'" :value="product"></option>
                            
                        </select>
                    <!--<p>**{{proion}}**</p>-->
                </div>
                <div class="Cell">
                    <span>price per kilo/litre</span>
                    <br/> {{proion.price}}
                </div>
                <div class="Cell">
                    <span>quantity needed: </span>
                    <input type="number" placeholder="gr / ml" v-model="quantity">
                </div>
                <div class="Cell">
                    <span>cost per dish</span><br/>
                    <span v-bind:value="cost">{{proion.price * quantity}}</span>
                </div>
                <div class="Cell">
                    <button @click="addprotodish">Add</button>
                </div>
            </div>
        </div>

      <br/>
      <br/>
      <br/>

      <!-- <div>
          <p>{{dishes[0]}}</p>
          <p>*************</p>
           <div v-for="item in dishes" >
              <div <div v-for="productsofdish in products">
                  **{{ products[0].name }}-{{ products[0].price }}**
                  <br/>
                  **{{ products[1].name }}-{{ products[1].price }}**

              </div>


          </div>


      </div> -->
        <!-- <dishcomponent v-bind="productsofdish">
    
            <productsofdishcomponent v-bind="dishes" >
               
                </productsofdishcomponent>   
        </dishcomponent> -->
        <!--<div v-for="d in dishes">-->
            <!--<ol>-->
                <!--<p>onama piatou: {{d.name}}</p>-->
                <!--<div v-for="product of d.productsofdish">-->
    <!---->
                    <!--<li>-->
                        <!--<span>-->
                        <!--<div class="Table">-->
                        <!--<div class="Row">-->
                            <!---->
                            <!--<div class="Cell"><p>yliko: {{product.name}}</p></div>-->
                            <!--<div class="Cell"><p>timi kilou/litrou: {{product.price}}</p></div>-->
                            <!--<div class="Cell"><p>vat: {{product.vat}}%</p></div>-->
                        <!--</div>-->
                        <!--</div>-->
                    <!--</span>-->
                    <!--</li>-->
    <!---->
                <!--</div>-->
            <!--</ol>-->
        <!--</div>-->
        <table>
            <tr class="Row">
                <th class="Cell">Onama ylikou</th>
                <th class="Cell">quantity per dish</th>
                <th class="Cell">Cost per dish</th>
            </tr>

                       <div v-for="item in listofproducts" class="Row">
                          <tr v-for="x in item.productsofdish" class="Cell">
                            <td ><p>{{x}}</p></td>

                            <td></td>
                          </tr>
                        </div>
                        <tr></tr>
                          <!--<productsofdishcomponent v-for="(product, index) in listofproducts" :key="index"
                                                   v-bind="product" >

                          </productsofdishcomponent>-->

                
            
        </table>
        <div v-for="x in listofproducts" >
            {{x.productsofdish.name}}
          {{x.productsofdish.price}}
          {{x.productsofdish.quantity}}


        </div>
    <!--<P>!*{{listofproducts}}*!</P>-->
    <!-- <productcomponent v-for="(item, index) in products" :key="index"
                       v-bind:item="item"
                       v-bind:index="index"
                       v-bind:product="item"
                       
                       v-on:removeelement="products.splice(index, 1)"> 
                       
    </productcomponent> -->



  <!--  <div> <p>skata</p></div>

<input  type="text" placeholder="search...yliko"/>
<tr v-for="product in products">
    data: {{ product.price}}
</tr>
-->
    
<div>{{dishes}}************</div>


  </div>

</template>

<script>
    import axios from 'axios';

    
    export default {
    
        name: 'dish',
    
        data() {
            return {
                msg: 'Dish page',
                name: '',
                product: {
                    name: '',
                    price: '',
                    vat: ''
                },
                nameinput:'',
                options: [],
                products: [],
                productsofdish:[],
                listofproducts: [],
                quantity: '',
                proion: '',
                dishes: [],
                cost: '',
                totalprice: '',
              x:'',

            }
        },
        options: {
            type: Array,
            default () {
                return product.price
            },
        },
        created() {
            axios.get('http://localhost:9000/products').then(response => {
                this.products = response.data;
                this.options = response.data;
            });
            axios.get('http://localhost:9000/dishes').then(response => {
                this.dishes = response.data;
            });
        },
        methods: {
    
            addit: function() {
                this.dishes.push({
                  name: this.nameinput,
                  productsofdish: this.listofproducts,
                  price: this.cost,

                });
              console.log(this.listofproducts);
               alert ("yo");
                  axios.post('http://localhost:9000/dishes', {
                        name: this.nameinput,
                        price: this.cost,
                        productsofdish: this.listofproducts,

                  }).then(() => {
                      axios.get('http://localhost:9000/dishes').then(response => {
                          this.dishes = response.data;
                      });
                  });
    
            },
            addprotodish: function() {


              this.listofproducts.push({
                    productsofdish: {
                        name: this.proion.name,
                        quantity: this.quantity,
                        price: this.quantity * this.proion.price
                    },

                });
              console.log(this.listofproducts);
                this.cost =  (this.quantity * this.proion.price) + this.cost,
                this.name = '',
                this.quantity = ''

                // alert("ta kataferes");


            }
        },
        components: {
            
            productsofdishcomponent: {
                props: {
                    product: Object,
                  listofproducts: Array,
                  productsofdish: Object,
                },
                template: '<div class="Row"><td class="Cell"><span>{{productsofdish.name}}</span></td><td class="Cell"><span>{{productsofdish.price}}</span></td></div>',
                  // '<div class="Cell"><ol><li v-for="(product, index) in products" :key="index">{{product.name}}-{{product.price}}€-{{product.vat}}%</li></ol></div>',
                // '<div class="Table"><div class="Row"><div class="Cell"><p>{{dishes}}</p></div></div></div>',
    
    

    
            },
            dishcomponent: {
                props: {
                    dishes: Array,
                    productsofdish: Array
                },
                template: '<div class="Table"><div class="Row"><div class="Cell"><p>skata</p></div></div></div>'
            },

    
            // vselect:{
            //     props:{
            //         options: axios.get('http://localhost:9000/products').then (response => { this.options = response.data; })
            //     },
            //     template:'',
            //    data: function(){
            //     return {
            //             options:[]
            //     }
            // },
    
            // }
    
    
        },
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    #dish {
        padding: 20px 15px 15px 15px;
        margin: 0 0 25px 0;
    }
    
    span,
    option,
    input {
        font-size: 20px;
    }
    
    .Table {
        display: table;
        width: 80%;
    }
    

    
    .Row {
        display: table-row;
        background-color: antiquewhite;

    }
    
    .Cell {
        display: table-cell;
        border: solid thin;

        padding-left: 5px;
        padding-right: 5px;
        width: 10%;
        background-color: aqua;
    }
</style>
