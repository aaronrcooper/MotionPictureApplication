<template>
  <MotionPictureForm @submitted="onSubmit"></MotionPictureForm>
</template>

<script>
import MotionPictureForm from "./MotionPictureForm";
import { MotionPictureApi } from "../api/MotionPictureApi";

export default {
  name: "AddMotionPicture",
  props: {},
  components: {
    MotionPictureForm,
  },
  data() {
    return {
      motionPicture: {
        id: null,
        name: null,
        description: null,
        releaseYear: null,
      }
    };
  },
  methods: {
    onSubmit: function (motionPicturePost) {
      if (!this.validateForm()) {
        return;
      }

      MotionPictureApi.postMotionPicture(motionPicturePost);
    },
    validateForm: function () {
      var formValid = true;
      this.clearFormErrors();

      return formValid;
    },
    clearFormErrors: function () {
      this.formErrors = [];
    },
  },
  beforeCreated: function () {
    console.log("parent");
  },
  computed: {
    submitDisabled() {
      return !this.name || !this.releaseYear || this.formErrors.length > 0;
    },
  },
};
</script>

<style>
</style>