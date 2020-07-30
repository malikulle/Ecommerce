<template>
  <div class="centerx">
    <vs-navbar
      v-model="indexActive"
      :color="colorx"
      text-color="rgba(255,255,255,.6)"
      active-text-color="rgba(255,255,255,1)"
      class="myNavbar margin-auto"
    >
      <div slot="title">
        <vs-navbar-title>
          <router-link exact to="/" tag="div">
            <span style="color:white; cursor:pointer;">ECommerce-Shop</span>
          </router-link>
        </vs-navbar-title>
      </div>
      <vs-navbar-item index="0">
        <router-link exact to="/" tag="a" v-if="!isLoggedIn">
          Home
        </router-link>
      </vs-navbar-item>
      <vs-navbar-item index="1">
        <router-link exact to="/login" tag="a" v-if="!isLoggedIn">
          Login
        </router-link>
      </vs-navbar-item>
      <vs-navbar-item index="2">
        <router-link exact to="/Register" tag="a" v-if="!isLoggedIn">
          Register
        </router-link>
      </vs-navbar-item>
      <vs-navbar-item index="0">
        <router-link exact to="/cart" tag="a" v-if="isLoggedIn">
          <vs-avatar
            :badge="cart.length"
            text="Cart"
            style="height: 22px;width: 25px;"
          />
        </router-link>
      </vs-navbar-item>
      <vs-dropdown
        v-if="isLoggedIn"
        vs-trigger-click
        left
        class="cursor-pointer pr-2 pl-2 ml-1 mr-md-3"
      >
        <a class="text-white-dark user-image" href="#"
          ><img :src="`${url}/img/${user.imageUrl}`" alt="User"
        /></a>
        <vs-dropdown-menu class="topbar-dd">
          <vs-dropdown-item
            ><vs-icon icon="person_outline" class="mr-1"></vs-icon>
            <router-link exact to="/profile" tag="a">
              <span>Profile</span>
            </router-link></vs-dropdown-item
          >
          <hr class="mb-1" />
          <vs-dropdown-item
            ><vs-icon icon="gps_not_fixed" class="mr-1"></vs-icon>
            <router-link exact to="/" tag="a" v-if="isLoggedIn">
              <span @click="logout">Logout</span>
            </router-link>
          </vs-dropdown-item>
        </vs-dropdown-menu>
      </vs-dropdown>
    </vs-navbar>
  </div>
</template>
<script>
import keys from "../../keys";
export default {
  data: () => ({
    url: keys.URL,
    colorx: "#2962FF",
    indexActive: 0,
  }),
  computed: {
    isLoggedIn() {
      return this.$store.getters.isAuthenticated;
    },
    user() {
      return this.$store.getters.user;
    },
    cart() {
      return this.$store.getters.cart;
    },
  },
  methods: {
    logout() {
      this.indexActive = 0;
      this.$store.dispatch("logout");
      this.$vs.notify({
        title: "Success",
        text: "Successfully Logged out!!",
        color: "primary",
      });
    },
  },
};
</script>
<style scoped></style>
