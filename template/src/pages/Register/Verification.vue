<template>
  <div class="mt-4">
    <vs-alert
      v-if="result"
      title="Success"
      active="true"
      color="success"
      class="mb-3"
    >
      Your Account is activated. You can login
    </vs-alert>
    <vs-alert v-else title="Danger" active="true" color="danger" class="mb-3">
      {{ message }}
    </vs-alert>
  </div>
</template>

<script>
export default {
  created() {
    if (!this.$route.query.token) {
      this.$router.push("/");
    } else {
      this.token = this.$route.query.token;
      this.verifyAccount();
    }
  },
  data() {
    return {
      token: null,
      result: true,
      message: null,
    };
  },
  methods: {
    async verifyAccount() {
      try {
        await this.$http.get("/api/auth/verifyAccount?token=" + this.token);
        this.result = true;
      } catch (error) {
        if (error.response.data && !error.response.data.result) {
          this.result = false;
          this.message = error.response.data.message;
        }
      }
    },
  },
};
</script>

<style></style>
