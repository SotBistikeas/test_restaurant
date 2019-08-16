<template>
  <b-card title="Login" sub-title style="max-width: 20rem;">
    <form @submit.prevent="loginUser">
      <BaseInput
        label="Name"
        v-model="name"
        :state="!$v.name.$error"
        errorMessage="Name is required"
      />

      <BaseInput
        label="Password"
        v-model="password"
        class="field"
        type="password"
        :class="{ error: $v.password.$error }"
        :state="!$v.password.$error"
        errorMessage="password is required"
      />

      <BaseButton type="submit" :disabled="$v.$anyError" variant="primary">Login</BaseButton>

      <p v-if="$v.$anyError">Please fill out all the fields</p>
    </form>
  </b-card>
</template>

<script>
import NProgress from "nprogress";
import { required } from "vuelidate/lib/validators";
import AuthService from "@/services/AuthService";

export default {
  beforeCreate: function() {},
  data() {
    return {
      name: "",
      password: ""
    };
  },

  validations: {
    name: { required },
    password: { required }
  },

  methods: {
    loginUser() {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        NProgress.start();
        this.$store
          .dispatch("login", {
            name: this.name,
            password: this.password
          })
          .then(() => this.$router.push("/"))
          .catch(err => console.log(err))
          .finally(() => NProgress.done());
        //AuthService.login(this.name, this.password);

        // this.$store
        //   .dispatch("product/createProduct", this.product)
        //   .then(() => {
        //     this.product = this.createFreshProductObject();
        //   })
        //   .catch(() => {
        //     NProgress.done();
        //   });
      }
    }
  }
};
</script>

<style scoped></style>
