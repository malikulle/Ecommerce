<template>
  <div class="mt-4">
    <vs-card>
      <vs-alert color="success" title="Success" active="true">
        Checkout Success
      </vs-alert>
    </vs-card>
  </div>
</template>

<script>
export default {
  created() {
    this.clearCart();
  },
  methods: {
    clearCart() {
      this.$store.dispatch("clearCart");
    },
  },
};
</script>

<style></style>
