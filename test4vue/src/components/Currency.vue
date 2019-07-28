<template>
  <span>{{formatPrice(value)}}</span>
</template>

<script>
export default {
  props: ["value"],
  methods: {
    formatPrice(value) {
      var formatter = new Intl.NumberFormat("el-GR", {
        style: "currency",
        currency: "EUR",
        minimumFractionDigits: value < 0.05 ? 3 : 2
      });
      return formatter.format(value);
    }
  }
};
</script>
