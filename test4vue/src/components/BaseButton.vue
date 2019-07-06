
<template>
  <b-button v-on="$listeners" v-bind="$attrs" class="button" :class="buttonClass">
    <slot />
  </b-button>
</template>


<script>
export default {
  inheritAttrs: false,

  props: {
    buttonClass: {
      type: String
    }
  }
};
</script>
