<template>
  <div>
    <b-card
      title="Details"
      header-tag="header"
      footer-tag="footer"
      style="max-width: 20rem;"
      class="mb-2"
      header-bg-variant="success"
      body-bg-variant="info"
      footer-bg-variant="danger"
      v-if="user.user"
    >
      <p>name: {{ user.user.name }}</p>
      <p>surname: {{ user.user.surname }}</p>
      <p>userName: {{ user.user.userName }}</p>
      <p>emailAddress: {{ user.user.emailAddress }}</p>
    </b-card>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";

export default {
  computed: {
    ...mapState(["user"])
  },
  beforeMount() {
    this.$store.dispatch("user/updateCurrentUser");
  }
};
</script>
