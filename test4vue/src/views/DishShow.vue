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
      <div>
        <table>
          <tbody>
            <tr>
              <td>Name</td>
              <td>{{ dish.name }}</td>
            </tr>
            <tr>
              <td>BaseCost</td>
              <td>{{ dish.baseCost }}</td>
            </tr>
            <tr>
              <td>RealCost</td>
              <td>{{ dish.realCost }}</td>
            </tr>
            <tr>
              <td>SalePriceExclTax</td>
              <td>{{ dish.salePriceExclTax }}</td>
            </tr>
            <tr>
              <td>salePriceInclTax</td>
              <td>{{ dish.salePriceInclTax }}</td>
            </tr>
            <tr>
              <td>BaseProfit</td>
              <td>{{ dish.baseProfit }}</td>
            </tr>
            <tr>
              <td>BaseProfitPerc</td>
              <td>{{ dish.baseProfitPerc }}</td>
            </tr>
            <tr>
              <td>RealProfit</td>
              <td>{{ dish.realProfit }}</td>
            </tr>
            <tr>
              <td>RealProfitPerc</td>
              <td>{{ dish.realProfitPerc }}</td>
            </tr>
            <tr>
              <td>FoodCostPerc</td>
              <td>{{ dish.foodCostPerc }}</td>
            </tr>
          </tbody>
        </table>
      </div>

      <br />
      <h5 slot="footer" class="mb-0" v-if="edit == true">
        <BaseInput v-model="editedname" label="Enter a new name" />
        <BaseButton pill size="sm" @click="updateIt()" variant="dark">Update</BaseButton>
      </h5>
      <b-form-checkbox v-model="del" switch>Delete</b-form-checkbox>
      <b-form-checkbox v-model="edit" switch>Edit name</b-form-checkbox>
      <b-form-checkbox v-model="add" switch>Add FoodIgredient</b-form-checkbox>
      <b-form-checkbox v-model="add2" switch>Add product</b-form-checkbox>

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
        <BaseInput label="Amount" v-model="amount" class="field" type="number" step="0.01" min="0.00" max="10000.00" />
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
      v-if="add2 == true"
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

    <div>
      <b-table striped hover :items="foodIngredients" :fields="foodIngredientFields" id="table" bordered responsive>
        <!-- A custom formatted column -->
        <template slot="id" slot-scope="data">
          <b-button variant="outline-danger" size="sm" @click="deleteThis(id, data.value)">Remove</b-button>
        </template>
        <template slot="foodIngredientId" slot-scope="data">{{ getFoodIngredientName(data.value) }}</template>
        <template slot="unitOfMeasureId" slot-scope="data">
          <UnitName :unitId="data.value" />
        </template>
        <template slot="cost" slot-scope="data">
          <Currency :value="data.value" />
        </template>
      </b-table>
    </div>
  </div>
</template>
<script>
import DishService from '@/services/DishService.js';
import ProductService from '@/services/ProductService.js';
import { faSort } from '@fortawesome/free-solid-svg-icons/faSort';
import axios from 'axios';
import UnitName from '@/components/UnitName.vue';
import Currency from '@/components/Currency.vue';
import FoodIngredientService from '@/services/FoodIngredientService.js';

export default {
  props: ['id'],
  components: {
    UnitName,
    Currency
  },
  data() {
    return {
      dish: {},
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
      add: false,
      add2: false,
      foodIngredients: [],
      foodIngredientFields: {
        foodIngredientId: {
          label: 'foodIngredientId',
          sortable: true
        },
        quantity: {
          label: 'Quantity per ingredient',
          sortable: true
        },
        unitOfMeasureId: {
          label: 'Unit of measure',
          sortable: false
        },
        cost: {
          label: 'Cost',
          sortable: true
        },
        id: {
          label: 'Actions',
          sortable: false
        }
      }
    };
  },

  beforeCreate() {
    ProductService.getAllProducts().then(response => {
      this.ProductOptions = response.data.result.items;
    });
    FoodIngredientService.getAllFoodIngredients().then(response => {
      this.FoodIngredientOptions = response.data.result.items;
    });
    this.$store.dispatch('fetchUnits');
  },
  created() {
    this.getIt();
    this.loadFoodIngredient();
    this.loadProducts();
    this.loadFoodIngredients();
  },
  methods: {
    getIt() {
      DishService.getDishById(this.id)
        .then(response => {
          this.dish = response.data.result;
        })
        .catch(error => {
          console.log(error.message);
        });
    },
    deleteIt() {
      DishService.deleteDishById(this.id)
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
      DishService.editDish({
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
      DishService.postFoodInDish(this.id, {
        foodIngredientId: this.FoodIngredient.id,
        quantity: this.amount,
        unitOfMeasureId: this.FoodIngredient.unitOfMeasureId,
        id: 0
      })
        .then(() => {
          this.getIt();
          this.loadFoodIngredients();
          this.amount = '';
          this.add = false;
        })
        .catch(error => {
          console.log(error.response);
        });
    },
    addProdToList() {
      alert('No Back End Yet!!!!');
    },
    deleteThis(id, data) {
      DishService.deleteFoodFromDish(id, data).then(() => this.loadFoodIngredients());
    },
    loadFoodIngredient() {
      FoodIngredientService.getAllFoodIngredients()
        .then(response => {
          this.FoodIngredientOptions = response.data.result.items;
        })
        .catch(error => {
          console.log(error.response);
        });
    },
    loadProducts() {
      ProductService.getAllProducts().then(response => {
        this.products = response.data.result;
      });
    },
    loadFoodIngredients() {
      DishService.getFoodIngredientsByDish(this.id)
        .then(response => {
          this.ingredients = response.data.result;
          this.foodIngredients = response.data.result;
        })
        .catch(error => {
          console.log(error.message);
        });
    },

    // MERIKES FORES DOULEBEI !!!!! OXI PANTA!!!!!!!
    getFoodIngredientName(foodIngredientId) {
      var p = this.FoodIngredientOptions.find(o => o.id == foodIngredientId);
      if (p != null) return p.name;
      else return null;
      // return this.FoodIngredientOptions.find(o => o.id == foodIngredientId)
      //   .name;
    }
  }
};
</script>

<style scoped></style>
