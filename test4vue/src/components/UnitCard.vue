<template>
    <div>

        <router-link class="box" :to="{name:'UnitShow', params : {id: unit.id}}">
            {{unit.name}} - {{ unit.id }}
        </router-link>
    </div>
</template>

<script>
    export default {
        props:{
            unit: Object
        }
    }
</script>

