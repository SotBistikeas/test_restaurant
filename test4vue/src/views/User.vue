<template>
    <div>
        <h1>this is {{ user.user.name }}'s page</h1>
        <h3>{{ user.user.name }} is: {{user.user.isActive}}</h3>
    

    </div>
</template>

<script>
import {mapState} from 'vuex';

export default {
    
    computed:{ 
    ...mapState(['user'])
        }
}
</script>
