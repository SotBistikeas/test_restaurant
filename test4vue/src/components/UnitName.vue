<template>
  <span>{{ getUnitName(unitId) }}</span>
</template>

<script>
import { mapState } from 'vuex';
export default {
  props: ['unitId', 'withLink'],
  computed: {
    ...mapState({ units: 'units' })
  },
  beforeCreate() {
    //this.$store.dispatch("fetchUnits");
  },
  methods: {
    getUnitName(id) {
      var u = this.units.find(o => o.id == id);
      if (u != null) {
        return `${u.name} (${u.symbol})`;
      } else {
        return null;
      }
    }
  }
};
</script>
