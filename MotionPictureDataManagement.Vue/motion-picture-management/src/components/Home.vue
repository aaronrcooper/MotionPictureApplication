<template>
  <div>
    <router-link class="float-right" to="/motion-picture">
      <button type="button" class="btn btn-primary">Add</button>
    </router-link>
    <MotionPictureTable />
  </div>
</template>

<script>
import MotionPictureTable from "./MotionPictureTable";

export default {
  name: "Home",
  components: {
    MotionPictureTable,
  },
};
</script>

<style>
</style>