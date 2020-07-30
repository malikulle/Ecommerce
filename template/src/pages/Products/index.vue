<template>
  <div class="mt-4">
    <vs-card>
      <vs-row>
        <vs-col vs-lg="3" vs-sm="6" vs-xs="12">
          <vs-list>
            <vs-list-item
              v-for="category in categories"
              :key="category.id"
              style="cursor:pointer;"
              :class="categoryId === category.id ? 'active' : ''"
            >
              <span @click="() => getProducts(category.id)">{{
                category.name
              }}</span></vs-list-item
            >
          </vs-list>
        </vs-col>
        <vs-col vs-lg="9" vs-sm="6" vs-xs="12">
          <vs-row>
            <vs-col
              v-for="product in products"
              :key="product.id"
              vs-lg="4"
              vs-sm="6"
              vs-xs="12"
            >
              <ProductCard :product="product" />
            </vs-col>
          </vs-row>
        </vs-col>
      </vs-row>
      <vs-pagination
        :maxIndex="totalPages"
        :maxItems="totalItems"
        :sizeArray="totalItems"
        :total="totalPages"
        v-model="currentx"
      ></vs-pagination>
    </vs-card>
  </div>
</template>

<script>
import ProductCard from "./ProductCard";
export default {
  components: {
    ProductCard,
  },
  async created() {
    await this.getCategories();
    await this.getProducts(0);
  },
  data() {
    return {
      categories: [],
      products: [],
      currentx: 1,
      categoryId: 0,
      totalPages: 1,
      totalItems: 1,
    };
  },
  methods: {
    async getCategories() {
      try {
        const { data } = await this.$http.get("/api/categories");
        this.categories = data;
      } catch (error) {
        // eslint-disable-next-line no-console
        console.log(error);
      }
    },
    async getProducts(categoryId) {
      this.$vs.loading();
      if (categoryId !== 0) {
        this.categoryId = categoryId;
      }
      try {
        const { data } = await this.$http.get(
          `/api/products/${categoryId}?page=${this.currentx}`
        );
        this.products = data.products;
        this.currentx = data.pageInfo.currentPage;
        this.totalPages = data.pageInfo.totalPages;
        this.totalItems = data.pageInfo.totalItems;
      } catch (error) {
        // eslint-disable-next-line no-console
        console.log(error);
      }
      this.$vs.loading.close();
    },
    setCategory(id) {
      this.categoryId = id;
      this.getCategoryProducts();
    },
  },
  watch: {
    currentx: async function() {
      await this.getProducts(this.categoryId);
    },
  },
};
</script>

<style lang="scss" scoped>
.active {
  color: white;
  background:#2962FF;
}
</style>
