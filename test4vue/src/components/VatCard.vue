<template>
  <div class="box">
    <router-link :to="{name:'VatShow', params:{ id : vat.id}}">
    <span>{{ vat.name }} </span>
    </router-link>
  </div>
</template>

<script>
  export default {
    props:{
      vat:Object
    },
  }
</script>

