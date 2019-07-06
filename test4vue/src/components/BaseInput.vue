
<template>
  <b-form-group :label="label">
    <b-form-input :value="value" @input="updateValue" v-bind="$attrs" v-on="listeners" class></b-form-input>
     <b-form-invalid-feedback id="input-live-feedback">
      {{errorMessage}}
    </b-form-invalid-feedback>
  </b-form-group>
</template>


<script>
export default {
  inheritAttrs: true,
  props: {
    label: {
      type: String,
      default: ""
    },
    value: [String, Number],
    errorMessage: String
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
      this.$emit("input", event);
    }
  }
};
</script>