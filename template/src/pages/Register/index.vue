<template>
  <div class="mt-4">
    <vs-card>
      <div class="vue-tempalte">
        <form>
          <h3 class="mb-4">Register</h3>

          <div class="form-group">
            <label>Name</label>
            <input
              type="text"
              v-model="$v.user.name.$model"
              class="form-control form-control-lg"
              :class="{ 'is-invalid': $v.user.name.$error }"
            />
          </div>
          <div class="form-group">
            <label>Surname</label>
            <input
              type="text"
              v-model="$v.user.surname.$model"
              :class="{ 'is-invalid': $v.user.surname.$error }"
              class="form-control form-control-lg"
            />
          </div>

          <div class="form-group">
            <label>Mobile Phone</label>
            <input
              type="text"
              v-model="$v.user.phone.$model"
              :class="{ 'is-invalid': $v.user.phone.$error }"
              class="form-control form-control-lg"
            />
          </div>

          <div class="form-group">
            <label>Email address</label>
            <input
              type="email"
              v-model="$v.user.email.$model"
              :class="{ 'is-invalid': $v.user.email.$error }"
              class="form-control form-control-lg"
            />
          </div>

          <div class="form-group">
            <label>Password</label>
            <input
              type="password"
              v-model="$v.user.password.$model"
              :class="{ 'is-invalid': $v.user.password.$error }"
              class="form-control form-control-lg"
            />
          </div>

          <div class="form-group">
            <label>Address</label>
            <textarea
              v-model="$v.user.address.$model"
              :class="{ 'is-invalid': $v.user.address.$error }"
              class="form-control form-control-lg"
            >
            </textarea>
          </div>

          <vs-button
            color="primary"
            class="btn-block"
            @click="register"
            type="filled"
            >Register</vs-button
          >

          <p class="forgot-password text-right">
            Already registered
            <router-link :to="{ name: 'login' }">Login</router-link>
          </p>
        </form>
      </div>
    </vs-card>
  </div>
</template>

<script>
import { required, numeric, email, minLength } from "vuelidate/lib/validators";
export default {
  data() {
    return {
      user: {
        name: "",
        surname: "",
        phone: "",
        address: "",
        email: "",
        password: "",
      },
      rePasswrod: "",
    };
  },
  methods: {
    async register() {
      if (this.$v.user.$invalid) {
        this.$v.$touch();
        this.$vs.notify({
          title: "Warning",
          text: "Please fill all the blanks",
          color: "warning",
        });
        return;
      }
      this.$vs.loading();

      try {
        const { data } = await this.$http.post("/api/auth/register", this.user);
        if (data.result) {
          this.$vs.notify({
            title: "Success",
            text: data.message,
            color: "success",
          });
          setTimeout(() => {
            this.$router.push("/login");
          }, 1500);
        }
      } catch (error) {
        if (error.response.data && !error.response.data.result) {
          this.$vs.notify({
            title: "Danger",
            text: error.response.data.message,
            color: "danger",
          });
        }
      }
      this.$vs.loading.close();
    },
  },
  validations: {
    user: {
      name: { required },
      surname: { required },
      phone: { required, numeric },
      address: { required },
      email: { required, email },
      password: { required, minLength: minLength(8) },
    },
  },
};
</script>

<style scoped>
* {
  box-sizing: border-box;
}

body {
  background: #2554ff !important;
  min-height: 100vh;
  display: flex;
  font-weight: 400;
}

body,
html,
.App,
.vue-tempalte,
.vertical-center {
  width: 100%;
  height: 100%;
}

.navbar-light {
  background-color: #ffffff;
  box-shadow: 0px 14px 80px rgba(34, 35, 58, 0.2);
}

.vertical-center {
  display: flex;
  text-align: left;
  justify-content: center;
  flex-direction: column;
}

.inner-block {
  width: 450px;
  margin: auto;
  background: #ffffff;
  box-shadow: 0px 14px 80px rgba(34, 35, 58, 0.2);
  padding: 40px 55px 45px 55px;
  border-radius: 15px;
  transition: all 0.3s;
}

.vertical-center .form-control:focus {
  border-color: #2554ff;
  box-shadow: none;
}

.vertical-center h3 {
  text-align: center;
  margin: 0;
  line-height: 1;
  padding-bottom: 20px;
}

label {
  font-weight: 500;
}

.forgot-password,
.forgot-password a {
  text-align: right;
  font-size: 13px;
  padding-top: 10px;
  color: #7a7a7a;
  margin: 0;
}

.forgot-password a {
  color: #2554ff;
}

.social-icons {
  text-align: center;
  font-family: "Open Sans";
  font-weight: 300;
  font-size: 1.5em;
  color: #222222;
}

.social-icons ul {
  list-style: none;
  margin: 0;
  padding: 0;
}
.social-icons ul li {
  display: inline-block;
  zoom: 1;
  width: 65px;
  vertical-align: middle;
  border: 1px solid #e3e8f9;
  font-size: 15px;
  height: 40px;
  line-height: 40px;
  margin-right: 5px;
  background: #f4f6ff;
}

.social-icons ul li a {
  display: block;
  font-size: 1.4em;
  margin: 0 5px;
  text-decoration: none;
}
.social-icons ul li a i {
  -webkit-transition: all 0.2s ease-in;
  -moz-transition: all 0.2s ease-in;
  -o-transition: all 0.2s ease-in;
  -ms-transition: all 0.2s ease-in;
  transition: all 0.2s ease-in;
}

.social-icons ul li a:focus i,
.social-icons ul li a:active i {
  transition: none;
  color: #222222;
}
</style>
