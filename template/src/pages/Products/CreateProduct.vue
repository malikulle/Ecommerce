<template>
  <div class="mt-4">
    <vs-card>
      <form encType="multipart/form-data">
        <div class="default-input d-flex align-items-center">
          <vs-input label="Name" class="inputx" v-model="product.name" />
          <vs-input
            label="Price"
            class="inputx ml-4"
            type="number"
            v-model="product.price"
          />
          <vs-select
            class="selectExample ml-4"
            label="Categories"
            multiple
            v-model="selectedCategories"
          >
            <vs-select-item
              multiple
              :key="index"
              :value="item.id"
              :text="item.name"
              v-for="(item, index) in categories"
            />
          </vs-select>
        </div>
        <div class="default-input d-flex align-items-center mt-2">
          <vs-input-number class="" label="Stock" v-model="product.stock" />
          <vs-input
            label="Image"
            class="inputx ml-4"
            type="file"
            @change="uploadImage"
          />
          <img
            v-if="product.imageUrl"
            :src="file || `${url}/img/${product.imageUrl}`"
            height="150"
            width="100"
          />
        </div>
        <div class="default-input d-flex align-items-center mt-3">
          <vs-textarea label="Description" v-model="product.description" />
        </div>
        <div class="default-input d-flex align-items-center mt-4 ">
          <vs-button class="btn-block" @click="create">Save</vs-button>
        </div>
      </form>
    </vs-card>
  </div>
</template>

<script>
import keys from "../../keys";
export default {
  data() {
    return {
      number: 0,
      url: keys.URL,
      product: {
        name: "",
        price: 0,
        description: "",
        stock: 0,
        imageUrl: null,
        productCategories: [],
      },
      file: null,
      categories: [],
      selectedCategories: [],
    };
  },
  created() {
    if (this.$route.params.id) {
      this.getById(this.$route.params.id);
    }
    this.getCategories();
  },
  methods: {
    async create() {
      try {
        if (this.product.price)
          this.product.price = parseInt(this.product.price);
        this.product.productCategories = [];
        if (this.selectedCategories.length > 0) {
          this.selectedCategories.map((item) => {
            if (this.$route.params.id) {
              this.product.productCategories.push({
                categoryId: item,
                productId: parseInt(this.$route.params.id),
              });
            } else {
              this.product.productCategories.push({ categoryId: item });
            }
          });
        }
        if (!this.$route.params.id) {
          await this.$http.post("/api/products", {
            product: this.product,
            file: this.file,
          });
        } else {
          await this.$http.put(
            "/api/products/" + parseInt(this.$route.params.id),
            {
              product: this.product,
              file: this.file,
            }
          );
        }
      } catch (error) {}
      this.$vs.notify({
        title: "Success",
        text: "Successfull",
        color: "primary",
      });
    },
    async uploadImage(e) {
      const file = e.target.files[0];
      const base = await this.toBase64(file);
      this.file = base;
    },
    toBase64(file) {
      return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = (error) => reject(error);
      });
    },
    async getCategories() {
      try {
        const { data } = await this.$http.get("/api/categories");
        this.categories = data;
      } catch (error) {}
    },
    async getById(id) {
      try {
        const { data } = await this.$http.get("/api/products/getById/" + id);
        data.productCategories.map((item) => {
          this.selectedCategories.push(item.categoryId);
        });
        data.productCategories = [];
        this.product = data;
      } catch (error) {}
    },
  },
};
</script>

<style></style>
