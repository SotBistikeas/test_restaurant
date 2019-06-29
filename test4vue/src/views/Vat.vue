<template>
    <div>
        <div v-for="v in vat" :key="v.id">
            <div v-for="item in v" :key="item.id">
                <div v-if="item.name != null">
                    {{ item.name }} ** {{item.id}} ** {{item.vat}}
                </div>
                
                
            </div>
        </div>    
    </div>
</template>

<script>
import axios from 'axios';

export default {
    data(){
            return {
                vat: [],
                x:{}
            }
        },
    created(){
        axios.get('http://localhost:21021/api/services/app/VatCategory/GetAll')
        .then(responce=>{
            this.x = responce.data
            
        })
        .then(responce =>{
            this.vat = this.x.result
        })
    }
}
   
</script>

