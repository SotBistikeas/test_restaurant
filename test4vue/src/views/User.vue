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
    >
      <span slot="header" class="mb-0" v-if="user.user.isActive = true">Active</span>

      <p>name: {{ user.user.name }}</p>
      <p>userName: {{ user.user.userName }}</p>
      <p>surname: {{ user.user.surname }}</p>
      <p>emailAddress: {{ user.user.emailAddress }}</p>
      <p>roleNames: {{ user.user.roleNames }}</p>
      
      <span slot="footer" class="mb-0">
        <p>password: {{ user.user.password }}</p>
      </span>
    </b-card>
  </div>
</template>

<script>
import { mapState } from 'vuex';

export default {
  computed: {
    ...mapState(['user'])
  }
};
</script>
