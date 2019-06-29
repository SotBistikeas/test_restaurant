
<template>
    <div>
    <!-- <h1>Product Create by user: "{{user.name}}"</h1>
    <h1>With id: {{user.id}}</h1>-->
    <h2>Create a new product</h2>

    <form @submit.prevent="createProduct">
        <BaseInput  label="Onoma:" 
                    v-model="product.onoma"
                    class="field"
                    :class="{error: $v.product.onoma.$error}"
                    @blur="$v.product.onoma.$touch()"
                    />
        <template v-if="$v.product.onoma.$error">
            <p v-if="!$v.product.onoma.required" class="errorMessage">Name is required</p>
        </template>    
        <BaseInput  label="Timi ana kilo:" 
                    v-model="product.timi"
                    class="field"
                    :class="{error: $v.product.timi.$error}"
                    @blur="$v.product.timi.$touch()"
                    />
        <template v-if="$v.product.timi.$error">
            <p v-if="!$v.product.timi.required" class="errorMessage">Price is required</p>
        </template>  
          <select v-model="product.vat">
            <option disabled value="">Please select one</option>
                <option>{{vatOption1.vat}}</option>
                <option>{{vatOption2.vat}}</option>
                <option>{{vatOption3.vat}}</option>
              
        </select> 
        
          <!-- <BaseSelect label="V.A.T" 
                    :options="vatOptions" 
                    v-model="product.vat" 
                    :class="{error : $v.product.vat.$error }"
                    @blur="$v.product.vat.$touch()"/> -->
        
        <template v-if="$v.product.vat.$error">
            <p v-if="!$v.product.vat.required" class="errorMessage">Vat is required</p>
        </template>  
        <BaseButton type="submit"
                    :disabled="$v.$anyError">
            Save</BaseButton>
        <p v-if="$v.$anyError">Please fill out all the fields</p>
    </form>

    </div>
</template>


<script>
import NProgress from 'nprogress';
import { required } from 'vuelidate/lib/validators';
import VatService from '@/services/VatService';
import axios from 'axios';

    export default {
        beforeCreate: function(){
            axios.get('http://localhost:21021/api/services/app/VatCategory/GetAll')
                .then(responce=>{
                this.x = responce.data       
                })
                .then(responce =>{
                    this.vatOption1 = this.x.result.items[0]
                    this.vatOption2 = this.x.result.items[1]
                    this.vatOption3 = this.x.result.items[2]
                })
                .catch(error => console.log('There was an error:' + error.responce)) 
        },
        data(){
            return {
                vatOptions:{},
                vatOption1:[],
                vatOption2:[],
                vatOption3:[],
                
                x:'',
                product: this.createFreshProductObject(),
            }
        },
        
        validations:{
            product:{
                onoma: {required},
                timi: {required},
                vat: {required},
            }
        },
        
        methods:{
            createProduct(){               
                this.$v.$touch()
                if (!this.$v.$invalid){
                    NProgress.start()
                    this.$store
                    .dispatch('product/createProduct', this.product)
                    .then(()=>{
                     //this.$router.push({
                       //  name:'ProductShow',
                        // params:{ id : this.product.id}
                    // })
                    this.product = this.createFreshProductObject()
                })
                .catch(()=>{
                    NProgress.done()
                })
                }

                
                
            },
            createFreshProductObject(){
                const user = this.$store.state.user.user
                const id = Math.floor(Math.random()*100000)              
                return {
                    id:id,
                    user:user,
                    name:'',
                    price:'',
                    vat:{},
                }
            }
        }
        
        
    }

    
    
</script>



<style scoped>
  
</style>
