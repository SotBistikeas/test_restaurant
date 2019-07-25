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

        <b-nav-item :to="{ name: 'DishCreate' }">New Dish</b-nav-item>
        <b-nav-item :to="{ name: 'DishList' }">Dish List</b-nav-item>
        
        <b-nav-item :to="{ name: 'FoodIngredient' }">New FoodIngredient</b-nav-item>
        <b-nav-item :to="{ name: 'FoodIngredientList' }">FoodIngredient List</b-nav-item>
        
        <b-nav-item :to="{ name: 'UnitList' }">Units List</b-nav-item>
        <b-nav-item :to="{ name: 'VatList' }">Vat List</b-nav-item>

      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
</template>

<script>
export default {};
</script>
