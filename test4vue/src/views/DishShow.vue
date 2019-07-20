<template>
  <div>
    <template v-if="!edit">
      <h1>Dish Show</h1>
      <!--<h3>id: {{dish.id}}</h3>-->
      <h3>name: {{ dish.name }}</h3>
      <h3>price: {{ dish.price }}</h3>
      <span class="list-group"
        >proionta piatou:

        <table>
          <tr>
            <td class="bake">Onoma</td>
            <td class="bake">posotita</td>
            <td class="bake">kostos ana piato</td>
          </tr>

          <tr v-for="p in dish.pds" :key="p.Id">
            <td class="bake">{{ p.name }}</td>
            <td class="bake">{{ p.Quan }}</td>
            <td class="bake">{{ p.Pri }}</td>
          </tr>
        </table>
      </span>
      <BaseButton v-on:click="deleteDish(dish.id)">Delet dish</BaseButton>
      <BaseButton v-on:click="edit = true" v-if="!edit">Edit</BaseButton>
    </template>

    <template v-if="edit">
      <p>time to edit</p>
      <BaseInput label="Onoma piatou:" v-model="dish.name" />
      <BaseButton v-on:click="edit = false">Cancel</BaseButton>
    </template>
  </div>
</template>
<script>
import NProgress from 'nprogress';
import axios from 'axios';
export default {
  data() {
    return {
      edit: false
    };
  },
  props: {
    dish: Object,
    required: true
  },
  methods: {
    deleteDish(id) {
      NProgress.start();
      axios
        .delete('http://localhost:3000/dishes/' + id)
        .then(() => {
          this.$router.push({
            name: 'DishList'
          });
        })
        .catch(() => {
          NProgress.done();
        });
    }
  }
};
</script>

<style scoped>
.location {
  margin-bottom: 0;
}
.location > .icon {
  margin-left: 10px;
}
.event-header > .title {
  margin: 0;
}
.list-group {
  margin: 0;
  padding: 0;
  list-style: none;
}
.list-group > .list-item {
  padding: 1em 0;
  border-bottom: solid 1px #e5e5e5;
}

.bake {
  border: 1px solid red;
  margin: 5px;
  width: 33%;
}
</style>
