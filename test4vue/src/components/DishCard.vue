<template>
  <div class="box">
    <router-link class="box" :to="{ name: 'DishShow', params: { id: dish.id } }">
      Name: {{ dish.name }} 
      Id: {{ dish.id }}
    </router-link>
  </div>
</template>

<script>
export default {
  props: {
    dish: Object
  }
};
</script>

<style scoped>
h3 {
  color: red;
}
.box {
  border: 1px dotted green;
}
</style>
