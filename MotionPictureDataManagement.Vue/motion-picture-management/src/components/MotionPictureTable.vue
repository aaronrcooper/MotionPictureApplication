<template>
  <table class="table">
    <thead>
      <tr>
        <th scope="col">Name</th>
        <th scope="col">Description</th>
        <th scope="col">Release Year</th>
        <th scope="col">Actions</th>
      </tr>
    </thead>
    <tbody>
      <MotionPictureTableRow
        v-for="motionPicture in motionPictures"
        v-bind:key="motionPicture.id"
        v-bind:motionPicture="motionPicture"
        @removed="removeMotionPicture"
      ></MotionPictureTableRow>
    </tbody>
  </table>
</template>

<script>
import MotionPictureTableRow from "./MotionPictureTableRow";
import { MotionPictureApi } from '../api/MotionPictureApi';

export default {
  name: "MotionPictureTable",
  data() {
    return {
      motionPictures: new Array(),
    };
  },
  components: {
    MotionPictureTableRow,
  },
  mounted() {
      MotionPictureApi.getMotionPictures()
        .then((response) => (this.motionPictures = response.data));
  },
  methods: {
    removeMotionPicture: function (id) {
      const motionPictureIndex = this.motionPictures.findIndex(
        (element) => element.id === id
      );
      this.motionPictures.splice(motionPictureIndex, 1);
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
