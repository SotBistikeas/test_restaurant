<template>
  <div>
    <span>Name: {{ unit.name }}</span> <br />
    <span>Id: {{ unit.id }}</span>
    <br /><br /><br />
    <router-link :to="{ name: 'UnitList' }">Back to Units List</router-link>
  </div>
</template>

<script>
import UnitOfMeasureService from '@/services/UnitOfMeasureService.js';

export default {
  props: ['id'],
  data() {
    return {
      unit: {
        result: {}
      }
    };
  },
  beforecreate() {
    UnitOfMeasureService.getUnitById(this.id)
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
