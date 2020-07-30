<template>
  <div class="mt-4">
    <vs-card>
      <h1>Checkout</h1>
      <vs-row>
        <vs-col vs-lg="8" vs-sm="8" vs-xs="12">
          <h4 class="mb-3">Order Details</h4>
          <vs-row class="mb-3">
            <vs-col vs-lg="6" vs-sm="6" vs-xs="12">
              <vs-input
                label="Name"
                v-model="$v.order.name.$model"
                :danger="$v.order.name.$error"
              />
            </vs-col>
            <vs-col vs-lg="6" vs-sm="6" vs-xs="12">
              <vs-input
                label="Surname"
                v-model="$v.order.surname.$model"
                :danger="$v.order.surname.$error"
              />
            </vs-col>
          </vs-row>
          <vs-row class="mb-3">
            <vs-col vs-lg="12" vs-sm="12" vs-xs="12">
              <label>Address</label>
              <vs-textarea
                v-model="$v.order.address.$model"
                :danger="$v.order.address.$error"
              />
            </vs-col>
          </vs-row>
          <vs-row class="mb-3">
            <vs-col vs-lg="4" vs-sm="4" vs-xs="12">
              <vs-input
                label="City"
                v-model="$v.order.city.$model"
                :danger="$v.order.city.$error"
              />
            </vs-col>
            <vs-col vs-lg="4" vs-sm="4" vs-xs="12">
              <vs-input
                label="Phone"
                v-model="$v.order.phone.$model"
                :danger="$v.order.phone.$error"
              />
            </vs-col>
            <vs-col vs-lg="4" vs-sm="4" vs-xs="12">
              <vs-input
                label="Email"
                v-model="$v.order.email.$model"
                :danger="$v.order.email.$error"
              />
            </vs-col>
          </vs-row>
          <hr class="mb-4" />
          <vs-checkbox class="justify-content-start"
            >Save This Information</vs-checkbox
          >
          <hr class="mb-4" />
          <vs-row class="mb-3">
            <vs-col vs-lg="12" vs-sm="12" vs-xs="12">
              <vs-radio
                v-model="order.paymentType"
                vs-value="1"
                style="float:left;"
                >Credit Card</vs-radio
              >
              <vs-radio v-model="order.paymentType" vs-value="2">EFT</vs-radio>
            </vs-col>
          </vs-row>
          <hr class="mb-4" />
          <h3>PAYMENT</h3>
          <hr class="mb-4" />
          <vs-row class="mb-3">
            <vs-col vs-lg="6" vs-sm="6" vs-xs="12">
              <vs-input
                label="Name on Credit Card"
                :disabled="true"
                :danger="$v.order.cardName.$error"
                v-model="$v.order.cardName.$model"
              />
            </vs-col>
            <vs-col vs-lg="6" vs-sm="6" vs-xs="12">
              <vs-input
                label="Credi Card Number"
                :disabled="true"
                :danger="$v.order.cardNumber.$error"
                v-model="$v.order.cardNumber.$model"
              />
            </vs-col>
          </vs-row>
          <vs-row class="mb-3">
            <vs-col vs-lg="4" vs-sm="4" vs-xs="12">
              <vs-input
                label="Month"
                :disabled="true"
                :danger="$v.order.expireMonth.$error"
                v-model="$v.order.expireMonth.$model"
              />
            </vs-col>
            <vs-col vs-lg="4" vs-sm="4" vs-xs="12">
              <vs-input
                label="Year"
                :disabled="true"
                :danger="$v.order.expireYear.$error"
                v-model="$v.order.expireYear.$model"
              />
            </vs-col>
            <vs-col vs-lg="4" vs-sm="4" vs-xs="12">
              <vs-input
                label="CVV"
                :disabled="true"
                v-model="$v.order.cvc.$model"
                :danger="$v.order.cvc.$error"
              />
            </vs-col>
          </vs-row>
          <vs-row class="mb-3">
            <vs-button color="primary" class="btn-block" @click="saveOrder"
              >Submit</vs-button
            >
          </vs-row>
        </vs-col>
        <vs-col vs-lg="4" vs-sm="4" vs-xs="12">
          <h4 class="mb-3">
            <span class="text-muted">Your Cart</span>
          </h4>
          <vs-list>
            <vs-list-item
              v-for="(item, index) in cart"
              :key="index"
              :title="item.name + '* ' + item.quantity"
              :subtitle="item.price * item.quantity + ' TL'"
            >
            </vs-list-item>
            <vs-list-item
              title="Total"
              :subtitle="
                cart.reduce(
                  (sum, item) => sum + item.price * item.quantity,
                  0
                ) + ' TL'
              "
            >
            </vs-list-item>
          </vs-list>
        </vs-col>
      </vs-row>
    </vs-card>
  </div>
</template>

<script>
import { required, email } from "vuelidate/lib/validators";
export default {
  data() {
    return {
      order: {
        name: "",
        surname: "",
        address: "",
        city: "",
        phone: "",
        email: "",
        paymentType: 1,
        cardName: "John Doe",
        cardNumber: "5528790000000008",
        expireMonth: 12,
        expireYear: 2030,
        cvc: "123",
        orderItems: [],
      },
    };
  },
  computed: {
    cart() {
      return this.$store.getters.cart;
    },
  },
  methods: {
    async saveOrder() {
      if (this.$v.order.$invalid) {
        this.$v.$touch();
        this.$vs.notify({
          title: "Warning",
          text: "Please fill all the blanks",
          color: "warning",
        });
        return;
      }
      this.order.orderItems = [];
      this.cart.forEach((item) => {
        this.order.orderItems.push({
          productId: item.id,
          quantity: item.quantity,
          price: item.price * item.quantity,
        });
      });
      try {
        const { data } = await this.$http.post("/api/orders", this.order);
        if (data.result) {
          this.$router.push("/success");
        } else {
        this.$router.push("/fail");
        }
      } catch (error) {}
    },
  },
  validations: {
    order: {
      name: {
        required,
      },
      surname: {
        required,
      },
      address: {
        required,
      },
      city: {
        required,
      },
      phone: {
        required,
      },
      email: {
        required,
        email,
      },
      cardName: {
        required,
      },
      cardNumber: {
        required,
      },
      expireMonth: {
        required,
      },
      expireYear: {
        required,
      },
      cvc: {
        required,
      },
    },
  },
};
</script>

<style></style>
