<template>
  <b-card title="Register" sub-title style="max-width: 20rem;">
    <form @submit.prevent="register">
      <BaseInput
        label="Name"
        v-model="name"
        :state="!$v.name.$error"
        errorMessage="Name is required"
      />
      <BaseInput
        label="Surname"
        v-model="surname"
        :state="!$v.surname.$error"
        errorMessage="Surname is required"
      />

      <BaseInput
        label="E-Mail"
        v-model="email"
        :state="!$v.email.$error"
        errorMessage="E-Mail is required"
      />

      <BaseInput
        label="Password"
        v-model="password"
        type="password"
        class="field"
        :class="{ error: $v.password.$error }"
        :state="!$v.password.$error"
        errorMessage="password is required"
      />

      <BaseInput
        label="Confirm Password"
        v-model="password_confirmation"
        type="password"
        class="field"
        :class="{ error: $v.password_confirmation.$error }"
        :state="!$v.password_confirmation.$error"
        errorMessage="Passwords must be the same"
      />

      <BaseButton type="submit" :disabled="$v.$anyError" variant="primary">Register</BaseButton>

      <p v-if="$v.$anyError">Please fill out all the fields</p>
    </form>
  </b-card>
</template>
<script>
import { required, sameAs, minLength, email } from "vuelidate/lib/validators";

export default {
  data() {
    return {
      name: "",
      surname: "",
      email: "",
      password: "",
      password_confirmation: ""
    };
  },
  validations: {
    name: { required, minLength: minLength(3) },
    surname: { required, minLength: minLength(3) },
    email: { required, email },
    password: { required, minLength: minLength(6) },
    password_confirmation: {
      sameAsPassword: sameAs("password")
    }
  },

  methods: {
    register: function() {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        let data = {
          name: this.name,
          surname: this.surname,
          userName: this.email,
          emailAddress: this.email,
          password: this.password
        };
        this.$store
          .dispatch("register", data)
          .then(() => this.$router.push("/login"))
          .catch(err => console.log(err));
      }
    }
  }
};
</script>