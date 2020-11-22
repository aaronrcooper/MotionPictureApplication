<template>
  <div class="new-motion-picture">
    <div class="btn-group float-right" role="group">
      <router-link to="/">
        <button type="button" class="btn btn-warning">Cancel</button>
      </router-link>
      <button
        type="button"
        class="btn btn-danger"
        v-if="motionPicture.id"
        @click="onDeleteClicked"
      >Delete</button>
    </div>

    <div class="form-group col-md-4">
      <label for="name">Name</label>
      <input
        type="text"
        class="form-control"
        id="name"
        ref="name"
        placeholder="Name"
        v-model="motionPicture.name"
        v-bind:class="{'has-error': nameHasErrors}"
        required
      />
      <div class="alert alert-danger" v-if="nameHasErrors">
        <div v-for="error in formErrors.name" v-bind:key="error">{{error}}</div>
      </div>
    </div>
    <div class="form-group col-md-4">
      <label for="description">Description</label>
      <textarea
        maxlength="500"
        class="form-control"
        id="description"
        ref="description"
        placeholder="Description"
        v-model="motionPicture.description"
        v-bind:class="{'has-error': descriptionHasErrors}"
        required
      ></textarea>
      <div class="alert alert-danger" v-if="descriptionHasErrors">
        <div v-for="error in formErrors.desciption.name" v-bind:key="error">{{error}}</div>
      </div>
    </div>
    <div class="form-group col-md-4">
      <label for="releaseYear">Release Year</label>
      <input
        type="text"
        class="form-control"
        id="releaseYear"
        placeholder="Release Year"
        min-length="4"
        max-length="4"
        v-model="releaseYearText"
        v-bind:class="{'has-error': releaseYearHasErrors}"
        required
      />
      <div class="alert alert-danger" v-if="releaseYearHasErrors">
        <div v-for="error in formErrors.releaseYear" v-bind:key="error">{{error}}</div>
      </div>
    </div>
    <div class="row">
      <button
        v-bind:disabled="submitDisabled"
        type="submit"
        class="btn btn-primary col-md-4"
        v-on:click="submit"
      >Submit</button>
    </div>
  </div>
</template>

<script>
import { MotionPictureApi } from "../api/MotionPictureApi";
import router from "../router";

export default {
  name: "MotionPictureForm",
  props: {
    id: String,
    copy: Boolean,
  },
  data() {
    return {
      motionPicture: {
        id: null,
        name: null,
        description: null,
        releaseYear: null,
      },
      releaseYearText: null,
      formErrors: {
        name: [],
        description: [],
        releaseYear: [],
      },
      nameLength: 50,
      desciptionLength: 500,
      releaseYearLength: 4,
    };
  },
  methods: {
    submit: function () {
      // If we fail validation, return
      if (!this.validateForm()) {
        return;
      }

    // emit to parent that the form has been submitted
      this.$emit("submitted", this.motionPicture);
    },

    getMotionPicture: function () {
      var motionPicturePromise = MotionPictureApi.getMotionPictureById(this.id);
      motionPicturePromise.then((result) => self.setMotionPictureForm(result));
    },

    validateForm: function () {
      // Clear form errors before we validate so we don't get duplicate errors
      this.clearFormErrors();

      this.validateFormName();
      this.validateFormDescription();
      this.validateFormReleaseYear();

      return this.formIsValid;
    },

    clearFormErrors: function () {
      this.formErrors.name = [];
      this.formErrors.description = [];
      this.formErrors.releaseYear = [];
    },
    setMotionPictureForm: function (motionPicture) {
      this.motionPicture = motionPicture;

      if (this.copy === true) {
        // We just want to copy this so make the ID null so we remove the property
        this.motionPicture.id = null;
      }
    },
    onDeleteClicked: function () {
      MotionPictureApi.deleteMotionPicture(this.id).then(() =>
        router.push("/")
      );
    },
    validateFormName: function () {
      var nameElementReference = this.$refs["name"];
      if (!this.motionPicture) {
        this.formErrors.name.push("Name is required");
      }
      if (this.motionPicture.name.length > this.nameLength) {
        this.formErrors.name.push(
          `Name cannot be longer than ${this.nameLength} characters`
        );
      }
      nameElementReference.border = "2px red";
    },
    validateFormDescription: function () {
      if (!this.motionPicture.description) {
        return;
      }

      if (this.motionPicture.description.length > this.desciptionLength) {
        this.formErrors.description.push(
          `Description cannot be longer than ${this.desciptionLength} characters`
        );
      }
    },
    validateFormReleaseYear: function () {
      if (!this.releaseYearText) {
        this.formErrors.releaseYear.push("Release year is required");
      }
      if (this.releaseYearText.length !== this.releaseYearLength) {
        this.formErrors.releaseYear.push(
          `Release year must be exactly ${this.releaseYearLength} characters`
        );
      }
    },
  },
  beforeCreate: function () {
    const self = this;
    if (this.copy) {
      MotionPictureApi.getMotionPictureById(this.id).then((result) => {
        self.setMotionPictureForm(result.data);
      });
    } else if (this.id) {
      MotionPictureApi.getMotionPictureById(this.id).then((result) =>
        self.setMotionPictureForm(result.data)
      );
    }
  },
  computed: {
    submitDisabled() {
      return !this.motionPicture.name || !this.motionPicture.releaseYear;
    },
    formIsValid() {
      return !(this.formErrors.name.length > 0
        || this.formErrors.description.length > 0
        || this.formErrors.releaseYear.length > 0);
    },
    descriptionLengthOfMax() {
      return `${this.motionPicture.description.length}/${this.desciptionLength}`;
    },
    nameHasErrors() {
      return this.formErrors.name.length > 0;
    },
    descriptionHasErrors() {
      return this.formErrors.description.length > 0;
    },
    releaseYearHasErrors() {
      return this.formErrors.releaseYear.length > 0;
    },
  },
  watch: {
    releaseYearText: function () {
      const parsedReleaseYear = Number(this.releaseYearText);
      if (isNaN(parsedReleaseYear)) {
        if (this.formErrors.releaseYear &&
          this.formErrors.releaseYear.FindIndex(
            (err) => err === "Release year must be a number"
          ) > -1
        ) {
          this.formErrors.releaseYear.push("Release year must be a number");
        }
      } else {
        this.motionPicture.releaseYear = Number(this.releaseYearText);
      }
    },
    motionPicture: function() {
      this.releaseYearText = this.motionPicture.releaseYear.toString();
    }
  },
};
</script>

<style scoped>
.has-error {
  border: 1px solid red;
}
</style>