<template>
  <div>
    <UnitCard class="gap" v-for="unit in units" :key="unit.id" :unit="unit" />
  </div>
</template>

<script>
import UnitCard from '@/components/UnitCard.vue';
import { mapState } from 'vuex';
export default {
  components: {
    UnitCard
  },
  beforeCreate() {
    this.$store.dispatch('fetchUnits');
  },
  computed: mapState(['units'])
};
</script>

<style scoped>
.gap {
  margin: 5px;
}
</style>
