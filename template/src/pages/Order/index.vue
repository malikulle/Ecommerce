<template>
  <div class="mt-4">
    <vs-card>
      <h1>Orders</h1>
      <hr />
      <table
        :key="index"
        v-for="(order, index) in orders"
        class="table table-bordered table-sm mb-3"
      >
        <thead class="bg-primary text-white">
          <tr>
            <td colspan="2">Order Id : #{{ order.id }}</td>
            <td>Price</td>
            <td>Quantity</td>
          </tr>
        </thead>
        <tbody>
          <tr :key="index" v-for="(orderItem, index) in order.orderItems">
            <td style="width:60px">
              <img
                :src="`${url}/img/${orderItem.imageUrl}`"
                alt="product"
                width="50"
              />
            </td>
            <td>
              {{ orderItem.name }}
            </td>
            <td>
              {{ orderItem.price }}
            </td>
            <td>
              {{ orderItem.quantity }}
            </td>
          </tr>
        </tbody>
        <tfoot>
          <tr>
            <td colspan="2">Customer Name :</td>
            <td>{{ order.name + " " + order.surname }}</td>
            <td rowspan="3">
              Total
              {{
                order.orderItems.reduce(
                  (sum, item) => sum + item.price * item.quantity,
                  0
                )
              }}
            </td>
          </tr>
          <tr>
              <td colspan="2">Adres : </td>
              <td>{{order.address}}</td>
          </tr>
          <tr>
              <td colspan="2">Phone : </td>
              <td>{{order.phone}}</td>
          </tr>
          <tr>
              <td colspan="2">Email : </td>
              <td>{{order.email}}</td>
          </tr>
          <tr>
              <td colspan="2">Payment Status : </td>
              <td>{{order.paymentType}}</td>
          </tr>
          <tr>
              <td colspan="2">Order Status : </td>
              <td>{{order.orderState}}</td>
          </tr>
        </tfoot>
      </table>
    </vs-card>
  </div>
</template>

<script>
import key from "../../keys";
export default {
  created() {
    this.getOrders();
  },
  data() {
    return {
      url: key.URL,
      orders: [],
    };
  },
  methods: {
    async getOrders() {
      try {
        const { data } = await this.$http.get("/api/orders");
        this.orders = data;
        console.log(this.orders);
      } catch (error) {}
    },
  },
};
</script>

<style></style>
