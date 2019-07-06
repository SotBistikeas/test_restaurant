<!--
<BaseButton type="submit" or @click="KANE_KATI" buttonClass="CLASS_FOR_THIS_PAGE">
"OTI_THELOUME"
</BaseButton>


-->
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
