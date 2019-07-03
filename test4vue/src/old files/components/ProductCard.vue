<template>
    <div>
        <!-- <router-link class="event-link" :to="{ name: 'ProductShow', params: { id: product.id } }"> -->
            <div class="event-card -shadow">
                <h3 class="title">name: {{product.result.items.name}}</h3>
                <h2>price per kilo: {{product.price}} €</h2>
                <h6>vat: {{ product.vat}} %</h6>
                <h6 v-if="product.organizer != null">organizer:  {{product.organizer}}</h6>
            </div>
        <!-- </router-link> -->
    </div>
</template>

<script>

    export default {  
        props:{
          product: Object,
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
