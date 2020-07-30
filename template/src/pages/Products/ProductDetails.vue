<template>
  <div class="mt-4">
    <vs-card>
      <vs-row>
        <vs-col vs-lg="3" vs-sm="6" vs-xs="12">
          <img
            :src="`${url}/img/${product.imageUrl}`"
            alt="product"
            class="img-fluid"
          />
        </vs-col>
        <vs-col vs-lg="9" vs-sm="6" vs-xs="12">
          <h1 class="mb-3">{{ product.name }}</h1>
          <hr />
          <div class="text-primary mb-3">Categories</div>
          <a
            v-for="(item, index) in product.productCategories"
            :key="index"
            href="#"
            class="btn btn-link p-0 mb-3"
          >
            <vs-chip color="primary">{{ item.name }}</vs-chip>
          </a>
          <hr />
          <div class="mb-3">
            <h4 class="text-primary mb-3">{{ product.price }} TL</h4>
            <vs-input-number v-model="number" label="Quantity:" />
            <hr />
            <vs-button
              color="primary"
              type="border"
              icon="favorite"
              :disabled="number <= 0"
              @click="() => addToCard(product)"
              >Add to Cart</vs-button
            >
          </div>
        </vs-col>
      </vs-row>
    </vs-card>
  </div>
</template>

<script>
import keys from "../../keys";
export default {
  created() {
    this.id = this.$route.params.id;
    this.getById(this.id);
  },
  data() {
    return {
      productId: 0,
      number: 1,
      product: {},
      url: keys.URL,
    };
  },
  methods: {
    async getById(id) {
      try {
        const { data } = await this.$http.get("/api/products/getById/" + id);
        this.product = data;
      } catch (error) {
        // eslint-disable-next-line no-console
        console.log(error);
        this.$vs.notify({
          color: "danger",
          title: "Warning !!",
          text: "Product Not Found",
        });
      }
    },
    addToCard(product) {
      if (!this.isLoggedIn) {
        this.$router.push("/login");
        this.$vs.notify({
          color: "primary",
          title: "Login",
          text: "Please First Login to add cart",
        });
      } else {
        const addedProduct = {
          ...product,
          quantity: this.number,
        };
        this.$store.dispatch("addToCart", addedProduct);
        this.$vs.notify({
          color: "primary",
          title: "Added",
          text: `${product.name} added to cart`,
        });
      }
    },
  },
  watch: {
    number: function(value) {
      if (value < 0) {
        this.number = 0;
      }
      if (value > this.product.stock) {
        this.number = this.product.stock;
      }
    },
  },
  computed: {
    isLoggedIn() {
      return this.$store.getters.isAuthenticated;
    },
  },
};
</script>

<style></style>
