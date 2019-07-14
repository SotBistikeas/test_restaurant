<template>
  <div id="app">
    <NavBar />
    <NotificationContainer />
    <b-container fluid>
      <router-view :key="$route.fullPath" />
    </b-container>
  </div>
</template>

<script>
import NavBar from "@/components/NavBar.vue";
import NotificationContainer from "@/components/NotificationContainer.vue";

export default {
  components: {
    NavBar,
    NotificationContainer
  }
};
</script>
