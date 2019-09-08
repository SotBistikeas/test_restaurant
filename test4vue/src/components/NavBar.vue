<template>
  <b-navbar toggleable="lg" type="dark" variant="primary">
    <b-navbar-brand>
      <router-link to="/">Home</router-link>
    </b-navbar-brand>
    <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
    <b-collapse id="nav-collapse" is-nav>
      <b-navbar-nav>
        <b-nav-item :to="{ name: 'ProductCreate' }">New Product</b-nav-item>
        <b-nav-item :to="{ name: 'ProductList' }">Product List</b-nav-item>

        <b-nav-item :to="{ name: 'FoodIngredient' }">New FoodIngredient</b-nav-item>
        <b-nav-item :to="{ name: 'FoodIngredientList' }">FoodIngredient List</b-nav-item>

        <b-nav-item :to="{ name: 'DishCreate' }">New Dish</b-nav-item>
        <b-nav-item :to="{ name: 'DishList' }">Dish List</b-nav-item>

        <b-nav-item :to="{ name: 'UnitList' }">Units List</b-nav-item>
        <b-nav-item :to="{ name: 'VatList' }">Vat List</b-nav-item>
        <b-nav-item :to="{ name: 'MonthlyExp' }">Monthly expenses</b-nav-item>
      </b-navbar-nav>
      <b-navbar-nav class="ml-auto" v-if="isLoggedIn">
        <b-nav-item-dropdown right>
          <!-- Using 'button-content' slot -->
          <template slot="button-content">
            <em>{{getFullName}}</em>
          </template>
          <b-dropdown-item :to="{ name: 'Home' }">Profile</b-dropdown-item>
          <b-dropdown-item @click="logout">Sign Out</b-dropdown-item>
        </b-nav-item-dropdown>
      </b-navbar-nav>
      <b-navbar-nav class="ml-auto" v-else>
        <b-nav-item right :to="{ name: 'Login' }">Login</b-nav-item>
        <b-nav-item right :to="{ name: 'Register' }">Sign up</b-nav-item>
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
</template>

<script>
export default {
  computed: {
    isLoggedIn: function() {
      return this.$store.getters.isLoggedIn;
    },
    getFullName: function() {
      return this.$store.state.userFullName;
    }
  },
  methods: {
    logout: function() {
      this.$store.dispatch("logout").then(() => {
        this.$router.push("/login");
      });
    }
  }
};
</script>
