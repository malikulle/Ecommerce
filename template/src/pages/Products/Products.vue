<template>
  <div class="mt-4">
    <vs-card>
      <vs-table @search="handleSearch" :sst="true" :data="products" search>
        <template slot="thead">
          <vs-th></vs-th>
          <vs-th>Id</vs-th>
          <vs-th>Is Active</vs-th>
          <vs-th>Name</vs-th>
          <vs-th>Price</vs-th>
          <vs-th>Stock</vs-th>
        </template>

        <template slot-scope="{ data }">
          <vs-tr :key="indextr" v-for="(tr, indextr) in data">
            <vs-td :data="data[indextr].imageUrl">
              <img
                :src="`${url}/img/${data[indextr].imageUrl}`"
                height="150"
                width="100"
              />
            </vs-td>
            <vs-td :data="data[indextr].id">
              {{ data[indextr].id }}
            </vs-td>

            <vs-td :data="data[indextr].isActive">
              {{ data[indextr].isActive }}
            </vs-td>

            <vs-td :data="data[indextr].name">
              {{ data[indextr].name }}
            </vs-td>

            <vs-td :data="data[indextr].price">
              {{ data[indextr].price }}
            </vs-td>

            <vs-td :data="data[indextr].stock">
              {{ data[indextr].stock }}
            </vs-td>

            <vs-td>
              <vs-button
                color="primary"
                type="filled"
                @click="() => $router.push(`/${data[indextr].id}/edit`)"
                >Edit</vs-button
              >
              <vs-button
                color="danger"
                class="ml-3"
                type="filled"
                @click="() => onDeleteClick(data[indextr].id)"
                >Remove</vs-button
              >
            </vs-td>
          </vs-tr>
        </template>
      </vs-table>
    </vs-card>
    <vs-popup class="holamundo" title="Delete" :active.sync="deletePopup">
      <p>Are you sure to delete category</p>
      <vs-button
        color="danger"
        class="mt-3"
        type="filled"
        @click="deleteProduct"
        >Remove</vs-button
      >
    </vs-popup>
  </div>
</template>

<script>
import keys from "../../keys";
export default {
  data() {
    return {
      deletePopup: false,
      url: keys.URL,
      products: [],
      copiedProducts: [],
      productId: null,
    };
  },
  created() {
    this.getProdcuts();
  },
  methods: {
    async getProdcuts() {
      try {
        const { data } = await this.$http.get("/api/products/all");

        this.products = data;
        this.copiedProducts = data;
      } catch (error) {}
    },
    async handleSearch(searching) {
      if (searching) {
        try {
          const { data } = await this.$http.get(
            `/api/products/all?name=${searching}`
          );
          this.products = data;
        } catch (error) {}
      } else {
        this.products = this.copiedProducts;
      }
    },
    onDeleteClick(id) {
      this.deletePopup = true;
      this.productId = id;
    },
    async deleteProduct() {
      if (this.productId) {
        try {
          const { data } = await this.$http.delete(
            "/api/products/" + this.productId
          );
          this.$vs.notify({
            title: "Success",
            text: data.message,
            color: "primary",
          });
          this.deletePopup = false;
          this.products = this.products.filter((x) => x.id !== this.productId);
        } catch (error) {}
      }
    },
  },
};
</script>

<style></style>
