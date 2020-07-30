import Vue from "vue";
import Vuex from "vuex";
Vue.use(Vuex);
/* eslint-disable */
const store = new Vuex.Store({
  state: {
    token: "",
    user: {
      id: null,
      name: null,
      surname: null,
      email: null,
      role: null,
      address: null,
      imageUrl: null,
    },
    isSidebarActive: false,
    themeColor: "#2962ff",
    cart: [],
  },
  mutations: {
    setToken(state, token) {
      localStorage.setItem("access_token", token);
      state.token = token;
    },
    setUser(state, user) {
      localStorage.setItem("user", JSON.stringify(user));
      state.user = user;
    },
    clearToken(state) {
      localStorage.clear();
      state.token = "";
      state.user = {
        id: null,
        name: null,
        surname: null,
        email: null,
        role: null,
        address: null,
        imageUrl: null,
      };
      state.cart = [];
    },
    IS_SIDEBAR_ACTIVE(state, value) {
      state.isSidebarActive = value;
    },
    addToCart(state, product) {
      const productInState = state.cart.find((x) => x.id === product.id);
      if (!productInState) {
        state.cart.push(product);
      } else {
        state.cart.forEach((item) => {
          if (item.id === product.id) {
            item.quantity += product.quantity;
          }
        });
      }
      localStorage.setItem("cart", JSON.stringify(state.cart));
    },
    removeFromCart(state, product) {
      state.cart = state.cart.filter((x) => x.id !== product.id);
      localStorage.setItem("cart", JSON.stringify(state.cart));
    },
    setCart(state, products) {
      state.cart = products;
    },
    clearCart(state){
      state.cart = []
      localStorage.setItem("cart", JSON.stringify(state.cart));
    }
  },
  actions: {
    login({ commit, dispatch, state }, autData) {
      commit(
        "setToken",
        `${autData.token.token_type} ${autData.token.access_token}`
      );
      commit("setUser", autData.user);
    },
    logout({ commit, dispatch, state }) {
      commit("clearToken");
    },
    addToCart({ commit, dispatch, state }, product) {
      commit("addToCart", product);
    },
    removeFromCart({ commit, dispatch, state }, product) {
      commit("removeFromCart", product);
    },
    clearCart({commit}){
      commit("clearCart")
    }
  },
  getters: {
    isAuthenticated(state) {
      return state.token !== "";
    },
    user(state) {
      return state.user;
    },
    cart(state) {
      return state.cart;
    },
  },
});

export default store;
