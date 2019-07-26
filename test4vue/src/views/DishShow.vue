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
      footer-bg-variant="warning"
    >
      <h5 slot="header" class="mb-0">{{ dish.name }}</h5>
      <div v-for="ingredient in ingredients" :key="ingredient.id">
        Id: {{ ingredient.id }} <br />
        Food-Id: {{ ingredient.foodIngredientId }} <br />
        Quantity: {{ ingredient.quantity }} <br />
        Unit-Id: {{ ingredient.unitOfMeasureId }}
      </div>
      <br />
      <h5 slot="footer" class="mb-0" v-if="edit == true">
        <BaseInput v-model="editedname" label="Enter a new name" />
        <BaseButton pill size="sm" @click="updateIt()" variant="dark">Update</BaseButton>
      </h5>
      <b-form-checkbox v-model="del" switch>Delete</b-form-checkbox>
      <b-form-checkbox v-model="edit" switch>Edit name</b-form-checkbox>
      <b-form-checkbox v-model="add" switch>Add product</b-form-checkbox>

      <div v-if="del == true">
        <BaseButton @click="deleteIt()" variant="danger">Delete</BaseButton>
      </div>

      <span slot="footer" class="mb-0">
        <router-link :to="{ name: 'DishList' }">Back to Dish List</router-link>
      </span>
    </b-card>

    <b-card
      title="Add Food igredient"
      header-tag="header"
      footer-tag="footer"
      style="max-width: 20rem;"
      class="mb-2"
      body-bg-variant="info"
      id="productCard"
      v-if="add == true"
    >
      <b-form-group label="Food Ingredients">
        <b-form-select v-model="FoodIngredient">
          <option disabled hidden :value="null">Please select one</option>
          <option v-for="option in FoodIngredientOptions" v-bind:value="option" v-bind:key="option.id">{{ option.name }}</option>
        </b-form-select>
        <baseInput label="Amount" v-model="amount" class="field" />
        <span v-if="amount != null">Cost per dish: {{ amount * FoodIngredient.cost }}</span>
      </b-form-group>
      <form v-on:submit.prevent="addFoodToList()">
        <BaseButton type="submit" variant="success">Add to Dish</BaseButton>
      </form>
    </b-card>
    <b-card
      title="Add Product"
      header-tag="header"
      footer-tag="footer"
      style="max-width: 20rem;"
      class="mb-2"
      body-bg-variant="info"
      id="productCard"
      v-if="add == true"
    >
      <b-form-group label="Products">
        <b-form-select v-model="product">
          <option disabled hidden :value="null">Please select one</option>
          <option v-for="option in ProductOptions" v-bind:value="option" v-bind:key="option.id">{{ option.name }}</option>
        </b-form-select>
      </b-form-group>
      <form v-on:submit.prevent="addProdToList()">
        <baseInput label="Amount" v-model="amount" class="field" />
        <span v-if="amount != null">Cost per dish: {{ amount * product.price }}</span>
        <BaseButton type="submit" variant="success">Add to Dish</BaseButton>
      </form>
    </b-card>
  </div>
</template>
<script>
import ApiService from '@/services/ApiService';

export default {
  props: ['id'],

  data() {
    return {
      dish: {
        result: {}
      },
      ProductOptions: [],
      FoodIngredientOptions: [],
      FoodIngredient: {},
      product: {},
      edit: false,
      editedname: '',
      ingredients: [],
      ingredient: {
        id: '',
        foodIngredientid: '',
        quantity: '',
        unitOfMeasureId: ''
      },
      del: false,
      amount: null,
      add: false
    };
  },

  beforeCreate() {
    ApiService.getAllProducts().then(response => {
      this.ProductOptions = response.data.result.items;
    });
    ApiService.getAllFoodIngredients().then(response => {
      this.FoodIngredientOptions = response.data.result.items;
    });
  },
  created() {
    this.getIt();
    this.loadFoodIngredient();
    this.loadProducts();
  },
  methods: {
    getIt() {
      ApiService.getDishById(this.id)
        .then(response => {
          this.dish = response.data.result;
        })
        .catch(error => {
          console.log(error.response);
        });
      ApiService.getFoodIngredientsByDish(this.id)
        .then(response => {
          this.ingredients = response.data.result;
        })
        .catch(error => {
          console.log(error.message);
        });
    },
    deleteIt() {
      ApiService.deleteDishById(this.id)
        .then(() => {
          this.$router.push({
            name: 'DishList'
          });
        })
        .catch(error => {
          console.log(error.message);
        });
    },
    updateIt() {
      ApiService.editDish({
        name: this.editedname,
        id: this.id
      })
        .then(response => {
          if (response.data.error == null) {
            this.getIt();
            this.edit = false;
            this.editedname = '';
          }
        })
        .catch(error => {
          console.log(error.message);
        });
    },
    addFoodToList() {
      ApiService.postFoodInDish(this.id, {
        foodIngredientId: this.FoodIngredient.id,
        quantity: this.amount,
        unitOfMeasureId: this.FoodIngredient.unitOfMeasureId,
        id: 0
      })
        .then(() => {
          this.getIt();
          this.loadFoodIngredient();
          this.amount = '';
          this.add = false;
        })
        .catch(error => {
          console.log(error.response);
        });
    },
    addProdToList() {
      alert('No Back End Yet!!!!');
      // ApiService.postFoodInDish(this.id, {
      //   foodIngredientId: this.FoodIngredient.id,
      //      quantity: this.amount,
      //      unitOfMeasureId: this.FoodIngredient.unitOfMeasureId,
      //      id: 0
      // })
      //   .then(() => {
      //     this.getIt();
      //     this.loadFoodIngredient();
      //     this.amount = '';
      //     this.add = false;
      //   })
      //   .catch(error => {
      //     console.log(error.response);
      //   });
    },
    loadFoodIngredient() {
      ApiService.getAllFoodIngredients()
        .then(response => {
          this.FoodIngredient = response.data.result;
        })
        .catch(error => {
          console.log(error.response);
        });
    },
    loadProducts() {
      ApiService.getAllProducts().then(response => {
        this.products = response.data.result;
      });
    }
  }
};
</script>

<style scoped></style>
