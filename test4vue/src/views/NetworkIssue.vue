<template>
  <div>
    <h1>Uh-Oh!!!</h1>
    <h3>It looks like you're experiencing network issues</h3>
    <h3>Please hit the back button and try again</h3>
    <router-link :to="{ name: 'Home' }">Or go back to home page</router-link>
  </div>
</template>
