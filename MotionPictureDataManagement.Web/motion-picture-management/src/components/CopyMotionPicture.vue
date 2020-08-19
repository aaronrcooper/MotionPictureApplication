<template>
  <MotionPictureForm v-if="motionPictureId" v-bind:id="motionPictureId" v-bind:copy="true" 
    @submitted="onSubmit"></MotionPictureForm>
</template>

<script>
import { MotionPictureApi } from "../api/MotionPictureApi";
import MotionPictureForm from "./MotionPictureForm";

export default {
  name: "CopyMotionPicture",
  data() {
    return {
      motionPictureId: this.$route.params.id
    };
  },
  components: { MotionPictureForm },
  methods: {
    postMotionPicture: function () {
      const motionPicturePost = this.getMotionPictureFromForm();

      if (!this.validateForm()) {
        return;
      }

      MotionPictureApi.postMotionPicture(motionPicturePost);
    },
    onSubmit: function (motionPictureToCopy) {
      MotionPictureApi.postMotionPicture(motionPictureToCopy);
    }
  }
};
</script>

<style>
</style>