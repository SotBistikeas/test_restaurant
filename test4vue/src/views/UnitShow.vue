<template>
  <b-card
    title="Details"
    header-tag="header"
    footer-tag="footer"
    style="max-width: 20rem;"
    class="mb-2"
    header-bg-variant="success"
    body-bg-variant="info"
    footer-bg-variant="warning"
  >
    <h5 slot="header" class="mb-0">{{ unit.name }}</h5>
    <h5>Name: {{ unit.name }}</h5>
    <br />
    <h5>Id: {{ unit.id }}</h5>
    <h6 slot="footer" class="mb-0">
      <router-link :to="{ name: 'UnitList' }">Back to Unit List</router-link>
    </h6>
  </b-card>
</template>

<script>
import ApiService from '@/services/ApiService.js';

export default {
  props: ['id'],
  data() {
    return {
      unit: {}
    };
  },
  beforeMount() {
     ApiService.getUnitById(this.id)
      .then(response => {
        this.unit = response.data.result;
      })
      .catch(error => {
        console.log(error.message);
      });
  }
};
</script>

<style scoped></style>
