<template>
  <div v-if="vat != null">
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
      <h5 slot="header" class="mb-0">{{ vat.name }}</h5>
      <p>name: {{ vat.name }}</p>

      <p>Vat id: {{ vat.id }}</p>

      <h6 slot="footer" class="mb-0">
        <router-link :to="{ name: 'VatList' }">Back to vat List</router-link>
      </h6>
    </b-card>
  </div>
</template>

<script>
import ApiService from '@/services/ApiService.js';

export default {
  props: ['id'],
  data() {
    return {
      vat: Object
    };
  },
  created() {
    ApiService.getVatById(this.id)
      .then(response => {
        this.vat = response.data.result;
      })
      .catch(error => {
        console.log(error.message);
      });
  }
};
</script>

<style scoped></style>
