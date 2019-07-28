<template>
  <div v-if="foodIngredient != null" id="test">
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
        <h5 slot="header" class="mb-0">{{ foodIngredient.name }}</h5>
        <p>
          unit id:
          <UnitName :unitId="foodIngredient.unitOfMeasureId" />
        </p>
        <p>Quantity: {{ foodIngredient.quantity }}</p>
        <p>
          Cost:
          <Currency :value="foodIngredient.cost" />
        </p>
        <h5 slot="footer" class="mb-0">
          <router-link :to="{ name: 'FoodIngredientList' }">Back to foodIngredient List</router-link>
        </h5>
        <h5 slot="footer" class="mb-0" v-if="edit == true">
          <BaseInput v-model="editedname" label="update name" />
          <BaseButton pill size="sm" @click="updateIt()" variant="dark">Update</BaseButton>
        </h5>
        <b-form-checkbox v-model="del" switch>Delete</b-form-checkbox>
        <b-form-checkbox v-model="edit" switch>Edit name</b-form-checkbox>

        <div v-if="del == true">
          <BaseButton @click="deleteIt()" variant="danger">Delete</BaseButton>
        </div>
      </b-card>
    </div>
    <div>
      <b-table striped hover :items="products" :fields="Fields" id="table" bordered responsive>
        <!-- A custom formatted column -->
        <template slot="id" slot-scope="data">
          <b-button variant="outline-danger" size="sm" @click="deleteThis(id, data.value)">Remove</b-button>
        </template>
        <template slot="productId" slot-scope="data">{{ getProductName(data.value) }}</template>
        <template slot="unitOfMeasureId" slot-scope="data">
          <UnitName :unitId="data.value" />
        </template>
        <template slot="cost" slot-scope="data">
          <Currency :value="data.value" />
        </template>
      </b-table>
    </div>
    <div>
      <ul>
        <li v-for="item in selected" :key="item">{{ item }}</li>
      </ul>
    </div>
    <b-card
      title="Add Product"
      header-tag="header"
      footer-tag="footer"
      style="max-width: 20rem;"
      class="mb-2"
      body-bg-variant="info"
      id="productCard"
    >
      <b-form v-on:submit.prevent="addToList()">
        <b-form-group label="Products">
          <b-form-select v-model="product">
            <option disabled hidden :value="null">Please select one</option>
            <option
              v-for="option in ProductOptions"
              v-bind:value="option"
              v-bind:key="option.id"
            >{{ option.name }}</option>
          </b-form-select>
        </b-form-group>
        <BaseInput label="quantity:" v-model="quantity" class="field" type="number" step="0.01" />
        <h6 v-if="!isNaN(product.price * quantity)">
          price per foodIngredient:
          <Currency :value="product.price * quantity" />
        </h6>
        <b-form-group label="Unit">
          <b-form-select v-model="unitOfMeasureId">
            <option disabled hidden :value="null">Please select one</option>
            <option
              v-for="option in unitOfMeasureOptions"
              v-bind:value="option.id"
              v-bind:key="option.id"
            >{{ option.name }}</option>
          </b-form-select>
        </b-form-group>
        <BaseButton type="submit" variant="success">Add to list</BaseButton>
      </b-form>
    </b-card>
    <br />
    <div>
      <b-form-group label="Using options array:">
        <b-form-checkbox-group
          id="checkbox-group-1"
          v-model="selected"
          :options="baharika.baharika"
        ></b-form-checkbox-group>
      </b-form-group>
    </div>
  </div>
</template>

<script>
import FoodIngredientService from "@/services/FoodIngredientService.js";
// import ProductService from '@/services/ProductService.js';
import axios from "axios";
import { mapState } from "vuex";
import ApiService from "@/services/ApiService.js";
import UnitName from "@/components/UnitName.vue";
import Currency from "@/components/Currency.vue";

