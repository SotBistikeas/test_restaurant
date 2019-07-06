<template>
    <div>
        <h2>Create a new Dish</h2>
        <form @submit.prevent="add" class="bake">
            <div>
                <v-select ref="select" v-model="prodsOfDish" label="onoma" :value="product.timi" placeholder="Choose one" :options="products">
                    <template slot="singleLabel" slot-scope="{ product }">{{ product.timi }}</template>
                </v-select>
                <!-- <span>{{prodsOfDish.timi}}</span> -->
                <BaseInput label="Quantity" v-model="quantity" />
                <BaseButton >Add to dish</BaseButton>
                <!-- <span v-if="!(isNaN(prodsOfDish.timi * quantity))">{{prodsOfDish.timi * quantity}}</span> -->
            </div>
        </form>
        <br/>
        <form class="bake">        
            <table v-if="pds.length > 0">
                <tr >
                    <td class="bake">Onoma</td>
                    <td class="bake">posotita</td>
                    <td class="bake">kostos ana piato</td>
                </tr>       
                <tr v-for="p in pds" :key="p.Id">
                    <td class="bake">{{ p.name }} </td>
                    <td class="bake">{{p.Quan}}</td>
                    <td class="bake">{{p.Pri}}</td>
                </tr>
            </table>       
        <br/>         
        <BaseInput label="Onoma piatou:" v-model="dish.name" :class="{error: $v.dishVal.name.$error}"
                    @blur="$v.dishVal.name.$touch()"/>
        <template v-if="$v.dishVal.name.$error">
            <p v-if="!$v.dishVal.name.required" class="errorMessage">Name is required</p>
        </template> 

        <BaseButton type="submit" @click="createDish" :disabled="$v.$anyError">Save Dish</BaseButton>
        <span v-if="this.TP != 0">Sub-Total: {{this.TP}}</span>
        </form>
    </div>
</template>

<script>
import NProgress from 'nprogress';
import vSelect from 'vue-select';
import ProductService from '@/services/ProductService.js';
import { required } from 'vuelidate/lib/validators';

    export default {
       components: {
        vSelect
    },
        data(){
            return {
                value:'',
                prodsOfDish: this.$store.state.prodsOfDish,
                products:[],
                dish: this.createFreshDishObject(),
                product:Object,
                quantity: this.$store.state.quantity,
                price: this.$store.state.price,
                tempPrice: this.$store.state.tempPrice,
                skato:{},
                pds: this.$store.state.pds,
                TP:'',
                
            }
        },
        validations:{
            dishVal:{
                name:{required},
            }
        },
        mounted(){
           ProductService.getAllProducts()
           .then(response => this.products = response.data)
        },
        methods:{
            
            add(){
                this.pds.push(
                     this.skato =  {
                     Id : this.prodsOfDish.id,
                     name: this.prodsOfDish.onoma,
                     Quan : this.quantity,
                     Pri : this.price = (this.quantity) * Number(this.prodsOfDish.timi),
                     
                     },
                this.TP = this.tempPrice += Number(this.price),
                this.dish.price = this.TP     
                     
                 )
            },
            createDish(){
                this.$v.$touch()
                if (!this.$v.$invalid){
                NProgress.start()
                this.$store
                .dispatch('dish/createDish', this.dish)
                .then(()=>{
                     //this.$router.push({
                       //  name:'DishShow',
                        // params:{ id : this.dish.id}
                    // })
                    console.log('1');
                    this.dish = this.createFreshDishObject(),
                    this.prodsOfDish = this.$store.state.prodsOfDish,
                    this.quantity = this.$store.state.quantity,
                    this.price = this.$store.state.price,
                    this.tempPrice = this.$store.state.tempPrice,
                    this.pds = this.$store.sate.pds,
                    this.TP = '';

                })
                .catch(()=>{
                    NProgress.done()
                    console.log("2");
                })
                }
                
            },
            createFreshDishObject(){
                const user = this.$store.state.user.user
                const id = Math.floor(Math.random()*100000)
                return {
                    id:id,
                    user:user,
                    name:'',
                    price:'',
                    pds:this.$store.state.pds,
                }
            }
        },
        
    }
</script>

<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
<style scoped>
.bake{
    border: 1px solid lightblue;
}
</style>