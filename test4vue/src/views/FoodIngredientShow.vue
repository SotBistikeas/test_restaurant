<template>
  <div v-if="FoodIngredient != null" id="test">
    <!-- <h2>Showing FoodIngredient #: {{FoodIngredient.id}}</h2>
    <div>
    <h3>name: {{ FoodIngredient.name }}</h3>
    <input type="text" v-model="editedname" label="update name"/>  
    <BaseButton pill size="sm" @click="updateIt()" variant="warning">Update name</BaseButton> 
    </div>
    <p>cost: {{ FoodIngredient.cost }}</p>
    <p>unit id: {{ FoodIngredient.unitOfMeasureId }}</p>
    <p>Quantity: {{ FoodIngredient.quantity }}</p>
    <BaseButton @click="deleteIt()" variant="danger">Delete</BaseButton>
     -->
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
        <h5 slot="header" class="mb-0">{{ FoodIngredient.name }}</h5>
        <p>cost: {{ FoodIngredient.cost }}</p>
        <p>unit id: {{ FoodIngredient.unitOfMeasureId }}</p>
        <p>Quantity: {{ FoodIngredient.quantity }}</p>
        <h5 slot="footer" class="mb-0" v-if="edit == true">
          <BaseInput v-model="editedname" label="update name" />
          <BaseButton pill size="sm" @click="updateIt()" variant="dark">Update</BaseButton>
        </h5>

        <BaseButton @click="deleteIt()" variant="danger">Delete</BaseButton>

        <b-form-checkbox v-model="edit" switch>Edit name</b-form-checkbox>
      </b-card>
    </div>
    <div>
      <b-table striped hover :items="products" :fields="Fields" id="table" bordered responsive>
        <!-- A custom formatted column -->
        <template slot="id" slot-scope="data">
          <b-button variant="outline-danger" size="sm" @click="deleteThis(id, data.value)">Remove</b-button>
        </template>
        <template slot="productId" slot-scope="data">{{ getProductName(data.value) }}</template>
      </b-table>
    </div>
    <div>
      <ul>
        <li v-for="item in selected" :key="item">
          {{ item }}
        </li>
      </ul>
    </div>
    <!-- <b-form-group label="Product id to remove from food ingredient">
      <b-form-select v-model="idToRemove">
        <option
          v-for="option in products"
          v-bind:value="option.id"
          v-bind:key="option.id"
        >{{option.productId}}</option>
      </b-form-select>
      <BaseButton @click="deleteThis(id, idToRemove)" variant="warning">Remove product from list</BaseButton>
    </b-form-group> -->
    <b-card
      title="Add Product"
      header-tag="header"
      footer-tag="footer"
      style="max-width: 20rem;"
      class="mb-2"
      body-bg-variant="info"
      id="productCard"
    >
      <b-form-group label="Products">
        <b-form-select v-model="product">
          <option disabled hidden :value="null">Please select one</option>
          <option v-for="option in ProductOptions" v-bind:value="option" v-bind:key="option.id">{{ option.name }}</option>
        </b-form-select>
      </b-form-group>
      <form v-on:submit.prevent="addToList()">
        <BaseInput label="quantity:" v-model="quantity" class="field" />
        <h6 v-if="!isNaN(product.price * quantity)">price per FoodIngredient: {{ product.price * quantity }}</h6>
        <BaseButton type="submit" variant="success">Add to list</BaseButton>
      </form>
    </b-card>
    <br />
    <div>
      <b-form-group label="Using options array:">
        <b-form-checkbox-group id="checkbox-group-1" v-model="selected" :options="baharika.baharika"></b-form-checkbox-group>
      </b-form-group>
    </div>
  </div>
</template>

<script>
import FoodIngredientService from '@/services/FoodIngredientService.js';
// import ProductService from '@/services/ProductService.js';
import axios from 'axios';
import { mapState } from 'vuex';

export default {
  props: ['id'],
  computed: {
    ...mapState({
      baharika: 'baharika'
    })
  },
  data() {
    return {
      edit: false,
      selected: [],
      ProductOptions: [],
      FoodIngredient: {
        result: {}
      },
      product: {
        productId: '',
        quantity: '',
        unitOfMeasureId: '',
        id: ''
      },
      quantity: '',
      products: [],
      productFields: {
        productId: {},
        quantity: {},
        unitOfMeasureId: {},
        id: {
          label: ''
        }
      },
      item: '',
      idToRemove: '',
      editedname: '',
      temp: '',
      Fields: {
        productId: {
          label: 'Product name',
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
        id: {
          label: 'Delete',
          sortable: false
        }
      }
    };
  },
  beforeCreate() {
    axios
      .get('http://localhost:21021/api/services/app/Product/GetAll?MaxResultCount=10000', { headers: { Accept: 'application/json' } })
      .then(response => {
        this.ProductOptions = response.data.result.items;
      });
  },
  created() {
    this.loadFoodIngredient();
    this.loadProducts();
  },
  methods: {
    loadFoodIngredient() {
      FoodIngredientService.getFoodIngredientById(this.id)
        .then(response => {
          this.FoodIngredient = response.data.result;
        })
        .catch(error => {
          console.log(error.response);
        });
    },
    loadProducts() {
      axios.get('http://localhost:21021/api/services/app/FoodIngredient/GetProducts?foodIngredientId=' + this.id).then(response => {
        this.products = response.data.result;
      });
    },
    deleteIt() {
      axios
        .delete('http://localhost:21021/api/services/app/FoodIngredient/Delete?Id=' + this.id)
        .then(response => {
          this.temp = response.data;
          this.$router.push({
            name: 'FoodIngredientList'
          });
          console.log('record with id:' + this.id + ' has been deleted');
        })
        .catch(error => {
          console.log(error.message);
        });
    },
    updateIt() {
      axios
        .put('http://localhost:21021/api/services/app/FoodIngredient/Update', {
          name: this.editedname,
          cost: this.FoodIngredient.cost,
          unitOfMeasureId: this.FoodIngredient.unitOfMeasureId,
          quantity: this.FoodIngredient.quantity,
          id: this.id
        })
        .then(response => {
          this.loadFoodIngredient();
          this.temp = response.data;
          this.editedname = '';
        })
        .catch(error => {
          console.log(error.message);
        });
    },
    deleteThis(foodIngredientId, itemId) {
      axios
        //.delete('http://localhost:21021/api/services/app/FoodIngredient/RemoveProductByProductId?foodIngredientId=' + foodIngredientId + '&productId=' + itemId)
        .delete(
          'http://localhost:21021/api/services/app/FoodIngredient/RemoveProduct?foodIngredientId=' + foodIngredientId + '&id=' + itemId
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
        .post('http://localhost:21021/api/services/app/FoodIngredient/AddProduct?foodIngredientId=' + this.id, {
          productId: this.product.id,
          quantity: this.quantity,
          unitOfMeasureId: this.product.unitOfMeasureId,
          id: 0
        })
        .then(() => {
          //clear old data
          this.product = {
            productId: '',
            quantity: '',
            unitOfMeasureId: '',
            id: ''
          };
          this.quantity = null;
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