export default {
  props: ["id"],
  components: {
    UnitName,
    Currency
  },
  data() {
    return {
      del: false,
      edit: false,
      selected: [],
      ProductOptions: [],
      //unitOfMeasureOptions: [],
      foodIngredient: {},
      product: {
        productId: "",
        quantity: "",
        unitOfMeasureId: "",
        id: ""
      },
      quantity: "",
      unitOfMeasureId: "",
      products: [],
      productFields: {
        productId: {},
        quantity: {},
        unitOfMeasureId: {},
        id: {
          label: ""
        }
      },
      item: "",
      idToRemove: "",
      editedname: "",
      temp: "",
      Fields: {
        productId: {
          label: "Product name",
          sortable: true
        },
        quantity: {
          label: "Quantity per ingredient",
          sortable: true
        },
        unitOfMeasureId: {
          label: "Unit of measure",
          sortable: false
        },
        cost: {
          label: "Cost",
          sortable: true
        },
        id: {
          label: "Delete",
          sortable: false
        }
      }
    };
  },
  computed: {
    ...mapState({
      baharika: "baharika",
      unitOfMeasures: "units"
    }),
    unitOfMeasureOptions: function() {
      if (this.product.id) {
        //find the unit of measure of this product
        var unit = this.unitOfMeasures.find(
          o => o.id == this.product.unitOfMeasureId
        );
        //find all unit of measures of the same type
        return this.unitOfMeasures.filter(
          o => o.unitOfMeasureType == unit.unitOfMeasureType
        );
      }
      return this.unitOfMeasures;
    }
  },
  beforeCreate() {
    axios
      .get(
        "http://localhost:21021/api/services/app/Product/GetAll?MaxResultCount=10000",
        { headers: { Accept: "application/json" } }
      )
      .then(response => {
        this.ProductOptions = response.data.result.items;
      });

    this.$store.dispatch("fetchUnits");
  },
  created() {
    this.loadFoodIngredient();
    this.loadProducts();
  },
  methods: {
    loadFoodIngredient() {
      FoodIngredientService.getFoodIngredientById(this.id)
        .then(response => {
          this.foodIngredient = response.data.result;
        })
        .catch(error => {
          console.log(error.response);
        });
    },
    loadProducts() {
      axios
        .get(
          "http://localhost:21021/api/services/app/FoodIngredient/GetProducts?foodIngredientId=" +
            this.id
        )
        .then(response => {
          this.products = response.data.result;
        });
    },
    deleteIt() {
      axios
        .delete(
          "http://localhost:21021/api/services/app/FoodIngredient/Delete?Id=" +
            this.id
        )
        .then(response => {
          this.temp = response.data;
          this.$router.push({
            name: "FoodIngredientList"
          });
          console.log("record with id:" + this.id + " has been deleted");
        })
        .catch(error => {
          console.log(error.message);
        });
    },
    updateIt() {
      axios
        .put("http://localhost:21021/api/services/app/FoodIngredient/Update", {
          name: this.editedname,
          cost: this.foodIngredient.cost,
          unitOfMeasureId: this.foodIngredient.unitOfMeasureId,
          quantity: this.foodIngredient.quantity,
          id: this.id
        })
        .then(response => {
          this.loadFoodIngredient();
          this.temp = response.data;
          this.editedname = "";
          this.edit = false;
        })
        .catch(error => {
          console.log(error.message);
        });
    },
    deleteThis(foodIngredientId, itemId) {
      axios
        .delete(
          "http://localhost:21021/api/services/app/FoodIngredient/RemoveProduct?foodIngredientId=" +
            foodIngredientId +
            "&id=" +
            itemId
        )
        .then(response => {
          this.temp = response.data;
          this.loadProducts();
        })
        .catch(error => {
          console.log(error.message);
        });
    },
    addToList() {
      axios
        .post(
          "http://localhost:21021/api/services/app/FoodIngredient/AddProduct?foodIngredientId=" +
            this.id,
          {
            productId: this.product.id,
            quantity: this.quantity,
            unitOfMeasureId: this.unitOfMeasureId,
            id: 0
          }
        )
        .then(() => {
          //clear old data
          this.product = {
            productId: "",
            quantity: "",
            unitOfMeasureId: "",
            id: ""
          };
          this.quantity = null;
          this.unitOfMeasureId = null;
          //reload table
          this.loadProducts();
        });
    },
    getProductName(productId) {
      var p = this.ProductOptions.find(o => o.id == productId);
      if (p != null) return p.name;
      else return null;
    }
  }
};
</script>

<style scoped>
#table {
  width: 50%;
}
#test {
  /* display: flex; */
  align-items: center;
  justify-content: space-between;
  align-content: flex-start;
}
#productCard {
  width: 30%;
}
</style>
