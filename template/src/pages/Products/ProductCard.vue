<template>
  <vs-card actionable>
    <div slot="header">
      <h3>
        {{ product.name }}
      </h3>
    </div>
    <div slot="media">
      <img :src="`${url}/img/${product.imageUrl}`" height="250" width="200" />
    </div>
    <div class="mb-2">
      <h6 class="mb-2">{{ product.price }} TL</h6>
      Lorem ipsum dolor sit amet, consectetur ipsum dolor sit amet, consectetur
      ipsum dolor sit amet, consectetur adipiscing elit
    </div>
    <vs-button @click="() => $router.push(`/${product.id}/product`)" color="primary" type="filled">View Detail</vs-button>
  </vs-card>
</template>

<script>
import keys from "../../keys"
export default {
  data() {
    return {
      url: keys.URL
    };
  },
  props: {
    product: {
      type: Object,
      required: true,
    },
  },
};
</script>

<style></style>
