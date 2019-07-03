<template>
    <div class="lol">

        <router-link :to="{name:'UnitShow', params : {id: unit.id}}">
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

<style scoped>
.lol{
    border: 1px dotted red;
    margin: 1px;
}
</style>