<template>
  <div class="mt-4">
    <vs-card v-if="cart.length > 0">
      <h1>Shipping Cart</h1>
      <hr />
      <vs-row>
        <vs-col vs-lg="8" vs-sm="8" vs-xs="12">
          <vs-table :data="cart">
            <template slot="thead">
              <vs-th></vs-th>
              <vs-th>Name</vs-th>
              <vs-th>Price</vs-th>
              <vs-th>Quantity</vs-th>
              <vs-th>Total</vs-th>
              <vs-th></vs-th>
            </template>

            <template slot-scope="{ data }">
              <vs-tr :key="indextr" v-for="(tr, indextr) in data">
                <vs-td :data="data[indextr].imageUrl">
                  <img
                    :src="`${url}/img/${data[indextr].imageUrl}`"
                    height="110"
                    width="100"
                  />
                </vs-td>
                <vs-td :data="data[indextr].name">
                  {{ data[indextr].name }}
                </vs-td>

                <vs-td :data="data[indextr].price">
                  {{ data[indextr].price }}
                </vs-td>

                <vs-td :data="data[indextr].quantity">
                  {{ data[indextr].quantity }}
                </vs-td>

                <vs-td :data="data[indextr].quantity">
                  {{ data[indextr].quantity * data[indextr].price }}
                </vs-td>
                <vs-td :data="data[indextr].stock">
                  <vs-button
                    color="danger"
                    type="filled"
                    @click="() => remove(data[indextr])"
                  >
                    <i class="fa fa-times fa-fw" />
                  </vs-button>
                </vs-td>
              </vs-tr>
            </template>
          </vs-table>
        </vs-col>
        <vs-col vs-lg="4" vs-sm="4" vs-xs="12">
          <div class="text-left">
            <h4>Cart Summary</h4>
          </div>
          <vs-table :data="cart">
            <template>
              <vs-tr>
                <vs-td>
                  <span class="font-weight-bold">Cart Total : </span>
                  {{
                    cart.reduce(
                      (sum, item) => sum + item.price * item.quantity,
                      0
                    )
                  }}
                  TL
                </vs-td>
              </vs-tr>
              <vs-tr>
                <vs-td>
                  <span class="font-weight-bold">Shippting : </span> Free
                </vs-td>
              </vs-tr>
              <vs-tr>
                <vs-td>
                  <span class="font-weight-bold">Order Total : </span>
                  {{
                    cart.reduce(
                      (sum, item) => sum + item.price * item.quantity,
                      0
                    )
                  }}
                  TL
                </vs-td>
              </vs-tr>
            </template>
          </vs-table>
          <div class="text-center mt-3">
            <router-link exact to="/" tag="a">
              <vs-button color="primary" type="filled" class="mr-3">
                <i class="fa fa-arrow-circle-left fa-fw" /> Continue Shopping
              </vs-button>
            </router-link>
            <router-link exact to="/checkout" tag="a">
              <vs-button color="primary" type="filled">
                <i class="fa fa-arrow-circle-right fa-fw" /> Checkout
              </vs-button>
            </router-link>
          </div>
        </vs-col>
      </vs-row>
    </vs-card>
    <vs-card v-else>
      <vs-alert color="danger" title="Info" active="true" class="container">
        No Product In Card
      </vs-alert>
    </vs-card>
  </div>
</template>

<script>
import keys from "../../keys";
export default {
  data() {
    return {
      url: keys.URL,
    };
  },
  computed: {
    cart() {
      return this.$store.getters.cart;
    },
  },
  methods: {
    remove(product) {
      this.$store.dispatch("removeFromCart", product);
    },
  },
};
</script>

<style></style>
