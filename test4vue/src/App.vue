<template>
  <div id="app">
    <mdb-container>
      <NavBar />
      <NotificationContainer />
      <router-view :key="$route.fullPath" />
    </mdb-container>
  </div>
</template>

<script>
import NavBar from "@/components/NavBar.vue";
import NotificationContainer from "@/components/NotificationContainer.vue";
import { mdbContainer } from "mdbvue";

export default {
  components: {
    NavBar,
    NotificationContainer,
    mdbContainer
  }
};
</script>


