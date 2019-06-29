<template>
    <div>
        <router-link class="event-link" :to="{ name: 'DishShow', params: { id: dish.id } }">
            <div class="event-card -shadow">
                <h3 class="title">name: {{dish.name}}</h3>
                <h3>price: {{dish.price}}</h3>
                
            </div>
        </router-link>
    </div>
</template>

<script>

    export default {  
        props:{
            dish:Object
        }
      }
</script>

<style scoped>
.event-card {
  padding: 20px;
  margin-bottom: 24px;
  transition: all 0.2s linear;
  cursor: pointer;
}
.event-card:hover {
  transform: scale(1.01);
  box-shadow: 0 3px 12px 0 rgba(0, 0, 0, 0.2), 0 1px 15px 0 rgba(0, 0, 0, 0.19);
}
.event-card > .title {
  margin: 0;
}
.event-link {
  color: black;
  text-decoration: none;
  font-weight: 100;
}
</style>