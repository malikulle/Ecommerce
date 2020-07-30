import Vue from "vue";
import Router from "vue-router";
import store from "./store/store";

import Home from "./pages/Home/index.vue";
import Login from "./pages/Login/index.vue";
import Register from "./pages/Register/index.vue";
import ProductDetails from "./pages/Products/ProductDetails.vue";
import Verification from "./pages/Register/Verification.vue";
import CreateProduct from "./pages/Products/CreateProduct.vue";
import CategoryList from "./pages/Category/CategoryList.vue";
import Products from "./pages/Products/Products.vue";
import Cart from "./pages/Cart/index.vue";
import Checkout from "./pages/Cart/Checkout.vue";
import Fail from "./pages/Response/Fail.vue"
import Success from "./pages/Response/Success.vue"
import Order from "./pages/Order/index.vue"
import Profile from "./pages/User/index.vue"

Vue.use(Router);

export default new Router({
  routes: [
    { path: "/", component: Home, name: "home" },
    { path: "/login", component: Login, name: "login" },
    { path: "/register", component: Register, name: "register" },
    { path: "/verifyAccount", component: Verification, name: "verifyAccount" },
    {
      path: "/createProduct",
      component: CreateProduct,
      name: "createProduct",
      beforeEnter(to, from, next) {
        const user = store.getters.user;
        const isAuthenticated = store.getters.isAuthenticated;

        if (user.role === "Admin" && isAuthenticated) {
          next();
        } else {
          next("/");
        }
      },
    },
    {
      path: "/:id/edit",
      component: CreateProduct,
      name: "editProduct",
      beforeEnter(to, from, next) {
        const user = store.getters.user;
        const isAuthenticated = store.getters.isAuthenticated;

        if (user.role === "Admin" && isAuthenticated) {
          next();
        } else {
          next("/");
        }
      },
    },
    { path: "/:id/product", component: ProductDetails, name: "productDetails" },
    {
      path: "/category",
      component: CategoryList,
      name: "category",
      beforeEnter(to, from, next) {
        const user = store.getters.user;
        const isAuthenticated = store.getters.isAuthenticated;

        if (user.role === "Admin" && isAuthenticated) {
          next();
        } else {
          next("/");
        }
      },
    },
    {
      path: "/products",
      component: Products,
      name: "products",
      beforeEnter(to, from, next) {
        const user = store.getters.user;
        const isAuthenticated = store.getters.isAuthenticated;

        if (user.role === "Admin" && isAuthenticated) {
          next();
        } else {
          next("/");
        }
      },
    },
    {
      path: "/cart",
      component: Cart,
      name: "cart",
      beforeEnter(to, from, next) {
        const isAuthenticated = store.getters.isAuthenticated;
        if (isAuthenticated) {
          next();
        } else {
          next("/");
        }
      },
    },
    {
      path: "/checkout",
      component: Checkout,
      name: "checkout",
      beforeEnter(to, from, next) {
        const isAuthenticated = store.getters.isAuthenticated;
        if (isAuthenticated) {
          next();
        } else {
          next("/");
        }
      },
    },
    {
      path: "/success",
      component: Success,
      name: "success",
      beforeEnter(to, from, next) {
        const isAuthenticated = store.getters.isAuthenticated;
        if (isAuthenticated) {
          next();
        } else {
          next("/");
        }
      },
    },
    {
      path: "/fail",
      component: Fail,
      name: "fail",
      beforeEnter(to, from, next) {
        const isAuthenticated = store.getters.isAuthenticated;
        if (isAuthenticated) {
          next();
        } else {
          next("/");
        }
      },
    },
    {
      path: "/orders",
      component: Order,
      name: "orders",
      beforeEnter(to, from, next) {
        const isAuthenticated = store.getters.isAuthenticated;
        if (isAuthenticated) {
          next();
        } else {
          next("/");
        }
      },
    },
    {
      path: "/profile",
      component: Profile,
      name: "orders",
      beforeEnter(to, from, next) {
        const isAuthenticated = store.getters.isAuthenticated;
        if (isAuthenticated) {
          next();
        } else {
          next("/");
        }
      },
    },
  ],
});
