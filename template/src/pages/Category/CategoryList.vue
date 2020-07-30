<template>
  <div class="mt-4">
    <vs-card>
      <vs-button
        color="primary"
        class="mb-4 "
        type="border"
        @click="() => openPopup()"
        >Create Category</vs-button
      >
      <vs-table :data="categories">
        <template slot="thead">
          <vs-th>Id</vs-th>
          <vs-th>Is Active</vs-th>
          <vs-th>Name</vs-th>
          <vs-th></vs-th>
        </template>

        <template slot-scope="{ data }">
          <vs-tr :key="indextr" v-for="(tr, indextr) in data">
            <vs-td :data="data[indextr].email">
              {{ data[indextr].id }}
            </vs-td>

            <vs-td :data="data[indextr].username">
              {{ data[indextr].isActive }}
            </vs-td>

            <vs-td :data="data[indextr].id">
              {{ data[indextr].name }}
            </vs-td>

            <vs-td>
              <vs-button
                color="primary"
                type="filled"
                @click="() => getCategory(data[indextr].id)"
                >Edit</vs-button
              >
              <vs-button
                color="danger"
                class="ml-3"
                type="filled"
                @click="() => clickRemove(data[indextr].id)"
                >Remove</vs-button
              >
            </vs-td>
          </vs-tr>
        </template>
      </vs-table>
    </vs-card>
    <vs-popup class="holamundo" title="Create" :active.sync="popupActive">
      <vs-input
        class="inputx"
        :danger="$v.category.name.$error"
        v-model="$v.category.name.$model"
        label="Category Name"
      />
      <vs-button
        color="primary"
        class="mt-3"
        type="filled"
        @click="createCategory"
        >Save</vs-button
      >
    </vs-popup>
    <vs-popup class="holamundo" title="Delete" :active.sync="deletePopup">
      <p>Are you sure to delete category</p>
      <vs-button
        color="danger"
        class="mt-3"
        type="filled"
        @click="deleteCategory"
        >Remove</vs-button
      >
    </vs-popup>
  </div>
</template>

<script>
import { required } from "vuelidate/lib/validators";
export default {
  async created() {
    await this.getCategories();
  },
  data() {
    return {
      users: [
        {
          id: 1,
          name: "Leanne Graham",
          username: "Bret",
          email: "Sincere@april.biz",
          website: "hildegard.org",
        },
        {
          id: 2,
          name: "Ervin Howell",
          username: "Antonette",
          email: "Shanna@melissa.tv",
          website: "anastasia.net",
        },
        {
          id: 3,
          name: "Clementine Bauch",
          username: "Samantha",
          email: "Nathan@yesenia.net",
          website: "ramiro.info",
        },
        {
          id: 4,
          name: "Patricia Lebsack",
          username: "Karianne",
          email: "Julianne.OConner@kory.org",
          website: "kale.biz",
        },
        {
          id: 5,
          name: "Chelsey Dietrich",
          username: "Kamren",
          email: "Lucio_Hettinger@annie.ca",
          website: "demarco.info",
        },
        {
          id: 6,
          name: "Mrs. Dennis Schulist",
          username: "Leopoldo_Corkery",
          email: "Karley_Dach@jasper.info",
          website: "ola.org",
        },
        {
          id: 7,
          name: "Kurtis Weissnat",
          username: "Elwyn.Skiles",
          email: "Telly.Hoeger@billy.biz",
          website: "elvis.io",
        },
        {
          id: 8,
          name: "Nicholas Runolfsdottir V",
          username: "Maxime_Nienow",
          email: "Sherwood@rosamond.me",
          website: "jacynthe.com",
        },
        {
          id: 9,
          name: "Glenna Reichert",
          username: "Delphine",
          email: "Chaim_McDermott@dana.io",
          website: "conrad.com",
        },
        {
          id: 10,
          name: "Clementina DuBuque",
          username: "Moriah.Stanton",
          email: "Rey.Padberg@karina.biz",
          website: "ambrose.net",
        },
      ],
      categories: [],
      popupActive: false,
      deletePopup: false,
      categoryId: null,
      category: {
        name: "",
      },
    };
  },
  methods: {
    openPopup() {
      this.category.name = null;
      this.popupActive = true;
    },
    async getCategories() {
      try {
        const { data } = await this.$http.get("/api/categories");
        this.categories = data;
      } catch (error) {}
    },
    async createCategory() {
      if (this.$v.category.$invalid) {
        this.$v.$touch();
        this.$vs.notify({
          title: "Warning",
          text: "Please fill all the blanks",
          color: "warning",
        });
        return;
      }
      try {
        if (this.category.id) {
          const { data } = await this.$http.put(
            "/api/categories/" + this.category.id,
            this.category
          );
          this.categories.forEach((item) => {
            if (item.id === data.data.id) {
              item.name = data.data.name;
            }
          });
          this.popupActive = false;
          this.$vs.notify({
            title: "Success",
            text: data.message,
            color: "primary",
          });
        } else {
          const { data } = await this.$http.post(
            "/api/categories",
            this.category
          );
          this.categories.push(data.data);
          this.popupActive = false;
          this.$vs.notify({
            title: "Success",
            text: data.message,
            color: "primary",
          });
        }
      } catch (error) {}
    },
    getCategory(id) {
      if (id) {
        this.category = this.categories.find((x) => x.id === id);
        this.popupActive = true;
      }
    },
    clickRemove(id) {
      this.categoryId = id;
      this.deletePopup = true;
    },
    async deleteCategory() {
      if (this.categoryId) {
        try {
          await this.$http.delete("/api/categories/" + this.categoryId);
          this.$vs.notify({
            title: "Success",
            text: "Category Removed",
            color: "primary",
          });
          this.deletePopup = false
          this.categories = this.categories.filter(x => x.id !== this.categoryId)
        } catch (error) {}
      }
    },
  },
  validations: {
    category: {
      name: { required },
    },
  },
};
</script>

<style scoped></style>
