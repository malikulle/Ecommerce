import Vue from "vue";
import Vuesax from "vuesax";
import axios from "axios";
import vuelidate from "vuelidate";
import App from "./App.vue";

import "vuesax/dist/vuesax.css"; //Vuesax styles
import "material-icons/iconfont/material-icons.css";
// Vuex Store
import store from "./store/store";

// Theme Configurations
import "prismjs";
import "prismjs/themes/prism.css";
import VsPrism from "./components/prism/VsPrism.vue";
Vue.component(VsPrism.name, VsPrism);

// Vue Router
import router from "./router";

const access_token = localStorage.getItem("access_token");
if (access_token) {
  store.commit("setToken", access_token);
  axios.defaults.headers["Authorization"] = access_token;
} else {
  store.commit("clearToken");
  delete axios.defaults.headers["Authorization"];
}

const user = localStorage.getItem("user");
if (user) {
  store.commit("setUser", JSON.parse(user));
}

const cart = localStorage.getItem("cart");
if (cart) {
  store.commit("setCart", JSON.parse(cart));
}

axios.defaults.baseURL = "https://localhost:44309";
axios.create({
  baseURL: axios.defaults.baseURL, // Build Location
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
  },
});
Vue.prototype.$http = axios;

Vue.config.productionTip = false;
Vue.config.devtools = true;
Vue.use(Vuesax);
Vue.use(vuelidate);

new Vue({
  store,
  router,
  render: (h) => h(App),
}).$mount("#app");
import "@/assets/scss/style.scss";
