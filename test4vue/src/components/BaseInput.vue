
<template>
  <b-form-group :label="label">
    <b-form-input :value="value" @input="updateValue" v-bind="$attrs" v-on="listeners" class></b-form-input>
  </b-form-group>
</template>


<script>
export default {
  inheritAttrs: false,
  props: {
    label: {
      type: String,
      default: ""
    },
    value: [String, Number]
  },
  computed: {
    listeners() {
      return {
        ...this.$listeners,
        input: this.updateValue
      };
    }
  },
  methods: {
    updateValue(event) {
      this.$emit("input", event.target.value);
    }
  }
};
</script>