<template>
  <div id="app">
    <SideBar v-if="isLoggedIn" :sidebarLinks="this.user.role === 'Admin' ? this.SideBarLink : this.SideBarLinkUser" />
    <Navbar />
    <div class="container">
      <transition name="slide" mode="out-in">
        <router-view></router-view>
      </transition>
    </div>
  </div>
</template>

<script>
import Navbar from "./pages/Navbar/Navbar";
import SideBar from "./layout/full/sidebar/SideBar";
import SideBarLink from "./layout/full/sidebar/sidebarlinks";
import SideBarLinkUser from "./layout/full/sidebar/sidebarlinks-user";
export default {
  name: "app",
  components: {
    Navbar,
    SideBar,
  },
  data: () => ({
    SideBarLink: SideBarLink,
    SideBarLinkUser: SideBarLinkUser,
  }),
  computed: {
    isLoggedIn() {
      return this.$store.getters.isAuthenticated;
    },
    user() {
      return this.$store.getters.user;
    },
  },
  methods: {},
};
</script>

<style>
.slide-enter {
  opacity: 0;
}

.slide-enter-active {
  animation: slide-in 1s ease-out forwards;
  transition: opacity 0.5s;
}

.slide-leave {
}

.slide-leave-active {
  animation: slide-out 1s ease-out forwards;
  transition: opacity 1s;
  opacity: 0;
  position: absolute;
}

@keyframes slide-in {
  from {
    transform: translateY(20px);
  }
  to {
    transform: translateY(0px);
  }
}

@keyframes slide-out {
  from {
    transform: translateY(0px);
  }
  to {
    transform: translateY(20px);
  }
}
</style>
