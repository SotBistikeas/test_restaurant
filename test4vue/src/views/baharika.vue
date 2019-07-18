<template>
    <div>
        <h1>lista me ola ta baharika</h1>
        <ol>
            <li v-for="b in baharika.baharika" :key="b">
                {{ b }}
            </li>
        </ol>
    

    </div>
</template>

<script>
import {mapState} from 'vuex';

export default {
    
    computed:{ 
    ...mapState({
        baharika: 'baharika'
        })
        }
}
</script>
